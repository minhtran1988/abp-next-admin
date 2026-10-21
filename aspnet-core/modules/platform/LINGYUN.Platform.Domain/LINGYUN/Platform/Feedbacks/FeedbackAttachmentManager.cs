using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Services;
using Volo.Abp.IO;

namespace LINGYUN.Platform.Feedbacks;
public class FeedbackAttachmentManager : DomainService
{
    private const string DefaultSaveToApplication = "abp-application";
    private const string DefaultSaveToTempPath = "feedback-upload-tmp";

    private readonly IBlobContainer<FeedbackContainer> _blobContainer;
    private readonly IApplicationInfoAccessor _applicationInfoAccessor;

    public FeedbackAttachmentManager(
        IBlobContainer<FeedbackContainer> blobContainer,
        IApplicationInfoAccessor applicationInfoAccessor)
    {
        _blobContainer = blobContainer;
        _applicationInfoAccessor = applicationInfoAccessor;
    }

    /// <summary>
    /// Write the file stream to temporary storage
    /// </summary>
    /// <param name="stream">File stream</param>
    /// <returns>Returns the temporarily stored file ID</returns>
    public async Task<FeedbackAttachmentTempFile> SaveToTempAsync(Stream stream)
    {
        var blobSize = stream.Length;
        var blobId = GuidGenerator.Create().ToString("N");
        var timeStampPath = Clock.Now.ToString("yyyy-MM-dd");

        var tempFilePath = Path.Combine(
            Path.GetTempPath(),
            _applicationInfoAccessor.ApplicationName ?? DefaultSaveToApplication,
            DefaultSaveToTempPath,
            timeStampPath);
        var tempSavedFile = Path.Combine(tempFilePath, $"{blobId}.png");

        DirectoryHelper.CreateIfNotExists(tempFilePath);

        using (var fs = new FileStream(tempSavedFile, FileMode.Create, FileAccess.Write))
        {
            await stream.CopyToAsync(fs);
        }

        return new FeedbackAttachmentTempFile
        {
            Path = timeStampPath,
            Size = blobSize,
            Id = blobId,
        };
    }
    /// <summary>
    /// Copy attachments from temporary storage to blob storage
    /// </summary>
    /// <param name="feedback">User feedback entity</param>
    /// <param name="file">Attachment temporary storage file</param>
    /// <returns>Saved attachment</returns>
    public async Task<FeedbackAttachmentFile> CopyFromTempAsync(Feedback feedback, string filePtah, string fileId)
    {
        var fileName = $"{filePtah}/{fileId}.png";
        var tempFilePath = Path.Combine(
            Path.GetTempPath(),
            _applicationInfoAccessor.ApplicationName ?? DefaultSaveToApplication,
            DefaultSaveToTempPath);
        var tempFromFile = Path.Combine(tempFilePath, fileName);
        var blobFile = $"{fileId}.png";
        var saveToBlobFile = $"{feedback.Id}/{blobFile}";

        if (!File.Exists(tempFromFile))
        {
            throw new BusinessException(PlatformErrorCodes.FeedackAttachmentLoseEffectiveness)
                .WithData("Name", fileId);
        }
        FeedbackAttachmentFile attachmentFile;

        using (var fs = new FileStream(tempFromFile, FileMode.Open, FileAccess.Read))
        {
            await _blobContainer.SaveAsync(saveToBlobFile, fs, true);

            attachmentFile = new FeedbackAttachmentFile(blobFile, fs.Length);
        }

        FileHelper.DeleteIfExists(tempFromFile);

        return attachmentFile;
    }
    /// <summary>
    /// Download attachments
    /// </summary>
    /// <param name="attachment">Attachment entity</param>
    /// <returns>Attachment file stream</returns>
    public async Task<Stream> DownloadAsync(FeedbackAttachment attachment)
    {
        var blobFile = $"{attachment.FeedbackId}/{attachment.Name}";

        return await _blobContainer.GetAsync(blobFile);
    }
    /// <summary>
    /// Delete attachment
    /// </summary>
    /// <param name="attachment">Attachment entity</param>
    /// <returns>Attachment file stream</returns>
    public async Task DeleteAsync(FeedbackAttachment attachment)
    {
        var blobFile = $"{attachment.FeedbackId}/{attachment.Name}";

        await _blobContainer.DeleteAsync(blobFile);
    }
}
