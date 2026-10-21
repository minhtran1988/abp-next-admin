using JetBrains.Annotations;
using System.Collections.Generic;
using Volo.Abp.Collections;
using Volo.Abp.Localization;

namespace LINGYUN.Abp.BackgroundTasks.Activities;

public class JobActionDefinition
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Type
    /// </summary>
    public JobActionType Type { get; }
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
    public IList<JobActionParamter> Paramters { get; }
    /// <summary>
    /// Notify provider
    /// </summary>
    public ITypeList<IJobExecutedProvider> Providers { get; }
    /// <summary>
    /// Additional attributes
    /// </summary>
    public Dictionary<string, object> Properties { get; }
    public JobActionDefinition(
        [NotNull] string name,
        [NotNull] JobActionType type,
        [NotNull] ILocalizableString displayName,
        [NotNull] IList<JobActionParamter> paramters,
        ILocalizableString? description = null)
    {
        Name = name;
        Type = type;
        DisplayName = displayName;
        Paramters = paramters;
        Description = description;

        Providers = new TypeList<IJobExecutedProvider>();
        Properties = new Dictionary<string, object>();
    }

    public virtual JobActionDefinition WithProvider<TProvider>()
        where TProvider : IJobExecutedProvider
    {
        Providers.Add(typeof(TProvider));

        return this;
    }

    public virtual JobActionDefinition WithProperty(string key, object value)
    {
        Properties[key] = value;
        return this;
    }

    public override string ToString()
    {
        return $"[{nameof(JobActionDefinition)} {Name}]";
    }
}
