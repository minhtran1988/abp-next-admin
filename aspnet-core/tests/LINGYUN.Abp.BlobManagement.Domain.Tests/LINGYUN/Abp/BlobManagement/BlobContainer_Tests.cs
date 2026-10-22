using Shouldly;
using System;
using Volo.Abp;
using Xunit;

namespace LINGYUN.Abp.BlobManagement;

/// <summary>
/// Storage container
/// </summary>
public class BlobContainer_Tests : AbpBlobManagementDomainTestBase
{
    [Theory]
    [InlineData("..")]
    [InlineData(".")]
    [InlineData("a/b")]
    [InlineData("a\\b")]
    [InlineData("x/../abp-c")]
    [InlineData("..\\..\\evil")]
    [InlineData("NUL")]
    [InlineData("con.txt")]
    [InlineData("COM1")]
    [InlineData("a:b")]
    [InlineData("a*b")]
    [InlineData("trailing.")]
    [InlineData("trailing ")]
    public void Should_Reject_Unsafe_Names(string name)
    {
        var container = new TestBlobContainer();

        var exception = Should.Throw<BusinessException>(() => container.SetName(name));

        exception.Code.ShouldBe(BlobManagementErrorCodes.Container.InvalidName);
        exception.Data["Name"].ShouldBe(name);
    }

    [Theory]
    [InlineData("public")]
    [InlineData("users")]
    [InlineData("my-container")]
    [InlineData("my_container.1")]
    [InlineData("\u5bf9\u8c61\u5b58\u50a8")]
    public void Should_Allow_Legitimate_Names(string name)
    {
        var container = new TestBlobContainer();

        container.SetName(name);

        container.Name.ShouldBe(name);
    }

    /// <summary>
    /// The entity constructor is internal, and the test subclass exposes the protected constructor
    /// </summary>
    private sealed class TestBlobContainer : BlobContainer
    {
        public TestBlobContainer()
        {
        }
    }
}
