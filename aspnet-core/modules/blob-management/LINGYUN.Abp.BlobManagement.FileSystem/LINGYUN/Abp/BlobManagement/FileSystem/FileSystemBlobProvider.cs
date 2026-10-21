using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.IO;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.BlobManagement.FileSystem;

public class FileSystemBlobProvider : IBlobProvider
{
    public const string ProviderName = "FileSystem";
    public string Name => ProviderName;

    protected ICurrentTenant CurrentTenant { get; }
    protected FileSystemBlobNamingNormalizer BlobNamingNormalizer { get; }
    protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }

    public FileSystemBlobProvider(
        ICurrentTenant currentTenant,
        FileSystemBlobNamingNormalizer blobNamingNormalizer,
        IBlobContainerConfigurationProvider configurationProvider)
    {
        CurrentTenant = currentTenant;
        BlobNamingNormalizer = blobNamingNormalizer;
        ConfigurationProvider = configurationProvider;
    }

    public virtual Task CreateContainerAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var filePath = CalculateBlobName(NormalizeContainerName(name));

        DirectoryHelper.CreateIfNotExists(filePath);

        return Task.CompletedTask;
    }

    public virtual Task DeleteContainerAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var filePath = CalculateBlobName(NormalizeContainerName(name));

        DirectoryHelper.DeleteIfExists(filePath);

        return Task.CompletedTask;
    }

    public virtual Task DeleteBlobAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        var fileName = CalculateBlobName(NormalizeContainerName(containerName.RemovePostFix("/")), blobName);

        FileHelper.DeleteIfExists(fileName);

        return Task.CompletedTask;
    }

    public virtual Task DeleteFolderAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        var filePath = CalculateBlobName(NormalizeContainerName(containerName.RemovePostFix("/")), blobName);

        var children = Directory.GetFileSystemEntries(filePath);
        if (children.Length > 0)
        {
            throw new BusinessException(
                BlobManagementErrorCodes.Blob.DeleteWithNotEmpty,
                "The current directory is not empty and cannot be deleted!");
        }

        DirectoryHelper.DeleteIfExists(filePath);

        return Task.CompletedTask;
    }

    public virtual Task<Stream?> DownloadBlobAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        var fileName = CalculateBlobName(NormalizeContainerName(containerName.RemovePostFix("/")), blobName);

        if (!File.Exists(fileName))
        {
            return Task.FromResult<Stream?>(null);
        }

        return Task.FromResult<Stream?>(File.OpenRead(fileName));
    }

    public virtual Task<string?> GeneratePresignedUrlAsync(
        string containerName,
        string blobName,
        TimeSpan expiration,
        bool isAttachmentContent = true,
        CancellationToken cancellationToken = default)
    {
        // Local file system does not need to generate download links
        return Task.FromResult<string?>(null);
    }

    public virtual Task CreateFolderAsync(
        string containerName, 
        string blobName,
        CancellationToken cancellationToken = default)
    {
        var filePath = CalculateBlobName(NormalizeContainerName(containerName.RemovePostFix("/")), blobName);

        DirectoryHelper.CreateIfNotExists(filePath);

        return Task.CompletedTask;
    }

    public async virtual Task UploadBlobAsync(
        string containerName, 
        string blobName, 
        Stream content,
        string? contentType = null,
        CancellationToken cancellationToken = default)
    {
        var fileName = CalculateBlobName(NormalizeContainerName(containerName.RemovePostFix("/")), blobName);

        DirectoryHelper.CreateIfNotExists(Path.GetDirectoryName(fileName)!);

        using var fileStream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite);

        await content.CopyToAsync(fileStream, cancellationToken);

        await fileStream.FlushAsync(cancellationToken);
    }

    protected virtual FileSystemBlobProviderConfiguration GetBlobConfiguration()
    {
        var configuration = ConfigurationProvider.Get<BlobManagementContainer>();
        var blobConfiguration = configuration.GetFileSystemConfiguration();
        return blobConfiguration;
    }

    protected virtual string NormalizeContainerName(string containerName)
    {
        if (!containerName.StartsWith("abp-"))
        {
            containerName = "abp-" + containerName;
        }
        return BlobNamingNormalizer.NormalizeContainerName(containerName);
    }

    /// <summary>
    /// Path comparison method
    /// </summary>
    /// <remarks>
    /// Windows and macOS (APFS/HFS+ default) are case-insensitive, while Linux and other Unix-like file systems are case-sensitive:
    /// When using case-ignoring comparison on a case-sensitive file system, sibling directories with case variations will be ignored.
    /// </remarks>
    protected static StringComparison PathComparison =>
        OperatingSystem.IsWindows() || OperatingSystem.IsMacOS()
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;

    /// <summary>
    /// Characters not allowed in names
    /// </summary>
    /// <remarks>
    /// Explicitly use the union of illegal characters on each platform (instead of <see cref="Path.GetInvalidFileNameChars"/>):
    /// Unix-like file names allow characters such as ':', but the same data may be used by Windows hosts, SMB shares,
    /// Or other blob providers for reuse, so they are rejected according to portability rules.
    /// </remarks>
    protected static readonly char[] InvalidNameCharacters =
    [
        '\0', '/', '\\', ':', '*', '?', '"', '<', '>', '|',
    ];

    protected virtual string CalculateBlobName(string bucketName, string blobName = "")
    {
        var blobConfiguration = GetBlobConfiguration();
        var basePath = Path.GetFullPath(blobConfiguration.BasePath);

        // The storage root directory of the current tenant: host is host, tenant is tenants/{tenantId}
        string tenantPath;
        if (CurrentTenant.Id == null)
        {
            tenantPath = Path.Combine(basePath, "host");
        }
        else
        {
            tenantPath = Path.Combine(basePath, "tenants", CurrentTenant.Id.Value.ToString("D"));
        }
        DirectoryHelper.CreateIfNotExists(tenantPath);

        var blobPath = tenantPath;

        if (blobConfiguration.AppendContainerNameToBasePath &&
            !bucketName.IsNullOrWhiteSpace())
        {
            CheckContainerName(bucketName);

            blobPath = Path.Combine(blobPath, bucketName);
        }
        if (!blobName.IsNullOrWhiteSpace())
        {
            // The full name of the module storage may start with / (such as /abp-container/folder/file.txt)
            var name = blobName.RemovePreFix("/");
            CheckBlobName(name);

            blobPath = Path.Combine(blobPath, name);
        }

        // Key: After parsing the .. / absolute path / delimiter variant, the result must still be within the current tenant directory
        // (Path.GetFullPath will eliminate .., so absolute path injection will also be intercepted)
        var fullPath = Path.GetFullPath(blobPath);
        var tenantRoot = tenantPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            + Path.DirectorySeparatorChar;

        if (!fullPath.StartsWith(tenantRoot, PathComparison))
        {
            throw BlobManagementNameValidator.CreateInvalidNameException(
                blobName,
                BlobManagementErrorCodes.Blob.InvalidName);
        }

        return fullPath;
    }

    /// <summary>
    /// Verify container name
    /// </summary>
    /// <remarks>
    /// The container name must be a single directory name: names containing delimiters will not escape the tenant directory (containment cannot intercept it),
    /// But it will be resolved to other container directories (container aliases) in the same tenant, thus bypassing container isolation.
    /// </remarks>
    protected virtual void CheckContainerName(string containerName)
    {
        if (containerName.IsNullOrWhiteSpace())
        {
            return;
        }

        BlobManagementNameValidator.CheckName(containerName, BlobManagementErrorCodes.Container.InvalidName);
    }

    /// <summary>
    /// Verify file/directory name (path)
    /// </summary>
    /// <remarks>
    /// The rules are consistent with the entity; this is just defense in depth, the real security boundary is the containment check of <see cref="CalculateBlobName"/>
    /// (Name validation does not cover symbolic links/join points).
    /// </remarks>
    protected virtual void CheckBlobName(string blobName)
    {
        if (blobName.IsNullOrWhiteSpace())
        {
            return;
        }

        BlobManagementNameValidator.CheckPath(blobName, BlobManagementErrorCodes.Blob.InvalidName);
    }
}
