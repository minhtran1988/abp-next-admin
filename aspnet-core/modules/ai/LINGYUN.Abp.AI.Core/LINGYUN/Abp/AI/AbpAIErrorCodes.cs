namespace LINGYUN.Abp.AI;
public static class AbpAIErrorCodes
{
    public const string Namespace = "Abp.AI";
    /// <summary>
    /// Workspace is not available: {Workspace}!
    /// </summary>
    public const string WorkspaceIsNotEnabled = Namespace + ":110001";
    /// <summary>
    /// ApiKey is missing or invalid!
    /// </summary>
    public const string MissingOrInvalidApiKey = Namespace + ":110002";
    /// <summary>
    /// The conversation has expired, please recreate the conversation!
    /// </summary>
    public const string ConversationHasExpired = Namespace + ":110101";
}
