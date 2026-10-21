# LINGYUN.Abp.TaskManagement.Domain

Task management domain module implements core business logic and domain model.

## Function

### Background job management
-Job life cycle management (create, update, delete)
- Job status control (start, stop, pause, resume, trigger)
- Supports different types of jobs:
  - One-time job
  - Periodic jobs (using cron expressions)
  - Continuous jobs (using time intervals)

### Job storage
- Store job information and execution status
- Track job execution history
- Clean up expired jobs
- Supports multi-tenancy

### Job synchronization
- Synchronize job status in distributed systems
- Handle job creation, update and delete events
- Maintain job queue consistency

### Job behavior
- Manage job-related behaviors
- Store behavior parameters
- Enable/disable behavior

### Job filtering and specification
- Filter jobs by multiple criteria:
  - type
  - Group
  - Name
  - Status
  - Priority
  - source
  - Creation time
  - Last run time
-Support complex job queries

### Job Log
- Record job execution details
- Track execution results and exceptions
- Supports multi-tenant logging

### Domain events
-Job status change event
-Job execution events
- Distributed event processing

### Job priority management
- Supports multiple priority levels:
  - low
  - lower than normal
  - normal
  - higher than normal
  - high

### Job source management
- Support different job sources:
  - User jobs
  - System work

### Multi-tenant support
- Tenant-specific job management
- Cross-tenant job operations
- Tenant isolation during job execution

### Domain Services
- Background job manager
-Job storage service
- Homework behavior service
-Job log service
