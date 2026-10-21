using System;
using Volo.Abp;

namespace LINGYUN.Abp.Features.LimitValidation;

public class RequiresLimitFeatureContext
{
    /// <summary>
    /// Function restriction strategy
    /// </summary>
    public LimitPolicy Policy { get; }
    /// <summary>
    /// Limit duration
    /// </summary>
    public int Interval { get; }
    /// <summary>
    /// Function limit times
    /// </summary>
    public int Limit { get; }
    /// <summary>
    /// Function limit number name
    /// </summary>
    public string LimitFeature { get; }

    public AbpFeaturesLimitValidationOptions Options { get; }
    public RequiresLimitFeatureContext(
        string limitFeature,
        AbpFeaturesLimitValidationOptions options,
        LimitPolicy policy = LimitPolicy.Month,
        int interval = 1,
        int limit = 1)
    {
        Policy = policy;
        Options = options;
        LimitFeature = limitFeature;
        Limit = Check.Positive(limit, nameof(limit));
        Interval = Check.Positive(interval, nameof(interval));
    }

    /// <summary>
    /// Get the effective time span, unit: s
    /// </summary>
    /// <returns></returns>
    public long GetEffectTicks(DateTime now)
    {
        return Options.EffectPolicys[Policy](now, Interval);
    }
}
