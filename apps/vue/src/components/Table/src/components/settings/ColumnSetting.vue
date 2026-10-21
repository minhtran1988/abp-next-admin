<template>
  <Tooltip placement="top">
    <template #title>
      <span>{{ t('component.table.settingColumn') }}</span>
    </template>
    <Popover
      placement="bottomLeft"
      trigger="click"
      @open-change="onOpenChange"
      :overlayClassName="`${prefixCls}__column-list`"
      :getPopupContainer="getPopupContainer"
    >
      <template #title>
        <div :class="`${prefixCls}__popover-title`">
          <Checkbox
            :indeterminate="indeterminate"
            v-model:checked="isColumnAllSelected"
            @change="onColumnAllSelectChange"
          >
            {{ t('component.table.settingColumnShow') }}
          </Checkbox>

          <Checkbox v-model:checked="isIndexColumnShow" @change="onIndexColumnShowChange">
            {{ t('component.table.settingIndexColumnShow') }}
          </Checkbox>
          <!-- Appears only after rowSelection is set -->
          <Checkbox
            v-model:checked="isRowSelectionShow"
            @change="onRowSelectionShowChange"
            v-if="defaultIsRowSelectionShow"
          >
            {{ t('component.table.settingSelectColumnShow') }}
          </Checkbox>

          <Checkbox v-model:checked="isAllowResizeColumn" @change="onColumnAllowResizeChange">
            {{ t('component.table.settingDragColumnShow') }}
          </Checkbox>


          <a-button size="small" type="link" @click="onReset">
            {{ t('common.resetText') }}
          </a-button>
        </div>
      </template>

      <template #content>
        <ScrollContainer>
          <Checkbox.Group v-model:value="columnCheckedOptions" ref="columnOptionsRef">
            <template v-for="opt in columnOptions" :key="opt.value">
              <div :class="`${prefixCls}__check-item`" :data-no="opt.value">
                <DragOutlined class="table-column-drag-icon" />
                <Checkbox :value="opt.value">
                  {{ opt.label }}
                </Checkbox>

                <Tooltip
                  placement="bottomLeft"
                  :mouseLeaveDelay="0.4"
                  :getPopupContainer="getPopupContainer"
                >
                  <template #title>
                    {{ t('component.table.settingFixedLeft') }}
                  </template>
                  <Icon
                    icon="line-md:arrow-align-left"
                    :class="[
                      `${prefixCls}__fixed-left`,
                      {
                        active: opt.fixed === 'left',
                        disabled: opt.value ? !columnCheckedOptions.includes(opt.value) : true,
                      },
                    ]"
                    @click="onColumnFixedChange(opt, 'left')"
                  />
                </Tooltip>
                <Divider type="vertical" />
                <Tooltip
                  placement="bottomLeft"
                  :mouseLeaveDelay="0.4"
                  :getPopupContainer="getPopupContainer"
                >
                  <template #title>
                    {{ t('component.table.settingFixedRight') }}
                  </template>
                  <Icon
                    icon="line-md:arrow-align-left"
                    :class="[
                      `${prefixCls}__fixed-right`,
                      {
                        active: opt.fixed === 'right',
                        disabled: opt.value ? !columnCheckedOptions.includes(opt.value) : true,
                      },
                    ]"
                    @click="onColumnFixedChange(opt, 'right')"
                  />
                </Tooltip>
              </div>
            </template>
          </Checkbox.Group>
        </ScrollContainer>
      </template>
      <SettingOutlined />
    </Popover>
  </Tooltip>
</template>
<script lang="ts" setup>
  import type { BasicColumn, ColumnOptionsType, ColumnChangeParam } from '../../types/table';
  import { ref, nextTick, unref, computed, useAttrs, watch, onMounted } from 'vue';
  import { Tooltip, Popover, Checkbox, Divider } from 'ant-design-vue';
  import type { CheckboxChangeEvent } from 'ant-design-vue/lib/checkbox/interface';
  import { SettingOutlined, DragOutlined } from '@ant-design/icons-vue';
  import { Icon } from '/@/components/Icon';
  import { ScrollContainer } from '/@/components/Container';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { useTableContext } from '../../hooks/useTableContext';
  import { useDesign } from '/@/hooks/web/useDesign';
  import { isFunction, isNil, isNumber } from '/@/utils/is';
  import { getPopupContainer as getParentContainer } from '/@/utils';
  import { cloneDeep, omit } from 'lodash-es';
  import Sortablejs from 'sortablejs';
  import { INDEX_COLUMN_FLAG } from '/@/components/Table/src/const';

  // List settings cache
  import { useTableSettingStore } from '/@/store/modules/tableSetting';
  import { useRoute } from 'vue-router';
  import { TableRowSelection } from '/@/components/Table/src/types/table';

  const tableSettingStore = useTableSettingStore();

  // defineOptions({ name: 'ColumnSetting' });
  const emit = defineEmits(['columns-change']);

  const route = useRoute();

  const { t } = useI18n();
  const { prefixCls } = useDesign('basic-column-setting');

  const attrs = useAttrs();
  const table = useTableContext();

  const props = withDefaults(
    defineProps<{
      /**
       * Whether to cache column settings
       */
      cache?: boolean;
    }>(),
    {
      cache: () => false,
    },
  );

  const getPopupContainer = () => {
    return isFunction(attrs.getPopupContainer) ? attrs.getPopupContainer() : getParentContainer();
  };

  // Whether it has been restored from cache
  let isRestored = false;
  let isInnerChange = false;

  // column options
  const columnOptions = ref<ColumnOptionsType[]>([]);
  const columnOptionsRef = ref(null);
  // selected column
  const columnCheckedOptions = ref<string[]>([]);
  // selected changes
  watch(columnCheckedOptions, () => {
    // Valid after restoring cache
    if (isRestored) {
      // display
      columnOptions.value
        .filter((o) => columnCheckedOptions.value.includes(o.value))
        .forEach((o) => {
          o.column.defaultHidden = false;
        });
      // hide
      columnOptions.value
        .filter((o) => !columnCheckedOptions.value.includes(o.value))
        .forEach((o) => {
          o.column.defaultHidden = true;
          o.fixed = undefined;
        });
      // Update select-all state from column options
      isColumnAllSelectedUpdate();

      // List column update
      tableColumnsUpdate();
      // Update column cache
      props.cache && columnOptionsSave();
    }
  });

  // Select all
  const isColumnAllSelected = ref<boolean>(false);
  const onColumnAllSelectChange = () => {
    if (columnCheckedOptions.value.length < columnOptions.value.length) {
      columnCheckedOptions.value = columnOptions.value.map((o) => o.value);
    } else {
      columnCheckedOptions.value = [];
    }
  };

  // Half-selected state
  const indeterminate = computed(() => {
    return (
      columnCheckedOptions.value.length > 0 &&
      columnCheckedOptions.value.length < columnOptions.value.length
    );
  });

  // Whether to display the serial number column
  const isIndexColumnShow = ref<boolean>(false);
  // Serial number column update
  const onIndexColumnShowChange = (e: CheckboxChangeEvent) => {
    // Update showIndexColumn
    showIndexColumnUpdate(e.target.checked);
    // Update showIndexColumn cache
    props.cache &&
      typeof route.name === 'string' &&
      tableSettingStore.setShowIndexColumn(route.name, e.target.checked);
  };

  // Whether to display the selection column
  const isRowSelectionShow = ref<boolean>(false);
  // Select column to update
  const onRowSelectionShowChange = (e: CheckboxChangeEvent) => {
    // Update showRowSelection
    showRowSelectionUpdate(e.target.checked);
    // Update showRowSelection cache
    props.cache &&
      typeof route.name === 'string' &&
      tableSettingStore.setShowRowSelection(route.name, e.target.checked);
  };

  // Whether the column size can be changed
  const isAllowResizeColumn = ref<boolean>(false);
  // Change column size
  const onColumnAllowResizeChange = (e: CheckboxChangeEvent) => {
    const columns = getTableColumns();
    columns.forEach((col) => {
      if (isNumber(col.width)) {
        col.resizable = e.target.checked;
      }
    });
    props.cache &&
      typeof route.name === 'string' &&
      tableSettingStore.setAllowResizeColumn(route.name, e.target.checked);
  }

  // Update column cache
  const columnOptionsSave = () => {
    if (typeof route.name === 'string') {
      // Use the route name as the cache key (if there are multiple tables in a route, you need to adjust the cache key source yourself)
      tableSettingStore.setColumns(route.name, columnOptions.value);
    }
  };

  // reset
  const onReset = () => {
    // Reset default value
    isIndexColumnShow.value = defaultIsIndexColumnShow;
    // Serial number column update
    onIndexColumnShowChange({
      target: { checked: defaultIsIndexColumnShow },
    } as CheckboxChangeEvent);
    // Reset default value
    isRowSelectionShow.value = defaultIsRowSelectionShow;
    // Select column to update
    onRowSelectionShowChange({
      target: { checked: defaultIsRowSelectionShow },
    } as CheckboxChangeEvent);
    // Reset default value
    columnOptions.value = cloneDeep(defaultColumnOptions);
    // Update form status
    formUpdate();
  };

  // Set the column's fixed
  const onColumnFixedChange = (opt: ColumnOptionsType, type: 'left' | 'right') => {
    if (type === 'left') {
      if (!opt.fixed || opt.fixed === 'right') {
        opt.fixed = 'left';
      } else {
        opt.fixed = undefined;
      }
    } else if (type === 'right') {
      if (!opt.fixed || opt.fixed === 'left') {
        opt.fixed = 'right';
      } else {
        opt.fixed = undefined;
      }
    }

    // List column update
    tableColumnsUpdate();
    // Update column cache
    props.cache && columnOptionsSave();
  };

  // Inherit logic
  const sortableFix = async () => {
    // There is a bug in Sortablejs. I don’t know in which step a childNode will be appended to el, so the childNode is cleared first.
    // Operations that may reproduce the above problem: drag an element, move it up and down quickly, and finally put it in the last position and let go
    if (columnOptionsRef.value) {
      const el = (columnOptionsRef.value as InstanceType<typeof Checkbox.Group>).$el;
      Array.from(el.children).forEach((item) => el.removeChild(item));
    }
    await nextTick();
  };

  // Whether the column displays logic
  const columnIfShow = (column?: Partial<Omit<BasicColumn, 'children'>>) => {
    if (column) {
      if ('ifShow' in column) {
        if (typeof column.ifShow === 'boolean') {
          return column.ifShow;
        } else if (column.ifShow) {
          return column.ifShow(column);
        }
      }
      return true;
    }
    return false;
  };

  // Get the data column
  const getTableColumns = () => {
    return table
      .getColumns({ ignoreIndex: true, ignoreAction: true })
      .filter((col) => columnIfShow(col));
  };

  // Set list columns
  const tableColumnsSet = (columns: BasicColumn[]) => {
    isInnerChange = true;
    table?.setColumns(columns);

    // Inherit logic
    const columnChangeParams: ColumnChangeParam[] = columns.map((col) => ({
      dataIndex: col.dataIndex ? col.dataIndex.toString() : '',
      fixed: col.fixed,
      visible: !col.defaultHidden,
    }));
    emit('columns-change', columnChangeParams);
  };

  // List column update
  const tableColumnsUpdate = () => {
    // all columns considered
    const columns = cloneDeep(table.getColumns());

    // Sort from the leftmost fixed column (except the serial number column)
    let count = columns.filter(
      (o) => o.flag !== INDEX_COLUMN_FLAG && (o.fixed === 'left' || o.fixed === true),
    ).length;

    // Serial number column advanced
    if (isIndexColumnShow.value) {
      count++;
    }

    // Sort by columnOptions and adjust the order and value of table.getColumns()
    for (const opt of columnOptions.value) {
      const colIdx = columns.findIndex((o) => o.dataIndex === opt.value);
      //
      if (colIdx > -1) {
        const target = columns[colIdx];
        target.defaultHidden = opt.column?.defaultHidden;
        target.fixed = opt.fixed;
        columns.splice(colIdx, 1);
        columns.splice(count++, 0, target); // Incremental insertion
      }
    }

    // Does action exist?
    const actionIndex = columns.findIndex((o) => o.dataIndex === 'action');
    if (actionIndex > -1) {
      const actionCol = columns.splice(actionIndex, 1);
      columns.push(actionCol[0]);
    }

    // Set list columns
    tableColumnsSet(columns);
  };

  // Open floating window
  const onOpenChange = async () => {
    await nextTick();

    if (columnOptionsRef.value) {
      // Register sorting instance
      const el = (columnOptionsRef.value as InstanceType<typeof Checkbox.Group>).$el;
      Sortablejs.create(unref(el), {
        animation: 500,
        delay: 400,
        delayOnTouchOnly: true,
        handle: '.table-column-drag-icon ',
        dataIdAttr: 'data-no',
        onEnd: (evt) => {
          const { oldIndex, newIndex } = evt;
          if (isNil(oldIndex) || isNil(newIndex) || oldIndex === newIndex) {
            return;
          }

          const options = cloneDeep(columnOptions.value);

          // sort
          if (oldIndex > newIndex) {
            options.splice(newIndex, 0, options[oldIndex]);
            options.splice(oldIndex + 1, 1);
          } else {
            options.splice(newIndex + 1, 0, options[oldIndex]);
            options.splice(oldIndex, 1);
          }

          // Update column options
          columnOptions.value = options;

          // List column update
          tableColumnsUpdate();
          // Update column cache
          props.cache && columnOptionsSave();
        },
      });
    }
  };

  // remove disappearing columns and push newly appearing columns
  const diff = () => {
    if (typeof route.name === 'string') {
      let cache = tableSettingStore.getColumns(route.name);
      if (cache) {
        // Are value and label consistent?
        if (
          JSON.stringify(columnOptions.value.map((o) => ({ value: o.value, label: o.label }))) !==
          JSON.stringify(cache.map((o) => ({ value: o.value, label: o.label })))
        ) {
          const map = columnOptions.value.reduce((map, item) => {
            map[item.value] = item.label;
            return map;
          }, {});
          if (Array.isArray(cache)) {
            // remove missing columns
            cache = cache.filter((o) => map[o.value]);
            // update label
            cache.forEach((o) => {
              o.label = map[o.value];
            });
            const cacheKeys = cache.map((o) => o.value);
            // push the new column
            cache = cache.concat(columnOptions.value.filter((o) => !cacheKeys.includes(o.value)));
            // Update cache
            tableSettingStore.setColumns(route.name, cache);
          }
        }
      }
    }
  };

  // Restore from cache
  const restore = () => {
    if (typeof route.name === 'string') {
      const isIndexColumnShowCache = tableSettingStore.getShowIndexColumn(route.name);
      // Restore after setting
      if (typeof isIndexColumnShowCache === 'boolean') {
        isIndexColumnShow.value = defaultIsIndexColumnShow && isIndexColumnShowCache;
      }

      const isRowSelectionShowCache = tableSettingStore.getShowRowSelection(route.name);
      // Restore after setting
      if (typeof isRowSelectionShowCache === 'boolean') {
        isRowSelectionShow.value = defaultIsRowSelectionShow && isRowSelectionShowCache;
      }

      const allowResizeColumnCache = tableSettingStore.getAllowResizeColumn(route.name);
      // Restore after setting
      if (typeof allowResizeColumnCache === 'boolean') {
        isAllowResizeColumn.value = defaultIsAllowResizeColumn && allowResizeColumnCache;
      }
    }
    // Serial number column update
    onIndexColumnShowChange({
      target: { checked: isIndexColumnShow.value },
    } as CheckboxChangeEvent);
    // Select column to update
    onRowSelectionShowChange({
      target: { checked: isRowSelectionShow.value },
    } as CheckboxChangeEvent);

    if (typeof route.name === 'string') {
      const cache = tableSettingStore.getColumns(route.name);
      // Restore after setting
      if (Array.isArray(cache)) {
        columnOptions.value = cache;
      }
    }
  };

  // Update selected columns from column options
  const columnCheckedOptionsUpdate = () => {
    columnCheckedOptions.value = columnOptions.value
      .filter((o) => !o.column?.defaultHidden)
      .map((o) => o.value);
  };
  // Update select-all state from column options
  const isColumnAllSelectedUpdate = () => {
    isColumnAllSelected.value = columnOptions.value.length === columnCheckedOptions.value.length;
  };
  // Update showIndexColumn
  const showIndexColumnUpdate = (showIndexColumn) => {
    isInnerChange = true;
    table.setProps({
      showIndexColumn,
    });
  };
  // update rowSelection
  const showRowSelectionUpdate = (showRowSelection) => {
    isInnerChange = true;
    table.setProps({
      rowSelection: showRowSelection
        ? {
            ...omit(defaultRowSelection, ['selectedRowKeys']),
            fixed: true,
          }
        : undefined,
    });
  };

  // Update form status
  const formUpdate = () => {
    // Update selected columns from column options
    columnCheckedOptionsUpdate();

    // Update select-all state from column options
    isColumnAllSelectedUpdate();

    // Update showIndexColumn
    showIndexColumnUpdate(isIndexColumnShow.value);

    // Update showRowSelection
    showRowSelectionUpdate(isRowSelectionShow.value);

    // List column update
    tableColumnsUpdate();
  };

  // Default value
  let defaultIsIndexColumnShow: boolean = false;
  let defaultIsRowSelectionShow: boolean = false;
  let defaultIsAllowResizeColumn: boolean = false;
  let defaultRowSelection: TableRowSelection<Recordable<any>>;
  let defaultColumnOptions: ColumnOptionsType[] = [];

  const init = async () => {
    if (!isRestored) {
      // Get the data column
      const columns = getTableColumns();

      // Inherit logic
      table.setCacheColumns?.(columns);

      // Generate default value
      const options: ColumnOptionsType[] = [];
      for (const col of columns) {
        // Only cache columns of type string
        options.push({
          label:
            typeof col.title === 'string'
              ? col.title
              : col.customTitle === 'string'
                ? col.customTitle
                : '',
          value:
            typeof col.dataIndex === 'string'
              ? col.dataIndex
              : col.title === 'string'
                ? col.title
                : '',
          column: {
            defaultHidden: col.defaultHidden,
          },
          fixed: col.fixed,
        });
      }

      // Default value cache
      defaultIsIndexColumnShow = table.getBindValues.value.showIndexColumn || false;
      defaultRowSelection = table.getRowSelection();
      defaultIsRowSelectionShow = !!defaultRowSelection; // Appears only after rowSelection is set
      defaultColumnOptions = options;

      // Default value assignment
      isIndexColumnShow.value = defaultIsIndexColumnShow;
      isRowSelectionShow.value = defaultIsRowSelectionShow;
      columnOptions.value = cloneDeep(options);

      // remove disappearing columns and push newly appearing columns
      props.cache && diff();

      // Restore from cache
      props.cache && restore();

      // Update form status
      formUpdate();

      isRestored = true;
    }
  };

  // initialization
  const once = async () => {
    // Execute only once
    await sortableFix();
    init();
  };
  once();

  // External column changes
  const getColumns = computed(() => {
    return table?.getColumns();
  });
  const getValues = computed(() => {
    return table?.getBindValues;
  });

  onMounted(() => {
    watch([getColumns, getValues], () => {
      if (!isInnerChange) {
        isRestored = false;
        console.log('onMounted isRestored');
        init();
      } else {
        isInnerChange = false;
      }
    });
  });
</script>
<style lang="less">
  @prefix-cls: ~'@{namespace}-basic-column-setting';

  .table-column-drag-icon {
    margin: 0 5px;
    cursor: move;
  }

  .@{prefix-cls} {
    &__popover-title {
      display: flex;
      position: relative;
      align-items: center;
      justify-content: space-between;
    }

    &__check-item {
      display: flex;
      align-items: center;
      min-width: 100%;
      padding: 4px 16px 8px 0;

      .ant-checkbox-wrapper {
        width: 100%;

        &:hover {
          color: @primary-color;
        }
      }
    }

    &__fixed-left,
    &__fixed-right {
      color: rgb(0 0 0 / 45%);
      cursor: pointer;

      &.active,
      &:hover {
        color: @primary-color;
      }

      &.disabled {
        color: @disabled-color;
        cursor: not-allowed;
      }
    }

    &__fixed-right {
      transform: rotate(180deg);
    }

    &__column-list {
      svg {
        width: 1em !important;
        height: 1em !important;
      }

      .ant-popover-inner-content {
        // max-height: 360px;
        padding-right: 0;
        padding-left: 0;
        // overflow: auto;
      }

      .ant-checkbox-group {
        display: inline-block;
        width: 100%;
        min-width: 260px;
        // flex-wrap: wrap;
      }

      .scrollbar {
        height: 220px;
      }
    }
  }
</style>