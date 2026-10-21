import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from "#/abp-core";

import type { GetUserPagedListInput } from "./user";

/** Organization */
interface OrganizationUnitDto extends AuditedEntityDto<string> {
	/** number */
	code: string;
	/** display name */
	displayName: string;
	/** Parent node identifier */
	parentId?: string;
}

/** Organization paging query object */
interface GetOrganizationUnitPagedListInput extends PagedAndSortedResultRequestDto {
	/** Filter characters */
	filter?: string;
}

/** Organization creation object */
interface OrganizationUnitCreateDto {
	/** display name */
	displayName: string;
	/** Parent node identifier */
	parentId?: string;
}

/** Organizational structure change object */
interface OrganizationUnitUpdateDto {
	/** display name */
	displayName: string;
}

/** Add department objects to the organization */
interface OrganizationUnitAddRoleInput {
	/** Role ID list */
	roleIds: string[];
}

/** Add user objects to the organization */
interface OrganizationUnitAddUserInput {
	/** User ID list */
	userIds: string[];
}

interface OrganizationUnitGetChildrenDto {
	/** Parent organization id */
	id: string;
	/** Recursive query for children */
	recursive?: boolean;
}

interface GetIdentityUsersInput extends PagedAndSortedResultRequestDto {
	filter?: string;
}

interface GetIdentityRolesInput extends PagedAndSortedResultRequestDto {
	filter?: string;
}

interface GetUnaddedUserListInput extends GetUserPagedListInput {
	id: string;
}

interface GetUnaddedRoleListInput extends GetUserPagedListInput {
	id: string;
}

interface OrganizationUnitAddUserDto {
	userIds: string[];
}

interface OrganizationUnitAddRoleDto {
	roleIds: string[];
}

export type {
	GetIdentityRolesInput,
	GetIdentityUsersInput,
	GetOrganizationUnitPagedListInput,
	GetUnaddedRoleListInput,
	GetUnaddedUserListInput,
	OrganizationUnitAddRoleDto,
	OrganizationUnitAddRoleInput,
	OrganizationUnitAddUserDto,
	OrganizationUnitAddUserInput,
	OrganizationUnitCreateDto,
	OrganizationUnitDto,
	OrganizationUnitGetChildrenDto,
	OrganizationUnitUpdateDto,
};
