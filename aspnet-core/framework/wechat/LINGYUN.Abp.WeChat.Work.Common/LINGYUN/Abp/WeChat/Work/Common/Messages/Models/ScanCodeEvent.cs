using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// User scan event
/// </summary>
public abstract class ScanCodeEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    /// <summary>
    /// Scan information
    /// </summary>
    [XmlElement("ScanCodeInfo")]
    public ScanCodeInfo ScanCodeInfo { get; set; } = default!;
}

public class ScanCodeInfo
{
    /// <summary>
    /// Scanning type, usually qrcode
    /// </summary>
    [XmlElement("ScanType")]
    public string ScanType { get; set; } = default!;
    /// <summary>
    /// Scanning result, that is, the string information corresponding to the QR code
    /// </summary>
    [XmlElement("ScanResult")]
    public string ScanResult { get; set; } = default!;
}
