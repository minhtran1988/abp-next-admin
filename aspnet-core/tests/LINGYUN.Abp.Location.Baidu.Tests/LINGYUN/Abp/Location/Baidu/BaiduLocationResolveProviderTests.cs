using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LINGYUN.Abp.Location.Baidu
{
    public class BaiduLocationResolveProviderTests : AbpLocationBaiduTestBase
    {
        private readonly ILocationResolveProvider _provider;

        public BaiduLocationResolveProviderTests()
        {
            _provider = GetRequiredService<ILocationResolveProvider>();
            _provider.ShouldBeOfType<BaiduLocationResolveProvider>();
        }

        [Theory]
        [InlineData(39.906049, 116.398773, 1000)]
        public async Task ReGeocode_Test(double lat, double lng, int radius = 50)
        {
            var location = await _provider.ReGeocodeAsync(lat, lng, radius);

            location.ShouldNotBeNull();
            location.Address.ShouldBe("\u5317\u4eac\u5e02\u897f\u57ce\u533a\u7164\u5e02\u8857\u4e0e\u524d\u95e8\u897f\u540e\u6cb3\u6cbf\u8857\u4ea4\u53c9\u8def\u53e3\u5f80\u897f\u5317\u7ea650\u7c73");
            location.FormattedAddress.ShouldBe("\u897f\u57ce\u533a\u6b63\u9633\u5e02\u573a");

            // TODO
        }

        [Theory]
        [InlineData("\u5317\u4eac\u5e02\u4e1c\u57ce\u533a\u5e7f\u573a\u4e1c\u4fa7\u8def", "\u5317\u4eac", 39.9102810304917, 116.40588509187855, "\u9053\u8def")]
        [InlineData("\u5929\u5b89\u95e8-\u57ce\u697c\u68c0\u7968\u5904(\u5165\u53e3)", "\u5317\u4eac", 39.91573869262374, 116.40366250438577, "NoClass")]
        [InlineData("\u56fd\u5bb6\u56fe\u4e66\u9986(\u53e4\u7c4d\u9986)", "\u5317\u4eac", 39.92968852456012, 116.39170686652291, "\u6559\u80b2")]
        public async Task Geocode_Test(string address, string city, double lat, double lng, string level)
        {
            var location = await _provider.GeocodeAsync(address, city);

            location.ShouldNotBeNull();
            location.Latitude.ShouldBe(lat);
            location.Longitude.ShouldBe(lng);
            location.Level.ShouldBe(level);
            location.Pomprehension.ShouldBe(0);

            // TODO
        }

        [Theory]
        [InlineData("111.206.145.41")]
        public async Task IPGeocode_Test(string ipAddress)
        {
            var location = await _provider.IPGeocodeAsync(ipAddress);

            location.ShouldNotBeNull();
            location.Location.Latitude.ShouldBe(39.91092300415039);
            location.Location.Longitude.ShouldBe(116.41338348388672);

            // TODO
        }
    }
}
