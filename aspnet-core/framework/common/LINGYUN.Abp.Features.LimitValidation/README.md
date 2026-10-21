[English](./README.en.md) | Simplified Chinese

# LINGYUN.Abp.Features.LimitValidation

Function cap verification component

Check the defined number of function calls to limit calls to the application by specific entities (tenants, users, clients, etc.)

The following strategies are preset:

LimitPolicy.Minute calculates traffic by minute
LimitPolicy.Hours calculates traffic by hour
LimitPolicy.Days calculates traffic based on days
LimitPolicy.Weeks calculates traffic by number of weeks
LimitPolicy.Month calculates traffic by month
LimitPolicy.Years calculates traffic by number of years

## Configuration usage


```csharp
[DependsOn(typeof(AbpFeaturesLimitValidationModule))]
public class YouProjectModule : AbpModule
{
  // other
}

public static class FakeFeatureNames 
{
    public const string GroupName = "FakeFeature.Tests";
    // Type limits the number of calls function name
    public const string ClassLimitFeature = GroupName + ".LimitFeature";
    // Function name to limit the number of method calls
    public const string MethodLimitFeature = GroupName + ".MethodLimitFeature";
    //Limit call interval function name
    public const string IntervalFeature = GroupName + ".IntervalFeature";
}

// Traffic limit depends on custom functions
public class FakeFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
        var featureGroup = context.AddGroup(FakeFeatureNames.GroupName);
        featureGroup.AddFeature(
            name: FakeFeatureNames.ClassLimitFeature,
            defaultValue: 1000.ToString(), // A maximum of 1000 calls are allowed within the cycle
            valueType: new ToggleStringValueType(new NumericValueValidator(1, 1000)));
        featureGroup.AddFeature(
            name: FakeFeatureNames.MethodLimitFeature,
            defaultValue: 100.ToString(), // A maximum of 100 calls are allowed in the cycle
            valueType: new ToggleStringValueType(new NumericValueValidator(1, 1000)));
        featureGroup.AddFeature(
            name: FakeFeatureNames.IntervalFeature,
            defaultValue: 1.ToString(), // Limit period
            valueType: new ToggleStringValueType(new NumericValueValidator(1, 1000)));
    }
}

//According to the preset parameters, the type is only allowed to be called 1000 times a day
[RequiresLimitFeature(FakeFeatureNames.ClassLimitFeature, FakeFeatureNames.IntervalFeature, LimitPolicy.Days)]
public class FakeLimitClass
{
    // According to the preset parameters, the method is only allowed to be called 100 times in one minute.
    [RequiresLimitFeature(FakeFeatureNames.MethodLimitFeature, FakeFeatureNames.IntervalFeature, LimitPolicy.Minute)]
    public void LimitMethod() 
    {
        // other...
    }
}
```

If you need to handle the function limit policy duration by yourself, please override the default policy of the corresponding policy. The returned clock scale unit is always seconds.

```csharp
[DependsOn(typeof(AbpFeaturesLimitValidationModule))]
public class YouProjectModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpFeaturesLimitValidationOptions>(options =>
        {
            options.MapEffectPolicy(LimitPolicy.Minute, (now, tick) => return 60;); // Indicates that no matter how many minutes (time), it will only be limited to 60 seconds
         });
    }
}
```
