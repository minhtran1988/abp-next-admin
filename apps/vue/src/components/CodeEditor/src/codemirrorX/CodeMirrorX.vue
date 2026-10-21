<template>
  <textarea style="overflow: auto" ref="codeEditor"></textarea>
</template>

<script lang="ts" setup>
  import { computed, ref, unref, watch, nextTick, onMounted } from 'vue';
  import CodeMirror from 'codemirror';
  import '../codemirror/codemirror.css';
  import 'codemirror/theme/idea.css';
  import 'codemirror/theme/dracula.css';

  import 'codemirror/mode/javascript/javascript';
  import 'codemirror/mode/htmlmixed/htmlmixed';
  import 'codemirror/mode/css/css';

  import 'codemirror/addon/fold/foldcode';
  import 'codemirror/addon/fold/foldgutter';
  import 'codemirror/addon/fold/foldgutter.css';
  import 'codemirror/addon/fold/brace-fold';
  import 'codemirror/addon/fold/xml-fold';
  import 'codemirror/addon/fold/comment-fold';
  import 'codemirror/addon/fold/markdown-fold';
  import 'codemirror/addon/fold/indent-fold';
  import 'codemirror/addon/edit/closebrackets';
  import 'codemirror/addon/edit/closetag';
  import 'codemirror/addon/edit/matchtags';
  import 'codemirror/addon/edit/matchbrackets';
  import 'codemirror/addon/selection/active-line';
  import 'codemirror/addon/scroll/annotatescrollbar';
  import 'codemirror/addon/dialog/dialog';
  import 'codemirror/addon/dialog/dialog.css';
  import 'codemirror/addon/display/autorefresh';
  import 'codemirror/addon/display/placeholder';
  import 'codemirror/addon/selection/mark-selection';

  import { MODE } from './../typing';
  import { isString } from '/@/utils/is';
  import { useAppStore } from '/@/store/modules/app';
  import { useWindowSizeFn } from '/@/hooks/event/useWindowSizeFn';

  const codeEditor = ref();
  const appStore = useAppStore();
  let editor: Nullable<CodeMirror.Editor>;

  const props = defineProps({
    mode: {
      type: String as PropType<MODE>,
      default: MODE.JSON,
      validator(value: any) {
        // This value must match one of the following strings
        return Object.values(MODE).includes(value);
      },
    },
    modelValue: {
      type: [Object, String] as PropType<Record<string, any> | string>,
      default: '',
    },
    readonly: {
      type: Boolean,
      default: false,
    },
  });
  const emits = defineEmits(['update:modelValue', 'format-error']);

  const getValue = computed(() => {
    const { mode, modelValue } = props;
    if (mode !== MODE.JSON) {
      return modelValue as string;
    }
    let result = modelValue;
    if (isString(result)) {
      try {
        result = JSON.parse(result);
      } catch (e) {
        emits('format-error', modelValue);
        return modelValue as string;
      }
    }
    return JSON.stringify(result, null, 2);
  });

  watch(
    () => getValue.value,
    async (value) => {
      await nextTick();
      const oldValue = editor?.getValue();
      if (value !== oldValue) {
        setTimeout(() => {
          editor?.setValue(value);
        }, 100);
      }
    },
    { flush: 'post' },
  );

  watch(
    () => appStore.getDarkMode,
    async () => {
      setTheme();
    },
    {
      immediate: true,
    },
  );

  function setTheme() {
    unref(editor)?.setOption('theme', appStore.getDarkMode === 'light' ? 'idea' : 'dracula');
  }

  onMounted(() => {
    editor = CodeMirror.fromTextArea(codeEditor.value, {
      value: '',
      autoRefresh: true,
      mode: props.mode,
      indentWithTabs: false, // When indenting, do you need to replace n*tab width spaces with n tab characters? The default is false
      smartIndent: true, // Automatic indentation, set whether to automatically indent according to the context (the same indentation amount as the previous line). Default is true
      lineNumbers: true, // Whether to display line numbers on the left side of the editor
      matchBrackets: true, // bracket matching
      readOnly: props.readonly,
      // Enable code folding related functions: Start
      foldGutter: true,
      lineWrapping: true,
      // Enable code folding related functions: End
      styleActiveLine: true, // Highlight the cursor line
    });
    // Listen to the editor's change event
    editor.on('change', () => {
      // Trigger the two-way binding of v-model
      emits('update:modelValue', editor!.getValue());
    });
    useWindowSizeFn(editor.refresh);
  });
</script>
