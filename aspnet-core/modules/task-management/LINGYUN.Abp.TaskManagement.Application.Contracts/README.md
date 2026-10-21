# LINGYUN.Abp.TaskManagement.Application.Contracts

The application contract of the task management module defines the interface and DTO of the application service.

## Function

### Application service interface
-Background job information service:
  - CRUD operations
  -Job control operations
  - Batch operations
  - Query operation
- Background job behavior service:
  - Behavior management
  - Behavior definition
  - Parameter handling
- Background job log service:
  - Log retrieval
  - Log filtering
  - Log deletion

### Data Transfer Objects (DTOs)
- Background job information DTOs:
  - Job information DTO
  -Job creation DTO
  - Job update DTO
  - Job list DTO
  - Job batch input DTO
- Background job behavior DTOs:
  - Behavioral DTO
  - Behavior creates DTO
  - Behavior update DTO
  - Behavior definition DTO
  - Behavior parameter DTO
- Background job log DTOs:
  - Log DTO
  - Log list DTO
  - Log filtering DTO

### Permissions
- Background jobs:
  -Create permissions
  - Update permissions
  - Delete permissions
  - Trigger permissions
  - Suspend permissions
  - restore permissions
  - Start permissions
  - Stop permissions
- Background job log:
  - View permissions
  - Delete permissions

### Remote service configuration
- Service name constant
- Service endpoint configuration
- Client proxy settings

### Verify
- Input validation
- Data annotation
- Custom validation rules

### Integrated functions
- ABP framework integration
- Dynamic query support
- Application service layer abstraction

### Module configuration
- Module dependencies
- Service registration
- Function management
