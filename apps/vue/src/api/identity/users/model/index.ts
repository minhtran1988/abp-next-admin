import { IdentityClaim } from "../../claims/model";

/** User Object Interface */
export interface IUser {
  /** Username */
  name: string;
  /** User account */
  userName: string;
  /** User abbreviation */
  surname?: string;
  /** Email address */
  email: string;
  /** Contact information */
  phoneNumber?: string;
  /** Two-factor authentication */
  twoFactorEnabled: boolean;
  /** Login lock */
  lockoutEnabled: boolean;
}

export interface ChangePassword {
  currentPassword?: string;
  newPassword: string;
}

export interface SetPassword {
  password: string;
}

export interface IdentityUserOrganizationUnitUpdateDto {
  organizationUnitIds: string[]
}

/** User object */
export interface User extends FullAuditedEntityDto<string>, IUser, IHasConcurrencyStamp {
  /** Tenant ID */
  tenentId?: string;
  /** Email has been verified */
  emailConfirmed: boolean;
  /** Contact information verified */
  phoneNumberConfirmed: boolean;
  /** Lock expiration time */
  lockoutEnd?: Date;
  /** Activated user */
  isActive: boolean;
  /** Role list */
  roleNames: string[];
}

export interface CreateOrUpdateUser extends ExtensibleObject {
   /** Username */
   name: string;
   /** User account */
   userName: string;
   /** User abbreviation */
   surname?: string;
   /** Email address */
   email: string;
   /** Contact information */
   phoneNumber?: string;
   /** Login lock */
   lockoutEnabled: boolean;
  /** Role list */
  roleNames?: string[];
  /** Password */
  password?: string;
}

/** Change user object */
export interface UpdateUser extends CreateOrUpdateUser {}

export interface CreateUser extends CreateOrUpdateUser {}

export interface GetUserPagedRequest extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UserClaim extends IdentityClaim {
  id: string;
}
