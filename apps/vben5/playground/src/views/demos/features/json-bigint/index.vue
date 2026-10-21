<script lang="ts" setup>
import { ref } from 'vue';

import { Page } from '@vben/common-ui';

import { Alert, Button, Card } from 'ant-design-vue';

import { getBigIntData } from '#/api/examples/json-bigint';

const response = ref('');
function fetchData() {
  getBigIntData().then((res) => {
    response.value = res;
  });
}
</script>
<template>
  <Page
    title="JSON BigInt Support"
    description="Parse the long integer (long/bigInt) returned by the backend. Code location: transformResponse in playground/src/api/request.ts"
  >
    <Card>
      <Alert>
        <template #message>
          The ID returned by some backend interfaces is a long integer, but JavaScript's native JSON parsing does not support long integers exceeding 2^53-1.
          In this case, it is recommended that the backend convert the long integer to a string type before returning the data. If the backend doesn’t accept our suggestion 😡…
          <br />
          After clicking the button below, a request will be initiated. The id field in the JSON data returned by the interface is a number that exceeds the integer range and has been automatically parsed into a string.
        </template>
      </Alert>
      <Button class="mt-4" type="primary" @click="fetchData">Initiate request</Button>
      <div>
        <pre>
        {{ response }}
        </pre>
      </div>
    </Card>
  </Page>
</template>
