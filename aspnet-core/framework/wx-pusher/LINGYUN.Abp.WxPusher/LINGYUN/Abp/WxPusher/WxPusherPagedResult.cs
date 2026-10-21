using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.WxPusher;

[Serializable]
public class WxPusherPagedResult<T>
{
    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }
    /// <summary>
    /// Current page number
    /// </summary>
    [JsonProperty("page")]
    public int Page { get; set; }
    /// <summary>
    /// Page size
    /// </summary>
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }
    /// <summary>
    /// Record list
    /// </summary>
    [JsonProperty("records")]
    public List<T> Records { get; set; } = new List<T>();
}
