using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram
{
    /// <summary>
    /// WeChat applet subscription message channel data test
    /// </summary>
    /// <remarks>
    /// Unified writing method: first new channel data → serialize and write notification data through <c>IJsonSerializer</c> →
    /// Let the channel actually publish and assert the captured template message.
    /// </remarks>
    public class WeChatMiniProgramChannelData_Tests : AbpNotificationsWeChatMiniProgramTestsBase
    {
        private const string Provider = WeChatMiniProgramNotificationPublishProvider.ProviderName;

        private FakeSubscribeMessager Messager
            => (FakeSubscribeMessager)GetRequiredService<LINGYUN.Abp.WeChat.MiniProgram.Messages.ISubscribeMessager>();

        // ---------- 1. Parse channel data ----------

        [Fact]
        public void Should_Resolve_New_Prefixed_Keys()
        {
            var data = WriteChannelData(new WeChatMiniProgramSubscribeChannelData
            {
                TemplateId = "TPL_FROM_PREFIXED_KEY",
                RedirectPage = "pages/order/detail?id=123",
                WeAppState = "developer",
                WeAppLanguage = "zh_CN"
            });

            var payload = ChannelDataResolver.Resolve<WeChatMiniProgramSubscribeChannelData>(data);

            payload.ShouldNotBeNull();
            payload!.TemplateId.ShouldBe("TPL_FROM_PREFIXED_KEY");
            payload.RedirectPage.ShouldBe("pages/order/detail?id=123");
            payload.WeAppState.ShouldBe("developer");
            payload.WeAppLanguage.ShouldBe("zh_CN");
        }

        [Fact]
        public void Should_Declare_Payload_When_Only_TemplateId_Is_Written()
        {
            // The jump parameter is an "optional read item" and must not participate in the existence determination.
            var data = WriteChannelData(new WeChatMiniProgramSubscribeChannelData
            {
                TemplateId = "TPL_ONLY"
            });

            ChannelDataResolver.IsDeclared(data, typeof(WeChatMiniProgramSubscribeChannelData))
                .ShouldBeTrue();
            ChannelDataResolver.Resolve<WeChatMiniProgramSubscribeChannelData>(data)!
                .RedirectPage.ShouldBeNull();
        }

        [Fact]
        public void Should_Match_Prefixed_Key_Case_Insensitively()
        {
            var data = new NotificationData();
            data.TrySetData(WeChatMiniProgramNotificationDataKeys.TemplateId, "TPL_CASE");

            ChannelDataResolver.IsDeclared(data, typeof(WeChatMiniProgramSubscribeChannelData))
                .ShouldBeTrue();
            ChannelDataResolver.Resolve<WeChatMiniProgramSubscribeChannelData>(data)!
                .TemplateId.ShouldBe("TPL_CASE");

            // Writing in another case should also hit
            var upper = new NotificationData();
            upper.TrySetData("WX-MP:TEMPLATE_ID", "TPL_UPPER");

            ChannelDataResolver.Resolve<WeChatMiniProgramSubscribeChannelData>(upper)!
                .TemplateId.ShouldBe("TPL_UPPER");
        }

        // ---------- 2. Actual release of channel (end-to-end) ----------

        [Fact]
        public async Task Should_Send_With_Resolved_Channel_Data()
        {
            Messager.Clear();

            var data = WriteChannelData(new WeChatMiniProgramSubscribeChannelData
            {
                TemplateId = "TPL_SEND",
                RedirectPage = "pages/a/b",
                WeAppState = "developer",
                WeAppLanguage = "en_US"
            });

            await PublishAsync(data, NewUser());

            Messager.SentTemplates.Count.ShouldBe(1);

            var sent = Messager.SentTemplates[0];
            sent.TemplateId.ShouldBe("TPL_SEND");
            sent.Page.ShouldBe("pages/a/b");
            sent.State.ShouldBe("developer");
            sent.Lang.ShouldBe("en_US");
        }

        [Fact]
        public async Task Should_Fall_Back_To_Default_TemplateId_When_Nothing_Written()
        {
            Messager.Clear();

            await PublishAsync(new NotificationData(), NewUser());

            Messager.SentTemplates.Count.ShouldBe(1);
            Messager.SentTemplates[0].TemplateId.ShouldBe("DEFAULT_TEMPLATE_ID");
        }

        [Fact]
        public async Task Should_Read_Legacy_Bare_Keys_As_Fallback()
        {
            Messager.Clear();

            // Historical unprefixed keys, installed producers may still be writing
            // (Test historical compatibility paths, so explicitly suppress deprecation warnings)
            var data = new NotificationData();
#pragma warning disable CS0618 // Type or member is obsolete: this test verifies the legacy compatibility fallback
            data.TrySetData(WeChatMiniProgramNotificationDataKeys.Legacy.TemplateId, "TPL_LEGACY");
            data.TrySetData(WeChatMiniProgramNotificationDataKeys.Legacy.RedirectPage, "pages/legacy");
#pragma warning restore CS0618

            await PublishAsync(data, NewUser());

            Messager.SentTemplates.Count.ShouldBe(1);
            Messager.SentTemplates[0].TemplateId.ShouldBe("TPL_LEGACY");
            Messager.SentTemplates[0].Page.ShouldBe("pages/legacy");
        }

        [Fact]
        public async Task Should_Prefer_Prefixed_Key_Over_Legacy_Key()
        {
            Messager.Clear();

            var data = WriteChannelData(new WeChatMiniProgramSubscribeChannelData
            {
                TemplateId = "TPL_NEW"
            });
            // Write legacy keys at the same time, those with prefixes must take precedence
#pragma warning disable CS0618 // Type or member is obsolete: this test verifies the prefixed key wins over the legacy key
            data.TrySetData(WeChatMiniProgramNotificationDataKeys.Legacy.TemplateId, "TPL_LEGACY");
#pragma warning restore CS0618

            await PublishAsync(data, NewUser());

            Messager.SentTemplates[0].TemplateId.ShouldBe("TPL_NEW");
        }

        [Fact]
        public async Task Should_Not_Send_When_TemplateId_Missing_And_No_Default()
        {
            Messager.Clear();

            // Override the default template to be empty
            var options = GetRequiredService<Microsoft.Extensions.Options.IOptions<AbpNotificationsWeChatMiniProgramOptions>>().Value;
            var original = options.DefaultTemplateId;
            options.DefaultTemplateId = string.Empty;

            try
            {
                var context = await PublishAsync(new NotificationData(), NewUser());

                Messager.SentTemplates.Count.ShouldBe(0);
                context.Reason.ShouldNotBeNullOrWhiteSpace();
            }
            finally
            {
                options.DefaultTemplateId = original;
            }
        }

        // ---------- 3. Template data is taken from the property bag ----------

        [Fact]
        public async Task Should_Pass_Extra_Properties_As_Template_Data()
        {
            Messager.Clear();

            var data = WriteChannelData(new WeChatMiniProgramSubscribeChannelData
            {
                TemplateId = "TPL_DATA"
            });
            data.TrySetData("character_string1", "SO20251001001");
            data.TrySetData("thing2", "SF Express");

            await PublishAsync(data, NewUser());

            var templateData = Messager.SentTemplates[0].Data;
            templateData.ShouldNotBeNull();
            templateData!.ShouldContainKey("character_string1");
            templateData["character_string1"].ShouldBe("SO20251001001");
            templateData.ShouldContainKey("thing2");
        }

        // ---------- Auxiliary ----------

        private static UserIdentifier NewUser() => new(Guid.NewGuid(), "tester");

        private async Task<NotificationPublishContext> PublishAsync(
            NotificationData data,
            UserIdentifier user)
        {
            var provider = GetRequiredService<WeChatMiniProgramNotificationPublishProvider>();

            var notification = new NotificationInfo
            {
                Name = "Test",
                ContentType = NotificationContentType.Text,
                Data = data
            };
            notification.SetId(1);

            var context = new NotificationPublishContext(notification, new[] { user });

            await provider.PublishAsync(context);

            return context;
        }
    }
}
