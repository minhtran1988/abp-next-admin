using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace LINGYUN.Abp.Features.LimitValidation;

/// <summary>
/// Limit on the number of calls to a single function
/// </summary>
/// <remarks>
/// It is necessary to distinguish between the limit duration and limit upper limit functions to facilitate more fine-grained restrictions.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class RequiresLimitFeatureAttribute : Attribute
{
    /// <summary>
    /// Function restriction strategy
    /// </summary>
    public LimitPolicy Policy { get; }
    /// <summary>
    /// Default limit duration
    /// </summary>
    public int DefaultLimit { get; }
    /// <summary>
    /// Limit upper limit name
    /// </summary>
    public string LimitFeature { get; }
    /// <summary>
    /// Default limit duration
    /// </summary>
    public int DefaultInterval { get; }
    /// <summary>
    /// Limit duration name
    /// </summary>
    public string IntervalFeature { get; }

    public RequiresLimitFeatureAttribute(
        [NotNull] string limitFeature,
        [NotNull] string intervalFeature,
        LimitPolicy policy = LimitPolicy.Month,
        int defaultLimit = 1,
        int defaultInterval = 1)
    {
        Check.NotNullOrWhiteSpace(limitFeature, nameof(limitFeature));
        Check.NotNullOrWhiteSpace(intervalFeature, nameof(intervalFeature));

        Policy = policy;
        LimitFeature = limitFeature;
        DefaultLimit = defaultLimit;
        IntervalFeature = intervalFeature;
        DefaultInterval = defaultInterval;
    }
}
