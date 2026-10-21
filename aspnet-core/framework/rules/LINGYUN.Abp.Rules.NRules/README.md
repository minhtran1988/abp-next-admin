# LINGYUN.Abp.Rules.NRules

## Module description

Rule engine implementation module based on [NRules](https://github.com/NRules/NRules).

### Basic module

* LINGYUN.Abp.Rules

### Function definition

* Provide rule engine implementation based on NRules
* Support automatic registration rules through dependency injection
* Support dynamic loading and execution of rules

### Configuration definition

* AbpNRulesOptions
  * DefinitionRules - Definition rule list, used to store all rule types registered through dependency injection

### How to use

1. Add `AbpNRulesModule` dependency

```csharp
[DependsOn(typeof(AbpNRulesModule))]
public class YouProjectModule : AbpModule
{
}
```

2. Create a rule class

```csharp
public class YourRule : RuleBase
{
    public override void Define()
    {
        //Define rule conditions and actions
        When()
            .Match<YourInput>(x => x.SomeCondition);
        Then()
            .Do(ctx => /* Execute rule action */);
    }
}
```

3. Register rules and execute them

```csharp
public class YourService 
{
    private readonly IRuleProvider _ruleProvider;

    public YourService(IRuleProvider ruleProvider)
    {
        _ruleProvider = ruleProvider;
    }

    public async Task ProcessAsync()
    {
        var input = new YourInput();
        //Execute rule verification
        await _ruleProvider.ExecuteAsync(input);
    }
}
```

[Return to directory](../../../README.md)
