# LINGYUN.Abp.TaskManagement.HttpApi

The HTTP API implementation of the task management module provides a RESTful interface for managing background jobs.

## Function

### Background job information API
- CRUD operations:
  - Create new job
  - Get job details
  - Update job properties
  - Delete job
-Job control operations:
  - Start job
  - Stop job
  - Pause job
  - Resume job
  - trigger job
- Batch operations:
  - Batch startup
  - Batch stop
  - Batch pause
  - Batch recovery
  - Batch trigger
  - Batch deletion
- Query operation:
  - Get a list of paginated jobs
  - Get job definition
  - Filter and sort jobs

### Background job behavior API
- Behavior management:
  - Add job behavior
  - Update behavior attributes
  - Delete behavior
  - Get a list of actions
- Behavior definition:
  - Get available behavior definitions
  - Query behavior definition

### Background job log API
- Log operations:
  - Get log details
  - Get paginated log list
  - Delete log
- Log filtering:
  - Filter by job
  - Filter by time range
  - Filter by status

### Authorization
- Permission-based access control:
  -Create job permissions
  - Update job permissions
  - Delete job permissions
  - Trigger job permissions
  - Suspend job permissions
  - Restore job permissions
  - Start job permissions
  - Stop job permission
  - Delete log permission

### API features
- RESTful interface
- Operations based on HTTP methods
- Route-based API version control
- Standardized response format
- Pagination support
- Dynamic filtering and sorting

### Localization
- Multi-language support
- Localized error messages
- Localized verification messages

### Integration
- ABP framework integration
-MVC integration
- Dynamic query support
- Verification support
