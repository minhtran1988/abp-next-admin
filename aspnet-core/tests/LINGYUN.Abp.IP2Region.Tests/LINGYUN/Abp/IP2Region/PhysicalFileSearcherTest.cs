using IP2Region.Net.Abstractions;
using LINGYUN.Abp.IP.Location;
using Microsoft.Extensions.Options;
using Shouldly;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace LINGYUN.Abp.IP2Region;
public class PhysicalFileSearcherTest : AbpIP2RegionPhysicalFileTestBase
{
    /// <summary>
    /// The offline library is configured as a physical file (taking precedence over built-in resources)
    /// </summary>
    [Fact]
    public void TestOptionsUsePhysicalFiles()
    {
        var options = GetRequiredService<IOptions<AbpIP2RegionOptions>>().Value;

        options.IPv4DatabaseFile.ShouldNotBe(AbpIP2RegionModule.DefaultIPv4DatabaseFile);
        options.IPv6DatabaseFile.ShouldNotBe(AbpIP2RegionModule.DefaultIPv6DatabaseFile);
        File.Exists(options.IPv4DatabaseFile).ShouldBeTrue();
        File.Exists(options.IPv6DatabaseFile).ShouldBeTrue();
    }

    /// <summary>
    /// IPv4/IPv6 offline libraries loaded from physical files can be queried normally.
    /// </summary>
    [Theory]
    [InlineData("114.114.114.114", "\u4e2d\u56fd|\u6c5f\u82cf\u7701|\u5357\u4eac\u5e02|0|CN")]
    [InlineData("8.8.8.8", "United States|California|0|Google LLC|US")]
    [InlineData("2001:4860:4860::8888", "United States|Florida|Miami|Google LLC|US")]
    [InlineData("2400:3200::1", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN")]
    public void TestSearchFromPhysicalFile(string ip, string shouldBeRegion)
    {
        var searcher = GetRequiredService<ISearcher>();

        searcher.Search(ip).ShouldBe(shouldBeRegion);
    }

    /// <summary>
    /// Location parsing uses the default remark rules (only the country name is displayed when the removal list is not configured)
    /// </summary>
    [Fact]
    public async Task TestResolveLocationFromPhysicalFile()
    {
        var resolver = GetRequiredService<IIPLocationResolver>();

        var result = await resolver.ResolveAsync("223.5.5.5");

        result.Location.ShouldNotBeNull();
        result.Location.Country.ShouldBe("\u4e2d\u56fd");
        result.Location.Province.ShouldBe("\u6d59\u6c5f\u7701");
        result.Location.City.ShouldBe("\u676d\u5dde\u5e02");
        result.Location.Remarks.ShouldBe("\u4e2d\u56fd");
    }
}
