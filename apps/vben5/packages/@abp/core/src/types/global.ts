/** Name/value key-value pair */
interface NameValue<T> {
  /** name */
  name: string;
  /** value */
  value: T;
}
interface AuthConfig {
  audience?: string;
  authority: string;
  clientId: string;
  clientSecret: string;
}
interface Environment {
  authConfig: AuthConfig;
  dev: boolean;
  prod: boolean;
}
/** Localized text data object */
interface LocalizableStringInfo {
  /** Text name */
  name: string;
  /** Resource name */
  resourceName: string;
}
/** Key-value pair dictionary object */
type Dictionary<TKey extends number | string | symbol = any, KValue = any> = {
  [key in TKey]: KValue;
};
/** Extended attribute dictionary */
interface ExtraPropertyDictionary {
  [key: string]: any;
}
/** Concurrency interface */
interface IHasConcurrencyStamp {
  /** Concurrency token, required when updating data */
  concurrencyStamp?: string;
}
/** Extended attribute interface */
interface IHasExtraProperties {
  /** Extended attributes */
  extraProperties: ExtraPropertyDictionary;
}
/** Selections */
interface ISelectionStringValueItem {
  [key: string]: any;
  /** Select items to display text multilingual objects */
  displayText: LocalizableStringInfo;
  /** Selection value */
  value: string;
}
/** Selection data source */
interface ISelectionStringValueItemSource {
  /** Selection list */
  items: ISelectionStringValueItem[];
}
/** validator */
interface Validator {
  /** name */
  name: string;
  /** Properties */
  properties: { [key: string]: string };
}
/** Value type */
interface ValueType {
  /** name */
  name: string;
  /** Properties */
  properties: { [key: string]: string };
  /** validator */
  validator: Validator;
}
/** Select item value type */
interface SelectionStringValueType extends ValueType {
  /** Selection data source */
  itemSource: ISelectionStringValueItemSource;
}
/** Multi-language description */
interface LanguageInfo {
  /**
   *Culture name
   * @summary Simplified Chinese: zh-Hans
   * @summary English：en-Us
   */
  cultureName: string;
  /** display name */
  displayName: string;
  /**
   * Two characters describing the name
   * @summary Simplified Chinese: zh
   * @summary English：en
   */
  twoLetterISOLanguageName: string;
  /** Culture name used for UI (backend only) */
  uiCultureName: string;
}
/** Time and date formatting description */
interface DateTimeFormat {
  /** Calendar algorithm type */
  calendarAlgorithmType: string;
  /** Date separator */
  dateSeparator: string;
  /** Long time date format */
  dateTimeFormatLong: string;
  /** Complete time and date format */
  fullDateTimePattern: string;
  /** Long time format */
  longTimePattern: string;
  /** Short date format */
  shortDatePattern: string;
  /** Short time format */
  shortTimePattern: string;
}
/** Current language description */
interface CurrentCulture {
  /** Culture name */
  cultureName: string;
  /** Time and date formatting configuration */
  dateTimeFormat: DateTimeFormat;
  /** display name */
  displayName: string;
  /** English name */
  englishName: string;
  /** Whether to write from right to left */
  isRightToLeft: boolean;
  /** name */
  name: string;
  /** local name */
  nativeName: string;
  /** Three character description name */
  threeLetterIsoLanguageName: string;
  /** Two character description name */
  twoLetterIsoLanguageName: string;
}
/** Windows time zone */
interface WindowsTimeZone {
  /** Time zone identifier */
  timeZoneId: string;
}
/** IANA time zone */
interface IanaTimeZone {
  /** Time zone name */
  timeZoneName: string;
}
/** Time zone description */
interface TimeZone {
  /** IANA time zone configuration */
  iana: IanaTimeZone;
  /** Windows time zone configuration */
  windows: WindowsTimeZone;
}
/** Clock description */
interface Clock {
  /** Clock type
   * @summary UTC Greenwich Time
   * @summary Local local time
   */
  kind: string;
}
/** Multi-language description */
interface MultiTenancyInfo {
  /** Whether to enable multi-tenancy */
  isEnabled: boolean;
}
/** Current tenant */
interface CurrentTenant {
  /** Tenant ID */
  id?: string;
  /** Whether the tenant is available */
  isAvailable: boolean;
  /** Tenant name */
  name?: string;
}
/** Current user */
interface CurrentUser {
  /** Email address */
  email: string;
  /** Whether the email has been confirmed */
  emailVerified: boolean;
  /** User ID */
  id?: string;
  /** Simulate tenant ID */
  impersonatorTenantId?: string;
  /** Simulated tenant name */
  impersonatorTenantName?: string;
  /** Impersonate user ID */
  impersonatorUserId?: string;
  /** Impersonated user name */
  impersonatorUserName?: string;
  /** Whether it has been authenticated */
  isAuthenticated: boolean;
  /** name */
  name?: string;
  /** Mobile phone number */
  phoneNumber?: string;
  /** Whether the mobile phone number has been confirmed */
  phoneNumberVerified: boolean;
  /** Role list */
  roles: string[];
  /** Session ID */
  sessionId?: string;
  /** Last name */
  surName?: string;
  /** Tenant ID */
  tenantId?: string;
  /** Username */
  userName: string;
}

interface IHasSimpleStateCheckers<
  TState extends IHasSimpleStateCheckers<TState>,
> {
  // eslint-disable-next-line no-use-before-define
  stateCheckers: ISimpleStateChecker<TState>[];
}

type SimpleStateRecord<
  TState extends IHasSimpleStateCheckers<TState>,
  TValue,
> = {
  [P in TState]: TValue;
};

type SimpleStateCheckerResult<TState extends IHasSimpleStateCheckers<TState>> =
  SimpleStateRecord<TState, boolean>;

interface SimpleStateCheckerContext<
  TState extends IHasSimpleStateCheckers<TState>,
> {
  state: TState;
}

interface SimpleBatchStateCheckerContext<
  TState extends IHasSimpleStateCheckers<TState>,
> {
  states: TState[];
}

interface ISimpleStateChecker<TState extends IHasSimpleStateCheckers<TState>> {
  isEnabled(context: SimpleStateCheckerContext<TState>): boolean;
  serialize(): string | undefined;
}

interface ISimpleBatchStateChecker<
  TState extends IHasSimpleStateCheckers<TState>,
> {
  isEnabled(
    context: SimpleBatchStateCheckerContext<TState>,
  ): SimpleStateCheckerResult<TState>;
  serialize(): string | undefined;
}

interface ISimpleStateCheckerSerializer {
  deserialize<TState extends IHasSimpleStateCheckers<TState>>(
    jsonObject: any,
    state: TState,
  ): ISimpleStateChecker<TState> | undefined;
  deserializeArray<TState extends IHasSimpleStateCheckers<TState>>(
    value: string,
    state: TState,
  ): ISimpleStateChecker<TState>[];
  serialize<TState extends IHasSimpleStateCheckers<TState>>(
    checker: ISimpleStateChecker<TState>,
  ): string | undefined;
  serializeArray<TState extends IHasSimpleStateCheckers<TState>>(
    stateCheckers: ISimpleStateChecker<TState>[],
  ): string | undefined;
}

export type {
  Clock,
  CurrentCulture,
  CurrentTenant,
  CurrentUser,
  DateTimeFormat,
  Dictionary,
  Environment,
  ExtraPropertyDictionary,
  IanaTimeZone,
  IHasConcurrencyStamp,
  IHasExtraProperties,
  IHasSimpleStateCheckers,
  ISelectionStringValueItem,
  ISelectionStringValueItemSource,
  ISimpleBatchStateChecker,
  ISimpleStateChecker,
  ISimpleStateCheckerSerializer,
  LanguageInfo,
  LocalizableStringInfo,
  MultiTenancyInfo,
  NameValue,
  SelectionStringValueType,
  SimpleBatchStateCheckerContext,
  SimpleStateCheckerContext,
  SimpleStateCheckerResult,
  TimeZone,
  Validator,
  ValueType,
  WindowsTimeZone,
};
