<script lang="ts" setup>
import { ref } from 'vue';

import { Page } from '@vben/common-ui';

import { Button, Card, Switch } from 'ant-design-vue';

import { useVbenForm } from '#/adapter/form';

defineOptions({
  name: 'ScrollToErrorTest',
});

const scrollEnabled = ref(true);

const [Form, formApi] = useVbenForm({
  scrollToFirstError: scrollEnabled.value,
  schema: [
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter username',
      },
      fieldName: 'username',
      label: 'username',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter your email',
      },
      fieldName: 'email',
      label: 'mailbox',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter your mobile phone number',
      },
      fieldName: 'phone',
      label: 'Mobile phone number',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter address',
      },
      fieldName: 'address',
      label: 'address',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter remarks',
      },
      fieldName: 'remark',
      label: 'Remarks',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter company name',
      },
      fieldName: 'company',
      label: 'company name',
      rules: 'required',
    },
    {
      component: 'Input',
      componentProps: {
        placeholder: 'Please enter the position',
      },
      fieldName: 'position',
      label: 'Position',
      rules: 'required',
    },
    {
      component: 'Select',
      componentProps: {
        options: [
          { label: 'male', value: 'male' },
          { label: 'female', value: 'female' },
        ],
        placeholder: 'Please select gender',
      },
      fieldName: 'gender',
      label: 'gender',
      rules: 'selectRequired',
    },
  ],
  showDefaultActions: false,
});

// Test validateAndSubmitForm (validate and submit)
async function testValidateAndSubmit() {
  await formApi.validateAndSubmitForm();
}

// Test validate (manually validate the entire form)
async function testValidate() {
  await formApi.validate();
}

// Test validateField (validate a single field)
async function testValidateField() {
  await formApi.validateField('username');
}

// Toggle scrolling function
function toggleScrollToError() {
  formApi.setState({ scrollToFirstError: scrollEnabled.value });
}

// Fill in some data to test
async function fillPartialData() {
  await formApi.resetForm();
  await formApi.setFieldValue('username', 'Test user');
  await formApi.setFieldValue('email', 'test@example.com');
}
</script>

<template>
  <Page
    description="Test the function of automatically scrolling to the wrong field when form validation fails"
    title="Scroll to error field test"
  >
    <Card title="Functional Test">
      <template #extra>
        <div class="flex items-center gap-2">
          <Switch v-model="scrollEnabled" @change="toggleScrollToError" />
          <span>Enable scrolling to error fields</span>
        </div>
      </template>

      <div class="space-y-4">
        <div class="rounded bg-blue-50 p-4">
          <h3 class="mb-2 font-medium">Test instructions:</h3>
          <ul class="list-inside list-disc space-y-1 text-sm">
            <li>All validation methods will automatically scroll to the first error field when validation fails</li>
            <li>You can control whether to enable the automatic scrolling function through the switch in the upper right corner</li>
          </ul>
        </div>

        <div class="rounded border p-4">
          <h4 class="mb-3 font-medium">Verification method test:</h4>
          <div class="flex flex-wrap gap-2">
            <Button type="primary" @click="testValidateAndSubmit">
              Test validateAndSubmitForm()
            </Button>
            <Button @click="testValidate"> Test validate() </Button>
            <Button @click="testValidateField"> Test validateField() </Button>
          </div>
          <div class="mt-2 text-xs text-gray-500">
            <p>• validateAndSubmitForm(): Validate the form and submit it</p>
            <p>• validate(): Manually validate the entire form</p>
            <p>• validateField(): Validate a single field (here the username field is tested)</p>
          </div>
        </div>

        <div class="rounded border p-4">
          <h4 class="mb-3 font-medium">Data filling test:</h4>
          <div class="flex flex-wrap gap-2">
            <Button @click="fillPartialData"> Fill in partial data </Button>
            <Button @click="() => formApi.resetForm()"> Clear the form </Button>
          </div>
          <div class="mt-2 text-xs text-gray-500">
            <p>• After filling in some data and validating, it will scroll to the first error field</p>
          </div>
        </div>

        <Form />
      </div>
    </Card>
  </Page>
</template>
