using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Report geographic location events
/// </summary>
[EventName("reporting_geo_location")]
public class ReportingGeoLocationEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Geographical location latitude
    /// </summary>
    [XmlElement("Latitude")]
    public double Latitude { get; set; }
    /// <summary>
    /// Geographical location longitude
    /// </summary>
    [XmlElement("Longitude")]
    public double Longitude { get; set; }
    /// <summary>
    /// Geographical location accuracy
    /// </summary>
    [XmlElement("Precision")]
    public double Precision { get; set; }
    /// <summary>
    /// For app type, wxwork is always returned in enterprise WeChat, but this field is not returned in WeChat.
    /// </summary>
    [XmlElement("AppType")]
    public string? AppType { get; set; }
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ReportingGeoLocationEvent>(this);
    }
}
