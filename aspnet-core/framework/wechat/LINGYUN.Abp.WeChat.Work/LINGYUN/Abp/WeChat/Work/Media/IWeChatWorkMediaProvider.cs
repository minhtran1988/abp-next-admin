using LINGYUN.Abp.WeChat.Work.Media.Models;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace LINGYUN.Abp.WeChat.Work.Media;
/// <summary>
/// Material management interface
/// </summary>
/// <remarks>
/// API: <see href="https://developer.work.weixin.qq.com/document/path/91054"/>
/// </remarks>
public interface IWeChatWorkMediaProvider
{
    /// <summary>
    /// Upload temporary materials
    /// </summary>
    /// <remarks>
    /// API: <see href="https://developer.work.weixin.qq.com/document/path/90253"/>
    /// </remarks>
    /// <param name="type">Media file type</param>
    /// <param name="media">File to be uploaded</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkMediaResponse> UploadAsync(
        string type,
        IRemoteStreamContent media,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get temporary materials
    /// </summary>
    /// <remarks>
    /// API: <see href="https://developer.work.weixin.qq.com/document/path/90254"/>
    /// </remarks>
    /// <param name="mediaId">Media file id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// 
    Task<IRemoteStreamContent> GetAsync(
        string mediaId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Upload pictures
    /// </summary>
    /// <remarks>
    /// API: <see href="https://developer.work.weixin.qq.com/document/path/90256"/>
    /// </remarks>
    /// <param name="image">Image to be uploaded</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkImageResponse> UploadImageAsync(
        IRemoteStreamContent image,
        CancellationToken cancellationToken = default);
}
