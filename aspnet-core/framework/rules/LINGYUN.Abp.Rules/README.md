# LINGYUN.Abp.Rules

## Module description

Rule engine basic module

### Basic module

### High-level modules

### Permission definition

### Function definition

### Configuration definition

### How to use


```csharp

    [DependsOn(
        typeof(AbpRulesModule))]
    public class YouProjectModule : AbpModule
    {
    }

    public class YouService 
    {
        private readonly IRuleProvider _ruleProvider;

        public YouService(IRuleProvider ruleProvider)
        {
            _ruleProvider = ruleProvider;
        }

        public async Task DoAsync()
        {
            var input = new YouInput();
            //Rule verification
            await _ruleProvider.ExecuteAsync(input);

            var inputParams = new object[1]
            {
                new InputParam()
            };
            // Rule verification with parameters
            await _ruleProvider.ExecuteAsync(input, inputParams);
        }
    }

```

### Update log
