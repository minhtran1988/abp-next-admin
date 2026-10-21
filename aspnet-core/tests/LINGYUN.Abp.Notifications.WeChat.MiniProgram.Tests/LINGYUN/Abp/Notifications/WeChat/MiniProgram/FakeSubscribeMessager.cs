using LINGYUN.Abp.WeChat.MiniProgram.Messages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// This test project does not enable nullable context globally, and this file needs to express "nullable parameter" semantics
#nullable enable

namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram;

/// <summary>
/// Capture <see cref="ISubscribeMessager"/> fake implementation
/// </summary>
/// <remarks>
/// Used to assert the content of the template message eventually sent by the channel without actually calling the WeChat interface.
/// </remarks>
public class FakeSubscribeMessager : ISubscribeMessager
{
    /// <summary>
    /// Template messages sent (in order)
    /// </summary>
    public List<SubscribeMessage> SentMessages { get; } = new();

    /// <summary>
    /// Sent "by template parameter" form of call
    /// </summary>
    public List<SentTemplateArgs> SentTemplates { get; } = new();

    public Task SendAsync(SubscribeMessage message, CancellationToken cancellation = default)
    {
        SentMessages.Add(message);
        return Task.CompletedTask;
    }

    public Task SendAsync(
        Guid toUser,
        string templateId,
        string? page = "",
        string? lang = "zh_CN",
        string? state = "formal",
        Dictionary<string, object?>? data = null,
        CancellationToken cancellation = default)
    {
        SentTemplates.Add(new SentTemplateArgs(toUser, templateId, page, lang, state, data));
        return Task.CompletedTask;
    }

    public void Clear()
    {
        SentMessages.Clear();
        SentTemplates.Clear();
    }

    public record SentTemplateArgs(
        Guid ToUser,
        string TemplateId,
        string? Page,
        string? Lang,
        string? State,
        Dictionary<string, object?>? Data);
}
