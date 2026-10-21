namespace LINGYUN.Abp.MessageService;

/// <summary>
/// Message system error code design
/// The status code is divided into two parts: the first 2 digits field and the last 3 digits status
/// 
/// <list type="table">
/// Domain part:
/// 01 input
/// 02 Group
/// 03 User
/// 04 Application
/// 05 Internal
/// 10 output
/// </list>
/// 
/// <list type="table">
/// Status part:
/// 200-299 Success
/// 300-399 Successful but there are subsequent operations
/// 400-499 Business exception
/// 500-599 Internal exception
/// 900-999 Input and output abnormality
/// </list>
/// 
/// </summary>
public class MessageServiceErrorCodes
{
    public const string Namespace = "LINGYUN.Abp.Message";
    /// <summary>
    /// Attempt to recall expired messages
    /// </summary>
    public const string ExpiredMessageCannotBeReCall = Namespace + ":01303";
    /// <summary>
    /// The message is incomplete
    /// </summary>
    public const string MessageIncomplete = Namespace + ":01400";
    /// <summary>
    /// You have not joined the group and cannot perform operations.
    /// </summary>
    public const string YouHaveNotJoinedGroup = Namespace + ":01401";
    /// <summary>
    /// Group application has been sent, waiting for administrator approval
    /// </summary>
    public const string YouHaveAddingToGroup = Namespace + ":02301";
    /// <summary>
    /// You need to verify the question before joining the group chat
    /// </summary>
    public const string YouNeedValidationQuestingByAddGroup = Namespace + ":02302";
    /// <summary>
    /// The administrator has enabled all members to be muted
    /// </summary>
    public const string GroupNotAllowedToSpeak = Namespace + ":02400";
    /// <summary>
    /// The administrator has banned the user from speaking
    /// </summary>
    public const string GroupUserHasBlack = Namespace + ":02403";
    /// <summary>
    /// Administrators do not allow anonymous comments
    /// </summary>
    public const string GroupNotAllowedToSpeakAnonymously = Namespace + ":02401";
    /// <summary>
    /// The group does not exist or has been disbanded
    /// </summary>
    public const string GroupNotFount = Namespace + ":02404";
    /// <summary>
    /// The user has rejected all messages
    /// </summary>
    public const string UserHasRejectAllMessage = Namespace + ":03400";
    /// <summary>
    /// The user has blocked the sender
    /// </summary>
    public const string UserHasBlack = Namespace + ":03401";
    /// <summary>
    /// Users are not allowed to speak anonymously
    /// </summary>
    public const string UserNotAllowedToSpeakAnonymously = Namespace + ":03402";
    /// <summary>
    /// Users do not accept comments from non-friends
    /// </summary>
    public const string UserHasRejectNotFriendMessage = Namespace + ":03403";
    /// <summary>
    /// The user receiving the message does not exist or has logged out
    /// </summary>
    public const string UseNotFount = Namespace + ":03404";
    /// <summary>
    /// The user refuses to add friends
    /// </summary>
    public const string UseRefuseToAddFriend = Namespace + ":03410";
    /// <summary>
    /// The other party is already your friend or has sent a verification request, and the operation cannot be repeated.
    /// </summary>
    public const string UseHasBeenAddedTheFriendOrSendAuthorization = Namespace + ":03411";
    /// <summary>
    /// Friend application has been sent, waiting for the other party's approval
    /// </summary>
    public const string YouHaveAddingTheUserToFriend = Namespace + ":03301";
    /// <summary>
    /// You need to verify the question before adding friends
    /// </summary>
    public const string YouNeedValidationQuestingByAddFriend = Namespace + ":03302";
}
