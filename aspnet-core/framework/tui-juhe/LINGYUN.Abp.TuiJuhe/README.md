# LINGYUN.Abp.TuiJuhe

Integrate TuiJuhe (only applicable to specific messages and personnel notifications, mass sending is not supported)

Implement TuiJuhe related API documents and have TuiJuhe open capabilities

For details, see TuiJuhe documentation: https://tui.juhe.cn/docs

## Module reference

```csharp
[DependsOn(typeof(AbpTuiJuheModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## Features

* TuiJuhe TuiJuhe feature grouping
* TuiJuhe.Enable enables TuiJuhe globally
* TuiJuhe.Message.Enable globally enables TuiJuhe message channel
* TuiJuhe.Message TuiJuhe message push
* TuiJuhe.Message.Enable enables TuiJuhe message push
* TuiJuhe.Message.SendLimit TuiJuhe message push limit number of times
* TuiJuhe.Message.SendLimitInterval TuiJuhe message push limit period (hours)

## Settings

* TuiJuhe.Security.Token user token.
