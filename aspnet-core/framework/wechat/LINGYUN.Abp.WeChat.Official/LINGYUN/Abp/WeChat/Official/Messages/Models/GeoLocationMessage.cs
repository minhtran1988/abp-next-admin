using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Official.Messages.Models;
/// <summary>
/// Geolocation message
/// </summary>
[EventName("geo_location")]
public class GeoLocationMessage : WeChatOfficialGeneralMessage
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
    public string Label { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatOfficialGeneralMessageEto<GeoLocationMessage>(this);
    }
}
