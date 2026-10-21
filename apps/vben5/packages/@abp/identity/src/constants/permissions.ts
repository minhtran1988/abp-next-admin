/** User permissions */
export const IdentityUserPermissions = {
  /** Add new */
  Create: 'AbpIdentity.Users.Create',
  /** Delete */
  Delete: 'AbpIdentity.Users.Delete',
  /** Management Statement */
  ManageClaims: 'AbpIdentity.Users.ManageClaims',
  /** Management permissions */
  ManagePermissions: 'AbpIdentity.Users.ManagePermissions',
  /** Update */
  Update: 'AbpIdentity.Users.Update',
  /** Impersonate user */
  Impersonation: 'AbpIdentity.Users.Impersonation',
};
/** Role permissions */
export const IdentityRolePermissions = {
  /** Add new */
  Create: 'AbpIdentity.Roles.Create',
  /** Delete */
  Delete: 'AbpIdentity.Roles.Delete',
  /** Management Statement */
  ManageClaims: 'AbpIdentity.Roles.ManageClaims',
  /** Management permissions */
  ManagePermissions: 'AbpIdentity.Roles.ManagePermissions',
  /** Update */
  Update: 'AbpIdentity.Roles.Update',
};
/** Declare type permissions */
export const IdentityClaimTypePermissions = {
  /** Add new */
  Create: 'AbpIdentity.IdentityClaimTypes.Create',
  /** Delete */
  Delete: 'AbpIdentity.IdentityClaimTypes.Delete',
  /** Update */
  Update: 'AbpIdentity.IdentityClaimTypes.Update',
};
/** Organization permissions */
export const OrganizationUnitPermissions = {
  /** Management role */
  ManageRoles: 'AbpIdentity.OrganizationUnits.ManageRoles',
  /** Manage users */
  ManageUsers: 'AbpIdentity.OrganizationUnits.ManageUsers',
};
/** Security log permissions */
export const SecurityLogPermissions = {
  Default: 'AbpAuditing.SecurityLog',
  /** Delete */
  Delete: 'AbpAuditing.SecurityLog.Delete',
};
/** User session permissions */
export const IdentitySessionPermissions = {
  Default: 'AbpIdentity.IdentitySessions',
  /** Remove */
  Revoke: 'AbpIdentity.IdentitySessions.Revoke',
};
/**
 * Search user permissions
 * @deprecated The background service will not be able to be used after deleting permissions.
 * @todo Need to check abp framework permission definition
 */
export const UserLookupPermissions = {
  Default: 'AbpIdentity.UserLookup',
};
