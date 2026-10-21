using JetBrains.Annotations;
using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.SimpleStateChecking;

namespace LINGYUN.Abp.AI.Tools;

public class AIToolDefinition : IHasSimpleStateCheckers<AIToolDefinition>
{
    /// <summary>
    /// Name
    /// </summary>
    [NotNull]
    public string Name { get; }
    /// <summary>
    /// provider
    /// </summary>
    [NotNull]
    public string Provider { get; }
    /// <summary>
    /// Description
    /// </summary>
    public ILocalizableString? Description { get; set; }
    /// <summary>
    /// Properties
    /// </summary>
    [NotNull]
    public Dictionary<string, object?> Properties { get; }
    /// <summary>
    /// Enable/disable tools
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Global tools
    /// </summary>
    public bool IsGlobal { get; set; }

    public List<ISimpleStateChecker<AIToolDefinition>> StateCheckers { get; }

    public AIToolDefinition(
        [NotNull] string name,
        [NotNull] string provider, 
        ILocalizableString? description = null)
    {
        Name = name;
        Provider = provider;
        Description = description;

        IsEnabled = true;
        Properties = new Dictionary<string, object?>();
        StateCheckers = new List<ISimpleStateChecker<AIToolDefinition>>();
    }

    public virtual AIToolDefinition WithProperty(string key, object? value)
    {
        Properties[key] = value;
        return this;
    }

    public override string ToString()
    {
        return $"[{nameof(AIToolDefinition)} {Name}]";
    }
}
