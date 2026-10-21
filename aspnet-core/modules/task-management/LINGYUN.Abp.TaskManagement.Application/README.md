# LINGYUN.Abp.TaskManagement.Application

The application layer implementation of the task management module provides the core function implementation of background job management.

## Function implementation

### Background job management service
- Background job information service (BackgroundJobInfoAppService)
  - Implement CRUD operations for jobs
  - Provide job control functions (start, stop, pause, resume, etc.)
  - Support batch operation function
  - Implement job query and filtering

### Background job behavior service (BackgroundJobActionAppService)
- Behavior management functions:
  - Add job behavior
  - Update job behavior
  - Delete job behavior
  - Get job behavior list
- Behavior definition management:
  - Get available behavior definitions
  - Behavior parameter configuration
  - Behavior enable/disable controls

### Background job log service (BackgroundJobLogAppService)
- Log management function:
  - Get log details
  - Get log list
  - Delete log records
- Log query function:
  -Support multi-condition combination query
  - Paging query
  - Sorting function
  - Advanced filtering

### Object mapping configuration
- AutoMapper configuration file:
  - Mapping from BackgroundJobInfo to BackgroundJobInfoDto
  - Mapping from BackgroundJobLog to BackgroundJobLogDto
  - Mapping from BackgroundJobAction to BackgroundJobActionDto

### Module configuration
- Dependent modules:
  - AbpAutoMapper
  - AbpDynamicQueryable
  - TaskManagementDomain
  - TaskManagementApplication.Contracts
- Service configuration:
  - Automatic object mapping configuration
  - Verify configuration

### Extended functions
- Expression expansion:
  - AndIf conditional expression
  - OrIf conditional expression
- Dynamic query support
- Localized resource integration

## Usage

1. Add module dependencies:
```csharp
[DependsOn(typeof(TaskManagementApplicationModule))]
public class YourModule : AbpModule
{
    // ...
}
```

2. Inject and use the service:
```csharp
public class YourService
{
    private readonly IBackgroundJobInfoAppService _jobInfoAppService;
    private readonly IBackgroundJobActionAppService _jobActionAppService;
    private readonly IBackgroundJobLogAppService _jobLogAppService;

    public YourService(
        IBackgroundJobInfoAppService jobInfoAppService,
        IBackgroundJobActionAppService jobActionAppService,
        IBackgroundJobLogAppService jobLogAppService)
    {
        _jobInfoAppService = jobInfoAppService;
        _jobActionAppService = jobActionAppService;
        _jobLogAppService = jobLogAppService;
    }
}
```
