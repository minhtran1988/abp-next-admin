namespace LINGYUN.Platform;

public static class PlatformErrorCodes
{
    private const string Namespace = "Platform";

    public const string VersionFileNotFound = Namespace + ":01404";
    /// <summary>
    /// Package version cannot be downgraded
    /// </summary>
    public const string PackageVersionDegraded = Namespace + ":01403";
    /// <summary>
    /// The same level menu already exists
    /// </summary>
    public const string DuplicateMenu = Namespace + ":02001";
    /// <summary>
    /// Nodes with submenus cannot be deleted
    /// </summary>
    public const string DeleteMenuHaveChildren = Namespace + ":02002";
    /// <summary>
    /// The menu level has reached the maximum value
    /// </summary>
    public const string MenuAchieveMaxDepth = Namespace + ":02003";
    /// <summary>
    /// The current menu cannot be the same as the previous menu
    /// </summary>
    public const string CannotSetSelfParentMenu = Namespace + ":02004";
    /// <summary>
    /// Menu metadata is missing necessary elements
    /// </summary>
    public const string MenuMissingMetadata = Namespace + ":02101";
    /// <summary>
    /// Metadata format does not match
    /// </summary>
    public const string MetaFormatMissMatch = Namespace + ":03001";
    /// <summary>
    /// User repeats favorite menu
    /// </summary>
    public const string UserDuplicateFavoriteMenu = Namespace + ":04400";
    /// <summary>
    /// User favorites menu not found
    /// </summary>
    public const string UserFavoriteMenuNotFound = Namespace + ":04404";
    /// <summary>
    /// Unable to comment on questions in {Status}
    /// </summary>
    public const string UnableFeedbackCommentInStatus = Namespace + ":05101";
    /// <summary>
    /// Cannot add duplicate attachment {Name}!
    /// </summary>
    public const string DuplicateFeedbackAttachment = Namespace + ":05102";
    /// <summary>
    /// User feedback that the attachment named {Name} was not found!
    /// </summary>
    public const string FeedackAttachmentNotFound = Namespace + ":05103";
    /// <summary>
    /// Attachment {Name} has expired, please upload again!
    /// </summary>
    public const string FeedackAttachmentLoseEffectiveness = Namespace + ":05104";
    /// <summary>
    /// Cannot add duplicate header {Key}!
    /// </summary>
    public const string DuplicateEmailMessageHeader = Namespace + ":06101";
    /// <summary>
    /// Cannot add duplicate attachment {Name}!
    /// </summary>
    public const string DuplicateEmailMessageAttachment = Namespace + ":06102";
    /// <summary>
    /// Enterprise information named {Name} already exists!
    /// </summary>
    public const string DuplicateEnterpriseName = Namespace + ":07101";
}
