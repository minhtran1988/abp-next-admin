[Client](./LINGYUN.Abp.Dapr.Client/README.md) | Dapr.Actors documentation

#Dapr.Actors integration

## Configuration usage

The project design is similar to **Volo.Abp.Http.Client**

### 1. Interface definition

```c#

// The RemoteService.Name defined on the interface will be registered to Dapr as the name of the Actor.
[RemoteService(Name = "System")]
public interface ISystemActor : IActor
{
    Task<string> GetAsync();
}

public class SystemActorInterfaceModule : AbpModule
{

}

```

### 2. Server

Reference LINGYUN.Abp.Dapr.Actors.AspNetCore

* Implement interface

```c#

public class SystemActor : Actor, ISystemActor 
{
    public Task<string> GetAsync() 
    {
        retuen Task.FromResult("System");
    }
}

```

* Create module

```c#

// The module will automatically search for services that implement IActor and register them as Dapr Actors
[DependsOn(
        typeof(AbpDaprActorsAspNetCoreModule))]
public class SystemActorServerModule : AbpModule
{

}

```

* Post to Dapr

```shell
# --app-port .net program mapping port
# -H Expose http listening port to the outside world
# -G Expose grpc listening port to the outside world
dapr run --app-id myapp --app-port 5000 -H 50000 -G 40001 -- dotnet run

```

### 3. Client

Quote LINGYUN.Abp.Dapr.Actors

* Configuration file **appsettings.json**

```json

{
    "RemoteServices": {
        "Shop": {
            "BaseUrl": "http://127.0.0.1:50000"
        }
    }
}

```

* Client code

```c#

//Module dependencies
[DependsOn(
        typeof(AbpDaprActorsModule))]
public class SystemActorClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // The registered proxy is similar to the Volo.Abp.Http.Client module
        context.Services.AddDaprActorProxies(
            typeof(SystemActorInterfaceModule).Assembly, // Search for the IActor definition under the SystemActorInterfaceModule module
            "Shop"
        );
    }
}

//To call the method, just rely on direct injection
public class InvokeClass
{
    private readonly ISystemActor _systemActor;

    public InvokeClass(ISystemActor systemActor)
    {
        _systemActor = systemActor; 
    }

    public async Task InvokeAsync()
    {
        await _systemActor.GetAsync();
    }
}

```

## Others
