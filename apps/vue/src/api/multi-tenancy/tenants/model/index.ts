/** Tenant query filter object */
export interface TenantGetByPaged extends PagedAndSortedResultRequestDto {
    /** Query filter field */
    filter?: string;
  }
  
  /** Tenant creation object */
  export interface TenantCreateOrEdit {
    /** Administrator email address */
    adminEmailAddress: string;
    /** Administrator password */
    adminPassword: string;
    /** Tenant name */
    name: string;
    /** Use shared database */
    useSharedDatabase: boolean;
    /**Default connection string */
    defaultConnectionString?: string;
  }
  
  /** Tenant object */
  export interface TenantDto extends FullAuditedEntityDto<string> {
    /** Tenant name */
    name: string;
  }
  
  /** Tenant connection string */
  export interface TenantConnectionString {
    /** name */
    name: string;
    /** value */
    value: string;
  }
  
  export interface FindTenantResult {
    name: string;
    tenantId: string;
    success: string;
    isActive: boolean;
  }
  