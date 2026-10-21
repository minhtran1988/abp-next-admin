import type { Ref } from 'vue';
import type { PermissionProps, PermissionTree } from '../types/permission';

import { computed, watch, unref, ref } from 'vue';
import { useLocalization } from '/@/hooks/abp/useLocalization';
import { get, update } from '/@/api/permission-management/permissions';
import { PermissionProvider } from '/@/api/permission-management/permissions/model';
import {
  generatePermissionTree,
  getGrantedPermissionKeys,
  getPermissionCount,
  getGrantPermissionCount,
  getPermissionsCount,
  getGrantPermissionsCount,
  find,
  updateParentGrant,
  updateChildrenGrant,
  toPermissionList,
} from '../utils/helper';
import { useAbpStoreWithOut } from '/@/store/modules/abp';

interface UsePermission {
  getPropsRef: Ref<PermissionProps>;
}

export function usePermissions({ getPropsRef }: UsePermission) {
  const { L } = useLocalization('AbpPermissionManagement');
  /** Popup layer title */
  const title = ref('');
  /** Permission tree */
  const permissionTree = ref<PermissionTree[]>([]);
  /**
   * Get authorization data
   * @param name Authorized provider name
   * @param key Authorization provider ID
   */
  function handleGetPermission(name: string, key?: string) {
    get({ providerName: name, providerKey: key }).then((res) => {
      title.value = `${L('Permissions')} - ${res.entityDisplayName}`;
      permissionTree.value = generatePermissionTree(res.groups);
    });
  }

  /** Current tab name
   * @description format: permission display name + (authorized quantity)
   */
  const permissionTab = computed(() => {
    return (tree: PermissionTree) => {
      const grantCount = getGrantPermissionCount(tree);
      return `${tree.displayName} (${grantCount})`;
    };
  });

  /** Whether to disable all permission checkboxes */
  const permissionTreeDisabled = computed(() => {
    const abpStore = useAbpStoreWithOut();
    const props = unref(getPropsRef);
    const { currentUser } = abpStore.getApplication;
    // if (props.providerName === 'R') {
    // // The currently logged in user group is prohibited from operating its own group permissions
    // // should be allowed
    //   return currentUser.roles.includes(props.providerKey!);
    // }
    // The currently logged in user is prohibited from operating their own permissions
    return currentUser.id === props.providerKey;
  });

  /** Identification of the authorized node under the current tab (check the checkbox) */
  const permissionGrantKeys = computed(() => {
    return (tree: PermissionTree) => {
      return getGrantedPermissionKeys(tree.children);
    };
  });

  /** The status of the permission checkbox under the current tab
   * @returns checked whether selected
   * @returns indeterminate Whether the state is uncertain (half-selected)
   */
  const permissionTabCheckState = computed(() => {
    return (tree: PermissionTree) => {
      const grantCount = getGrantPermissionCount(tree);
      const permissionCount = getPermissionCount(tree);
      return {
        indeterminate: grantCount > 0 && grantCount < permissionCount,
        checked: grantCount == permissionCount,
      };
    };
  });

  /** Permission tree check box status */
  const permissionTreeCheckState = computed(() => {
    const treeList = unref(permissionTree);
    const grantCount = getGrantPermissionsCount(treeList);
    const permissionCount = getPermissionsCount(treeList);
    return {
      indeterminate: grantCount > 0 && grantCount < permissionCount,
      checked: grantCount == permissionCount,
    };
  });

  /** Change all authorization data */
  function handleGrantAllPermission(e) {
    if (!unref(permissionTreeDisabled)) {
      updateChildrenGrant(unref(permissionTree), e.target.checked);
    }
  }

  /** Change the authorization data under the current tab */
  function handleGrantPermissions(tree, e) {
    if (!unref(permissionTreeDisabled)) {
      updateChildrenGrant(tree.children, e.target.checked);
    }
  }

  /** Event when an authorized node is selected */
  function handlePermissionGranted(tree, _selectedKeys, info) {
    const permission = find(tree.children, info.node.eventKey) as PermissionTree;
    if (!permission) return;
    permission.isGranted = info.checked;
    if (permission.parentName && info.checked) {
      // After the child permission is authorized, the parent permission must also be authorized.
      updateParentGrant(tree, permission.parentName, true);
    }
    if (permission.children && !info.checked) {
      // Cancel authorization for parent permissions, and cancel authorization for all child permissions
      updateChildrenGrant(permission.children, false);
    }
  }

  /** Save authorization data */
  function handleSavePermission() {
    const props = unref(getPropsRef);
    const provider: PermissionProvider = {
      providerName: props.providerName,
      providerKey: props.providerKey,
    };
    const permissions = toPermissionList(unref(permissionTree));
    return update(provider, {
      permissions: permissions,
    });
  }

  /** Monitor the authorization provider identification and retrieval permissions */
  watch(
    () => unref(getPropsRef).providerKey,
    (key) => {
      permissionTree.value = [];
      if (key) {
        const props = unref(getPropsRef);
        handleGetPermission(props.providerName, key);
      }
    },
  );

  return {
    title,
    permissionTree,
    permissionTab,
    permissionGrantKeys,
    permissionTabCheckState,
    permissionTreeCheckState,
    permissionTreeDisabled,
    handlePermissionGranted,
    handleSavePermission,
    handleGrantAllPermission,
    handleGrantPermissions,
  };
}
