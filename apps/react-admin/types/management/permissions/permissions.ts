interface PermissionProvider {
	providerKey?: string;
	providerName: string;
}

interface PermissionDto {
	allowedProviders: string[];
	displayName: string;
	grantedProviders: PermissionProvider[];
	isGranted: boolean;
	name: string;
	parentName?: string;
}

interface PermissionGroupDto {
	displayName: string;
	name: string;
	permissions: PermissionDto[];
}

interface PermissionUpdateDto {
	/** Authorization or not */
	isGranted: boolean;
	/** Permission name */
	name: string;
}

interface PermissionsUpdateDto {
	permissions: PermissionUpdateDto[];
}

interface PermissionResultDto {
	entityDisplayName: string;
	groups: PermissionGroupDto[];
}

interface PermissionTree {
	/** Child node */
	children: PermissionTree[];
	/** Whether to disable */
	disabled: boolean;
	/** display name */
	displayName: string;
	/** Authorization or not */
	isGranted?: boolean;
	isRoot: boolean;
	/** Permission identification */
	name: string;
	/** Parent node */
	parentName?: string;
}

export type {
	PermissionDto,
	PermissionGroupDto,
	PermissionProvider,
	PermissionResultDto,
	PermissionsUpdateDto,
	PermissionTree,
};
