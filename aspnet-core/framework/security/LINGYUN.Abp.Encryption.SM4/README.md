# LINGYUN.Abp.Encryption.SM4

Data encryption module, using the National Secret SM4 algorithm, uses **AbpStringEncryptionOptions** to configure seamless switching (the key length is fixed at 128 bits to meet the algorithm requirements)

## Configuration usage


```csharp
[DependsOn(typeof(AbpEncryptionSM4Module))]
public class YouProjectModule : AbpModule
{
  // other
}
```
