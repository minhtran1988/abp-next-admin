using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Pop up the event push of the geographical location selector
/// </summary>
[EventName("location_select")]
public class GeoLocationSelectPushEevent : WeChatWorkEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    /// <summary>
    /// Location information sent
    /// </summary>
    [XmlElement("SendLocationInfo")]
    public LocationInfo SendLocationInfo { get; set; } = default!;
    /// <summary>
    /// For app type, wxwork is always returned in enterprise WeChat, but this field is not returned in WeChat.
    /// </summary>
    [XmlElement("AppType")]
    public string? AppType { get; set; }

    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<GeoLocationSelectPushEevent>(this);
    }
}

public class LocationInfo
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
    /// The name of the POI, may be empty
    /// </summary>
    [XmlElement("Poiname")]
    public string? PoiName { get; set; }
}
