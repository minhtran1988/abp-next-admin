using LINGYUN.Abp.IP.Location;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Model;
using MaxMind.GeoIP2.Responses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace LINGYUN.Abp.MaxMindGeoIP2;

/// <summary>
/// MaxMind GeoIP2 location analysis contributor
/// </summary>
/// <remarks>
public class MaxMindGeoIP2IPLocationResolveContributor : IPLocationResolveContributorBase
{
    public const string ContributorName = "MaxMindGeoIP2";
    public override string Name => ContributorName;

    public override Task ResolveAsync(IIPLocationResolveContext context)
    {
        var reader = context.ServiceProvider.GetRequiredService<IGeoIP2DatabaseReader>();

        CityResponse? response;
        try
        {
            if (!reader.TryCity(context.IpAddress, out response) || response == null)
            {
                // Intranet, reserved addresses and uncollected addresses are not recorded in the offline library
                context.Location = new IPLocation();

                return Task.CompletedTask;
            }
        }
        catch (Exception exception)
        {
            // Offline library reading exceptions (such as file corruption) do not interrupt the business. After recording the log, return with no data.
            context.ServiceProvider
                .GetService<ILogger<MaxMindGeoIP2IPLocationResolveContributor>>()
                ?.LogWarning(exception, "Failed to query the MaxMind GeoIP2 database for {IpAddress}.", context.IpAddress);

            context.Location = new IPLocation();

            return Task.CompletedTask;
        }

        context.Location = CreateIPLocation(
            context,
            GetName(response.Country),
            GetName(response.MostSpecificSubdivision),
            GetName(response.City));

        return Task.CompletedTask;
    }

    /// <summary>
    /// Get the name of the current culture. If there is no corresponding culture, fall back to <see cref="NamedEntity.Name"/>
    /// </summary>
    protected virtual string? GetName(NamedEntity? entity)
    {
        if (entity == null)
        {
            return null;
        }

        var culture = CultureInfo.CurrentUICulture;

        // First match by the complete cultural name (such as zh-CN), then match by the language name (such as zh), and finally fall back to the default name of the offline library
        var name = GetNameByCulture(entity, culture.Name)
            ?? GetNameByCulture(entity, culture.TwoLetterISOLanguageName)
            ?? entity.Name;

        return Normalize(name);
    }

    /// <summary>
    /// Get the name of the specified culture from <see cref="NamedEntity.Names"/> (keys are not case-sensitive)
    /// </summary>
    protected virtual string? GetNameByCulture(NamedEntity entity, string? culture)
    {
        if (culture.IsNullOrWhiteSpace() || entity.Names == null)
        {
            return null;
        }

        if (entity.Names.TryGetValue(culture!, out var name))
        {
            return name;
        }

        foreach (var item in entity.Names)
        {
            if (item.Key.Contains(culture, StringComparison.OrdinalIgnoreCase))
            {
                return item.Value;
            }
        }

        return null;
    }

    private static string? Normalize(string? value)
    {
        return value.IsNullOrWhiteSpace() || string.Equals(value, "0", StringComparison.Ordinal)
            ? null
            : value;
    }
}
