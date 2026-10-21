using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Geolocation message
/// </summary>
[EventName("geo_location")]
public class GeoLocationMessage : WeChatWorkGeneralMessage
{
    /// <summary>
    /// Geographical location latitude
    /// </summary>
    [XmlElement("Location_X")]
    public double Latitude { get; set; }
    /// <summary>
    /// Geographical location longitude
    /// </summary>
    [XmlElement("Location_Y")]
    public double Longitude { get; set; }
    /// <summary>
    /// Map zoom size
    /// </summary>
    [XmlElement("Scale")]
    public double Scale { get; set; }
    /// <summary>
    /// Geographical location information
    /// </summary>
    [XmlElement("Label")]
    public string? Label { get; set; }
    /// <summary>
    /// For app type, wxwork is always returned in enterprise WeChat, but this field is not returned in WeChat.
    /// </summary>
    [XmlElement("AppType")]
    public string? AppType { get; set; }
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkGeneralMessageEto<GeoLocationMessage>(this);
    }
}
