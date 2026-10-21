namespace LINGYUN.Abp.BlobManagement.Features;

public static class BlobManagementFeatureNames
{
    public const string GroupName = "AbpBlobManagement";
    /// <summary>
    /// Enable object storage management
    /// </summary>
    public const string Enable = GroupName + ".Enable";
    /// <summary>
    /// Whether to allow unauthorized users to access public directories
    /// </summary>
    public const string PublicAccess = GroupName + ".PublicAccess";

    public class Blob
    {
        public const string Default = GroupName + ".Blob";
        /// <summary>
        /// Enable Blob
        /// </summary>
        public const string Enable = Default + ".Enable";
        /// <summary>
        /// Generate temporary file download link
        /// </summary>
        public const string GenerateDownloadUrl = Default + ".GenerateDownloadUrl";
        /// <summary>
        /// Download file function
        /// </summary>
        public const string DownloadFile = Default + ".DownloadFile";
        /// <summary>
        /// Limit the number of download file functions
        /// </summary>
        public const string DownloadLimit = Default + ".DownloadLimit";
        /// <summary>
        /// Download file function limit number of cycles
        /// </summary>
        public const string DownloadInterval = Default + ".DownloadInterval";
        /// <summary>
        /// Upload file function
        /// </summary>
        public const string UploadFile = Default + ".UploadFile";
        /// <summary>
        /// Limit the number of times to upload files
        /// </summary>
        public const string UploadLimit = Default + ".UploadLimit";
        /// <summary>
        /// Upload file function limit number of cycles
        /// </summary>
        public const string UploadInterval = Default + ".UploadInterval";
        /// <summary>
        /// Maximum upload file
        /// </summary>
        public const string MaxUploadFileCount = Default + ".MaxUploadFileCount";
    }
}
