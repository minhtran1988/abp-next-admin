# LINGYUN.Abp.IM

The basic module of instant messaging module.

## Features

* Provide instant messaging infrastructure
* Provide message sender provider interface
* Extensible message sender provider

## Configuration usage

Module configuration uses the `AbpIMOptions` option class:

```csharp
Configure<AbpIMOptions>(options =>
{
    //Add custom message sender provider
    options.Providers.Add<CustomMessageSenderProvider>();
});
```

## More

[English document](README.EN.md)
