# LINGYUN.Abp.Telemetry.SkyWalking

Distributed tracing system `SkyWalking` integration

## Features


## Module reference

```csharp
[DependsOn(typeof(AbpTelemetrySkyWalkingModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Configuration items

```shell

# Switch to the main program directory
cd my-host-project-path

#Install skyapm command line tool
dotnet tool install -g SkyAPM.DotNet.CLI

# Generate SkyWalking configuration file, localhost:11800 is the Grpc port exposed by the SkyWalking instance you are running.
dotnet skyapm config auth_server localhost:11800
```

```csharp

public class YouProjectModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SkyApmExtensions>(skyapm =>
        {
            skyapm.AddCap();

            // other...
        });
    }
}


```

## More documentation

* [SkyWalking](https://skywalking.apache.org/)
* [SkyWalking .NET](https://github.com/SkyAPM/SkyAPM-dotnet)
