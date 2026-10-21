import type {
  ExtensibleObject,
  FullAuditedEntityDto,
  IHasConcurrencyStamp,
  IHasExtraProperties,
  PagedAndSortedResultRequestDto,
} from '@abp/core';

/** User Object Interface */
interface IUser {
  /** Email address */
  email: string;
  /** Login lock */
  lockoutEnabled: boolean;
  /** Username */
  name: string;
  /** Contact information */
  phoneNumber?: string;
  /** User abbreviation */
  surname?: string;
  /** Two-factor authentication */
  twoFactorEnabled: boolean;
  /** User account */
  userName: string;
}

/** Change password data transfer object */
interface ChangeMyPasswordInput {
  /** Current password */
  currentPassword?: string;
  /** New password */
  newPassword: string;
}

interface ChangeUserPasswordInput {
  /** New password */
  password: string;
}

/** User organization data transfer object */
interface IdentityUserOrganizationUnitUpdateDto {
  /** Organization identification list */
  organizationUnitIds: string[];
}
/** User entity data transfer object */
interface IdentityUserDto
  extends
    FullAuditedEntityDto<string>,
    IHasConcurrencyStamp,
    IHasExtraProperties,
    IUser {
  /** Number of failed accesses */
  accessFailedCount?: number;
  /** Email has been verified */
  emailConfirmed: boolean;
  /** Activated user */
  isActive: boolean;
  /** Last password change time */
  lastPasswordChangeTime?: Date;
  /** Lock expiration time */
  lockoutEnd?: Date;
  /** Contact information verified */
  phoneNumberConfirmed: boolean;
  /** Role list */
  roleNames: string[];
  /** Tenant ID */
  tenantId?: string;
}
/** User entity data view object */
type IdentityUserVto = Omit<
  IdentityUserDto,
  'creationTime' | 'id' | 'isDeleted'
> & { creationTime?: Date; id?: string; password?: string };
/** Create or update user entity data transfer object */
interface IdentityUserCreateOrUpdateDto extends ExtensibleObject {
  /** Email address */
  email: string;
  /** Login lock */
  lockoutEnabled: boolean;
  /** Username */
  name: string;
  /** Password */
  password?: string;
  /** Contact information */
  phoneNumber?: string;
  /** Role list */
  roleNames?: string[];
  /** User abbreviation */
  surname?: string;
  /** User account */
  userName: string;
}
/** Get user paging list data transfer object */
interface GetUserPagedListInput extends PagedAndSortedResultRequestDto {
  /** Filter conditions */
  filter?: string;
}

type IdentityUserCreateDto = IdentityUserCreateOrUpdateDto;
type IdentityUserUpdateDto = IdentityUserCreateOrUpdateDto;

interface UserLookupCountInput {
  filter?: string;
}

interface UserLookupSearchInput
  extends PagedAndSortedResultRequestDto, UserLookupCountInput {}

export type {
  ChangeMyPasswordInput,
  ChangeUserPasswordInput,
  GetUserPagedListInput,
  IdentityUserCreateDto,
  IdentityUserDto,
  IdentityUserOrganizationUnitUpdateDto,
  IdentityUserUpdateDto,
  IdentityUserVto,
  UserLookupCountInput,
  UserLookupSearchInput,
};
