using Newtonsoft.Json;
using System.Collections.Generic;

namespace LINGYUN.Abp.PushPlus;

public class PushPlusPagedResponse<T>
{
    /// <summary>
    /// Current page number
    /// </summary>
    [JsonProperty("pageNum")]
    public int PageNum { get; set; }
    /// <summary>
    /// Page size
    /// </summary>
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }
    /// <summary>
    /// Total number of rows
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }
    /// <summary>
    /// Total number of pages
    /// </summary>
    [JsonProperty("pages")]
    public int Pages { get; set; }
    /// <summary>
    /// Message list
    /// </summary>
    [JsonProperty("list")]
    public List<T> Items { get; set; }

    public PushPlusPagedResponse()
    {
        Items = new List<T>();
    }
}
