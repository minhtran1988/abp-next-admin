using System;
using System.Text.RegularExpressions;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.BlobStoring.Aliyun;

public class AliyunBlobNamingNormalizer : IBlobNamingNormalizer, ITransientDependency
{
    /// <summary>
    /// Alibaba Cloud object naming convention
    /// https://help.aliyun.com/document_detail/31827.html?spm=a2c4g.11186623.6.607.37b332eaM3NKzY
    /// </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public virtual string NormalizeBlobName(string blobName)
    {
        // Cannot start with a forward slash (/) or a backslash (\).
        return blobName
            .RemovePreFix("/")
            .RemovePreFix("\\");
    }

    /// <summary>
    /// Alibaba Cloud BucketName naming convention
    /// https://help.aliyun.com/document_detail/31885.html?spm=a2c4g.11186623.6.583.56081c62w6meOR
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    public virtual string NormalizeContainerName(string containerName)
    {
        // Lowercase letters, numbers and dashes (-)
        containerName = containerName.ToLower();
        containerName = Regex.Replace(containerName, "[^a-z0-9-]", string.Empty);

        // Cannot start with a dash (-)
        containerName = Regex.Replace(containerName, "^-", string.Empty);
        // Cannot end with a dash (-)
        containerName = Regex.Replace(containerName, "-$", string.Empty);

        // The length must be between 3-63
        if (containerName.Length < 3)
        {
            var length = containerName.Length;
            for (var i = 0; i < 3 - length; i++)
            {
                containerName += "0";
            }
        }

        if (containerName.Length > 63)
        {
            containerName = containerName.Substring(0, 63);
        }

        return containerName;
    }
}
