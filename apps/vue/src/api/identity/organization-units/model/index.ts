/** Organization */
export interface OrganizationUnit extends AuditedEntityDto<string> {
    /** Parent node identifier */
    parentId?: string;
    /** number */
    code: string;
    /** display name */
    displayName: string;
  }
  
  /** Organization paging query object */
  export interface GetOrganizationUnitPagedRequest extends PagedAndSortedResultRequestDto {
    /** Filter characters */
    filter?: string;
  }
  
  /** Organization creation object */
  export interface CreateOrganizationUnit {
    /** display name */
    displayName: string;
    /** Parent node identifier */
    parentId?: string;
  }
  
  /** Organizational structure change object */
  export interface UpdateOrganizationUnit {
    /** display name */
    displayName: string;
  }
  
  /** Add department objects to the organization */
  export interface OrganizationUnitAddRole {
    /** Role ID list */
    roleIds: string[];
  }
  
  /** Add user objects to the organization */
  export interface OrganizationUnitAddUser {
    /** User ID list */
    userIds: string[];
  }
  