import type {
	Field,
	FieldBeetWeen,
	FieldContains,
	FieldDefineValidator,
	FieldLength,
	FieldMatch,
	FieldRange,
	FieldRegular,
	FieldValidator,
	Rule,
} from "./validations";

/** Rule Creator */
interface RuleCreator {
	/** Custom validator */
	defineValidator(field: FieldDefineValidator): Rule[];
	/** Whether input and value match */
	doNotMatch(field: FieldMatch): Rule[];
	/** Field {0} is not a valid credit card number */
	fieldDoNotValidCreditCardNumber(field: Field): Rule[];
	/** Field {0} is not a valid email address */
	fieldDoNotValidEmailAddress(field: Field): Rule[];
	/** Field {0} is not a valid fully qualified http, https or ftp URL. */
	fieldDoNotValidFullyQualifiedUrl(field: Field): Rule[];
	/** Field {0} is not a valid mobile phone number */
	fieldDoNotValidPhoneNumber(field: Field): Rule[];
	/** The field is an invalid value */
	fieldInvalid(field: FieldValidator): Rule[];
	/** Verification failed */
	fieldIsNotValid(field: FieldValidator): Rule[];
	/** The value of field {0} must be within the range of {1} and {2} */
	fieldMustBeetWeen(field: FieldBeetWeen): Rule[];
	/** Field {0} must be a string or array with a maximum length of '{1}' */
	fieldMustBeStringOrArrayWithMaximumLength(field: FieldLength): Rule[];
	/** Field {0} must be a string or array with a minimum length of '{1}' */
	fieldMustBeStringOrArrayWithMinimumLength(field: FieldLength): Rule[];
	/** Field {0} must be a string with a maximum length of {1} */
	fieldMustBeStringWithMaximumLength(field: FieldLength): Rule[];
	/** Field {0} must be a string with a minimum length of {2} and a maximum length of {1} */
	fieldMustBeStringWithMinimumLengthAndMaximumLength(field: FieldRange): Rule[];
	/** Field {0} does not match the regular expression */
	fieldMustMatchRegularExpression(field: FieldRegular): Rule[];
	/** The {0} field only allows files with the following extensions: {1} */
	fieldOnlyAcceptsFilesExtensions(field: FieldContains): Rule[];
	/** Field {0} cannot be empty */
	fieldRequired(field: Field): Rule[];
	/** Get an error enumeration verification message */
	mapEnumValidMessage(enumName: string, args?: any[] | Record<string, string> | undefined): string;
}

export type { RuleCreator };
