using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;

namespace LINGYUN.Abp.Features.LimitValidation;

public class AbpFeaturesLimitValidationOptions
{
    public IDictionary<LimitPolicy, Func<DateTime, int, long>> EffectPolicys { get; }

    public AbpFeaturesLimitValidationOptions()
    {
        EffectPolicys = new Dictionary<LimitPolicy, Func<DateTime, int, long>>();
    }
    /// <summary>
    /// Change function restriction policy duration calculation method
    /// </summary>
    /// <param name="policy">Restriction policy</param>
    /// <param name="func">Customized calculation method</param>
    /// <remarks>
    /// The return value must be seconds scale
    /// </remarks>
    public void MapEffectPolicy(LimitPolicy policy,[NotNull] Func<DateTime, int, long> func)
    {
        Check.NotNull(func, nameof(func));

        if (EffectPolicys.ContainsKey(policy))
        {
            EffectPolicys[policy] = func;
        }
        else
        {
            EffectPolicys.Add(policy, func);
        }
    }

    internal void MapDefaultEffectPolicys()
    {
        MapEffectPolicy(LimitPolicy.Minute, (now, tick) =>
        {
            return (long)(now.AddMinutes(tick) - now).TotalSeconds;
        });

        MapEffectPolicy(LimitPolicy.Hours, (now, tick) =>
        {
            return (long)(now.AddHours(tick) - now).TotalSeconds;
        });

        MapEffectPolicy(LimitPolicy.Days, (now, tick) =>
        {
            // If calculated by day, the current day should be taken
            return (long)(now.Date.AddDays(tick) - now).TotalSeconds;
        });

        MapEffectPolicy(LimitPolicy.Weeks,(now, tick) =>
        {
            // If calculated by week, the current week should be taken
            var nowDate = now.Date;
            var dayOfWeek = (int)nowDate.DayOfWeek - 1;
            if (nowDate.DayOfWeek == DayOfWeek.Sunday)
            {
                dayOfWeek = 6;
            }
            var utcOnceDayOfWeek = nowDate.AddDays(-dayOfWeek);

            return (long)(utcOnceDayOfWeek.AddDays(tick * 7) - now).TotalSeconds;
        });

        MapEffectPolicy(LimitPolicy.Month, (now, tick) =>
        {
            // If calculated on a monthly basis, the current month should be taken
            var utcOnceDayOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, 0);

            return (long)(utcOnceDayOfMonth.AddMonths(tick) - now).TotalSeconds; 
        });

        MapEffectPolicy(LimitPolicy.Years, (now, tick) => 
        {
            // Calculation by year should take the current year
            var utcOnceDayOfYear = new DateTime(now.Year, 1, 1, 0, 0, 0, 0);

            return (long)(utcOnceDayOfYear.AddYears(tick) - now).TotalSeconds; 
        });
    }
}
