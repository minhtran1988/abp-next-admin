using LINGYUN.Abp.BackgroundTasks;
using LINGYUN.Abp.Saas.Features;
using LINGYUN.Abp.Saas.Tenants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Emailing;
using Volo.Abp.Features;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace LINGYUN.Abp.Saas.Jobs;
public class TenantUsageMonitoringJob : IJobRunnable
{
    #region Definition Paramters

    public readonly static IReadOnlyList<JobDefinitionParamter> Paramters =
        new List<JobDefinitionParamter>
        {
            new JobDefinitionParamter(PropertyAdminEmail, LocalizableStatic.Create("Saas:AdminEmail"), required: true),
            new JobDefinitionParamter(PropertyTenantId, LocalizableStatic.Create("Saas:TenantId"), required: true),
        };

    #endregion

    public const string PropertyAdminEmail = "AdminEmail";
    public const string PropertyTenantId = "TenantId";

    public async virtual Task ExecuteAsync(JobRunnableContext context)
    {
        var clock = context.GetRequiredService<IClock>();
        var currentTenant = context.GetRequiredService<ICurrentTenant>();
        var repository = context.GetRequiredService<ITenantRepository>();
        var featureChecker = context.GetRequiredService<IFeatureChecker>();
        using (currentTenant.Change(null))
        {
            var tenantId = context.GetJobData<Guid>(PropertyTenantId);
            // TODO: The tenant has been deleted and the job needs to be canceled.
            var tenant = await repository.GetAsync(tenantId);
            if (!tenant.DisableTime.HasValue)
            {
                return;
            }
            using (currentTenant.Change(tenantId))
            {
                var allowExpirationDays = await featureChecker.GetAsync(SaasFeatureNames.Tenant.ExpirationReminderDays, 15);
                if (tenant.DisableTime <= clock.Now.AddDays(allowExpirationDays))
                {
                    var adminEmail = context.GetString(PropertyAdminEmail);
                    var emailSender = context.GetRequiredService<IEmailSender>();
                    // TODO: Need to use template to send
                    await emailSender.SendAsync(adminEmail, "Resource timeout warning", "Hello, your platform resources have reached the timeout warning time. Expired resources will be recycled and cannot be recovered. Please pay attention to extend the usage time or back up the resources in time!");
                    return;
                }

                var expiredRecoveryTime = await featureChecker.GetAsync(SaasFeatureNames.Tenant.ExpiredRecoveryTime, 15);
                if (clock.Now > tenant.DisableTime && clock.Now.Subtract(TimeSpan.FromDays(expiredRecoveryTime)) <= tenant.DisableTime)
                {
                    // The tenant has expired and the maximum grace time has been reached, delete the tenant
                    await repository.DeleteAsync(tenant);
                }
            }
        }
    }
}
