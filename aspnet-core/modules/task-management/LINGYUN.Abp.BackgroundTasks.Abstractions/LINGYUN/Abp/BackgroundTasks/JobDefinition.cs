using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp.Localization;

namespace LINGYUN.Abp.BackgroundTasks;

public class JobDefinition
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Job type
    /// </summary>
    public Type JobType { get; }
    /// <summary>
    /// Is it visible to the client?
    /// Default: true.
    /// </summary>
    public bool IsVisibleToClients { get; set; }
    /// <summary>
    /// Display name
    /// </summary>
    public ILocalizableString DisplayName { get; }
    /// <summary>
    /// Description
    /// </summary>
    public ILocalizableString? Description { get; }
    /// <summary>
    /// Parameter list
    /// </summary>
    public IReadOnlyList<JobDefinitionParamter>? Paramters { get; }
    public JobDefinition(
        [NotNull] string name,
        [NotNull] Type jobType,
        [NotNull] ILocalizableString displayName,
        [CanBeNull] IReadOnlyList<JobDefinitionParamter>? paramters = null,
        [CanBeNull] ILocalizableString? description = null,
        bool isVisibleToClients = true)
    {
        Name = name;
        JobType = jobType;
        DisplayName = displayName;
        Description = description;
        Paramters = paramters ?? new JobDefinitionParamter[0];
        IsVisibleToClients = isVisibleToClients;
    }
}
