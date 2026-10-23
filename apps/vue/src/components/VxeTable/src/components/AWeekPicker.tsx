import { getDatePickerCellValue } from './ADatePicker';
import {
  createEditRender,
  createCellRender,
  createFormItemRender,
  createExportMethod,
} from './common';

export default {
  renderEdit: createEditRender(),
  renderCell: createCellRender(getDatePickerCellValue, () => {
    return ['YYYY-WW [week]'];
  }),
  renderItemContent: createFormItemRender(),
  exportMethod: createExportMethod(getDatePickerCellValue, () => {
    return ['YYYY-WW [week]'];
  }),
};
