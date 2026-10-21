using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.PushPlus.Topic;
/// <summary>
/// Group interface
/// </summary>
public interface IPushPlusTopicProvider
{
    /// <summary>
    /// Get the group list
    /// </summary>
    /// <param name="current">Current page number</param>
    /// <param name="pageSize">The size of each page, the maximum value is 50</param>
    /// <param name="topicType">Group type; 0-created by me, 1-joined by me</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusPagedResponse<PushPlusTopic>> GetTopicListAsync(
        int current,
        int pageSize,
        PushPlusTopicType topicType = PushPlusTopicType.Create,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the details of the group I created
    /// </summary>
    /// <param name="topicId">Group ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusTopicProfile> GetTopicProfileAsync(
        int topicId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the details of the group I joined
    /// </summary>
    /// <param name="topicId">Group ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusTopicForMe> GetTopicForMeProfileAsync(
        int topicId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Add new group
    /// </summary>
    /// <param name="topicCode">Group coding</param>
    /// <param name="topicName">Group name</param>
    /// <param name="contact">Contact information</param>
    /// <param name="introduction">Group introduction</param>
    /// <param name="receiptMessage">Reply content after joining</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> CreateTopicAsync(
        string topicCode,
        string topicName,
        string contact,
        string introduction,
        string receiptMessage = "",
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the group QR code
    /// </summary>
    /// <param name="topicId">Group ID</param>
    /// <param name="forever">QR code type; 0-temporary QR code, 1-permanent QR code</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusTopicQrCode> GetTopicQrCodeAsync(
        int topicId,
        PushPlusTopicQrCodeType forever = PushPlusTopicQrCodeType.Temporary,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Exit the group
    /// </summary>
    /// <param name="topicId">Group ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> QuitTopicAsync(
        int topicId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get users in the group
    /// </summary>
    /// <param name="current">Current page number</param>
    /// <param name="pageSize">The size of each page, the maximum value is 50</param>
    /// <param name="topicId">Group ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PushPlusPagedResponse<PushPlusTopicUser>> GetSubscriberListAsync(
        int current,
        int pageSize,
        int topicId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete users in the group
    /// </summary>
    /// <param name="topicRelationId">User ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> UnSubscriberAsync(
        int topicRelationId,
        CancellationToken cancellationToken = default);
}
