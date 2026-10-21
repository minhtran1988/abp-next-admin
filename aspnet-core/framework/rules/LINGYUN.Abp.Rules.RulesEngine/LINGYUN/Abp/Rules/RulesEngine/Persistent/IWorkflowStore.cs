using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.Rules.RulesEngine.Persistent;

/// <summary>
/// Implement this interface for retrieving rules from other persistent stores
/// </summary>
public interface IWorkflowStore
{
    Task<IEnumerable<Workflow>> GetWorkflowsAsync(Type inputType, CancellationToken cancellationToken = default);

    Task<Workflow?> GetWorkflowAsync(string workflowName, CancellationToken cancellationToken = default);
}
