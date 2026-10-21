<script lang="ts" setup>
import type { UploadFile } from 'ant-design-vue';

import { h, ref, toRaw } from 'vue';

import { Page } from '@vben/common-ui';

import { useDebounceFn } from '@vueuse/core';
import { Button, Card, message, Spin, Tag } from 'ant-design-vue';
import dayjs from 'dayjs';

import { useVbenForm, z } from '#/adapter/form';
import { getAllMenusApi } from '#/api';
import { upload_file } from '#/api/examples/upload';
import { $t } from '#/locales';

import DocButton from '../doc-button.vue';

const keyword = ref('');
const fetching = ref(false);
// Simulate remote data acquisition
function fetchRemoteOptions({ keyword = 'options' }: Record<string, any>) {
  fetching.value = true;
  return new Promise((resolve) => {
    setTimeout(() => {
      const options = Array.from({ length: 10 }).map((_, index) => ({
        label: `${keyword}-${index}`,
        value: `${keyword}-${index}`,
      }));
      resolve(options);
      fetching.value = false;
    }, 1000);
  });
}

const [BaseForm, baseFormApi] = useVbenForm({
  // Common to all form items, they can be overridden individually within the form.
  commonConfig: {
    // Display a colon after the label
    colon: true,
    // All form items
    componentProps: {
      class: 'w-full',
    },
  },
  fieldMappingTime: [['rangePicker', ['startTime', 'endTime'], 'YYYY-MM-DD']],
  // Submit function
  handleSubmit: onSubmit,
  handleValuesChange(_values, fieldsChanged) {
    message.info(`The following fields of the form have changed: ${fieldsChanged.join(', ')}`);
  },

  // Vertical layout, label and input are on different lines, the value is vertical
  // Horizontal layout, label and input are on the same line
  layout: 'horizontal',
  schema: [
    {
      // The component needs to be registered in #/adapter.ts and add the type
      component: 'Input',
      // Parameters of the corresponding component
      componentProps: {
        placeholder: 'Please enter username',
      },
      // Field name
      fieldName: 'username',
      // Label displayed on the interface
      label: 'string',
      rules: 'required',
    },
    {
      component: 'Input',
      fieldName: 'desc',
      // Description displayed on the interface
      description: 'This is the form description',
      label: 'String (with description)',
    },
    {
      // The component needs to be registered in #/adapter.ts and add the type
      component: 'ApiSelect',
      // Parameters of the corresponding component
      componentProps: {
        // Convert menu interface to options format
        afterFetch: (data: { name: string; path: string }[]) => {
          return data.map((item: any) => ({
            label: item.name,
            value: item.path,
          }));
        },
        // Menu interface
        api: getAllMenusApi,
        autoSelect: 'first',
      },
      // Field name
      fieldName: 'api',
      // Label displayed on the interface
      label: 'ApiSelect',
    },
    {
      component: 'ApiSelect',
      // Parameters of the corresponding component
      componentProps: () => {
        return {
          api: fetchRemoteOptions,
          // Disable local filtering
          filterOption: false,
          // If data is being obtained, use the slot to display a loading
          notFoundContent: fetching.value ? undefined : null,
          // Record when the search term changes, use useDebounceFn to stabilize it.
          onSearch: useDebounceFn((value: string) => {
            keyword.value = value;
          }, 300),
          // Remote search parameters. When the search term changes, params will also be updated.
          params: {
            keyword: keyword.value || undefined,
          },
          showSearch: true,
        };
      },
      // Field name
      fieldName: 'remoteSearch',
      // Label displayed on the interface
      label: 'remote search',
      renderComponentContent: () => {
        return {
          notFoundContent: fetching.value ? h(Spin) : undefined,
        };
      },
      rules: 'selectRequired',
    },
    {
      component: 'ApiTreeSelect',
      // Parameters of the corresponding component
      componentProps: {
        // Menu interface
        api: getAllMenusApi,
        // Convert menu interface to options format
        labelField: 'name',
        valueField: 'path',
        childrenField: 'children',
      },
      // Field name
      fieldName: 'apiTree',
      // Label displayed on the interface
      label: 'ApiTreeSelect',
    },
    {
      component: 'InputPassword',
      componentProps: {
        placeholder: 'Please enter password',
      },
      fieldName: 'password',
      label: 'password',
    },
    {
      component: 'InputNumber',
      componentProps: {
        placeholder: 'Please enter',
      },
      fieldName: 'number',
      label: 'Number (with suffix)',
      suffix: () => '¥',
    },
    {
      component: 'IconPicker',
      fieldName: 'icon',
      label: 'icon',
    },
    {
      colon: false,
      component: 'Select',
      componentProps: {
        allowClear: true,
        filterOption: true,
        options: [
          {
            label: 'Option 1',
            value: '1',
          },
          {
            label: 'Option 2',
            value: '2',
          },
        ],
        placeholder: 'Please select',
        showSearch: true,
      },
      fieldName: 'options',
      label: () => h(Tag, { color: 'warning' }, () => '😎Customized: '),
    },
    {
      component: 'RadioGroup',
      componentProps: {
        options: [
          {
            label: 'Option 1',
            value: '1',
          },
          {
            label: 'Option 2',
            value: '2',
          },
        ],
      },
      fieldName: 'radioGroup',
      label: 'radio group',
    },
    {
      component: 'Radio',
      fieldName: 'radio',
      label: '',
      renderComponentContent: () => {
        return {
          default: () => ['Radio'],
        };
      },
    },
    {
      component: 'CheckboxGroup',
      componentProps: {
        name: 'cname',
        options: [
          {
            label: 'Option 1',
            value: '1',
          },
          {
            label: 'Option 2',
            value: '2',
          },
        ],
      },
      fieldName: 'checkboxGroup',
      label: 'Multiple selection group',
    },
    {
      component: 'Checkbox',
      fieldName: 'checkbox',
      label: '',
      renderComponentContent: () => {
        return {
          default: () => ['I have read and agree'],
        };
      },
      rules: z
        .boolean()
        .refine((v) => v, { message: 'Why do you not agree? Check it!' }),
    },
    {
      component: 'Mentions',
      componentProps: {
        options: [
          {
            label: 'afc163',
            value: 'afc163',
          },
          {
            label: 'zombieJ',
            value: 'zombieJ',
          },
        ],
        placeholder: 'Please enter',
      },
      fieldName: 'mentions',
      label: 'mentions',
    },
    {
      component: 'Rate',
      fieldName: 'rate',
      label: 'rating',
    },
    {
      component: 'Switch',
      componentProps: {
        class: 'w-auto',
      },
      fieldName: 'switch',
      help: () =>
        ['This is a multi-line help message', 'Second line', 'Third line'].map((v) => h('p', v)),
      label: 'switch',
    },
    {
      component: 'DatePicker',
      fieldName: 'datePicker',
      label: 'Date selection box',
    },
    {
      component: 'RangePicker',
      fieldName: 'rangePicker',
      label: 'range selector',
    },
    {
      component: 'TimePicker',
      fieldName: 'timePicker',
      label: 'Time selection box',
    },
    {
      component: 'TreeSelect',
      componentProps: {
        allowClear: true,
        placeholder: 'Please select',
        showSearch: true,
        treeData: [
          {
            label: 'root 1',
            value: 'root 1',
            children: [
              {
                label: 'parent 1',
                value: 'parent 1',
                children: [
                  {
                    label: 'parent 1-0',
                    value: 'parent 1-0',
                    children: [
                      {
                        label: 'my leaf',
                        value: 'leaf1',
                      },
                      {
                        label: 'your leaf',
                        value: 'leaf2',
                      },
                    ],
                  },
                  {
                    label: 'parent 1-1',
                    value: 'parent 1-1',
                  },
                ],
              },
              {
                label: 'parent 2',
                value: 'parent 2',
              },
            ],
          },
        ],
        treeNodeFilterProp: 'label',
      },
      fieldName: 'treeSelect',
      label: 'tree selection',
    },
    {
      component: 'Upload',
      componentProps: {
        // For more attributes, see: https://ant.design/components/upload-cn
        accept: '.png,.jpg,.jpeg',
        // Automatically carry authentication information
        customRequest: upload_file,
        disabled: false,
        maxCount: 1,
        // Unit: MB
        maxSize: 2,
        multiple: false,
        showUploadList: true,
        // Built-in style for upload list, supports four basic styles text, picture, picture-card and picture-circle
        listType: 'picture-card',
        // The onChange event has been rewritten. If you need to customize it, please extend it on this basis.
        handleChange: ({ file }: { file: UploadFile }) => {
          const { name, status } = file;
          if (status === 'done') {
            message.success(`${name} ${$t('examples.form.upload-success')}`);
          } else if (status === 'error') {
            message.error(`${name} ${$t('examples.form.upload-fail')}`);
          }
        },
      },
      fieldName: 'files',
      label: $t('examples.form.file'),
      renderComponentContent: () => {
        return {
          default: () => $t('examples.form.upload-image'),
        };
      },
      rules: 'selectRequired',
    },
    {
      component: 'Upload',
      componentProps: {
        accept: '.png,.jpg,.jpeg',
        customRequest: upload_file,
        maxCount: 1,
        maxSize: 2,
        listType: 'picture-card',
        // Whether to enable image cropping (multi-select or non-pictures do not raise the cropping box)
        crop: true,
        // Crop ratio
        aspectRatio: '1:1',
      },
      fieldName: 'cropImage',
      label: $t('examples.form.crop-image'),
      renderComponentContent: () => {
        return {
          default: () => $t('examples.form.upload-image'),
        };
      },
      rules: 'selectRequired',
    },
  ],
  // The large screen displays 3 items in one line, the medium screen displays 2 items in one line, and the small screen displays 1 item in one line.
  wrapperClass: 'grid-cols-1 md:grid-cols-2 lg:grid-cols-3',
});

function onSubmit(values: Record<string, any>) {
  const files = toRaw(values.files) as UploadFile[];
  const cropImage = (toRaw(values.cropImage) ?? []) as UploadFile[];
  const doneFiles = files.filter((file) => file.status === 'done');
  const failedFiles = files.filter((file) => file.status !== 'done');
  const doneCrop = cropImage.filter((file) => file.status === 'done');
  const failedCrop = cropImage.filter((file) => file.status !== 'done');

  const msg = [
    ...doneFiles.map((file) => file.response?.url || file.url),
    ...failedFiles.map((file) => file.name),
  ].join(', ');
  const msgCrop = [
    ...doneCrop.map((file) => file.response?.url || file.url),
    ...failedCrop.map((file) => file.name),
  ].join(', ');

  if (failedFiles.length === 0) {
    message.success({
      content: `${$t('examples.form.upload-urls')}: ${msg}`,
    });
  } else {
    message.error({
      content: `${$t('examples.form.upload-error')}: ${msg}`,
    });
    return;
  }
  if (doneCrop.length > 0 && failedCrop.length === 0) {
    message.success({
      content: `${$t('examples.form.upload-urls')}: ${msgCrop}`,
    });
  } else if (failedCrop.length > 0) {
    message.error({
      content: `${$t('examples.form.upload-error')}: ${msgCrop}`,
    });
    return;
  }
  // If necessary, replace it with the required urls before submitting
  values.files = doneFiles.map((file) => file.response?.url || file.url);
  values.cropImage = doneCrop.map((file) => file.response?.url || file.url);
  message.success({
    content: `form values: ${JSON.stringify(values)}`,
  });
}

function handleSetFormValue() {
  /**
   * Set form values (multiple)
   */
  baseFormApi.setValues({
    checkboxGroup: ['1'],
    datePicker: dayjs('2022-01-01'),
    files: [
      {
        name: 'example.png',
        status: 'done',
        uid: '-1',
        url: 'https://unpkg.com/@vbenjs/static-source@0.1.7/source/logo-v1.webp',
      },
    ],
    mentions: '@afc163',
    number: 3,
    options: '1',
    password: '2',
    radioGroup: '1',
    rangePicker: [dayjs('2022-01-01'), dayjs('2022-01-02')],
    rate: 3,
    switch: true,
    timePicker: dayjs('2022-01-01 12:00:00'),
    treeSelect: 'leaf1',
    username: '1',
  });

  // Set a single form value
  baseFormApi.setFieldValue('checkbox', true);
}
</script>

<template>
  <Page
    content-class="flex flex-col gap-4"
    description="Basic example of form component, please note that some simple comments will be added to the parameter code used on this page for easy understanding, please review it carefully."
    title="Form component"
  >
    <template #description>
      <div class="text-muted-foreground">
        <p>
          Basic example of form component. Please note that some simple comments will be added to the parameter code used on this page for easy understanding. Please review it carefully.
        </p>
      </div>
    </template>
    <template #extra>
      <DocButton class="mb-2" path="/components/common-ui/vben-form" />
    </template>
    <Card title="Basic Example">
      <template #extra>
        <Button type="primary" @click="handleSetFormValue">Set form value</Button>
      </template>
      <BaseForm />
    </Card>
  </Page>
</template>
