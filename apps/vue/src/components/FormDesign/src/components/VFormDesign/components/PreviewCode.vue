<template>
  <div>
    <div class="v-json-box">
      <CodeEditor :value="editorJson" ref="myEditor" :mode="MODE.JSON" />
    </div>
    <div class="copy-btn-box">
      <a-button
        @click="handleCopyJson"
        type="primary"
        class="copy-btn"
        data-clipboard-action="copy"
        :data-clipboard-text="editorJson"
      >
        Copy data
      </a-button>
      <a-button @click="handleExportJson" type="primary">Export code</a-button>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { unref } from 'vue';
  import { CodeEditor, MODE } from '/@/components/CodeEditor';
  import { useCopyToClipboard } from '/@/hooks/web/useCopyToClipboard';
  import { useMessage } from '/@/hooks/web/useMessage';

  const props = defineProps({
    fileFormat: {
      type: String,
      default: 'json',
    },
    editorJson: {
      type: String,
      default: '',
    },
  });

  const exportData = (data: string, fileName = `file.${props.fileFormat}`) => {
    let content = 'data:text/csv;charset=utf-8,';
    content += data;
    const encodedUri = encodeURI(content);
    const actions = document.createElement('a');
    actions.setAttribute('href', encodedUri);
    actions.setAttribute('download', fileName);
    actions.click();
  };
  const handleExportJson = () => {
    exportData(props.editorJson);
  };
  const { clipboardRef, copiedRef } = useCopyToClipboard();
  const { createMessage } = useMessage();
  const handleCopyJson = () => {
    // copy data
    const value = props.editorJson;
    if (!value) {
      createMessage.warning('The code is empty!');
      return;
    }
    clipboardRef.value = value;
    if (unref(copiedRef)) {
      createMessage.warning('Copy successfully!');
    }
  };
</script>

<style lang="less" scoped>
  // modal copy button style
  .copy-btn-box {
    padding-top: 8px;
    text-align: center;

    .copy-btn {
      margin-right: 8px;
    }
  }
</style>
