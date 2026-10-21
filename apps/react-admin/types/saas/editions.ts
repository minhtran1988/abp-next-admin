import type { ExtensibleAuditedEntityDto, IHasConcurrencyStamp, PagedAndSortedResultRequestDto } from "#/abp-core";

interface EditionDto extends ExtensibleAuditedEntityDto<string>, IHasConcurrencyStamp {
	/** display name */
	displayName: string;
}

interface EditionCreateOrUpdateBase {
	/** display name */
	displayName: string;
}

type EditionCreateDto = EditionCreateOrUpdateBase;

interface EditionUpdateDto extends EditionCreateOrUpdateBase, IHasConcurrencyStamp {}

interface GetEditionPagedListInput extends PagedAndSortedResultRequestDto {
	filter?: string;
}

export type { EditionCreateDto, EditionDto, EditionUpdateDto, GetEditionPagedListInput };
