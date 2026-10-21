<!--
 * @Description: Right property panel control form property panel
-->
<template>
  <div class="properties-content">
    <Form class="properties-body" label-align="left" layout="vertical">
      <!--      <e-upload v-model="fileList"></e-upload>-->

      <FormItem label="Form layout">
        <RadioGroup button-style="solid" v-model:value="formConfig.layout">
          <RadioButton value="horizontal">horizontal</RadioButton>
          <RadioButton value="vertical" :disabled="formConfig.labelLayout === 'Grid'">
            vertical
          </RadioButton>
          <RadioButton value="inline" :disabled="formConfig.labelLayout === 'Grid'">
            Inside the industry
          </RadioButton>
        </RadioGroup>
      </FormItem>

      <!-- <Row> -->
      <FormItem label="Label layout">
        <RadioGroup
          buttonStyle="solid"
          v-model:value="formConfig.labelLayout"
          @change="lableLayoutChange"
        >
          <RadioButton value="flex">Fixed</RadioButton>
          <RadioButton value="Grid" :disabled="formConfig.layout !== 'horizontal'">
            grid
          </RadioButton>
        </RadioGroup>
      </FormItem>
      <!-- </Row> -->
      <FormItem label="Label width (px)" v-show="formConfig.labelLayout === 'flex'">
        <InputNumber
          :style="{ width: '100%' }"
          v-model:value="formConfig.labelWidth"
          :min="0"
          :step="1"
        />
      </FormItem>
      <div v-if="formConfig.labelLayout === 'Grid'">
        <FormItem label="labelCol">
          <Slider v-model:value="formConfig.labelCol!.span" :max="24" />
        </FormItem>
        <FormItem label="wrapperCol">
          <Slider v-model:value="formConfig.wrapperCol!.span" :max="24" />
        </FormItem>

        <FormItem label="Label alignment">
          <RadioGroup button-style="solid" v-model:value="formConfig.labelAlign">
            <RadioButton value="left">Left</RadioButton>
            <RadioButton value="right">Keep right</RadioButton>
          </RadioGroup>
        </FormItem>

        <FormItem label="Control size">
          <RadioGroup button-style="solid" v-model:value="formConfig.size">
            <RadioButton value="default">Default</RadioButton>
            <RadioButton value="small">small</RadioButton>
            <RadioButton value="large">Large</RadioButton>
          </RadioGroup>
        </FormItem>
      </div>
      <FormItem label="Form Properties">
        <Col
          ><Checkbox v-model:checked="formConfig.colon" v-if="formConfig.layout == 'horizontal'"
            >Display colon after label</Checkbox
          ></Col
        >
        <Col><Checkbox v-model:checked="formConfig.disabled">disable</Checkbox></Col>
        <Col><Checkbox v-model:checked="formConfig.hideRequiredMark">Hide required mark</Checkbox></Col>
      </FormItem>
    </Form>
  </div>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { useFormDesignState } from '../../../hooks/useFormDesignState';
  import { InputNumber, Slider, Checkbox, Col, RadioChangeEvent } from 'ant-design-vue';
  // import RadioButtonGroup from '/@/components/RadioButtonGroup.vue';
  import { Form, FormItem, Radio } from 'ant-design-vue';
  export default defineComponent({
    name: 'FormProps',
    components: {
      InputNumber,
      Slider,
      Checkbox,
      RadioGroup: Radio.Group,
      RadioButton: Radio.Button,
      Form,
      FormItem,
      Col,
    },
    setup() {
      const { formConfig } = useFormDesignState();
      formConfig.value = formConfig.value || {
        labelCol: { span: 24 },
        wrapperCol: { span: 24 },
      };
      const lableLayoutChange = (e: RadioChangeEvent) => {
        if (e.target.value === 'Grid') {
          formConfig.value.layout = 'horizontal';
        }
      };
      return { formConfig, lableLayoutChange };
    },
  });
</script>
