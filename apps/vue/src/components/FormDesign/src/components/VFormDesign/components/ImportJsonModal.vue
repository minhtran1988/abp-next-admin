<!--
 * @Description: Import JSON template
-->
<template>
  <Modal
    title="JSON data"
    :visible="state.visible"
    @ok="handleImportJson"
    @cancel="handleCancel"
    cancelText="Close"
    :destroyOnClose="true"
    wrapClassName="v-code-modal"
    style="top: 20px"
    :width="850"
  >
    <p class="hint-box">The import format is as follows:</p>
    <div class="v-json-box">
      <CodeEditor v-model:value="state.json" ref="myEditor" :mode="MODE.JSON" />
    </div>

    <template #footer>
      <Button @click="handleCancel">Cancel</Button>
      <Upload
        class="upload-button"
        :beforeUpload="beforeUpload"
        :showUploadList="false"
        accept="application/json"
      >
        <Button type="primary">Import json file</Button>
      </Upload>
      <Button type="primary" @click="handleImportJson">OK</Button>
    </template>
  </Modal>
</template>
<script lang="ts" setup>
  import { reactive } from 'vue';
  import { Button } from 'ant-design-vue';
  // import message from '../../../utils/message';
  import { useFormDesignState } from '../../../hooks/useFormDesignState';
  // import { codemirror } from 'vue-codemirror-lite';
  import { IFormConfig } from '../../../typings/v-form-component';
  import { formItemsForEach, generateKey } from '../../../utils';
  import { CodeEditor, MODE } from '/@/components/CodeEditor';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { Upload, Modal } from 'ant-design-vue';
  const { createMessage } = useMessage();
  const state = reactive({
    visible: false,
    json: `{
"schemas": [
{
  "component": "input",
  "label": "input box",
  "field": "input_2",
  "span": 24,
  "props": {
    "type": "text"
  }
}
],
"layout": "horizontal",
"labelLayout": "flex",
"labelWidth": 100,
"labelCol": {},
"wrapperCol": {}
}`,
    jsonData: {
      schemas: {},
      config: {},
    },
    handleSetSelectItem: null,
  });
  const { formDesignMethods } = useFormDesignState();
  const handleCancel = () => {
    state.visible = false;
  };
  const showModal = () => {
    state.visible = true;
  };
  const handleImportJson = () => {
    // Import JSON
    try {
      const editorJsonData = JSON.parse(state.json) as IFormConfig;
      editorJsonData.schemas &&
        formItemsForEach(editorJsonData.schemas, (formItem) => {
          generateKey(formItem);
        });
      formDesignMethods.setFormConfig({
        ...editorJsonData,
        activeKey: 1,
        currentItem: { component: '' },
      });
      handleCancel();
      createMessage.success('Import successful');
    } catch {
      createMessage.error('Import failed, data format is incorrect');
    }
  };
  const beforeUpload = (e: File) => {
    // Import via json file
    const reader = new FileReader();
    reader.readAsText(e);
    reader.onload = function () {
      state.json = this.result as string;
      handleImportJson();
    };
    return false;
  };

  defineExpose({
    showModal,
  });
</script>

<style lang="less" scoped>
  .upload-button {
    margin: 0 10px;
  }
</style>
