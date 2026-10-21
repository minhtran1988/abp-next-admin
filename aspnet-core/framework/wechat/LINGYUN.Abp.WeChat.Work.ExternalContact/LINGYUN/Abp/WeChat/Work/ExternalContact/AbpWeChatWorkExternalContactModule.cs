using LINGYUN.Abp.WeChat.Common;
using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
using LINGYUN.Abp.WeChat.Work.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact;
/// <summary>
/// Enterprise WeChat customer contact module
/// </summary>
[DependsOn(typeof(AbpWeChatWorkModule))]
public class AbpWeChatWorkExternalContactModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpWeChatWorkMessageResolveOptions>(options =>
        {
            // Enterprise customer change event
            options.MapEvent("change_external_contact", context =>
            {
                var changeType = context.GetMessageData("ChangeType");
                return changeType switch
                {
                    "add_external_contact" => context.GetWeChatMessage<ExternalContactCreateEvent>(),
                    "edit_external_contact" => context.GetWeChatMessage<ExternalContactUpdateEvent>(),
                    "add_half_external_contact" => context.GetWeChatMessage<ExternalContactCreateHalfEvent>(),
                    "del_external_contact" => context.GetWeChatMessage<ExternalContactDeleteEvent>(),
                    "del_follow_user" => context.GetWeChatMessage<ExternalContactDeleteFollowUserEvent>(),
                    "transfer_fail" => context.GetWeChatMessage<ExternalContactTransferFailEvent>(),
                    "msg_audit_approved" => context.GetWeChatMessage<ExternalContactMsgAuditApprovedEvent>(),
                    _ => throw new AbpWeChatException($"Contact change event change_external_contact:{changeType} is not mounted!"),
                };
            });
            // Customer group change event
            options.MapEvent("change_external_chat", context =>
            {
                var changeType = context.GetMessageData("ChangeType");
                switch (changeType)
                {
                    case "create": return context.GetWeChatMessage<ExternalChatCreateEvent>();
                    case "update":
                        // Customer group change event
                        var updateDetail = context.GetMessageData("UpdateDetail");
                        return updateDetail switch
                        {
                            "add_member" => context.GetWeChatMessage<ExternalChatAddMemberEvent>(),
                            "del_member" => context.GetWeChatMessage<ExternalChaDelMemberEvent>(),
                            "change_owner" => context.GetWeChatMessage<ExternalChatChangeOwnerEvent>(),
                            "change_name" => context.GetWeChatMessage<ExternalChatChangeNameEvent>(),
                            "change_notice" => context.GetWeChatMessage<ExternalChatChangeNoticeEvent>(),
                            _ => throw new AbpWeChatException($"Contact change event change_external_chat:{changeType}:{updateDetail} is not mounted!"),
                        };
                    case "dismiss": return context.GetWeChatMessage<ExternalChatDismissEvent>();
                    default: throw new AbpWeChatException($"Contact change event change_external_chat:{changeType} is not mounted!");
                }
            });
            // Enterprise customer tag event
            options.MapEvent("change_external_tag", context =>
            {
                var changeType = context.GetMessageData("ChangeType");
                return changeType switch
                {
                    "create" => context.GetWeChatMessage<ExternalTagCreateEvent>(),
                    "update" => context.GetWeChatMessage<ExternalTagUpdateEvent>(),
                    "delete" => context.GetWeChatMessage<ExternalTagDeleteEvent>(),
                    "shuffle" => context.GetWeChatMessage<ExternalTagShuffleEvent>(),
                    _ => throw new AbpWeChatException($"Contact change event change_external_tag:{changeType} is not mounted!"),
                };
            });
            // Generate session callback event
            options.MapEvent("msgaudit_notify", context => context.GetWeChatMessage<ExternalContactMsgAuditNotifyEvent>());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpWeChatWorkExternalContactModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<WeChatWorkResource>()
                .AddVirtualJson("/LINGYUN/Abp/WeChat/Work/ExternalContact/Localization/Resources");
        });
    }
}
