namespace LINGYUN.Abp.Notifications;
public static class NotificationDefinitionWeChatWorkExtensions
{
    private const string Prefix = "wx-work:";
    private const string AgentIdKey = Prefix + "agent_id";
    private const string ToTagKey = Prefix + "to_tag";
    private const string ToPartyKey = Prefix + "to_party";

    /// <summary>
    /// Set to send to all applications
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    public static NotificationDefinition WithAllAgent(
        this NotificationDefinition notification)
    {
        return notification.WithAgentId("@all");
    }

    /// <summary>
    /// Set message application ID
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="agentId"></param>
    /// <returns></returns>
    public static NotificationDefinition WithAgentId(
        this NotificationDefinition notification,
        string agentId)
    {
        return notification.WithProperty(AgentIdKey, agentId);
    }

    /// <summary>
    /// Get the message application ID
    /// </summary>
    /// <param name="notification"></param>
    public static string? GetAgentIdOrNull(
        this NotificationDefinition notification)
    {
        if (notification.Properties.TryGetValue(AgentIdKey, out var agentIdDefine))
        {
            return agentIdDefine?.ToString();
        }

        return null;
    }

    /// <summary>
    /// Specify the tag to receive the message, tag ID list, multiple receivers separated by '|', up to 100 are supported.
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static NotificationDefinition WithTag(
        this NotificationDefinition notification,
        string tag)
    {
        return notification.WithProperty(ToTagKey, tag);
    }

    /// <summary>
    /// Get the label of the received message
    /// </summary>
    /// <param name="notification"></param>
    public static string? GetTagOrNull(
        this NotificationDefinition notification)
    {
        if (notification.Properties.TryGetValue(ToTagKey, out var tagDefine))
        {
            return tagDefine?.ToString();
        }

        return null;
    }

    /// <summary>
    /// Specify the department to receive the message, department ID list, multiple recipients separated by '|', up to 100 are supported.
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="party"></param>
    /// <returns></returns>
    public static NotificationDefinition WithParty(
        this NotificationDefinition notification,
        string party)
    {
        return notification.WithProperty(ToPartyKey, party);
    }

    /// <summary>
    /// Get the department that received the message
    /// </summary>
    /// <param name="notification"></param>
    public static string? GetPartyOrNull(
        this NotificationDefinition notification)
    {
        if (notification.Properties.TryGetValue(ToPartyKey, out var partyDefine))
        {
            return partyDefine?.ToString();
        }

        return null;
    }
}
