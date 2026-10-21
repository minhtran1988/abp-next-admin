using JetBrains.Annotations;
using System.Collections.Generic;

namespace LINGYUN.Abp.Rules.RulesEngine;

public class AbpRulesEngineResolveOptions
{
    /// <summary>
    /// Merge rules
    /// If true, continue executing the next provider after the previous provider parses the rules.
    /// If false, execute the rule immediately after the previous provider parsed the rule
    /// Default: false
    /// </summary>
    public bool MergingWorkflows { get; set; }
    /// <summary>
    /// Rule parsing provider
    /// </summary>
    [NotNull]
    public List<IWorkflowsResolveContributor> WorkflowsResolvers { get; }

    public AbpRulesEngineResolveOptions()
    {
        MergingWorkflows = false;
        WorkflowsResolvers = new List<IWorkflowsResolveContributor>();
    }
}
