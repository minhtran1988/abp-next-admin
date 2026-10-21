# LINGYUN.Abp.TaskManagement.Domain.Shared

Task management domain shared module, including shared domain models, enumerations and constants.

## Function

### Permission management
- Task scheduling platform permissions
- Background job permissions
- Job behavior permissions
- Job log permissions

### Job type and status
-Job type:
  - One-time job (executed only once)
  - Periodic jobs (run periodically according to given conditions)
  - Continuous jobs (run repeatedly according to given conditions)
- Job status:
  - undefined
  - Completed
  - Running
  - in queue
  - Suspended
  - Retry on failure
  - Stopped

### Priority level
- low
- lower than normal
- normal
- higher than normal
- high

### Job properties
- Basic information:
  - Group
  - Name
  - Description
  - type
  - Status
  - start/end time
- Execute settings:
  - time interval (seconds)
  - Cron expressions
  - Lock timeout
  - Priority
  - Maximum number of triggers
  - Maximum number of retries
- Tracking information:
  - Creation time
  - Last execution time
  - next expected time
  - Number of triggers
  - number of attempts
  -Execution results

### Localization
- Support multiple languages
- Error code localization
- UI text localization

### Multi-tenant support
- Tenant-level job management
- System-level job management

### Source type
- User jobs
- System built-in jobs
