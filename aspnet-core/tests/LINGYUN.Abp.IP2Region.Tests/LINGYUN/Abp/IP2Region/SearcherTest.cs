using IP2Region.Net.Abstractions;
using IP2Region.Net.XDB;
using LINGYUN.Abp.IP.Location;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;
using Volo.Abp.VirtualFileSystem;
using Xunit;

namespace LINGYUN.Abp.IP2Region;
public class SearcherTest : AbpIP2RegionTestBase
{
    private const string IPv4XdbFile = "/LINGYUN/Abp/IP2Region/Resources/ip2region_v4.xdb";
    private const string IPv6XdbFile = "/LINGYUN/Abp/IP2Region/Resources/ip2region_v6.xdb";

    /// <summary>
    /// The IPv6 offline database has a large amount of data (about 670,000 segments), and the benchmark test is sampled and verified according to this step size.
    /// </summary>
    private const int IPv6BenchSampleStep = 512;

    private readonly Stream _xdbStream;

    public SearcherTest()
    {
        var virtualFileProvider = GetRequiredService<IVirtualFileProvider>();
        _xdbStream = virtualFileProvider.GetFileInfo(IPv4XdbFile).CreateReadStream();
    }

    private Stream CreateXdbStream(string fileName)
    {
        var virtualFileProvider = GetRequiredService<IVirtualFileProvider>();
        return virtualFileProvider.GetFileInfo(fileName).CreateReadStream();
    }

    [Theory]
    [InlineData("8.8.8.8", "United States")]
    [InlineData("36.133.108.1", "\u91cd\u5e86\u5e02")]
    [InlineData("111.26.31.1", "\u5409\u6797\u7701\u5409\u6797\u5e02")]
    [InlineData("220.246.0.1", "\u9999\u6e2f\u7279\u522b\u884c\u653f\u533a")]
    [InlineData("103.151.173.211", "Japan")]
    [InlineData("103.151.191.5", "Indonesia")]
    public async Task TestSearchLocation(string ip, string shouldBeRemarks)
    {
        var resolver = GetRequiredService<IIPLocationResolver>();
        var result = await resolver.ResolveAsync(ip);
        result.Location.Remarks.ShouldBe(shouldBeRemarks);
    }

    [Theory]
    [InlineData("2001:4860:4860::8888", "United States")]
    [InlineData("2606:4700:4700::1111", "United Kingdom")]
    [InlineData("2400:3200::1", "\u6d59\u6c5f\u7701\u676d\u5dde\u5e02")]
    [InlineData("2001:200::1", "Japan")]
    [InlineData("240e:4c:4008::1", "\u5317\u4eac\u5e02")]
    [InlineData("2408:8000::1", "\u5317\u4eac\u5e02")]
    public async Task TestSearchLocationIPv6(string ip, string shouldBeRemarks)
    {
        var resolver = GetRequiredService<IIPLocationResolver>();
        var result = await resolver.ResolveAsync(ip);
        result.Location.Remarks.ShouldBe(shouldBeRemarks);
    }

    [Theory]
    [InlineData("114.114.114.114", "\u4e2d\u56fd|\u6c5f\u82cf\u7701|\u5357\u4eac\u5e02|0|CN")]
    [InlineData("119.29.29.29", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u817e\u8baf|CN")]
    [InlineData("223.5.5.5", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN")]
    [InlineData("180.76.76.76", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u767e\u5ea6|CN")]
    [InlineData("8.8.8.8", "United States|California|0|Google LLC|US")]
    public void TestSearchCacheContent(string ip, string shouldBeRegion)
    {
        var contentSearcher = new AbpSearcher(CachePolicy.Content, _xdbStream);
        var region = contentSearcher.Search(ip);
        region.ShouldBe(shouldBeRegion);
    }

    [Theory]
    [InlineData("114.114.114.114", "\u4e2d\u56fd|\u6c5f\u82cf\u7701|\u5357\u4eac\u5e02|0|CN")]
    [InlineData("119.29.29.29", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u817e\u8baf|CN")]
    [InlineData("223.5.5.5", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN")]
    [InlineData("180.76.76.76", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u767e\u5ea6|CN")]
    [InlineData("8.8.8.8", "United States|California|0|Google LLC|US")]
    public void TestSearchCacheVector(string ip, string shouldBeRegion)
    {
        var vectorSearcher = new AbpSearcher(CachePolicy.VectorIndex, _xdbStream);
        var region = vectorSearcher.Search(ip);
        region.ShouldBe(shouldBeRegion);
    }

    [Theory]
    [InlineData("114.114.114.114", "\u4e2d\u56fd|\u6c5f\u82cf\u7701|\u5357\u4eac\u5e02|0|CN")]
    [InlineData("119.29.29.29", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u817e\u8baf|CN")]
    [InlineData("223.5.5.5", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN")]
    [InlineData("180.76.76.76", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u767e\u5ea6|CN")]
    [InlineData("8.8.8.8", "United States|California|0|Google LLC|US")]
    public void TestSearchCacheFile(string ip, string shouldBeRegion)
    {
        var fileSearcher = new AbpSearcher(CachePolicy.File, _xdbStream);
        var region = fileSearcher.Search(ip);
        region.ShouldBe(shouldBeRegion);
    }

    [Theory]
    [InlineData("2001:4860:4860::8888", "United States|Florida|Miami|Google LLC|US")]
    [InlineData("2606:4700:4700::1111", "United Kingdom|England|London|Cloudflare, Inc.|GB")]
    [InlineData("2400:3200::1", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN")]
    [InlineData("2001:200::1", "Japan|Tokyo|Tokyo|WIDE Project|JP")]
    [InlineData("240e:4c:4008::1", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u7535\u4fe1|CN")]
    [InlineData("2a00:1450:4001:81b::200e", "Germany|Hesse|Frankfurt am Main|Google LLC|DE")]
    public void TestSearchIPv6CacheContent(string ip, string shouldBeRegion)
    {
        using var ipv6XdbStream = CreateXdbStream(IPv6XdbFile);
        // Single file mode: only load IPv6 offline library
        using var searcher = new AbpSearcher(CachePolicy.Content, ipv6XdbStream);

        searcher.Search(ip).ShouldBe(shouldBeRegion);
    }

    [Theory]
    [InlineData("2001:4860:4860::8888", "United States|Florida|Miami|Google LLC|US")]
    [InlineData("2606:4700:4700::1111", "United Kingdom|England|London|Cloudflare, Inc.|GB")]
    [InlineData("2400:3200::1", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN")]
    [InlineData("2001:200::1", "Japan|Tokyo|Tokyo|WIDE Project|JP")]
    [InlineData("240e:4c:4008::1", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u7535\u4fe1|CN")]
    [InlineData("2a00:1450:4001:81b::200e", "Germany|Hesse|Frankfurt am Main|Google LLC|DE")]
    public void TestSearchIPv6CacheVector(string ip, string shouldBeRegion)
    {
        using var ipv6XdbStream = CreateXdbStream(IPv6XdbFile);
        using var searcher = new AbpSearcher(CachePolicy.VectorIndex, ipv6XdbStream);

        searcher.Search(ip).ShouldBe(shouldBeRegion);
    }

    [Theory]
    [InlineData("2001:4860:4860::8888", "United States|Florida|Miami|Google LLC|US")]
    [InlineData("2606:4700:4700::1111", "United Kingdom|England|London|Cloudflare, Inc.|GB")]
    [InlineData("2400:3200::1", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN")]
    [InlineData("2001:200::1", "Japan|Tokyo|Tokyo|WIDE Project|JP")]
    [InlineData("240e:4c:4008::1", "\u4e2d\u56fd|\u5317\u4eac\u5e02|\u5317\u4eac\u5e02|\u7535\u4fe1|CN")]
    [InlineData("2a00:1450:4001:81b::200e", "Germany|Hesse|Frankfurt am Main|Google LLC|DE")]
    public void TestSearchIPv6CacheFile(string ip, string shouldBeRegion)
    {
        using var ipv6XdbStream = CreateXdbStream(IPv6XdbFile);
        using var searcher = new AbpSearcher(CachePolicy.File, ipv6XdbStream);

        searcher.Search(ip).ShouldBe(shouldBeRegion);
    }

    /// <summary>
    /// When loading IPv4 and IPv6 offline libraries at the same time, automatically route according to the address family
    /// </summary>
    [Theory]
    [InlineData("114.114.114.114", "\u4e2d\u56fd|\u6c5f\u82cf\u7701|\u5357\u4eac\u5e02|0|CN")]
    [InlineData("8.8.8.8", "United States|California|0|Google LLC|US")]
    [InlineData("2001:4860:4860::8888", "United States|Florida|Miami|Google LLC|US")]
    [InlineData("2400:3200::1", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN")]
    [InlineData("2001:200::1", "Japan|Tokyo|Tokyo|WIDE Project|JP")]
    public void TestSearchDualDatabase(string ip, string shouldBeRegion)
    {
        using var ipv4XdbStream = CreateXdbStream(IPv4XdbFile);
        using var ipv6XdbStream = CreateXdbStream(IPv6XdbFile);
        using var searcher = new AbpSearcher(CachePolicy.File, ipv4XdbStream, ipv6XdbStream);

        searcher.Search(ip).ShouldBe(shouldBeRegion);
    }

    /// <summary>
    /// IPv4-mapped IPv6 addresses (::ffff:x.x.x.x) will be normalized to IPv4 queries
    /// </summary>
    [Theory]
    [InlineData("::ffff:114.114.114.114", "\u4e2d\u56fd|\u6c5f\u82cf\u7701|\u5357\u4eac\u5e02|0|CN")]
    [InlineData("::ffff:8.8.8.8", "United States|California|0|Google LLC|US")]
    public void TestSearchIPv4MappedIPv6(string ip, string shouldBeRegion)
    {
        using var ipv4XdbStream = CreateXdbStream(IPv4XdbFile);
        using var ipv6XdbStream = CreateXdbStream(IPv6XdbFile);
        using var searcher = new AbpSearcher(CachePolicy.File, ipv4XdbStream, ipv6XdbStream);

        searcher.Search(ip).ShouldBe(shouldBeRegion);
    }

    /// <summary>
    /// Special addresses such as intranet, loopback, and reserved addresses do not return the country/province/city, and Remarks is empty.
    /// </summary>
    [Theory]
    [InlineData("10.0.0.1")]
    [InlineData("172.16.0.1")]
    [InlineData("192.168.1.1")]
    [InlineData("169.254.1.1")]
    [InlineData("100.64.0.1")]
    [InlineData("192.0.2.1")]
    [InlineData("224.0.0.1")]
    [InlineData("255.255.255.255")]
    [InlineData("fd00::1")]
    [InlineData("fe80::1")]
    [InlineData("ff02::1")]
    [InlineData("2001:db8::1")]
    [InlineData("64:ff9b::1")]
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
    public async Task TestSearchWithoutLocation(string ip)
    {
        var resolver = GetRequiredService<IIPLocationResolver>();

        var result = await resolver.ResolveAsync(ip);

        result.Location.ShouldBeNull();
    }

    /// <summary>
    /// Default option: built-in offline library (virtual file system path) + CachePolicy.Content (concurrency safety)
    /// </summary>
    [Fact]
    public void TestDefaultOptions()
    {
        var options = GetRequiredService<IOptions<AbpIP2RegionOptions>>().Value;

        options.IPv4DatabaseFile.ShouldBe(AbpIP2RegionModule.DefaultIPv4DatabaseFile);
        options.IPv6DatabaseFile.ShouldBe(AbpIP2RegionModule.DefaultIPv6DatabaseFile);
        options.CachePolicy.ShouldBe(CachePolicy.Content);
    }

    /// <summary>
    /// The singleton queryer should return correct results when concurrently querying IPv4 / IPv6 (the caching strategy needs to ensure concurrency safety)
    /// </summary>
    [Fact]
    public async Task TestSearchConcurrently()
    {
        var searcher = GetRequiredService<ISearcher>();

        var expected = new (string Ip, string Region)[]
        {
            ("114.114.114.114", "\u4e2d\u56fd|\u6c5f\u82cf\u7701|\u5357\u4eac\u5e02|0|CN"),
            ("8.8.8.8", "United States|California|0|Google LLC|US"),
            ("223.5.5.5", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN"),
            ("2001:4860:4860::8888", "United States|Florida|Miami|Google LLC|US"),
            ("2400:3200::1", "\u4e2d\u56fd|\u6d59\u6c5f\u7701|\u676d\u5dde\u5e02|\u963f\u91cc|CN"),
        };

        var tasks = Enumerable.Range(0, Environment.ProcessorCount * 8)
            .Select(_ => Task.Run(() =>
            {
                for (var i = 0; i < 50; i++)
                {
                    foreach (var (ip, region) in expected)
                    {
                        searcher.Search(ip).ShouldBe(region);
                    }
                }
            }));

        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// When the IPv6 offline library is not provided, the IPv6 query returns null (the IPv4 offline library is not used for parsing), and the IPv4 query is not affected.
    /// </summary>
    [Fact]
    public void TestSearchIPv6WithoutDatabase()
    {
        using var ipv4XdbStream = CreateXdbStream(IPv4XdbFile);
        using var searcher = new AbpSearcher(CachePolicy.File, ipv4XdbStream, null);

        searcher.Search("2001:4860:4860::8888").ShouldBeNull();
        searcher.Search("114.114.114.114").ShouldBe("\u4e2d\u56fd|\u6c5f\u82cf\u7701|\u5357\u4eac\u5e02|0|CN");
    }

    /// <summary>
    /// IPv6 offline database query results should be consistent with official source data (sampling verification)
    /// </summary>
    [Theory]
    [InlineData(CachePolicy.VectorIndex)]
    [InlineData(CachePolicy.File)]
    public void TestBenchSearchIPv6(CachePolicy cachePolicy)
    {
        using var ipv6XdbStream = CreateXdbStream(IPv6XdbFile);
        using var searcher = new AbpSearcher(cachePolicy, ipv6XdbStream);

        var srcPath = Path.Combine(AppContext.BaseDirectory, "ipv6_source.txt");
        var lineIndex = 0;

        foreach (var line in File.ReadLines(srcPath))
        {
            // The amount of data is large, sampling according to step size
            if (lineIndex++ % IPv6BenchSampleStep != 0)
            {
                continue;
            }

            var ps = line.Trim().Split("|", 3);

            if (ps.Length != 3)
            {
                throw new ArgumentException($"invalid ip segment line {line}", nameof(line));
            }

            var sip = IpAddressToBigInteger(ps[0]);
            var eip = IpAddressToBigInteger(ps[1]);
            var mip = (sip + eip) / 2;

            BigInteger[] temp = [sip, (sip + mip) / 2, mip, (mip + eip) / 2, eip];

            foreach (var ip in temp)
            {
                var ipAddress = BigIntegerToIpAddress(ip);
                var region = searcher.Search(ipAddress);

                if (region != ps[2])
                {
                    throw new Exception($"failed search {ipAddress} with ({region}!={ps[2]})");
                }
            }
        }
    }

    [Theory]
    [InlineData(CachePolicy.Content)]
    [InlineData(CachePolicy.VectorIndex)]
    [InlineData(CachePolicy.File)]
    public void TestBenchSearch(CachePolicy cachePolicy)
    {
        var searcher = new AbpSearcher(cachePolicy, _xdbStream);
        var srcPath = Path.Combine(AppContext.BaseDirectory, "ipv4_source.txt");

        foreach (var line in File.ReadLines(srcPath))
        {
            var ps = line.Trim().Split("|", 3);

            if (ps.Length != 3)
            {
                throw new ArgumentException($"invalid ip segment line {line}", nameof(line));
            }

            var sip = Util.IpAddressToUInt32(ps[0]);
            var eip = Util.IpAddressToUInt32(ps[1]);
            var mip = Util.GetMidIp(sip, eip);

            uint[] temp = { sip, Util.GetMidIp(sip, mip), mip, Util.GetMidIp(mip, eip), eip };

            foreach (var ip in temp)
            {
                var region = searcher.Search(ip);

                if (region != ps[2])
                {
                    throw new Exception($"failed search {ip} with ({region}!={ps[2]})");
                }
            }
        }
    }

    private static BigInteger IpAddressToBigInteger(string ip)
        => new(IPAddress.Parse(ip).GetAddressBytes(), isUnsigned: true, isBigEndian: true);

    private static IPAddress BigIntegerToIpAddress(BigInteger value)
    {
        var bytes = value.ToByteArray(isUnsigned: true, isBigEndian: true);
        if (bytes.Length == 16)
        {
            return new IPAddress(bytes);
        }

        var padded = new byte[16];
        Array.Copy(bytes, 0, padded, 16 - bytes.Length, bytes.Length);

        return new IPAddress(padded);
    }
}
