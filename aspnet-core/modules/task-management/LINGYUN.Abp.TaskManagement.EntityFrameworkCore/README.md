# LINGYUN.Abp.TaskManagement.EntityFrameworkCore

Entity Framework Core implementation of the task management module, providing database access and persistence.

## Function

### Database context
- TaskManagementDbContext is used to manage database operations
- Configurable table prefix and schema
- Supports multi-tenancy

### Entity configuration
- Background job information:
  - Table name: {prefix}BackgroundJobs
  - Index Name and Group
  - Properties with length constraints
  - Support additional attributes
- Background job log:
  - Table name: {prefix}BackgroundJobLogs
  - Index JobGroup and JobName
  - Properties with length constraints
- Background job behavior:
  - Table name: {prefix}BackgroundJobActions
  - Index Name
  - Supports additional properties for parameters

### Warehousing implementation
- Backend job information warehousing:
  - CRUD operations
  - Job status management
  - Job filtering and querying
  - Support job expiration
  - Waiting job list management
  - Periodic task management
-Background job log storage:
  - Log storage and retrieval
  - Log filtering and querying
  - Pagination support
- Backend job behavior warehousing:
  - Behavior storage and retrieval
  - Parameter management

### Query function
- Dynamic sorting
- Pagination
- Filter by specification
- Asynchronous operations
- Tracking-free queries for read-only operations

### Performance optimization
- Efficient indexing
- Support batch operations
- Optimized query for job status

### Multi-tenant support
- Tenant specific data isolation
- Cross-tenant operation
- Tenant-aware warehousing

### Integrated functions
- ABP framework integration
- Entity Framework Core conventions
- Value converters for complex types
- Additional attribute support
