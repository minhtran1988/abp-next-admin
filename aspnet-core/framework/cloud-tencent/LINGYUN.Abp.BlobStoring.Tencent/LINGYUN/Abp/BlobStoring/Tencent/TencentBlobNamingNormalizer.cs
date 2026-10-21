using System.Text.RegularExpressions;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.BlobStoring.Tencent;

public class TencentBlobNamingNormalizer : IBlobNamingNormalizer, ITransientDependency
{
    /// <summary>
    /// Tencent Cloud object naming convention
    /// https://cloud.tencent.com/document/product/436/13324
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public virtual string NormalizeBlobName(string blobName)
    {
        // It is not allowed to start with a forward slash / or a backslash \.
        blobName = Regex.Replace(blobName, "^/", string.Empty);
        blobName = blobName.StartsWith("\\") ? blobName.Substring(1) : blobName;

        // Object keys do not support ASCII control characters in
        // Character up (↑), character down (↓), character right (→), character left (←),
        // Correspond to CAN(24), EM(25), SUB(26), ESC(27) respectively.
        blobName = blobName.Replace("↑", "");
        blobName = blobName.Replace("↓", "");
        blobName = blobName.Replace("←", "");
        blobName = blobName.Replace("→", "");
        
        // TODO: There are so many requests... I won’t write anything else for the time being.

        return blobName;
    }

    /// <summary>
    /// Tencent Cloud BucketName naming convention
    /// https://cloud.tencent.com/document/product/436/13312
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    public virtual string NormalizeContainerName(string containerName)
    {
        // Only lowercase English letters and numbers are supported, namely [a-z, 0-9], dash "-" and their combinations.
        containerName = containerName.ToLower();
        containerName = Regex.Replace(containerName, "[^a-z0-9-]", string.Empty);

        // Cannot start with a dash (-)
        containerName = Regex.Replace(containerName, "^-", string.Empty);
        // Cannot end with a dash (-)
        containerName = Regex.Replace(containerName, "-$", string.Empty);

        // The maximum allowed characters of the bucket name are affected by the number of characters in the region abbreviation and APPID. The total number of characters in the complete requested domain name is up to 60 characters.
        // For example, the total length of the requested domain name 123456789012345678901-1250000000.cos.ap-beijing.myqcloud.com is 60 characters.
        if (containerName.Length > 60)
        {
            containerName = containerName.Substring(0, 60);
        }

        return containerName;
    }
}
