using RulesEngine.Models;

namespace LINGYUN.Abp.Rules.RulesEngine;

public class AbpRulesEngineOptions
{
    /// <summary>
    /// Whether to ignore tenants
    /// </summary>
    public bool IgnoreMultiTenancy { get; set; }
    /// <summary>
    /// The rule engine is configurable
    /// </summary>
    public ReSettings Settings { get; set; }

    public AbpRulesEngineOptions()
    {
        Settings = new ReSettings
        {
            NestedRuleExecutionMode = NestedRuleExecutionMode.Performance
        };
    }
}
