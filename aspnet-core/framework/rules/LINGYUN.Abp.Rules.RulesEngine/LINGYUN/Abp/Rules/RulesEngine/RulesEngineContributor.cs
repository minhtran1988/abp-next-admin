using RulesEngine;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.Rules.RulesEngine;

public class RulesEngineContributor : RuleContributorBase, ISingletonDependency
{
    private readonly IRulesEngine _ruleEngine;
    private readonly IWorkflowsResolver _workflowRulesResolver;

    public RulesEngineContributor(
        IRulesEngine ruleEngine,
        IWorkflowsResolver workflowRulesResolver)
    {
        _ruleEngine = ruleEngine;
        _workflowRulesResolver = workflowRulesResolver;
    }

    public override void Initialize(RulesInitializationContext context)
    {
        _workflowRulesResolver.Initialize(context);
    }

    public override async Task ExecuteAsync<T>(T input, object[]? @params = null, CancellationToken cancellationToken = default)
    {
        var result = await _workflowRulesResolver.ResolveWorkflowsAsync(typeof(T));

        if (result.Workflows.Any())
        {
            await ExecuteRulesAsync(input, result.Workflows.ToArray(), @params);
        }
    }

    public override void Shutdown()
    {
        _workflowRulesResolver.Shutdown();
    }

    protected async virtual Task ExecuteRulesAsync<T>(T input, Workflow[] workflows, object[]? @params = null) where T : notnull
    {
        // TODO: Performance defect. The rule file will be compiled repeatedly every time it is called.
        _ruleEngine.AddOrUpdateWorkflow(workflows);

        // Pass in the entity parameters participating in the verification
        var inputs = new List<object>()
        {
            input
        };
        if (@params != null && @params.Any())
        {
            inputs.AddRange(@params);
        }
        // Other parameters can be deduced in the same way

        foreach (var workflow in workflows)
        {
            // Execute current rules
            var ruleResult = await _ruleEngine.ExecuteAllRulesAsync(workflow.WorkflowName, inputs.ToArray());
            // User-defined extension method, rule verification error throws exception
            ruleResult.ThrowOfFaildExecute();
        }
    }
}
