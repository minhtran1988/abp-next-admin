# LINGYUN.Abp.Wrapper

The wrapper module is used to uniformly wrap API return results and exception handling.

## Function

* Unified return result packaging
* Flexible exception handling mechanism
* Supports multiple ignore strategies
* Configurable empty result handling
* Custom exception handler

## Installation

```bash
dotnet add package LINGYUN.Abp.Wrapper
```

## Configuration usage

```csharp
[DependsOn(typeof(AbpWrapperModule))]
public class YouProjectModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpWrapperOptions>(options =>
        {
            // enable wrapper
            options.IsEnabled = true;
            
            // Customize the error code for unhandled exceptions
            options.CodeWithUnhandled = "500";
            
            // Ignore URLs with specific prefixes
            options.IgnorePrefixUrls.Add("/api/health");
            
            //Add custom exception handler
            options.AddHandler<CustomException>(new CustomExceptionHandler());
        });
    }
}
```

## Configuration item description

* AbpWrapperOptions.IsEnabled Whether to wrap the return result, default: false
* AbpWrapperOptions.CodeWithUnhandled Return error code when an unhandled exception occurs, default 500
*AbpWrapperOptions.CodeWithSuccess returns code for successful processing, default 0
* AbpWrapperOptions.ErrorWithEmptyResult When requesting a resource, whether to return an error message if the resource is empty, default false
* AbpWrapperOptions.HttpStatusCode wrapped Http response code, default: 200
* AbpWrapperOptions.CodeWithEmptyResult returns the error code when returning an empty object, default: 404
* AbpWrapperOptions.MessageWithEmptyResult returns an error message when an empty object is returned, default: Not Found

* AbpWrapperOptions.IgnorePrefixUrls specifies which addresses starting with Url do not need to be processed
* AbpWrapperOptions.IgnoreNamespaces specifies which namespaces do not need to be processed at the beginning
* AbpWrapperOptions.IgnoreControllers specifies which controllers do not need to be processed
* AbpWrapperOptions.IgnoreReturnTypes specifies which return result types do not need to be processed
* AbpWrapperOptions.IgnoreExceptions specifies which exception types do not need to be handled
* AbpWrapperOptions.IgnoredInterfaces specifies which interfaces do not need to be processed (the default implementation of the **IWrapDisabled** interface is not processed)


## Usage example

### 1. Basic usage

```csharp
public class TestController : AbpController
{
    public async Task<WrapResult<string>> GetAsync()
    {
        return new WrapResult<string>("0", "Hello World");
    }
}
```

### 2. Ignore packaging

```csharp
[IgnoreWrapResult]
public class HealthController : AbpController
{
    public async Task<string> GetAsync()
    {
        return "OK";
    }
}
```

### 3. Custom exception handling

```csharp
public class CustomExceptionHandler : IExceptionWrapHandler
{
    public void Wrap(ExceptionWrapContext context)
    {
        context.WithCode("CUSTOM_ERROR")
               .WithMessage("A custom exception occurred")
               .WithDetails(context.Exception.Message);
    }
}
```

## Link

* [English document](./README.EN.md)
