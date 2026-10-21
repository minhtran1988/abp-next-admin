import type {
	ExtensibleAuditedEntityDto,
	ExtensibleObject,
	IHasConcurrencyStamp,
	NameValue,
	PagedAndSortedResultRequestDto,
} from "#/abp-core";

type TenantConnectionStringDto = NameValue<string>;

type TenantConnectionStringSetInput = NameValue<string>;

interface TenantDto extends ExtensibleAuditedEntityDto<string>, IHasConcurrencyStamp {
	/** Disable time */
	disableTime?: string;
	/** VersionId */
	editionId?: string;
	/** Version name */
	editionName?: string;
	/** Activation time */
	enableTime?: string;
	/** Is it available */
	isActive: boolean;
	/** name */
	name: string;
	/** name */
	normalizedName: string;
}

interface GetTenantPagedListInput extends PagedAndSortedResultRequestDto {
	filter?: string;
}

interface TenantCreateOrUpdateBase extends ExtensibleObject {
	/** Disable time */
	disableTime?: string;
	/** VersionId */
	editionId?: string;
	/** Activation time */
	enableTime?: string;
	/** Is it available */
	isActive: boolean;
	/** name */
	name: string;
}

interface TenantCreateDto extends TenantCreateOrUpdateBase {
	adminEmailAddress: string;
	adminPassword: string;
	connectionStrings?: TenantConnectionStringSetInput[];
	defaultConnectionString?: string;
	useSharedDatabase: boolean;
}

interface TenantConnectionStringCheckInput {
	connectionString: string;
	name?: string;
	provider: string;
}

interface TenantUpdateDto extends IHasConcurrencyStamp, TenantCreateOrUpdateBase {}

export type {
	GetTenantPagedListInput,
	TenantConnectionStringCheckInput,
	TenantConnectionStringDto,
	TenantConnectionStringSetInput,
	TenantCreateDto,
	TenantDto,
	TenantUpdateDto,
};
