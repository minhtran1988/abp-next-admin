import {
  IField,
  IFieldBeetWeen,
  IFieldContains,
  IFieldLength,
  IFieldRange,
  IFieldMatch,
  IFieldRegular,
  IFieldValidator,
  IDefineFieldValidator,
} from './typing';

import { useLocalization } from './useLocalization';
import { isEmail, isPhone } from '/@/utils/is';
import { Rule } from '/@/components/Form';
import { RuleType } from 'ant-design-vue/lib/form/interface';

export enum ValidationEnum {
  DoNotMatch = "'{0}' and '{1}' do not match.",
  FieldInvalid = 'The field {0} is invalid.',
  FieldIsNotValid = '{0} is not valid.',
  FieldMustBeStringOrArrayWithMaximumLength = "The field {0} must be a string or array type with a maximum length of '{1}'.",
  FieldMustBeStringOrArrayWithMinimumLength = "The field {0} must be a string or array type with a minimum length of '{1}'.",
  FieldMustBeStringWithMaximumLength = 'The field {0} must be a string with a maximum length of {1}.',
  FieldMustBeStringWithMinimumLengthAndMaximumLength = 'The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.',
  FieldMustBeetWeen = 'The field {0} must be between {1} and {2}.',
  FieldMustMatchRegularExpression = "The field {0} must match the regular expression '{1}'.",
  FieldDoNotValidCreditCardNumber = 'The {0} field is not a valid credit card number.',
  FieldDoNotValidEmailAddress = 'The {0} field is not a valid e-mail address.',
  FieldDoNotValidFullyQualifiedUrl = 'The {0} field is not a valid fully-qualified http, https, or ftp URL.',
  FieldDoNotValidPhoneNumber = 'The {0} field is not a valid phone number.',
  FieldRequired = 'The {0} field is required.',
  FieldOnlyAcceptsFilesExtensions = 'The {0} field only accepts files with the following extensions: {1}',
  ThisFieldIsInvalid = 'ThisFieldIsInvalid.',
  ThisFieldIsNotAValidCreditCardNumber = 'ThisFieldIsNotAValidCreditCardNumber.',
  ThisFieldIsNotAValidEmailAddress = 'ThisFieldIsNotAValidEmailAddress.',
  ThisFieldIsNotAValidFullyQualifiedHttpHttpsOrFtpUrl = 'ThisFieldIsNotAValidFullyQualifiedHttpHttpsOrFtpUrl',
  ThisFieldIsNotAValidPhoneNumber = 'ThisFieldIsNotAValidPhoneNumber.',
  ThisFieldIsNotValid = 'ThisFieldIsNotValid.',
  ThisFieldIsRequired = 'ThisFieldIsRequired.',
  ThisFieldMustBeAStringOrArrayTypeWithAMaximumLength = 'ThisFieldMustBeAStringOrArrayTypeWithAMaximumLengthOf{0}',
  ThisFieldMustBeAStringOrArrayTypeWithAMinimumLength = 'ThisFieldMustBeAStringOrArrayTypeWithAMinimumLengthOf{0}',
  ThisFieldMustBeAStringWithAMaximumLength = 'ThisFieldMustBeAStringWithAMaximumLengthOf{0}',
  ThisFieldMustBeAStringWithAMinimumLengthAndAMaximumLength = 'ThisFieldMustBeAStringWithAMinimumLengthOf{1}AndAMaximumLengthOf{0}',
  ThisFieldMustBeBetween = 'ThisFieldMustBeBetween{0}And{1}',
  ThisFieldMustMatchTheRegularExpression = 'ThisFieldMustMatchTheRegularExpression{0}',
  ThisFieldOnlyAcceptsFilesWithTheFollowingExtensions = 'ThisFieldOnlyAcceptsFilesWithTheFollowingExtensions:{0}',
}

/** Rule Creator */
interface IRuleCreator {
  /** Custom validator */
  defineValidator(field: IDefineFieldValidator): Rule[];
  /** Whether input and value match */
  doNotMatch(field: IFieldMatch): Rule[];
  /** The field is an invalid value */
  fieldInvalid(field: IFieldValidator): Rule[];
  /** Verification failed */
  fieldIsNotValid(field: IFieldValidator): Rule[];
  /** Field {0} must be a string or array with a maximum length of '{1}' */
  fieldMustBeStringOrArrayWithMaximumLength(field: IFieldLength): Rule[];
  /** Field {0} must be a string or array with a minimum length of '{1}' */
  fieldMustBeStringOrArrayWithMinimumLength(field: IFieldLength): Rule[];
  /** Field {0} must be a string with a maximum length of {1} */
  fieldMustBeStringWithMaximumLength(field: IFieldLength): Rule[];
  /** Field {0} must be a string with a minimum length of {2} and a maximum length of {1} */
  fieldMustBeStringWithMinimumLengthAndMaximumLength(field: IFieldRange): Rule[];
  /** The value of field {0} must be within the range of {1} and {2} */
  fieldMustBeetWeen(field: IFieldBeetWeen): Rule[];
  /** Field {0} does not match the regular expression */
  fieldMustMatchRegularExpression(field: IFieldRegular): Rule[];
  /** Field {0} is not a valid credit card number */
  fieldDoNotValidCreditCardNumber(field: IField): Rule[];
  /** Field {0} is not a valid email address */
  fieldDoNotValidEmailAddress(field: IField): Rule[];
  /** Field {0} is not a valid fully qualified http, https or ftp URL. */
  fieldDoNotValidFullyQualifiedUrl(field: IField): Rule[];
  /** Field {0} is not a valid mobile phone number */
  fieldDoNotValidPhoneNumber(field: IField): Rule[];
  /** Field {0} cannot be empty */
  fieldRequired(field: IField): Rule[];
  /** The {0} field only allows files with the following extensions: {1} */
  fieldOnlyAcceptsFilesExtensions(field: IFieldContains): Rule[];
}

export function useValidation() {
  const { L } = useLocalization('AbpValidation');

  function _getFieldName(field: IField) {
    return __getFieldName(field.name ?? '', field.resourceName, field.prefix, field.connector);
  }

  function __getFieldName(
    fieldName: string,
    resourceName?: string,
    prefix?: string,
    connector?: string,
  ) {
    if (fieldName && resourceName) {
      fieldName = prefix ? `${prefix}${connector ?? ':'}${fieldName}` : fieldName;
      const { L: l } = useLocalization(resourceName);
      return l(fieldName);
    }
    return fieldName;
  }

  function _createRule(options: {
    message?: string;
    type?: RuleType | 'array';
    required?: boolean;
    len?: number;
    min?: number;
    max?: number;
    trigger?: 'blur' | 'change' | ['change', 'blur'];
    validator?: (rule: any, value: any, callback: any, source?: any, options?: any) => Promise<void> | void;
  }): Rule[] {
    return [
      {
        message: options.message,
        type: options.type,
        required: options.required,
        len: options.len,
        min: options.min,
        max: options.max,
        validator: options.validator,
        trigger: options.trigger,
      },
    ];
  }

  function _createValidator(
    field: IField,
    useNameEnum: string,
    notNameEnum: string,
    required?: boolean,
  ): Rule {
    const message = field.name ? L(useNameEnum, [_getFieldName(field)]) : L(notNameEnum);
    return {
      required: required,
      message: message,
      type: field.type,
      trigger: field.trigger,
    };
  }

  function _createLengthValidator(
    field: IFieldLength,
    checkMaximum: boolean,
    useNameEnum: string,
    notNameEnum: string,
    required?: boolean,
  ): Rule {
    const message = field.name
      ? L(useNameEnum, [_getFieldName(field), field.length])
      : L(notNameEnum, [field.length]);

    function checkLength(value: string | any[]) {
      return checkMaximum ? field.length > value.length : value.length > field.length;
    }

    return {
      required: required,
      message: message,
      type: field.type,
      trigger: field.trigger,
      validator: (_, value: string) => {
        if (!checkLength(value)) {
          return Promise.reject(message);
        }
        return Promise.resolve();
      },
    };
  }

  function _createLengthRangValidator(
    field: IFieldRange,
    useNameEnum: string,
    notNameEnum: string,
    required?: boolean,
  ): Rule {
    const message = field.name
      ? L(useNameEnum, [_getFieldName(field), field.minimum, field.maximum])
      : L(notNameEnum, [field.minimum, field.maximum]);
    return {
      required: required,
      message: message,
      type: field.type,
      trigger: field.trigger,
      validator: (_, value: string) => {
        if (value.length < field.minimum || value.length > field.maximum) {
          return Promise.reject(message);
        }
        return Promise.resolve();
      },
    };
  }

  function _createBeetWeenValidator(field: IFieldBeetWeen): Rule {
    const message = field.name
      ? L(ValidationEnum.FieldMustBeetWeen, [_getFieldName(field), field.start, field.end])
      : L(ValidationEnum.ThisFieldMustBeBetween, [field.start, field.end]);
    return {
      message: message,
      trigger: field.trigger,
      validator: (_, value) => {
        // beetween no longer performs must-lose checks, but instead performs digital validity checks
        if (isNaN(value)) {
          return Promise.reject(message);
        }
        if (value < field.start || value > field.end) {
          return Promise.reject(message);
        } else {
          return Promise.resolve();
        }
      },
    };
  }

  function _createRegularExpressionValidator(
    field: IFieldRegular,
    required?: boolean,
  ): Rule {
    const message = field.name
      ? L(ValidationEnum.FieldMustMatchRegularExpression, [_getFieldName(field), field.expression])
      : L(ValidationEnum.ThisFieldMustMatchTheRegularExpression, [field.expression]);
    return {
      required: required,
      message: message,
      type: field.type,
      trigger: field.trigger,
      pattern: new RegExp(field.expression),
    };
  }

  function _createEmailValidator(field: IField, required?: boolean): Rule {
    const message = field.name
      ? L(ValidationEnum.FieldDoNotValidEmailAddress, [_getFieldName(field)])
      : L(ValidationEnum.ThisFieldIsNotAValidEmailAddress);
    return {
      required: required,
      message: message,
      type: field.type,
      trigger: field.trigger,
      validator: (_, value: string) => {
        if (!isEmail(value)) {
          return Promise.reject(message);
        }
        return Promise.resolve();
      },
    };
  }

  function _createPhoneValidator(field: IField, required?: boolean): Rule {
    const message = field.name
      ? L(ValidationEnum.FieldDoNotValidPhoneNumber, [_getFieldName(field)])
      : L(ValidationEnum.ThisFieldIsNotAValidPhoneNumber);
    return {
      required: required,
      message: message,
      type: field.type,
      trigger: field.trigger,
      validator: (_, value: string) => {
        if (!isPhone(value)) {
          return Promise.reject(message);
        }
        return Promise.resolve();
      },
    };
  }

  const ruleCreator: IRuleCreator = {
    defineValidator(field: IDefineFieldValidator) {
      return _createRule(field);
    },
    doNotMatch(field: IFieldMatch) {
      const message = L(
        ValidationEnum.DoNotMatch,
        [__getFieldName(field.name, field.resourceName, field.prefix),
        __getFieldName(field.matchField, field.resourceName, field.prefix)],
      );
      return _createRule({
        required: field.required,
        message: message,
        type: field.type,
        trigger: field.trigger,
        validator: (_, value: string) => {
          if (value !== field.matchValue) {
            return Promise.reject(message);
          }
          return Promise.resolve();
        },
      });
    },
    fieldInvalid(field: IFieldValidator) {
      const message = field.name
        ? L(ValidationEnum.FieldInvalid, [_getFieldName(field)])
        : L(ValidationEnum.ThisFieldIsInvalid);
      return _createRule({
        required: field.required,
        message: message,
        type: field.type,
        trigger: field.trigger,
        validator: (_, value: any) => {
          if (!field.validator(value)) {
            return Promise.reject(message);
          }
          return Promise.resolve();
        },
      });
    },
    fieldIsNotValid(field: IFieldValidator) {
      const message = field.name
        ? L(ValidationEnum.FieldIsNotValid, [_getFieldName(field)])
        : L(ValidationEnum.ThisFieldIsNotValid);
      return _createRule({
        required: field.required,
        message: message,
        type: field.type,
        trigger: field.trigger,
        validator: (_, value: any) => {
          if (field.validator(value)) {
            return Promise.reject(message);
          }
          return Promise.resolve();
        },
      });
    },
    fieldMustBeStringOrArrayWithMaximumLength(field: IFieldLength) {
      return [
        _createLengthValidator(
          field,
          true,
          ValidationEnum.FieldMustBeStringOrArrayWithMaximumLength,
          ValidationEnum.ThisFieldMustBeAStringOrArrayTypeWithAMaximumLength,
        ),
      ];
    },
    fieldMustBeStringOrArrayWithMinimumLength(field: IFieldLength) {
      return [
        _createLengthValidator(
          field,
          false,
          ValidationEnum.FieldMustBeStringOrArrayWithMinimumLength,
          ValidationEnum.ThisFieldMustBeAStringOrArrayTypeWithAMinimumLength,
        ),
      ];
    },
    fieldMustBeStringWithMaximumLength(field: IFieldLength) {
      return [
        _createLengthValidator(
          field,
          true,
          ValidationEnum.FieldMustBeStringWithMaximumLength,
          ValidationEnum.ThisFieldMustBeAStringWithAMaximumLength,
        ),
      ];
    },
    fieldMustBeStringWithMinimumLengthAndMaximumLength(field: IFieldRange) {
      return [
        _createLengthRangValidator(
          field,
          ValidationEnum.FieldMustBeStringWithMinimumLengthAndMaximumLength,
          ValidationEnum.ThisFieldMustBeAStringWithAMinimumLengthAndAMaximumLength,
        ),
      ];
    },
    fieldMustBeetWeen(field: IFieldBeetWeen) {
      return [_createBeetWeenValidator(field)];
    },
    fieldMustMatchRegularExpression(field: IFieldRegular) {
      return [_createRegularExpressionValidator(field)];
    },
    fieldDoNotValidCreditCardNumber(field: IField) {
      if (field.name) {
        return _createRule({
          message: L(ValidationEnum.FieldDoNotValidCreditCardNumber, [_getFieldName(field)]),
          type: field.type,
          trigger: field.trigger,
        });
      }
      return _createRule({
        message: L(ValidationEnum.ThisFieldIsNotAValidCreditCardNumber),
        type: field.type,
        trigger: field.trigger,
      });
    },
    fieldDoNotValidEmailAddress(field: IField) {
      return [_createEmailValidator(field)];
    },
    fieldDoNotValidFullyQualifiedUrl(field: IField) {
      if (field.name) {
        return _createRule({
          message: L(ValidationEnum.FieldDoNotValidFullyQualifiedUrl, [_getFieldName(field)]),
          type: field.type,
          trigger: field.trigger,
        });
      }
      return _createRule({
        message: L(ValidationEnum.ThisFieldIsNotAValidFullyQualifiedHttpHttpsOrFtpUrl),
        type: field.type,
        trigger: field.trigger,
      });
    },
    fieldDoNotValidPhoneNumber(field: IField) {
      return [_createPhoneValidator(field)];
    },
    fieldRequired(field: IField) {
      return [
        _createValidator(
          field,
          ValidationEnum.FieldRequired,
          ValidationEnum.ThisFieldIsRequired,
          true,
        ),
      ];
    },
    fieldOnlyAcceptsFilesExtensions(field: IFieldContains) {
      const message = field.name
        ? L(ValidationEnum.FieldOnlyAcceptsFilesExtensions, [_getFieldName(field), field.value])
        : L(ValidationEnum.ThisFieldMustMatchTheRegularExpression, [field.value]);
      return _createRule({
        message: message,
        type: field.type,
        trigger: field.trigger,
        validator: (_, value: string) => {
          if (!field.value.includes(value)) {
            return Promise.reject(message);
          }
          return Promise.resolve();
        },
      });
    },
  };

  return {
    ruleCreator,
  };
}
