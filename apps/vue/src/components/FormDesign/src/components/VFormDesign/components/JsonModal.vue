<!--
 * @Description: Render JSON data
-->
<template>
  <Modal
    title="JSON data"
    :footer="null"
    :visible="visible"
    @cancel="handleCancel"
    :destroyOnClose="true"
    wrapClassName="v-code-modal"
    style="top: 20px"
    width="850px"
  >
    <PreviewCode :editorJson="editorJson" />
  </Modal>
</template>
<script lang="ts">
  import { computed, defineComponent, reactive, toRefs } from 'vue';
  import PreviewCode from './PreviewCode.vue';
  import { IFormConfig } from '../../../typings/v-form-component';
  import { formatRules, removeAttrs } from '../../../utils';
  import { Modal } from 'ant-design-vue';
  export default defineComponent({
    name: 'JsonModal',
    components: {
      PreviewCode,
      Modal,
    },
    emits: ['cancel'],
    setup(_props, { emit }) {
      const state = reactive<{
        visible: boolean;
        jsonData: IFormConfig;
      }>({
        visible: false, //Control json data pop-up display
        jsonData: {} as IFormConfig, // json data
      });
      /**
       * Display Json data pop-up box
       * @param jsonData
       */
      const showModal = (jsonData: IFormConfig) => {
        formatRules(jsonData.schemas);
        state.jsonData = jsonData;
        state.visible = true;
      };
      // Calculate json data
      const editorJson = computed(() => {
        return JSON.stringify(removeAttrs(state.jsonData), null, '\t');
      });
      // Close the pop-up box
      const handleCancel = () => {
        state.visible = false;
        emit('cancel');
      };
      return { ...toRefs(state), editorJson, handleCancel, showModal };
    },
  });
</script>
