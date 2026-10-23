using LINGYUN.Abp.AuditLogging.OpenObserve.Localization;
using Microsoft.Extensions.Localization;
using Shouldly;
using Volo.Abp.Localization;
using Xunit;

namespace LINGYUN.Abp.AuditLogging.OpenObserve;

public class AuditLoggingOpenObserveLocalization_Tests : AbpAuditLoggingOpenObserveTestBase
{
    private readonly IStringLocalizer<AuditLoggingOpenObserveResource> _localizer;

    public AuditLoggingOpenObserveLocalization_Tests()
    {
        _localizer = GetRequiredService<IStringLocalizer<AuditLoggingOpenObserveResource>>();
    }

    [Fact]
    public void Should_Localize_Delete_Not_Supported_Message()
    {
        using (CultureHelper.Use("zh-Hans"))
        {
            _localizer[AuditLoggingOpenObserveErrorCodes.DeleteNotSupported]
                .Value
                .ShouldBe("\u5f53\u524d\u65e5\u5fd7\u8bbe\u65bd\u4e0d\u652f\u6301\u5220\u9664\u8bb0\u5f55!");
        }
    }
}
