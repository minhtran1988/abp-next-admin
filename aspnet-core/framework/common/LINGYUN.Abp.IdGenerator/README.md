# LINGYUN.Abp.IdGenerator

## Introduction

`LINGYUN.Abp.IdGenerator` is a distributed ID generator module that mainly implements the Snowflake algorithm to generate distributed unique IDs.

## Function

* Snowflake algorithm ID generator (`SnowflakeIdGenerator`)
  * Support custom work machine ID and data center ID
  * Support custom serial number digits
  * Support time rollback processing
  * Provide unique ID generation in a distributed environment

## Configuration items

### SnowflakeIdOptions

* `WorkerIdBits` (default: 5) - Number of digits in the worker machine ID
* `DatacenterIdBits` (default: 5) - Number of data center ID bits
* `Sequence` (default: 0) - starting sequence number value
* `SequenceBits` (default: 12) - number of sequence number digits
* `UsePreviousInTimeRollback` (default: true) - whether to use the previous timestamp when rolling back time
* `WorkerId` - Worker machine ID, if not specified, it is obtained from the environment variable WORKERID or randomly generated
* `DatacenterId` - Data center ID, if not specified, it is obtained from the environment variable DATACENTERID or randomly generated

## Installation

```bash
dotnet add package LINGYUN.Abp.IdGenerator
```

## Use

1. Add `[DependsOn(typeof(AbpIdGeneratorModule))]` to your module class.

```csharp
[DependsOn(typeof(AbpIdGeneratorModule))]
public class YourModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SnowflakeIdOptions>(options =>
        {
            options.WorkerId = 1;
            options.DatacenterId = 1;
        });
    }
}
```

2. Inject and use the ID generator:

```csharp
public class YourService
{
    private readonly IDistributedIdGenerator _idGenerator;

    public YourService(IDistributedIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public long CreateId()
    {
        return _idGenerator.Create();
    }
}
```

## Link

* [English document](./README.EN.md)
