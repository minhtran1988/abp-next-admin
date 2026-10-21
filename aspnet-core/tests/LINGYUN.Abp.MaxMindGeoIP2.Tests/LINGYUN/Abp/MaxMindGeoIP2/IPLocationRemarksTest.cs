using LINGYUN.Abp.IP.Location;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;
using Xunit;

namespace LINGYUN.Abp.MaxMindGeoIP2;

/// <summary>
/// Remarks combination rule test
/// </summary>
/// <remarks>
/// The rules are processed uniformly by LINGYUN.Abp.IP.Location, and are shared by all location analysis modules (IP2Region, MaxMind):
/// * Province name matching RemoveProvince -> Remarks only use cities;
/// * Country name matching RemoveCountry -> Remarks use province and city combination (province and city with the same name only city);
/// * None matched -> Remarks Only use country names.
/// Name matching ignores case and leading and trailing whitespace, and is suitable for IP address resolution in all countries.
/// </remarks>
public class IPLocationRemarksTest
{
    [Theory]
    // Neither configured: only use country name
    [InlineData("223.5.5.5", new string[0], new string[0], "China", "Zhejiang Province", "Hangzhou City", "China")]
    // Match country name (China): province and city combination
    [InlineData("2400:3200::1", new[] { "China" }, new string[0], "China", "Zhejiang Province", "Hangzhou City", "Hangzhou City, Zhejiang Province")]
    [InlineData("36.133.108.1", new[] { "China" }, new string[0], "China", "Chongqing", "Chongqing City", "Chongqing City")]
    [InlineData("220.246.0.1", new[] { "China" }, new string[0], "China", "Hong Kong Special Administrative Region", null, "Hong Kong Special Administrative Region")]
    [InlineData("114.114.114.114", new[] { "China" }, new string[0], "China", null, null, null)]
    // Unmatched country name: unaffected
    [InlineData("103.151.173.211", new[] { "China" }, new string[0], "Japan", "Tokyo", "Tokyo", "Japan")]
    [InlineData("2a00:1450:4001:81b::200e", new[] { "China" }, new string[0], "Germany", "Hesse", "Frankfurt", "Germany")]
    // Match province names: only use cities (same applies to IPs in other countries)
    [InlineData("111.26.31.1", new string[0], new[] { "Jilin Province" }, "China", "Jilin Province", "Jilin City", "Jilin City")]
    [InlineData("180.76.76.76", new string[0], new[] { "Beijing City" }, "China", "Beijing City", null, null)]
    [InlineData("2001:200::1", new string[0], new[] { "Tokyo" }, "Japan", "Tokyo", "Shinjuku City", "Shinjuku City")]
    // Provinces take precedence over countries
    [InlineData("8.8.8.8", new[] { "China" }, new[] { "Zhejiang Province" }, "China", "Zhejiang Province", "Hangzhou City", "Hangzhou City")]
    public void TestRemarks(
        string ip,
        string[] removeCountry,
        string[] removeProvince,
        string country,
        string? province,
        string? city,
        string? expectedRemarks)
    {
        var services = new ServiceCollection();
        services.Configure<AbpIPLocationResolveOptions>(options =>
        {
            options.RemoveCountry = removeCountry;
            options.RemoveProvince = removeProvince;
        });

        var context = new IPLocationResolveContext(IPAddress.Parse(ip), services.BuildServiceProvider());

        var location = new TestIPLocationResolveContributor()
            .Create(context, country, province, city);

        context.IpAddress.ShouldBe(IPAddress.Parse(ip));

        location.Country.ShouldBe(country);
        location.Province.ShouldBe(province);
        location.City.ShouldBe(city);

        if (expectedRemarks == null)
        {
            location.Remarks.ShouldBeNullOrEmpty();
        }
        else
        {
            location.Remarks.ShouldBe(expectedRemarks);
        }
    }

    private sealed class TestIPLocationResolveContributor : MaxMindGeoIP2IPLocationResolveContributor
    {
        public IPLocation Create(
            IIPLocationResolveContext context,
            string? country,
            string? province,
            string? city)
        {
            return CreateIPLocation(context, country, province, city);
        }
    }
}
