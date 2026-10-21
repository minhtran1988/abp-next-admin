<!--
 * @Description: Regular verification option component
-->
<template>
  <div class="rule-props-content">
    <Form v-if="formConfig.currentItem && formConfig.currentItem['rules']">
      <div
        v-for="(item, index) of formConfig.currentItem['rules']"
        :key="index"
        class="rule-props-item"
      >
        <Icon
          icon="ant-design:close-circle-filled"
          class="rule-props-item-close"
          @click="removeRule(index)"
        />
        <FormItem label="regular" :labelCol="{ span: 6 }" :wrapperCol="{ span: 16 }">
          <AutoComplete
            v-model:value="item.pattern"
            placeholder="Please enter a regular expression"
            :dataSource="patternDataSource"
          />
        </FormItem>
        <FormItem label="Copywriting" :labelCol="{ span: 6 }" :wrapperCol="{ span: 16 }">
          <Input v-model:value="item.message" placeholder="Please enter the prompt copy" />
        </FormItem>
      </div>
    </Form>
    <a @click="addRules">
      <Icon icon="ant-design:file-add-outlined" />
      Add regular
    </a>
  </div>
</template>
<script lang="ts">
  import { ref, defineComponent } from 'vue';
  import { remove } from '../../../utils';
  import { useFormDesignState } from '../../../hooks/useFormDesignState';
  import { isArray } from 'lodash-es';
  import { Form, FormItem, AutoComplete, Input } from 'ant-design-vue';
  import Icon from '/@/components/Icon';
  export default defineComponent({
    name: 'RuleProps',
    components: {
      Form,
      FormItem,
      AutoComplete,
      Input,
      Icon,
    },
    setup() {
      // Get the status of ancestor components
      const { formConfig } = useFormDesignState();
      // Detach currentItem
      /**
       * Add a regular check to determine whether the rules of the current component are an array. If not, use the set method to reset it to an array, and then add a regular check.
       */
      const addRules = () => {
        if (!isArray(formConfig.value.currentItem!.rules))
          formConfig.value.currentItem!['rules'] = [];
        formConfig.value.currentItem!.rules?.push({ pattern: '', message: '' });
      };
      /**
       * Delete the regular check. When the regular rule is 0, delete the rules attribute.
       * @param index {number} The index of the rule to be deleted
       */
      const removeRule = (index: number) => {
        remove(formConfig.value.currentItem!.rules as Array<any>, index);
        if (formConfig.value.currentItem!.rules?.length === 0)
          delete formConfig.value.currentItem!['rules'];
      };
      const patternDataSource = ref([
        {
          value: '/^(?:(?:\\+|00)86)?1[3-9]\\d{9}$/',
          text: 'Mobile phone number',
        },
        {
          value: '/^((ht|f)tps?:\\/\\/)?[\\w-]+(\\.[\\w-]+)+:\\d{1,5}\\/?$/',
          text: 'URL with port number',
        },
        {
          value:
            '/^(((ht|f)tps?):\\/\\/)?[\\w-]+(\\.[\\w-]+)+([\\w.,@?^=%&:/~+#-\\(\\)]*[\\w@?^=%&/~+#-\\(\\)])?$/',
          text: 'URL with parameters',
        },
        {
          value: '/^[0-9A-HJ-NPQRTUWXY]{2}\\d{6}[0-9A-HJ-NPQRTUWXY]{10}$/',
          text: 'Unified Social Credit Code',
        },
        {
          value: '/^(s[hz]|S[HZ])(000[\\d]{3}|002[\\d]{3}|300[\\d]{3}|600[\\d]{3}|60[\\d]{4})$/',
          text: 'Stock code',
        },
        {
          value: '/^([a-f\\d]{32}|[A-F\\d]{32})$/',
          text: 'md5 format (32 bit)',
        },
        {
          value: '/^[a-f\\d]{4}(?:[a-f\\d]{4}-){4}[a-f\\d]{12}$/i',
          text: 'GUID/UUID',
        },
        {
          value: '/^\\d+(?:\\.\\d+){2}$/',
          text: 'Version number (x.y.z) format',
        },
        {
          value:
            '/^https?:\\/\\/(.+\\/)+.+(\\.(swf|avi|flv|mpg|rm|mov|wav|asf|3gp|mkv|rmvb|mp4))$/i',
          text: 'Video link address',
        },
        {
          value: '/^https?:\\/\\/(.+\\/)+.+(\\.(gif|png|jpg|jpeg|webp|svg|psd|bmp|tif))$/i',
          text: 'Picture link address',
        },
        {
          value: '/^-?\\d+(,\\d{3})*(\\.\\d{1,2})?$/',
          text: 'Number/currency amount (supports negative numbers, thousands separators)',
        },
        {
          value:
            '/(?:^[1-9]([0-9]+)?(?:\\.[0-9]{1,2})?$)|(?:^(?:0)$)|(?:^[0-9]\\.[0-9](?:[0-9])?$)/',
          text: 'Number/currency amount',
        },
        {
          value: '/^[1-9]\\d{9,29}$/',
          text: 'Bank card number',
        },
        {
          value: '/^(?:[\u4e00-\u9fa5·]{2,16})$/',
          text: 'Chinese name',
        },
        {
          value: '/(^[a-zA-Z][a-zA-Z\\s]{0,20}[a-zA-Z]$)/',
          text: 'English name',
        },
        {
          value:
            '/^[\\u4eac\\u6d25\\u6caa\\u6e1d\\u5180\\u8c6b\\u4e91\\u8fbd\\u9ed1\\u6e58\\u7696\\u9c81\\u65b0\\u82cf\\u6d59\\u8d63\\u9102\\u6842\\u7518\\u664b\\u8499\\u9655\\u5409\\u95fd\\u8d35\\u7ca4\\u9752\\u85cf\\u5ddd\\u5b81\\u743c\\u4f7f\\u9886][A-HJ-NP-Z](?:((\\d{5}[A-HJK])|([A-HJK][A-HJ-NP-Z0-9][0-9]{4}))|[A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9\\u6302\\u5b66\\u8b66\\u6e2f\\u6fb3])$/',
          text: 'License plate number (new energy)',
        },
        {
          value:
            '/^[\\u4eac\\u6d25\\u6caa\\u6e1d\\u5180\\u8c6b\\u4e91\\u8fbd\\u9ed1\\u6e58\\u7696\\u9c81\\u65b0\\u82cf\\u6d59\\u8d63\\u9102\\u6842\\u7518\\u664b\\u8499\\u9655\\u5409\\u95fd\\u8d35\\u7ca4\\u9752\\u85cf\\u5ddd\\u5b81\\u743c\\u4f7f\\u9886][A-HJ-NP-Z][A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9\\u6302\\u5b66\\u8b66\\u6e2f\\u6fb3]$/',
          text: 'License plate number (non-new energy)',
        },
        {
          value:
            '/^[\\u4eac\\u6d25\\u6caa\\u6e1d\\u5180\\u8c6b\\u4e91\\u8fbd\\u9ed1\\u6e58\\u7696\\u9c81\\u65b0\\u82cf\\u6d59\\u8d63\\u9102\\u6842\\u7518\\u664b\\u8499\\u9655\\u5409\\u95fd\\u8d35\\u7ca4\\u9752\\u85cf\\u5ddd\\u5b81\\u743c\\u4f7f\\u9886][A-HJ-NP-Z][A-HJ-NP-Z0-9]{4,5}[A-HJ-NP-Z0-9\\u6302\\u5b66\\u8b66\\u6e2f\\u6fb3]$/',
          text: 'License plate number (new energy + non-new energy)',
        },
        {
          value:
            '/^(([^<>()[\\]\\\\.,;:\\s@"]+(\\.[^<>()[\\]\\\\.,;:\\s@"]+)*)|(".+"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$/',
          text: 'email',
        },
        {
          value: '/^(?:(?:\\d{3}-)?\\d{8}|^(?:\\d{4}-)?\\d{7,8})(?:-\\d+)?$/',
          text: 'landline',
        },
        {
          value:
            '/^[1-9]\\d{5}(?:18|19|20)\\d{2}(?:0[1-9]|10|11|12)(?:0[1-9]|[1-2]\\d|30|31)\\d{3}[\\dXx]$/',
          text: 'ID number',
        },
        {
          value:
            '/(^[EeKkGgDdSsPpHh]\\d{8}$)|(^(([Ee][a-fA-F])|([DdSsPp][Ee])|([Kk][Jj])|([Mm][Aa])|(1[45]))\\d{7}$)/',
          text: 'Passport',
        },
        {
          value:
            '/^(?:[\u3400-\u4DB5\u4E00-\u9FEA\uFA0E\uFA0F\uFA11\uFA13\uFA14\uFA1F\uFA21\uFA23\uFA24\uFA27-\uFA29]|[\uD840-\uD868\uD86A-\uD86C\uD86F-\uD872\uD874-\uD879][\uDC00-\uDFFF]|\uD869[\uDC00-\uDED6\uDF00-\uDFFF]|\uD86D[\uDC00-\uDF34\uDF40-\uDFFF]|\uD86E[\uDC00-\uDC1D\uDC20-\uDFFF]|\uD873[\uDC00-\uDEA1\uDEB0-\uDFFF]|\uD87A[\uDC00-\uDFE0])+$/',
          text: 'Chinese characters',
        },
        {
          value: '/^\\d+\\.\\d+$/',
          text: 'decimal',
        },
        {
          value: '/^\\d{1,}$/',
          text: 'Number',
        },
        {
          value: '/^[1-9][0-9]{4,10}$/',
          text: 'qq number',
        },
        {
          value: '/^[A-Za-z0-9]+$/',
          text: 'Numbers and letters',
        },
        {
          value: '/^[a-zA-Z]+$/',
          text: 'English letters',
        },
        {
          value: '/^[a-z]+$/',
          text: 'lowercase English letters',
        },
        {
          value: '/^[A-Z]+$/',
          text: 'uppercase English letters',
        },
        {
          value: '/^[a-zA-Z0-9_-]{4,16}$/',
          text: 'Username verification, 4 to 16 characters (letters, numbers, underscores, minus signs)',
        },
        {
          value: '/^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$/',
          text: 'Hex color',
        },
        {
          value: '/^[a-zA-Z][-_a-zA-Z0-9]{5,19}$/',
          text: 'WeChat ID',
        },
        {
          value: '/^(0[1-7]|1[0-356]|2[0-7]|3[0-6]|4[0-7]|5[1-7]|6[1-7]|7[0-5]|8[013-6])\\d{4}$/',
          text: 'Postal Code (China)',
        },
        {
          value: '/^[^A-Za-z]*$/',
          text: 'cannot contain letters',
        },
        {
          value: '/^\\+?[1-9]\\d*$/',
          text: 'Positive integer, not including 0',
        },
        {
          value: '/^-[1-9]\\d*$/',
          text: 'Negative integer, excluding 0',
        },
        {
          value: '/^-?[0-9]\\d*$/',
          text: 'integer',
        },
        {
          value: '/^(-?\\d+)(\\.\\d+)?$/',
          text: 'Floating point number',
        },
        {
          value: '/^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)+$/',
          text: 'email (supports Chinese email)',
        },
      ]);
      return { addRules, removeRule, formConfig, patternDataSource };
    },
  });
</script>

<style lang="less" scoped>
  :deep(.icon) {
    width: 1em;
    height: 1em;
    vertical-align: -0.15em;
    fill: currentColor;
    overflow: hidden;
  }

  .rule-props-content {
    :deep(.ant-form-item) {
      margin-bottom: 0;
    }

    .rule-props-item {
      position: relative;
      background-color: #f0eded;
      padding: 3px 2px;
      border-radius: 5px;
      margin-bottom: 5px;

      :deep(.ant-form-item) {
        border: 0 !important;
      }

      &-close {
        position: absolute;
        top: -5px;
        right: -5px;
        color: #ccc;
        cursor: pointer;
        border-radius: 7px;
        background-color: #a3a0a0;
        z-index: 999;

        &:hover {
          color: #00c;
        }
      }
    }
  }
</style>
