using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.IP.Location;
public class AbpIPLocationResolveOptions
{
    [NotNull]
    public List<IIPLocationResolveContributor> IPLocationResolvers { get; }

    /// <summary>
    /// Country names removed from remarks (Remarks)
    /// </summary>
    /// <remarks>
    /// When matching names (ignoring case and leading and trailing blanks, applicable to IPs in all countries), the country name will no longer appear in the remarks.
    /// Notes: Use the combination of province and city instead: If both provinces and cities exist, use "province + city" (when the province and city have the same name, only the city, such as Chongqing), and when there are only provinces, use the province.
    /// 
    /// For example, after setting it to "China": the comment of China IP is "province + city" (such as Hangzhou City, Zhejiang Province), and other countries still only display the country name.
    /// The default is an empty collection (country names participate in the remark combination).
    /// </remarks>
    [NotNull]
    public string[] RemoveCountry { get; set; }

    /// <summary>
    /// Province names removed from remarks (Remarks)
    /// </summary>
    /// <remarks>
    /// When matching names (ignoring case and leading and trailing blanks, applicable to IPs from all countries), the province name no longer appears in the remarks, and only the city (such as Hangzhou) is used in the remarks;
    /// Unmatched IPs are not affected. The default is an empty set (province names participate in the remark combination).
    /// </remarks>
    [NotNull]
    public string[] RemoveProvince { get; set; }

    public AbpIPLocationResolveOptions()
    {
        IPLocationResolvers = new List<IIPLocationResolveContributor>();

        RemoveCountry = Array.Empty<string>();
        RemoveProvince = Array.Empty<string>();
    }
}
