<template>
  <div>
    <Form layout="vertical">
      <FormItem label="Select triggered action" name="text" class="user-type">
        <RadioGroup v-model:value="config.type">
          <Radio value="WEBHOOK">Send network request</Radio>
          <Radio value="EMAIL">Send email</Radio>
        </RadioGroup>
      </FormItem>
      <div v-if="config.type === 'WEBHOOK'">
        <FormItem label="Request address" name="text">
          <InputGroup compact>
            <Select style="width: 20%" v-model:value="config.http.method" placeholder="URL">
              <Option value="GET">GET</Option>
              <Option value="POST">POST</Option>
              <Option value="PUT">PUT</Option>
              <Option value="DELETE">DELETE</Option>
            </Select>
            <Input
              style="width: 80%"
              placeholder="Please enter URL address"
              size="middle"
              v-model:value="config.http.url"
            />
          </InputGroup>
        </FormItem>
        <FormItem name="text">
          <template #label>
            <span style="margin-right: 10px">Header request header</span>
            <Button type="link" @click="addItem(config.http.headers)"> + Add</Button>
          </template>
          <div
            style="margin-top: 6px"
            v-for="(header, index) in config.http.headers"
            :key="header.name"
          >
            -
            <Input
              placeholder="parameter name"
              size="small"
              style="width: 100px"
              v-model:value="header.name"
            />
            <RadioGroup size="small" style="margin: 0 5px" v-model:value="header.isField">
              <RadioButton :value="true">Form</RadioButton>
              <RadioButton :value="false">Fixed</RadioButton>
            </RadioGroup>
            <Select
              v-if="header.isField"
              style="width: 180px"
              v-model:value="header.value"
              size="small"
              placeholder="Please select form fields"
            >
              <Option v-for="form in forms" :key="form.id" :value="form.title">{{
                form.title
              }}</Option>
            </Select>
            <Input
              v-else
              placeholder="Please set field value"
              size="small"
              v-model:value="header.value"
              style="width: 180px"
            />
            <DeleteOutlined
              @click="delItem(config.http.headers, index)"
              style="margin-left: 5px; color: #c75450; cursor: pointer"
            />
          </div>
        </FormItem>
        <FormItem name="text">
          <template #label>
            <span style="margin-right: 10px">Header request parameters </span>
            <Button style="margin-right: 20px" type="link" @click="addItem(config.http.params)">
              + Add</Button
            >
            <span>Parameter type - </span>
            <RadioGroup size="small" style="margin: 0 5px" v-model:value="config.http.contentType">
              <RadioButton value="JSON">json</RadioButton>
              <RadioButton value="FORM">form</RadioButton>
            </RadioGroup>
          </template>
          <div
            style="margin-top: 6px"
            v-for="(param, index) in config.http.params"
            :key="param.name"
          >
            -
            <Input
              placeholder="parameter name"
              size="small"
              style="width: 100px"
              v-model:value="param.name"
            />
            <RadioGroup size="small" style="margin: 0 5px" v-model:value="param.isField">
              <RadioButton :value="true">Form</RadioButton>
              <RadioButton :value="false">Fixed</RadioButton>
            </RadioGroup>
            <Select
              v-if="param.isField"
              style="width: 180px"
              v-model:value="param.value"
              size="small"
              placeholder="Please select form fields"
            >
              <Option v-for="form in forms" :key="form.id" :value="form.title">{{
                form.title
              }}</Option>
            </Select>
            <Input
              v-else
              placeholder="Please set field value"
              size="small"
              v-model:value="param.value"
              style="width: 180px"
            />
            <DeleteOutlined
              @click="delItem(config.http.params, index)"
              style="margin-left: 5px; color: #c75450; cursor: pointer"
            />
          </div>
          <div> </div>
        </FormItem>
        <FormItem name="text">
          <template #label>
            <span>Request result processing</span>
            <span style="margin-left: 20px">Custom script: </span>
            <Switch v-model:checked="config.http.handlerByScript"></Switch>
          </template>
          <span class="item-desc" v-if="config.http.handlerByScript">
            👉 If the return value is true, the process will be passed; if it is false, the process will be rejected.
            <div
              >Support functions
              <span style="color: dodgerblue"
                >setFormByName(
                <span style="color: #939494">'Form field name', 'Form field value'</span>
                )</span
              >
              Modifiable form data
            </div>
          </span>
          <span class="item-desc" v-else>👉 Pass regardless of the request result</span>
          <div v-if="config.http.handlerByScript">
            <div>
              <span>Request successful😀:</span>
              <Textarea v-model:value="config.http.success" :rows="3"></Textarea>
            </div>
            <div>
              <span>Request failed😥:</span>
              <Textarea v-model:value="config.http.fail" :rows="3"></Textarea>
            </div>
          </div>
        </FormItem>
      </div>
      <div v-else-if="config.type === 'EMAIL'">
        <FormItem label="Email subject" name="text">
          <Input placeholder="Please enter the email subject" size="middle" v-model:value="config.email.subject" />
        </FormItem>
        <FormItem label="recipient" name="text">
          <Select
            size="small"
            style="width: 100%"
            v-model:value="config.email.to"
            filterable
            multiple
            allow-create
            default-first-option
            placeholder="Please enter recipient"
          >
            <Option v-for="item in config.email.to" :key="item" :value="item">{{ item }}</Option>
          </Select>
        </FormItem>
        <FormItem label="Email text" name="text">
          <Textarea
            v-model:value="config.email.content"
            :rows="4"
            placeholder="Email content, supports variable extraction of form data ${form field name} "
          ></Textarea>
        </FormItem>
      </div>
    </Form>
  </div>
</template>

<script setup lang="ts">
  import { computed, reactive, toRefs } from 'vue';
  import { Button, Form, Input, Select, Switch, Textarea, Radio } from 'ant-design-vue';
  import { DeleteOutlined } from '@ant-design/icons-vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useFlowStoreWithOut } from '/@/store/modules/flow';

  const FormItem = Form.Item;
  const Option = Select.Option;
  const InputGroup = Input.Group;
  const RadioGroup = Radio.Group;
  const RadioButton = Radio.Button;

  const flowStore = useFlowStoreWithOut();

  defineProps({
    config: {
      type: Object,
      default: () => {
        return {};
      },
    },
  });
  const forms = computed(() => {
    return flowStore.design.formItems || [];
  });
  const state = reactive({
    cmOptions: {
      tabSize: 4, // tab
      indentUnit: 4,
      styleActiveLine: true, // Highlight selected line
      lineNumbers: true, // Display line numbers
      styleSelectedText: true,
      line: true,
      foldGutter: true, // block slot
      gutters: ['CodeMirror-linenumbers', 'lock', 'warn'],
      highlightSelectionMatches: { showToken: /w/, annotateScrollbar: true }, // This option can be enabled to highlight all instances of the currently selected content
      mode: 'javascript',
      // hint.js options
      hintOptions: {
        // Whether to automatically complete when there is only one match
        completeSingle: false,
      },
      // Shortcut keys provide three modes: sublime, emacs, vim
      keyMap: 'sublime',
      matchBrackets: true,
      showCursorWhenSelecting: false,
      // scrollbarStyle:null,
      // readOnly:true, //Whether it is read only
      theme: 'material', // theme material
      extraKeys: { Ctrl: 'autocomplete' }, // Can be used to specify additional key bindings for the editor, as well as key bindings defined by keyMap
      lastLineBefore: 0,
    },
  });
  const { createMessage } = useMessage();

  function addItem(items: any) {
    if (
      items.length > 0 &&
      (items[items.length - 1].name.trim() === '' || items[items.length - 1].value.trim() === '')
    ) {
      createMessage.warning('Please complete the previous items before adding');
      return;
    }
    items.push({ name: '', value: '', isField: true });
  }

  function delItem(items: any, index: number) {
    items.splice(index, 1);
  }

  defineExpose({
    ...toRefs(state),
  });
</script>

<style lang="less" scoped>
  .item-desc {
    color: #939494;
  }
</style>
