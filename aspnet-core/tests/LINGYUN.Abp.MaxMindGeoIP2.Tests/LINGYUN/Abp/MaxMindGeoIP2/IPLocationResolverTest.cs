using LINGYUN.Abp.IP.Location;
using MaxMind.Db;
using MaxMind.GeoIP2;
using Microsoft.Extensions.Options;
using Shouldly;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace LINGYUN.Abp.MaxMindGeoIP2;
public class IPLocationResolverTest : AbpMaxMindGeoIP2TestBase
{
    /// <summary>
    /// Default options: Database files must be provided by the user (offline library is no longer built-in), memory mapping mode
    /// </summary>
    [Fact]
    public void TestDefaultOptions()
    {
        var defaultOptions = new AbpMaxMindGeoIP2Options();
        defaultOptions.DatabaseFile.ShouldBeNullOrEmpty();
        defaultOptions.DatabaseFileExists().ShouldBeFalse();
        defaultOptions.FileAccessMode.ShouldBe(FileAccessMode.MemoryMapped);

        // Test host: The offline library path is provided by the configuration (fallback to the local offline library file when not configured)
        var options = GetRequiredService<IOptions<AbpMaxMindGeoIP2Options>>().Value;
        options.FileAccessMode.ShouldBe(FileAccessMode.MemoryMapped);
    }

    /// <summary>
    /// The offline library should be loaded correctly (GeoLite2-City), the Name of GeoIP2 is the default name of the offline library, and the localized name is in Names
    /// </summary>
    [Fact]
    public void TestDatabaseReader()
    {
        var reader = GetRequiredService<IGeoIP2DatabaseReader>();

        reader.Metadata.DatabaseType.ShouldBe("GeoLite2-City");

        reader.TryCity(IPAddress.Parse("223.5.5.5"), out var response).ShouldBeTrue();
        response!.Country.Name.ShouldBe("China");
        response.Country.Names["zh-CN"].ShouldBe("\u4e2d\u56fd");
        response.MostSpecificSubdivision.Name.ShouldBe("Zhejiang");
        response.MostSpecificSubdivision.Names["zh-CN"].ShouldBe("\u6d59\u6c5f");
        response.City.Name.ShouldBe("Hangzhou");
        response.City.Names["zh-CN"].ShouldBe("\u676d\u5dde");

        reader.TryCity(IPAddress.Parse("2400:3200::1"), out var ipv6Response).ShouldBeTrue();
        ipv6Response!.Country.IsoCode.ShouldBe("CN");
    }

    /// <summary>
    /// Parse the location through IIPLocationResolver (remarks when the current culture is Simplified Chinese, the rules are consistent with the IP2Region module)
    /// </summary>
    [Theory]
    [InlineData("8.8.8.8", "\u7f8e\u56fd")]
    [InlineData("2001:4860:4860::8888", "\u7f8e\u56fd")]
    [InlineData("223.5.5.5", "\u6d59\u6c5f\u676d\u5dde")]
    [InlineData("2400:3200::1", "\u6d59\u6c5f\u676d\u5dde")]
    [InlineData("103.151.173.211", "\u65e5\u672c")]
    [InlineData("2a00:1450:4001:81b::200e", "\u5fb7\u56fd")]
    [InlineData("220.246.0.1", "\u9999\u6e2f")]
    public async Task TestSearchLocation(string ip, string shouldBeRemarks)
    {
        UseCulture("zh-CN");

        var resolver = GetRequiredService<IIPLocationResolver>();

        var result = await resolver.ResolveAsync(ip);

        result.Location.ShouldNotBeNull();
        result.Location.Remarks.ShouldBe(shouldBeRemarks);
        result.AppliedResolvers.ShouldContain(MaxMindGeoIP2IPLocationResolveContributor.ContributorName);
    }

    /// <summary>
    /// When the country name matches the removal list (RemoveCountry), it is removed from the remarks. When there is only country data, the remarks are empty.
    /// </summary>
    [Theory]
    [InlineData("114.114.114.114")]
    [InlineData("180.76.76.76")]
    public async Task TestSearchRemovedCountryOnly(string ip)
    {
        UseCulture("zh-CN");

        var resolver = GetRequiredService<IIPLocationResolver>();

        var result = await resolver.ResolveAsync(ip);

        result.Location.ShouldNotBeNull();
        result.Location.Country.ShouldBe("\u4e2d\u56fd");
        result.Location.Remarks.ShouldBeNullOrEmpty();
    }

    /// <summary>
    /// The geographical name is obtained from Names according to the current culture. If the complete culture does not exist, it is obtained according to the language. If neither exists, it falls back to Name.
    /// </summary>
    [Theory]
    // Complete culture name (zh-CN / en / ja, etc.)
    [InlineData("zh-CN", "223.5.5.5", "\u6d59\u6c5f\u676d\u5dde")]
    [InlineData("en-US", "223.5.5.5", "China")]
    [InlineData("ja-JP", "2a00:1450:4001:81b::200e", "\u30c9\u30a4\u30c4\u9023\u90a6\u5171\u548c\u56fd")]
    // The complete culture does not exist (es-MX), get it by language name (es)
    [InlineData("es-MX", "8.8.8.8", "Estados Unidos")]
    // The offline library does not have this language (ko), fall back to Name (default language of the offline library)
    [InlineData("ko-KR", "8.8.8.8", "United States")]
    [InlineData("ko-KR", "223.5.5.5", "China")]
    public async Task TestSearchLocationByCulture(string culture, string ip, string shouldBeRemarks)
    {
        UseCulture(culture);

        var resolver = GetRequiredService<IIPLocationResolver>();

        var result = await resolver.ResolveAsync(ip);

        result.Location.ShouldNotBeNull();
        result.Location.Remarks.ShouldBe(shouldBeRemarks);
    }

    /// <summary>
    /// Intranet, loopback, reserved addresses and addresses not included in the offline library: Country/Province/City/Remarks are all empty
    /// </summary>
    [Theory]
    [InlineData("10.0.0.1")]
    [InlineData("172.16.0.1")]
    [InlineData("192.168.1.1")]
    [InlineData("169.254.1.1")]
    [InlineData("224.0.0.1")]
    [InlineData("fe80::1")]
    [InlineData("1.1.1.1")]
    public async Task TestSearchSpecialLocation(string ip)
    {
        var resolver = GetRequiredService<IIPLocationResolver>();

        var result = await resolver.ResolveAsync(ip);

        result.Location.ShouldNotBeNull();
        result.Location.Remarks.ShouldBeNullOrEmpty();
        result.Location.Country.ShouldBeNull();
        result.Location.Province.ShouldBeNull();
        result.Location.City.ShouldBeNull();
    }

    /// <summary>
    /// Illegal addresses, local loopbacks and unspecified addresses are handled uniformly by IIPLocationResolver and do not return the location.
    /// </summary>
    [Theory]
    [InlineData("not-an-ip")]
    [InlineData("999.999.999.999")]
    [InlineData("127.0.0.1")]
    [InlineData("::1")]
    [InlineData("0.0.0.0")]
    [InlineData("::")]
    public async Task TestSearchInvalidLocation(string ip)
    {
        var resolver = GetRequiredService<IIPLocationResolver>();

        var result = await resolver.ResolveAsync(ip);

        result.Location.ShouldBeNull();
    }

    /// <summary>
    /// The query is singleton and thread-safe, and concurrent queries should return correct results.
    /// </summary>
    [Fact]
    public async Task TestSearchConcurrently()
    {
        UseCulture("zh-CN");

        var resolver = GetRequiredService<IIPLocationResolver>();

        var expected = new (string Ip, string Remarks)[]
        {
            ("8.8.8.8", "\u7f8e\u56fd"),
            ("223.5.5.5", "\u6d59\u6c5f\u676d\u5dde"),
            ("220.246.0.1", "\u9999\u6e2f"),
            ("2001:4860:4860::8888", "\u7f8e\u56fd"),
            ("2400:3200::1", "\u6d59\u6c5f\u676d\u5dde"),
        };

        var tasks = Enumerable.Range(0, System.Environment.ProcessorCount * 4)
            .Select(_ => Task.Run(async () =>
            {
                for (var i = 0; i < 20; i++)
                {
                    foreach (var (ip, remarks) in expected)
                    {
                        var result = await resolver.ResolveAsync(ip);
                        result.Location.Remarks.ShouldBe(remarks);
                    }
                }
            }));

        await Task.WhenAll(tasks);
    }

    private static void UseCulture(string cultureName)
    {
        var culture = new CultureInfo(cultureName);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
    }
}
