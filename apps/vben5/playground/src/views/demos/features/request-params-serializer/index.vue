<script lang="ts" setup>
import { computed, ref, watchEffect } from 'vue';

import { Page } from '@vben/common-ui';

import { Card, Radio, RadioGroup } from 'ant-design-vue';

import { getParamsData } from '#/api/examples/params';

const params = { ids: [2512, 3241, 4255] };
const paramsSerializer = ref<'brackets' | 'comma' | 'indices' | 'repeat'>(
  'brackets',
);
const response = ref('');
const paramsStr = computed(() => {
  // Write a piece of code to extract the parameter part from the complete URL
  const url = response.value;
  return new URL(url).searchParams.toString();
});

watchEffect(() => {
  getParamsData(params, paramsSerializer.value).then((res) => {
    response.value = res.request.responseURL;
  });
});
</script>
<template>
  <Page
    title="Request parameter serialization"
    description="Different backend interfaces may parse array type GET parameters in different ways. We have preset several array serialization methods and configure paramsSerializer to achieve different serialization methods."
  >
    <Card>
      <RadioGroup v-model:value="paramsSerializer" name="paramsSerializer">
        <Radio value="brackets">brackets</Radio>
        <Radio value="comma">comma</Radio>
        <Radio value="indices">indices</Radio>
        <Radio value="repeat">repeat</Radio>
      </RadioGroup>
      <div class="mt-4 flex flex-col gap-4">
        <div>
          <h3>Parameters that need to be submitted</h3>
          <div>{{ JSON.stringify(params, null, 2) }}</div>
        </div>
        <template v-if="response">
          <div>
            <h3>Access address</h3>
            <pre>{{ response }}</pre>
          </div>
          <div>
            <h3>Parameter string</h3>
            <pre>{{ paramsStr }}</pre>
          </div>
          <div>
            <h3>Parameter decoding</h3>
            <pre>{{ decodeURIComponent(paramsStr) }}</pre>
          </div>
        </template>
      </div>
    </Card>
  </Page>
</template>
