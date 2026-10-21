using MaxMind.Db;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using Shouldly;
using System.Net;
using Xunit;

namespace LINGYUN.Abp.MaxMindGeoIP2;

/// <summary>
/// Empty GeoIP2 interpreter
/// </summary>
/// <remarks>
/// Used when no offline library file is provided, which is equivalent to interpreting an empty content: all queries return "no data"
/// </remarks>
public class EmptyGeoIP2DatabaseReader_Tests
{
    private readonly IGeoIP2DatabaseReader _reader = new EmptyGeoIP2DatabaseReader();

    [Fact]
    public void Should_Not_Find_Any_City()
    {
        _reader.TryCity(IPAddress.Parse("223.5.5.5"), out var response).ShouldBeFalse();
        response.ShouldBeNull();

        _reader.TryCity("223.5.5.5", out var stringResponse).ShouldBeFalse();
        stringResponse.ShouldBeNull();
    }

    [Fact]
    public void Should_Not_Find_Any_Data()
    {
        _reader.TryCountry(IPAddress.Parse("223.5.5.5"), out var country).ShouldBeFalse();
        country.ShouldBeNull();

        _reader.TryAnonymousIP(IPAddress.Parse("223.5.5.5"), out var anonymousIp).ShouldBeFalse();
        anonymousIp.ShouldBeNull();

        _reader.TryAnonymousPlus(IPAddress.Parse("223.5.5.5"), out var anonymousPlus).ShouldBeFalse();
        anonymousPlus.ShouldBeNull();

        _reader.TryAsn(IPAddress.Parse("223.5.5.5"), out var asn).ShouldBeFalse();
        asn.ShouldBeNull();

        _reader.TryConnectionType(IPAddress.Parse("223.5.5.5"), out var connectionType).ShouldBeFalse();
        connectionType.ShouldBeNull();

        _reader.TryDomain(IPAddress.Parse("223.5.5.5"), out var domain).ShouldBeFalse();
        domain.ShouldBeNull();

        _reader.TryEnterprise(IPAddress.Parse("223.5.5.5"), out var enterprise).ShouldBeFalse();
        enterprise.ShouldBeNull();

        _reader.TryIsp(IPAddress.Parse("223.5.5.5"), out var isp).ShouldBeFalse();
        isp.ShouldBeNull();
    }

    /// <summary>
    /// Non-Try methods behave as if they were a real interpreter miss: throw AddressNotFoundException
    /// </summary>
    [Fact]
    public void Should_Throw_Address_Not_Found()
    {
        Should.Throw<AddressNotFoundException>(() => _reader.City(IPAddress.Parse("223.5.5.5")));
        Should.Throw<AddressNotFoundException>(() => _reader.City("223.5.5.5"));
        Should.Throw<AddressNotFoundException>(() => _reader.Country(IPAddress.Parse("223.5.5.5")));
        Should.Throw<AddressNotFoundException>(() => _reader.Country("223.5.5.5"));
        Should.Throw<AddressNotFoundException>(() => _reader.Asn(IPAddress.Parse("223.5.5.5")));
        Should.Throw<AddressNotFoundException>(() => _reader.Isp("223.5.5.5"));
        Should.Throw<AddressNotFoundException>(() => _reader.Enterprise(IPAddress.Parse("223.5.5.5")));
        Should.Throw<AddressNotFoundException>(() => _reader.AnonymousIP("223.5.5.5"));
        Should.Throw<AddressNotFoundException>(() => _reader.AnonymousPlus("223.5.5.5"));
        Should.Throw<AddressNotFoundException>(() => _reader.ConnectionType("223.5.5.5"));
        Should.Throw<AddressNotFoundException>(() => _reader.Domain("223.5.5.5"));
    }

    /// <summary>
    /// The empty interpreter does not provide database metadata
    /// </summary>
    [Fact]
    public void Should_Not_Provide_Metadata()
    {
        _reader.Metadata.ShouldBeNull();
    }

    /// <summary>
    /// Option: If the database file is not specified, it is determined that it does not exist.
    /// </summary>
    [Fact]
    public void Should_Check_Database_File()
    {
        var options = new AbpMaxMindGeoIP2Options();
        options.DatabaseFileExists().ShouldBeFalse();

        options.DatabaseFile = "not-exists.mmdb";
        options.DatabaseFileExists().ShouldBeFalse();
    }
}
