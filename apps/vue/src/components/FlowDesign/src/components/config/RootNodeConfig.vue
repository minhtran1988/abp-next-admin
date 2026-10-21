<template>
  <div>
    <p class="desc">Select the person/department that can initiate the approval. If not selected, it will be open to everyone by default</p>
    <Button size="small" @click="selectOrg" type="primary" round>
      <template #icon>
        <PlusOutlined />
      </template>
      Please select</Button
    >
    <OrgItems v-model:value="select" />
    <OrgPicker
      title="Please select the person/department that can initiate this approval"
      multiple
      ref="orgPickerRef"
      :selected="select"
      @ok="selected"
    />
  </div>
</template>

<script setup lang="ts">
  import { computed, nextTick, reactive, ref, unref } from 'vue';
  import { Button } from 'ant-design-vue';
  import { PlusOutlined } from '@ant-design/icons-vue';
  import OrgItems from '../OrgItems.vue';
  import OrgPicker from '../OrgPicker.vue';

  const props = defineProps({
    config: {
      type: Object,
      default: () => {
        return {};
      },
    },
  });
  const orgPickerRef = ref<any>();
  const select = computed(() => {
    return props.config.assignedUser;
  });
  const state = reactive({
    showOrgSelect: false,
  });

  function selectOrg() {
    nextTick(() => {
      const orgPicker = unref(orgPickerRef);
      orgPicker?.show();
    });
  }

  function selected(sel: any) {
    select.value.length = 0;
    sel.forEach((val) => select.value.push(val));
  }

  function removeOrgItem(index: number) {
    select.value.splice(index, 1);
  }
</script>

<style scoped></style>
