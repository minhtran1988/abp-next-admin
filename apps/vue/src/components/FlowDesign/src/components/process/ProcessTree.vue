<script lang="ts">
  import {
    computed,
    defineComponent,
    reactive,
    ref,
    unref,
    h,
    resolveComponent,
    getCurrentInstance,
  } from 'vue';
  import { Button } from 'ant-design-vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useFlowStoreWithOut } from '/@/store/modules/flow';
  // Import all node components
  import HttpEndPoint from '../nodes/HttpEndPointNode.vue';
  import Approval from '../nodes/ApprovalNode.vue';
  import Cc from '../nodes/CcNode.vue';
  import Concurrent from '../nodes/ConcurrentNode.vue';
  import Condition from '../nodes/ConditionNode.vue';
  import Trigger from '../nodes/TriggerNode.vue';
  import Delay from '../nodes/DelayNode.vue';
  import Empty from '../nodes/EmptyNode.vue';
  import Root from '../nodes/RootNode.vue';
  import Node from '../nodes/Node.vue';

  import DefaultProps, { PrimaryNodes } from './DefaultNodeProps';
  import { cloneDeep } from 'lodash-es';

  export default defineComponent({
    name: 'ProcessTree',
    components: {
      httpendpoint: HttpEndPoint,
      Approval,
      Cc,
      Concurrent,
      Condition,
      Trigger,
      Delay,
      Empty,
      Root,
      Node,
    },
    emits: ['selectedNode'],
    setup: (_, { emit, expose }) => {
      const instance = getCurrentInstance();

      const { createMessage } = useMessage();
      const flowStore = useFlowStoreWithOut();
      const nodeMap = computed(() => {
        return flowStore.nodeMap;
      });
      const dom = computed(() => {
        return flowStore.design.process;
      });
      const state = reactive({
        valid: true,
      });
      const rootRef = ref<any>();

      function getDomTree(node) {
        toMapping(node);
        if (isPrimaryNode(node)) {
          // Ordinary business node
          let childDoms = getDomTree(node.children);
          decodeAppendDom(node, childDoms);
          return [h('div', { class: { 'primary-node': true } }, { default: () => childDoms })];
        } else if (isBranchNode(node)) {
          let index = 0;
          // Traverse branch nodes, including parallel and conditional nodes
          let branchItems = node.branchs.map((branchNode) => {
            // Process the child nodes within each branch
            toMapping(branchNode);
            let childDoms = getDomTree(branchNode.children);
            decodeAppendDom(branchNode, childDoms, { level: index + 1, size: node.branchs.length });
            // Insert 4 horizontal lines to block out the left and right half lines of the conditional node
            insertCoverLine(index, childDoms, node.branchs);
            // Traverse the tail branch of the sub-branch
            index++;
            return h('div', { class: { 'branch-node-item': true } }, { default: () => childDoms });
          });
          // Insert button to add branch/condition
          branchItems.unshift(
            h(
              'div',
              { class: { 'add-branch-btn': true } },
              {
                default: () => [
                  h(
                    Button,
                    {
                      class: { 'add-branch-btn-el': true },
                      size: 'small',
                      shape: 'round',
                      onClick: () => addBranchNode(node),
                      innerHTML: `Add ${isConditionNode(node) ? 'Condition' : 'branch'}`,
                    },
                    { default: () => [] },
                  ),
                ],
              },
            ),
          );
          let bchDom = [
            h('div', { class: { 'branch-node': true } }, { default: () => branchItems }),
          ];
          // Continue traversing the nodes after the branch
          let afterChildDoms = getDomTree(node.children);
          return [h('div', {}, { default: () => [bchDom, afterChildDoms] })];
        } else if (isEmptyNode(node)) {
          // Empty node exists at the end of the branch
          let childDoms = getDomTree(node.children);
          decodeAppendDom(node, childDoms);
          return [h('div', { class: { 'empty-node': true } }, { default: () => childDoms })];
        } else {
          // Traversing to the end, no child nodes
          return [];
        }
      }

      // Insert dom to the same level when decoding and rendering
      function decodeAppendDom(node, dom, props = {} as any) {
        props.config = node;
        const component = resolveComponent(node.type.toLowerCase());
        dom?.unshift(
          h(component, {
            ...props,
            ref: node.id,
            key: node.id,
            // Define events, insert nodes, delete nodes, select nodes, copy/move
            onInsertNode: (type) => insertNode(type, node),
            onDelNode: () => delNode(node),
            onSelected: () => selectNode(node),
            onCopy: () => copyBranch(node),
            onLeftMove: () => branchMove(node, -1),
            onRightMove: () => branchMove(node, 1),
          }),
        );
      }

      // id is mapped to map and used to traverse upwards
      function toMapping(node) {
        if (node && node.id) {
          // console.log("node=> " + node.id + " name:" + node.name + " type:" + node.type)
          nodeMap.value.set(node.id, node);
        }
      }

      function insertCoverLine(index, doms, branchs) {
        if (index === 0) {
          // leftmost branch
          doms.unshift(h('div', { class: { 'line-top-left': true } }, { default: () => [] }));
          doms.unshift(h('div', { class: { 'line-bot-left': true } }, { default: () => [] }));
        } else if (index === branchs.length - 1) {
          // rightmost branch
          doms.unshift(h('div', { class: { 'line-top-right': true } }, { default: () => [] }));
          doms.unshift(h('div', { class: { 'line-bot-right': true } }, { default: () => [] }));
        }
      }

      function copyBranch(node) {
        let parentNode = nodeMap.value.get(node.parentId);
        let branchNode = cloneDeep(node);
        branchNode.name = branchNode.name + '-copy';
        forEachNode(parentNode, branchNode, (parent, node) => {
          let id = getRandomId();
          console.log(node, 'new id =>' + id, 'old nodeId:' + node.id);
          node.id = id;
          node.parentId = parent.id;
        });
        parentNode.branchs.splice(parentNode.branchs.indexOf(node), 0, branchNode);
        instance?.proxy?.$forceUpdate();
      }

      function branchMove(node, offset) {
        let parentNode = nodeMap.value.get(node.parentId);
        let index = parentNode.branchs.indexOf(node);
        let branch = parentNode.branchs[index + offset];
        parentNode.branchs[index + offset] = parentNode.branchs[index];
        parentNode.branchs[index] = branch;
        instance?.proxy?.$forceUpdate();
      }

      // Determine whether it is the main business node
      function isPrimaryNode(node) {
        return node && PrimaryNodes.includes(node.type);
      }

      function isBranchNode(node) {
        return node && (node.type === 'CONDITIONS' || node.type === 'CONCURRENTS');
      }

      function isEmptyNode(node) {
        return node && node.type === 'EMPTY';
      }

      // It is a branch node
      function isConditionNode(node) {
        return node.type === 'CONDITIONS';
      }

      // It is a branch node
      function isBranchSubNode(node) {
        return node && (node.type === 'CONDITION' || node.type === 'CONCURRENT');
      }

      function isConcurrentNode(node) {
        return node.type === 'CONCURRENTS';
      }

      function getRandomId() {
        return `node_${new Date().getTime().toString().substring(5)}${Math.round(
          Math.random() * 9000 + 1000,
        )}`;
      }

      // Select a node
      function selectNode(node) {
        console.log('selectNode', node);
        flowStore.selectNode(node);
        emit('selectedNode', node);
      }

      // Process node insertion logic
      function insertNode(type, parentNode) {
        console.log('insertNode', type, parentNode);
        const rootEl = unref(rootRef);
        rootEl?.click();
        // Cache the following nodes
        let afterNode = parentNode.children;
        // Insert new node
        parentNode.children = {
          id: getRandomId(),
          parentId: parentNode.id,
          props: {},
          type: type,
        };
        switch (type) {
          case 'APPROVAL':
            insertApprovalNode(parentNode);
            break;
          case 'CC':
            insertCcNode(parentNode);
            break;
          case 'DELAY':
            insertDelayNode(parentNode);
            break;
          case 'TRIGGER':
            insertTriggerNode(parentNode);
            break;
          case 'CONDITIONS':
            insertConditionsNode(parentNode);
            break;
          case 'CONCURRENTS':
            insertConcurrentsNode(parentNode);
            break;
          case 'HTTPENDPOINT':
            insertHttpEndPointNode(parentNode);
            break;
          default:
            break;
        }
        // Splice subsequent nodes
        if (isBranchNode({ type: type })) {
          if (afterNode && afterNode.id) {
            afterNode.parentId = parentNode.children.children.id;
          }
          parentNode.children.children.children = afterNode;
        } else {
          if (afterNode && afterNode.id) {
            afterNode.parentId = parentNode.children.id;
          }
          parentNode.children.children = afterNode;
        }
        instance?.proxy?.$forceUpdate();
      }

      function insertApprovalNode(parentNode) {
        parentNode.children.name = 'Approver';
        parentNode.children.props = cloneDeep(DefaultProps.APPROVAL_PROPS);
      }

      function insertCcNode(parentNode) {
        parentNode.children.name = 'Cc';
        parentNode.children.props = cloneDeep(DefaultProps.CC_PROPS);
      }

      function insertDelayNode(parentNode) {
        parentNode.children.name = 'Delayed processing';
        parentNode.children.props = cloneDeep(DefaultProps.DELAY_PROPS);
      }

      function insertTriggerNode(parentNode) {
        parentNode.children.name = 'trigger';
        parentNode.children.props = cloneDeep(DefaultProps.TRIGGER_PROPS);
      }

      function insertConditionsNode(parentNode) {
        parentNode.children.name = 'Conditional branch';
        parentNode.children.children = {
          id: getRandomId(),
          parentId: parentNode.children.id,
          type: 'EMPTY',
        };
        parentNode.children.branchs = [
          {
            id: getRandomId(),
            parentId: parentNode.children.id,
            type: 'CONDITION',
            props: cloneDeep(DefaultProps.CONDITION_PROPS),
            name: 'Condition 1',
            children: {},
          },
          {
            id: getRandomId(),
            parentId: parentNode.children.id,
            type: 'CONDITION',
            props: cloneDeep(DefaultProps.CONDITION_PROPS),
            name: 'Condition 2',
            children: {},
          },
        ];
      }

      function insertConcurrentsNode(parentNode) {
        parentNode.children.name = 'Parallel branch';
        parentNode.children.children = {
          id: getRandomId(),
          parentId: parentNode.children.id,
          type: 'EMPTY',
        };
        parentNode.children.branchs = [
          {
            id: getRandomId(),
            name: 'Branch 1',
            parentId: parentNode.children.id,
            type: 'CONCURRENT',
            props: {},
            children: {},
          },
          {
            id: getRandomId(),
            name: 'Branch 2',
            parentId: parentNode.children.id,
            type: 'CONCURRENT',
            props: {},
            children: {},
          },
        ];
      }

      function insertHttpEndPointNode(parentNode) {
        parentNode.children.name = 'HttpEndPoint';
        parentNode.children.props = cloneDeep(DefaultProps.HTTPENDPOINT_PROPS);
      }

      function getBranchEndNode(conditionNode) {
        if (!conditionNode.children || !conditionNode.children.id) {
          return conditionNode;
        }
        return getBranchEndNode(conditionNode.children);
      }

      function addBranchNode(node) {
        if (node.branchs.length < 8) {
          node.branchs.push({
            id: getRandomId(),
            parentId: node.id,
            name: (isConditionNode(node) ? 'Condition' : 'branch') + (node.branchs.length + 1),
            props: isConditionNode(node) ? cloneDeep(DefaultProps.CONDITION_PROPS) : {},
            type: isConditionNode(node) ? 'CONDITION' : 'CONCURRENT',
            children: {},
          });
        } else {
          createMessage.warning('Only 8 items can be added at most😥');
        }
      }

      // Delete the current node
      function delNode(node) {
        console.log('Delete node', node);
        // Get the parent node of this node
        let parentNode = nodeMap.value.get(node.parentId);
        if (parentNode) {
          // Determine whether the parent node of this node is a branch node
          if (isBranchNode(parentNode)) {
            // remove this branch
            parentNode.branchs.splice(parentNode.branchs.indexOf(node), 1);
            // Handle the situation where there is only 1 branch left
            if (parentNode.branchs.length < 2) {
              // Get the parent node of the condition group
              let ppNode = nodeMap.value.get(parentNode.parentId);
              // Determine whether the only branch has a business node
              if (parentNode.branchs[0].children && parentNode.branchs[0].children.id) {
                // Merge the head of the only remaining branch into the trunk
                ppNode.children = parentNode.branchs[0].children;
                ppNode.children.parentId = ppNode.id;
                // Search for the last node at the end of the only branch
                let endNode = getBranchEndNode(parentNode.branchs[0]);
                // Subsequent nodes are spliced, here we need to take the node after EMPTY
                endNode.children = parentNode.children.children;
                if (endNode.children && endNode.children.id) {
                  endNode.children.parentId = endNode.id;
                }
              } else {
                // Directly merge the nodes behind the branch, here we need to take the node after EMPTY
                ppNode.children = parentNode.children.children;
                if (ppNode.children && ppNode.children.id) {
                  ppNode.children.parentId = ppNode.id;
                }
              }
            }
          } else {
            // If not, delete it directly
            if (node.children && node.children.id) {
              node.children.parentId = parentNode.id;
            }
            parentNode.children = node.children;
          }
          instance?.proxy?.$forceUpdate();
        } else {
          createMessage.warning('An error occurred, the superior node cannot be found😥');
        }
      }

      function validateProcess() {
        state.valid = true;
        let err = [];
        validate(err, dom.value);
        return err;
      }

      function validateNode(err, node) {
        const nodeRef = instance?.refs[node.id] as unknown as any;
        if (nodeRef?.validate) {
          state.valid = nodeRef.validate(err);
        }
      }

      // Update the dom of the specified node
      function nodeDomUpdate(node) {
        const nodeRef = instance?.refs[node.id] as unknown as any;
        nodeRef?.$forceUpdate();
      }

      // Given a starting node, traverse all internal nodes
      function forEachNode(parent, node, callback) {
        if (isBranchNode(node)) {
          callback(parent, node);
          forEachNode(node, node.children, callback);
          node.branchs.map((branchNode) => {
            callback(node, branchNode);
            forEachNode(branchNode, branchNode.children, callback);
          });
        } else if (isPrimaryNode(node) || isEmptyNode(node) || isBranchSubNode(node)) {
          callback(parent, node);
          forEachNode(node, node.children, callback);
        }
      }

      // Verify all node settings
      function validate(err, node) {
        if (isPrimaryNode(node)) {
          validateNode(err, node);
          validate(err, node.children);
        } else if (isBranchNode(node)) {
          // Check each branch
          node.branchs.map((branchNode) => {
            // Verify condition node
            validateNode(err, branchNode);
            // Verify the nodes behind the condition node
            validate(err, branchNode.children);
          });
          validate(err, node.children);
        } else if (isEmptyNode(node)) {
          validate(err, node.children);
        }
      }

      expose({
        validateProcess,
      });

      return () => {
        nodeMap.value.clear();
        let processTrees = getDomTree(dom.value);
        // Insert end node
        processTrees.push(
          h(
            'div',
            { style: { 'text-align': 'center' } },
            {
              default: () => [h('div', { class: { 'process-end': true }, innerHTML: 'process-end' })],
            },
          ),
        );
        return h('div', { class: { _root: true }, ref: rootRef }, { default: () => processTrees });
      };
    },
  });
</script>

<style lang="less" scoped>
  ._root {
    margin: 0 auto;
  }

  .process-end {
    width: 100px;
    margin: 0 auto;
    margin-bottom: 20px;
    border-radius: 15px;
    padding: 5px 10px;
    font-size: small;
    color: #747474;
    background-color: #f2f2f2;
    box-shadow: 0 0 10px 0 #bcbcbc;
  }

  .primary-node {
    display: flex;
    align-items: center;
    flex-direction: column;
  }

  .branch-node {
    display: flex;
    justify-content: center;
    /* border-top: 2px solid #cccccc;
    border-bottom: 2px solid #cccccc;*/
  }

  .branch-node-item {
    position: relative;
    display: flex;
    background: white;
    flex-direction: column;
    align-items: center;
    border-top: 2px solid #cccccc;
    border-bottom: 2px solid #cccccc;

    &:before {
      content: '';
      position: absolute;
      top: 0;
      left: calc(50% - 1px);
      margin: auto;
      width: 2px;
      height: 100%;
      background-color: #cacaca;
    }

    .line-top-left,
    .line-top-right,
    .line-bot-left,
    .line-bot-right {
      position: absolute;
      width: 50%;
      height: 4px;
      background-color: white;
    }

    .line-top-left {
      top: -2px;
      left: -1px;
    }

    .line-top-right {
      top: -2px;
      right: -1px;
    }

    .line-bot-left {
      bottom: -2px;
      left: -1px;
    }

    .line-bot-right {
      bottom: -2px;
      right: -1px;
    }
  }

  .add-branch-btn {
    position: absolute;
    width: 80px;

    .add-branch-btn-el {
      z-index: 999;
      position: absolute;
      top: -15px;
    }
  }

  .empty-node {
    display: flex;
    justify-content: center;
    flex-direction: column;
    align-items: center;
  }
</style>
