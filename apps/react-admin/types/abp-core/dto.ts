import type {
	Clock,
	CurrentCulture,
	CurrentTenant,
	CurrentUser,
	Dictionary,
	ExtraPropertyDictionary,
	IHasExtraProperties,
	LanguageInfo,
	MultiTenancyInfo,
	NameValue,
	TimeZone,
} from "./global";

/** Wrapper data transfer object */
interface WrapResult<T> {
	/**
	 * Error code
	 * @summary '0' means normal
	 */
	code: string;
	/** Error details */
	details?: string;
	/** Error message */
	message: string;
	/** Return data */
	result: T;
}
/** Remote service verification error description */
interface RemoteServiceValidationErrorInfo {
	/** Field name list */
	members: string[];
	/** Error message */
	message: string;
}
/** Remote service error description (only applicable to interface proxy) */
interface RemoteServiceErrorInfo {
	/** Error code */
	code?: string;
	/** Abnormal data */
	data?: { [key: string]: any };
	/** Error details */
	details?: string;
	/** Error message */
	message?: string;
	/** Verification error list */
	validationErrors: RemoteServiceValidationErrorInfo[];
}
/** Extended attribute data transfer object */
interface ExtensibleObject {
	/** Extended attributes */
	extraProperties: ExtraPropertyDictionary;
}
/** Entity data transfer object */
interface EntityDto<TPrimaryKey> {
	/** Entity ID */
	id: TPrimaryKey;
}
/** Entity new attribute data transfer object */
interface CreationAuditedEntityDto<TPrimaryKey> extends EntityDto<TPrimaryKey> {
	/** Creation time */
	creationTime: Date;
	/** Creator ID */
	creatorId?: string;
}
/** Entity adds user attribute data transfer object */
interface CreationAuditedEntityWithUserDto<TPrimaryKey, TUserDto> extends CreationAuditedEntityDto<TPrimaryKey> {
	/** Creator entity data transfer object */
	creator: TUserDto;
}
/** Entity audit attribute data transfer object */
interface AuditedEntityDto<TPrimaryKey> extends CreationAuditedEntityDto<TPrimaryKey> {
	/** Last changed time */
	lastModificationTime?: Date;
	/** Last change person */
	lastModifierId?: string;
}
/** Entity audit user attribute data transfer object */
interface AuditedEntityWithUserDto<TPrimaryKey, TUserDto> extends AuditedEntityDto<TPrimaryKey> {
	/** Creator entity data transfer object */
	creator: TUserDto;
	/** Changer entity data transfer object */
	lastModifier: TUserDto;
}
/** Entity audit full attribute data transfer object */
interface FullAuditedEntityDto<TPrimaryKey> extends AuditedEntityDto<TPrimaryKey> {
	/** Delete person ID */
	deleterId?: string;
	/** Delete time */
	deletionTime?: Date;
	/** Whether it has been deleted */
	isDeleted: boolean;
}
/** Entity audit user full attribute data transmission object */
interface FullAuditedEntityWithUserDto<TPrimaryKey, TUserDto>
	extends AuditedEntityWithUserDto<TPrimaryKey, TUserDto>,
		FullAuditedEntityDto<TPrimaryKey> {
	/** Delete person entity data transfer object */
	deleter: TUserDto;
}
/** Entity extended attribute data transfer object */
interface ExtensibleEntityDto<TKey> extends ExtensibleObject, EntityDto<TKey> {}
/** New extended attribute data transfer object for entities */
interface ExtensibleCreationAuditedEntityDto<TPrimaryKey>
	extends CreationAuditedEntityDto<TPrimaryKey>,
		ExtensibleEntityDto<TPrimaryKey> {}
/** Entity adds user extended attribute data transfer object */
interface ExtensibleCreationAuditedEntityWithUserDto<TPrimaryKey, TUserDto>
	extends CreationAuditedEntityWithUserDto<TPrimaryKey, TUserDto>,
		ExtensibleEntityDto<TPrimaryKey> {}
/** Entity audit extended attribute data transfer object */
interface ExtensibleAuditedEntityDto<TPrimaryKey>
	extends AuditedEntityDto<TPrimaryKey>,
		ExtensibleEntityDto<TPrimaryKey> {}
/** Entity audit user extended attribute data transfer object */
interface ExtensibleAuditedEntityWithUserDto<TPrimaryKey, TUserDto>
	extends AuditedEntityWithUserDto<TPrimaryKey, TUserDto>,
		ExtensibleEntityDto<TPrimaryKey> {}
/** Entity audit full attribute extended data transfer object */
interface ExtensibleFullAuditedEntityDto<TPrimaryKey>
	extends FullAuditedEntityDto<TPrimaryKey>,
		ExtensibleEntityDto<TPrimaryKey> {}
/** Entity audit user full attribute extended data transfer object */
interface ExtensibleFullAuditedEntityWithUserDto<TPrimaryKey, TUserDto>
	extends FullAuditedEntityWithUserDto<TPrimaryKey, TUserDto>,
		ExtensibleEntityDto<TPrimaryKey> {}
/** Maximum requested data transfer object */
interface LimitedResultRequestDto {
	/** Maximum return data size */
	maxResultCount?: number;
}
/** Maximum request extended data transfer object */
interface ExtensibleLimitedResultRequestDto extends LimitedResultRequestDto, ExtensibleObject {}
/** Sorting request data transfer object */
interface SortedResultRequest {
	/** Sorting field
	 * @summary Ascending order: field name + asc; Descending order: field name + desc
	 * @example fieldA asc
	 * @example fieldB desc
	 */
	sorting?: string;
}
/** Paging request data transfer object */
interface PagedResultRequestDto extends LimitedResultRequestDto {
	/**
	 * Skip data size
	 * @summary The size of data skipped during paging, calculation method: (current page - 1) * The maximum return result, cannot be less than 0
	 * @example (pageNum - 1) * pageSize
	 */
	skipCount?: number;
}
/** Paging sorting request data transfer object */
interface PagedAndSortedResultRequestDto extends PagedResultRequestDto, SortedResultRequest {}
/** Paging sort request extended data transfer object */
interface ExtensiblePagedAndSortedResultRequestDto extends PagedAndSortedResultRequestDto, ExtensibleObject {}
/** List data transfer object */
interface ListResultDto<T> {
	/** Return to project list */
	items: T[];
}
/** List extension data transfer object */
interface ExtensibleListResultDto<T> extends ListResultDto<T>, ExtensibleObject {}
/** Paging list data transfer object */
interface PagedResultDto<T> extends ListResultDto<T> {
	/** Maximum number of qualified items */
	totalCount: number;
}
/** Paginated list extended data transfer object */
interface ExtensiblePagedResultDto<T> extends PagedResultDto<T>, ExtensibleObject {}
/** Paginated list extended data transfer object */
interface ExtensiblePagedResultRequestDto extends PagedResultRequestDto, ExtensibleObject {}
/** Application localization resource data transfer object */
interface ApplicationLocalizationResourceDto {
	/** Inherit resource name list */
	baseResources: string[];
	/** Localized text dictionary list */
	texts: Dictionary<string, string>;
}
/** Application localization data transfer object */
interface ApplicationLocalizationDto {
	/** Current locale */
	currentCulture: CurrentCulture;
	/** Resource dictionary list */
	resources: Dictionary<string, ApplicationLocalizationResourceDto>;
}
/** Application localization configuration data transfer object */
interface ApplicationLocalizationConfigurationDto {
	/** Current locale */
	currentCulture: CurrentCulture;
	/**Default localized resource name */
	defaultResourceName: string;
	/** Multi-language file configuration mapping (backend only) */
	languageFilesMap: Dictionary<string, NameValue<any>[]>;
	/** Supported multi-language list */
	languages: LanguageInfo[];
	/** Multi-language resource mapping (backend only) */
	languagesMap: Dictionary<string, NameValue<any>[]>;
	/** Localized resource dictionary list */
	resources: Dictionary<string, ApplicationLocalizationResourceDto>;
	/** Localized resource text dictionary list */
	values: Dictionary<string, Dictionary<string, string>>;
}
/** Application authorization data transfer object */
interface ApplicationAuthConfigurationDto {
	/** List of authorized dictionaries */
	grantedPolicies: Dictionary<string, boolean>;
}
/** Application settings data transfer object */
interface ApplicationSettingConfigurationDto {
	/** Set value dictionary list */
	values: Dictionary<string, string>;
}
/** Application function data transfer object */
interface ApplicationFeatureConfigurationDto {
	/** Function value dictionary list */
	values: Dictionary<string, string>;
}
/** Application global function data transfer object */
interface ApplicationGlobalFeatureConfigurationDto {
	/** Enabled feature name list */
	enabledFeatures: string[];
}
/** Application time zone data transfer object */
interface TimingDto {
	/** Time zone configuration definition */
	timeZone: TimeZone;
}
/** Localized text data object */
interface LocalizableStringDto {
	/** Text name */
	name: string;
	/** Resource name */
	resource?: string;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyApiGetDto {
	isAvailable: boolean;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyApiCreateDto {
	isAvailable: boolean;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyApiUpdateDto {
	isAvailable: boolean;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyUiTableDto {
	isAvailable: boolean;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyUiFormDto {
	isAvailable: boolean;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyUiLookupDto {
	displayPropertyName: string;
	filterParamName: string;
	resultListPropertyName: string;
	url: string;
	valuePropertyName: string;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyApiDto {
	onCreate: ExtensionPropertyApiCreateDto;
	onGet: ExtensionPropertyApiGetDto;
	onUpdate: ExtensionPropertyApiUpdateDto;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyUiDto {
	lookup: ExtensionPropertyUiLookupDto;
	onCreateForm: ExtensionPropertyUiFormDto;
	onEditForm: ExtensionPropertyUiFormDto;
	onTable: ExtensionPropertyUiTableDto;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyAttributeDto {
	config: Dictionary<string, any>;
	typeSimple: string;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionPropertyDto {
	api: ExtensionPropertyApiDto;
	attributes: ExtensionPropertyAttributeDto[];
	configuration: Dictionary<string, any>;
	defaultValue: any;
	displayName?: LocalizableStringDto;
	type: string;
	typeSimple: string;
	ui: ExtensionPropertyUiDto;
}
/** Extended attributes (for request proxy, ignored) */
interface EntityExtensionDto {
	configuration: Dictionary<string, any>;
	properties: Dictionary<string, ExtensionPropertyDto>;
}
/** Extended attributes (for request proxy, ignored) */
interface ModuleExtensionDto {
	configuration: Dictionary<string, any>;
	entities: Dictionary<string, EntityExtensionDto>;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionEnumFieldDto {
	name: string;
	value: any;
}
/** Extended attributes (for request proxy, ignored) */
interface ExtensionEnumDto {
	fields: ExtensionEnumFieldDto[];
	localizationResource: string;
}
/** Extended attributes (for request proxy, ignored) */
interface ObjectExtensionsDto {
	enums: Dictionary<string, ExtensionEnumDto>;
	modules: Dictionary<string, ModuleExtensionDto>;
}
/** Application configuration data transfer object */
interface ApplicationConfigurationDto extends IHasExtraProperties {
	/** Authorization configuration */
	auth: ApplicationAuthConfigurationDto;
	/** Clock configuration */
	clock: Clock;
	/** Current tenant */
	currentTenant: CurrentTenant;
	/** Current user */
	currentUser: CurrentUser;
	/** Function configuration */
	features: ApplicationFeatureConfigurationDto;
	/** Global function configuration */
	globalFeatures: ApplicationGlobalFeatureConfigurationDto;
	/** Localization configuration */
	localization: ApplicationLocalizationConfigurationDto;
	/** Multi-tenant configuration */
	multiTenancy: MultiTenancyInfo;
	/** Entity object extension configuration */
	objectExtensions: ObjectExtensionsDto;
	/** System settings */
	setting: ApplicationSettingConfigurationDto;
	// timing: Timing;
}

export type {
	ApplicationAuthConfigurationDto,
	ApplicationConfigurationDto,
	ApplicationFeatureConfigurationDto,
	ApplicationGlobalFeatureConfigurationDto,
	ApplicationLocalizationConfigurationDto,
	ApplicationLocalizationDto,
	ApplicationLocalizationResourceDto,
	ApplicationSettingConfigurationDto,
	AuditedEntityDto,
	AuditedEntityWithUserDto,
	CreationAuditedEntityDto,
	CreationAuditedEntityWithUserDto,
	EntityDto,
	EntityExtensionDto,
	ExtensibleAuditedEntityDto,
	ExtensibleAuditedEntityWithUserDto,
	ExtensibleCreationAuditedEntityDto,
	ExtensibleCreationAuditedEntityWithUserDto,
	ExtensibleEntityDto,
	ExtensibleFullAuditedEntityDto,
	ExtensibleFullAuditedEntityWithUserDto,
	ExtensibleLimitedResultRequestDto,
	ExtensibleListResultDto,
	ExtensibleObject,
	ExtensiblePagedAndSortedResultRequestDto,
	ExtensiblePagedResultDto,
	ExtensiblePagedResultRequestDto,
	ExtensionEnumDto,
	ExtensionEnumFieldDto,
	ExtensionPropertyApiCreateDto,
	ExtensionPropertyApiDto,
	ExtensionPropertyApiGetDto,
	ExtensionPropertyApiUpdateDto,
	ExtensionPropertyAttributeDto,
	ExtensionPropertyDto,
	ExtensionPropertyUiDto,
	ExtensionPropertyUiFormDto,
	ExtensionPropertyUiLookupDto,
	ExtensionPropertyUiTableDto,
	FullAuditedEntityDto,
	FullAuditedEntityWithUserDto,
	LimitedResultRequestDto,
	ListResultDto,
	LocalizableStringDto,
	ModuleExtensionDto,
	NameValue,
	ObjectExtensionsDto,
	PagedAndSortedResultRequestDto,
	PagedResultDto,
	PagedResultRequestDto,
	RemoteServiceErrorInfo,
	SortedResultRequest,
	TimingDto,
	WrapResult,
};
