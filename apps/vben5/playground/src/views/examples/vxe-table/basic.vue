<script lang="ts" setup>
import type { VxeGridListeners, VxeGridProps } from '#/adapter/vxe-table';

import { Page } from '@vben/common-ui';

import { Button, message } from 'ant-design-vue';

import { useVbenVxeGrid } from '#/adapter/vxe-table';

import DocButton from '../doc-button.vue';
import { MOCK_TABLE_DATA } from './table-data';

interface RowType {
  address: string;
  age: number;
  id: number;
  name: string;
  nickname: string;
  role: string;
}

const gridOptions: VxeGridProps<RowType> = {
  columns: [
    { title: 'serial number', type: 'seq', width: 50 },
    { field: 'name', title: 'Name' },
    { field: 'age', sortable: true, title: 'Age' },
    { field: 'nickname', title: 'Nickname' },
    { field: 'role', title: 'Role' },
    { field: 'address', showOverflow: true, title: 'Address' },
  ],
  data: MOCK_TABLE_DATA,
  pagerConfig: {
    enabled: false,
  },
  sortConfig: {
    multiple: true,
  },
};

const gridEvents: VxeGridListeners<RowType> = {
  cellClick: ({ row }) => {
    message.info(`cell-click: ${row.name}`);
  },
};

// @ts-ignore
const [Grid, gridApi] = useVbenVxeGrid<RowType>({
  // Release the comment to view the type of form component
  // formOptions: {
  //   schema: [
  //     {
  //       component: 'Switch',
  //       fieldName: 'name',
  //     },
  //   ],
  // },
  gridEvents,
  gridOptions,
});

// Release the comment to view the type of the current table instance
// gridApi.grid

const showBorder = gridApi.useStore((state) => state.gridOptions?.border);
const showStripe = gridApi.useStore((state) => state.gridOptions?.stripe);

function changeBorder() {
  gridApi.setGridOptions({
    border: !showBorder.value,
  });
}

function changeStripe() {
  gridApi.setGridOptions({
    stripe: !showStripe.value,
  });
}

function changeLoading() {
  gridApi.setLoading(true);
  setTimeout(() => {
    gridApi.setLoading(false);
  }, 2000);
}
</script>

<template>
  <Page
    description="The table component is often used to quickly develop data display and interactive interfaces. The sample data is static data. This component is a simple secondary encapsulation of vxe-table. Most of the properties and methods are consistent with vxe-table."
    title="Table basic example"
  >
    <template #extra>
      <DocButton path="/components/common-ui/vben-vxe-table" />
    </template>
    <Grid table-title="Basic List" table-title-help="Tips">
      <!-- <template #toolbar-actions>
        <Button class="mr-2" type="primary">Left slot</Button>
      </template> -->
      <template #toolbar-tools>
        <Button class="mr-2" type="primary" @click="changeBorder">
          {{ showBorder ? 'Hide' : 'Show' }} border
        </Button>
        <Button class="mr-2" type="primary" @click="changeLoading">
          Show loading
        </Button>
        <Button type="primary" @click="changeStripe">
          {{ showStripe ? 'Hide' : 'Show' }} Zebra stripe
        </Button>
      </template>
    </Grid>
  </Page>
</template>
