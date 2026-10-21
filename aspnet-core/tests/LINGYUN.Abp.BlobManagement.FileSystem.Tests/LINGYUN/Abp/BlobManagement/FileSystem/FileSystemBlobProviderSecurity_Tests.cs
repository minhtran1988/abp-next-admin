using Shouldly;
using System;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace LINGYUN.Abp.BlobManagement.FileSystem;

/// <summary>
/// FileSystem provider path security
/// </summary>
/// <remarks>
/// Override directory traversal, absolute path injection, NTFS alternate data stream, Windows normalization collision, preserved device name,
/// Container name traversal, container aliasing, and multi-tenant override; at the same time, confirm that legal names are not affected.
/// The provider is registered by the production module according to BlobManagement:Provider=FileSystem, and the test is resolved from DI
/// </remarks>
public class FileSystemBlobProviderSecurity_Tests : AbpBlobManagementFileSystemTestBase
{
    private const string ContainerName = "abp-c";

    private static string BasePath => AbpBlobManagementFileSystemTestsModule.BasePath;

    private static string OutsidePath => AbpBlobManagementFileSystemTestsModule.OutsidePath;

    private IBlobProvider BlobProvider => GetRequiredService<IBlobProvider>();

    private ICurrentTenant CurrentTenant => GetRequiredService<ICurrentTenant>();

    [Theory]
    [InlineData("..\\..\\..\\outside\\pwn.txt")]
    [InlineData("../../outside/pwn.txt")]
    [InlineData("..\\..\\..\\..\\..\\outside/pwn.txt")]
    [InlineData("folder/../../outside/pwn.txt")]
    [InlineData("folder\\..\\..\\..\\outside\\pwn.txt")]
    // Unix-like systems must also be rejected: delimiters and illegal characters are judged by cross-platform union
    [InlineData("..\\..\\..\\etc\\passwd")]
    [InlineData("C:\\Windows\\Temp\\pwn.txt")]
    [InlineData("..")]
    [InlineData("folder/..")]
    // NTFS alternate data stream / Windows illegal characters
    [InlineData("pwn.txt:evil")]
    [InlineData("pwn*star.txt")]
    [InlineData("pwn?question.txt")]
    [InlineData("pwn\"quote.txt")]
    [InlineData("pwn|pipe.txt")]
    [InlineData("pwn<less.txt")]
    [InlineData("pwn>greater.txt")]
    // Windows normalized collision and reserved device name
    [InlineData("victim.txt.")]
    [InlineData("victim.txt ")]
    [InlineData("NUL")]
    [InlineData("con.txt")]
    [InlineData("COM1")]
    [InlineData("lpt9.log")]
    [InlineData("\\\\127.0.0.1\\c$\\pwn.txt")]
    public async Task Should_Reject_Unsafe_Blob_Names_On_Upload(string blobName)
    {
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.UploadBlobAsync(ContainerName, blobName, new MemoryStream(new byte[] { 1, 2, 3 })));

        exception.Code.ShouldBe(BlobManagementErrorCodes.Blob.InvalidName);
        Directory.GetFileSystemEntries(OutsidePath).ShouldBeEmpty();
    }

    [Fact]
    public async Task Should_Reject_Absolute_Path_On_Upload()
    {
        var absolute = Path.Combine(OutsidePath, "pwn-absolute.txt");

        var exception = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.UploadBlobAsync(ContainerName, absolute, new MemoryStream(new byte[] { 1 })));

        exception.Code.ShouldBe(BlobManagementErrorCodes.Blob.InvalidName);
        File.Exists(absolute).ShouldBeFalse();
    }

    [Fact]
    public async Task Should_Reject_Traversal_On_Download()
    {
        var secret = Path.Combine(OutsidePath, "secret.txt");
        await File.WriteAllTextAsync(secret, "TOP-SECRET");

        var exception = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.DownloadBlobAsync(ContainerName, "..\\..\\..\\outside\\secret.txt"));

        exception.Code.ShouldBe(BlobManagementErrorCodes.Blob.InvalidName);
    }

    [Fact]
    public async Task Should_Reject_Traversal_On_Delete()
    {
        var canary = Path.Combine(OutsidePath, "canary.txt");
        await File.WriteAllTextAsync(canary, "delete-me");

        var exception = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.DeleteBlobAsync(ContainerName, "..\\..\\..\\outside\\canary.txt"));

        exception.Code.ShouldBe(BlobManagementErrorCodes.Blob.InvalidName);
        File.Exists(canary).ShouldBeTrue();
    }

    [Fact]
    public async Task Should_Reject_Traversal_On_Folder_Operations()
    {
        var emptyFolder = Path.Combine(OutsidePath, "empty-folder");
        Directory.CreateDirectory(emptyFolder);

        var createException = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.CreateFolderAsync(ContainerName, "..\\..\\..\\outside\\new-folder"));
        createException.Code.ShouldBe(BlobManagementErrorCodes.Blob.InvalidName);

        var deleteException = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.DeleteFolderAsync(ContainerName, "..\\..\\..\\outside\\empty-folder"));
        deleteException.Code.ShouldBe(BlobManagementErrorCodes.Blob.InvalidName);

        Directory.Exists(emptyFolder).ShouldBeTrue();
    }

    [Theory]
    // Escape: Container name with .. trying to leave BasePath
    [InlineData("abp-c/../..")]
    [InlineData("..\\..\\evil")]
    [InlineData("../../evil")]
    // Container alias: The result is still in BasePath and will fall into other container directories
    [InlineData("x/../abp-c")]
    [InlineData("x\\..\\abp-c")]
    [InlineData("users/../../abp-users")]
    // Windows normalized collision
    [InlineData("abp-c.")]
    [InlineData("abp-c ")]
    [InlineData(".")]
    [InlineData("..")]
    public async Task Should_Reject_Unsafe_Container_Names(string containerName)
    {
        var exception = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.CreateContainerAsync(containerName));

        exception.Code.ShouldBe(BlobManagementErrorCodes.Container.InvalidName);
    }

    [Fact]
    public async Task Should_Not_Allow_Container_Alias_To_Reach_Another_Container()
    {
        await BlobProvider.UploadBlobAsync(ContainerName, "original.txt", new MemoryStream(new byte[] { 1 }));
        var original = Path.Combine(BasePath, "host", ContainerName, "original.txt");
        File.Exists(original).ShouldBeTrue();

        var uploadException = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.UploadBlobAsync("x/../abp-c", "aliased.txt", new MemoryStream(new byte[] { 2 })));
        uploadException.Code.ShouldBe(BlobManagementErrorCodes.Container.InvalidName);
        File.Exists(Path.Combine(BasePath, "host", ContainerName, "aliased.txt")).ShouldBeFalse();

        var downloadException = await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.DownloadBlobAsync("x/../abp-c", "original.txt"));
        downloadException.Code.ShouldBe(BlobManagementErrorCodes.Container.InvalidName);
    }

    [Theory]
    [InlineData("abp-c:evil", "abp-cevil")]
    [InlineData("abp-c*star", "abp-cstar")]
    [InlineData("abp-c?question", "abp-cquestion")]
    public async Task Should_Neutralize_Invalid_Characters_Of_Container_Names(string containerName, string expectedDirectoryName)
    {
        // ABP's FileSystemBlobNamingNormalizer will strip illegal characters
        await BlobProvider.CreateContainerAsync(containerName);

        Directory.Exists(Path.Combine(BasePath, "host", expectedDirectoryName)).ShouldBeTrue();
    }

    [Theory]
    [InlineData("public")]
    [InlineData("users")]
    [InlineData("my-container")]
    [InlineData("Object Storage")]
    public async Task Should_Allow_Legitimate_Container_Names(string containerName)
    {
        await BlobProvider.CreateContainerAsync(containerName);

        Directory.Exists(Path.Combine(BasePath, "host", "abp-" + containerName)).ShouldBeTrue();
    }

    [Theory]
    [InlineData("file.txt")]
    [InlineData("folder/file.txt")]
    [InlineData("folder/sub/file.txt")]
    [InlineData("Directory/Subdirectory/File.txt")]
    [InlineData("/folder/leading-slash.txt")]
    [InlineData("folder/with space.txt")]
    public async Task Should_Allow_Legitimate_Blob_Names(string blobName)
    {
        await BlobProvider.UploadBlobAsync(ContainerName, blobName, new MemoryStream(Encoding.UTF8.GetBytes("legit-content")));

        var expected = Path.Combine(
            BasePath, "host", ContainerName,
            blobName.RemovePreFix("/").Replace('\\', Path.DirectorySeparatorChar));
        File.Exists(expected).ShouldBeTrue();

        using (var stream = await BlobProvider.DownloadBlobAsync(ContainerName, blobName))
        {
            stream.ShouldNotBeNull();
            using var reader = new StreamReader(stream!);
            (await reader.ReadToEndAsync()).ShouldBe("legit-content");
        }

        await BlobProvider.DeleteBlobAsync(ContainerName, blobName);
        File.Exists(expected).ShouldBeFalse();
    }

    [Fact]
    public async Task Should_Keep_Nested_Folder_Operations_Working()
    {
        await BlobProvider.CreateFolderAsync(ContainerName, "folder/sub/empty");

        var folder = Path.Combine(BasePath, "host", ContainerName, "folder", "sub", "empty");
        Directory.Exists(folder).ShouldBeTrue();

        await File.WriteAllTextAsync(Path.Combine(folder, "file.txt"), "x");
        await Should.ThrowAsync<BusinessException>(async () =>
            await BlobProvider.DeleteFolderAsync(ContainerName, "folder/sub/empty"));

        File.Delete(Path.Combine(folder, "file.txt"));
        await BlobProvider.DeleteFolderAsync(ContainerName, "folder/sub/empty");
        Directory.Exists(folder).ShouldBeFalse();
    }

    [Fact]
    public async Task Should_Isolate_Host_And_Tenant_Directories()
    {
        var tenantId = Guid.NewGuid();

        await BlobProvider.UploadBlobAsync(ContainerName, "host.txt", new MemoryStream(new byte[] { 1 }));
        File.Exists(Path.Combine(BasePath, "host", ContainerName, "host.txt")).ShouldBeTrue();

        using (CurrentTenant.Change(tenantId))
        {
            await BlobProvider.UploadBlobAsync(ContainerName, "tenant.txt", new MemoryStream(new byte[] { 2 }));
            File.Exists(Path.Combine(BasePath, "tenants", tenantId.ToString("D"), ContainerName, "tenant.txt")).ShouldBeTrue();

            // The tenant will be rejected when reading the host directory (relative path outside the tenant directory)
            await Should.ThrowAsync<BusinessException>(async () =>
                await BlobProvider.DownloadBlobAsync(ContainerName, "..\\..\\..\\host\\abp-c\\host.txt"));
        }
    }

    /// <summary>
    /// Path inclusion comparison must be consistent with file system case semantics
    /// </summary>
    [Fact]
    public void Should_Use_File_System_Case_Semantics_For_Containment()
    {
        var expected = OperatingSystem.IsWindows() || OperatingSystem.IsMacOS()
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;

        var property = typeof(LINGYUN.Abp.BlobManagement.FileSystem.FileSystemBlobProvider).GetProperty(
            "PathComparison",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

        property.ShouldNotBeNull();
        property!.GetValue(null).ShouldBe(expected);
    }
}
