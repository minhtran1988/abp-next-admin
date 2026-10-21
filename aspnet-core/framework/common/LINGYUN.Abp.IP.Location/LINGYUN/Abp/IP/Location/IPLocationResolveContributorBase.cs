using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace LINGYUN.Abp.IP.Location;
public abstract class IPLocationResolveContributorBase : IIPLocationResolveContributor
{
    public abstract string Name { get; }

    public abstract Task ResolveAsync(IIPLocationResolveContext context);

    /// <summary>
    /// Constructed from country/province/city <see cref="IPLocation"/>, and press <see cref="AbpIPLocationResolveOptions"/> to generate remarks (Remarks)
    /// </summary>
    /// <remarks>
    /// Remarks rules shared by all IP location resolution contributors (name matching ignores case and leading and trailing whitespace, applicable to IP address resolution in all countries):
    /// * When the province name matches <see cref="AbpIPLocationResolveOptions.RemoveProvince"/>, the province is removed from the remark, and the remark uses the city (such as Hangzhou);
    /// * When the country name matches <see cref="AbpIPLocationResolveOptions.RemoveCountry"/>, the country is removed from the note,
    /// Remarks: Use the combination of province and city (when the province and city have the same name, only use the city, such as Chongqing; when there are only provinces, use the province, such as the Hong Kong Special Administrative Region);
    /// * When there is no match, use the country name (such as China, Japan, the United States) for remarks;
    /// * When the matched name is removed and there is no level to display, the comment will be empty.
    /// </remarks>
    protected virtual IPLocation CreateIPLocation(
        IIPLocationResolveContext context,
        string? country = null,
        string? province = null,
        string? city = null)
    {
        var options = context.ServiceProvider.GetRequiredService<IOptions<AbpIPLocationResolveOptions>>().Value;

        var ipLocation = new IPLocation(country, province, city);

        // When the configuration name is matched, the corresponding level is removed from the remarks.
        var useCountry = !IsExcluded(options.RemoveCountry, country);
        var useProvince = !IsExcluded(options.RemoveProvince, province);

        // 220.246.0.0|220.246.255.255|China|Hong Kong|0|PCCW
        if (!useProvince)
        {
            // Province name matches the removal list: Note the city (such as Hangzhou)
            ipLocation.Remarks = string.IsNullOrWhiteSpace(city) ? null : city;
        }
        // 36.133.232.0|36.133.239.255|China|Chongqing|Chongqing City|Mobile
        else if (!useCountry)
        {
            // Country name matches the removal list: Note using province and city combination
            if (!string.IsNullOrWhiteSpace(province) &&
                !string.IsNullOrWhiteSpace(city))
            {
                if (province!.Length <= city!.Length &&
                    city.StartsWith(province, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Chongqing City
                    ipLocation.Remarks = city;
                }
                // 111.26.31.0|111.26.31.127|China|Jilin Province|Jilin City|Mobile
                else
                {
                    // Jilin City, Jilin Province
                    ipLocation.Remarks = $"{province}{city}";
                }
            }
            // 220.246.0.0|220.246.255.255|China|Hong Kong SAR|0
            else if (!string.IsNullOrWhiteSpace(province))
            {
                // Hong Kong Special Administrative Region
                ipLocation.Remarks = province;
            }
            else if (!string.IsNullOrWhiteSpace(city))
            {
                ipLocation.Remarks = city;
            }
            else
            {
                // The matched name has been removed from the remark (for example, the remark is empty when there is only "China")
                ipLocation.Remarks = null;
            }
        }
        // 103.151.173.0|103.151.173.255|Japan|Tokyo|Tokyo|IKUUU Network
        else
        {
            // Japan / United States / Germany (only use country name if no removal list is matched)
            ipLocation.Remarks = country;
        }

        return ipLocation;
    }

    /// <summary>
    /// Whether the name matches the removal list (ignoring case and leading and trailing blanks)
    /// </summary>
    protected virtual bool IsExcluded(string[] names, string? name)
    {
        if (names == null || names.Length == 0 || string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        foreach (var item in names)
        {
            if (!string.IsNullOrWhiteSpace(item) &&
                string.Equals(item.Trim(), name!.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
