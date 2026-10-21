using System.Collections.Generic;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;

[XmlRoot("Item")]
public class MemberExtendAttribute
{
    /// <summary>
    /// Extended attributes
    /// </summary>
    [XmlElement("Item")]
    public List<MemberExtend>? Items { get;set; }
}

public class MemberExtend
{
    /// <summary>
    /// Extended attribute type: 0-text 1-web page
    /// </summary>
    [XmlElement("Type")]
    public byte Type { get; set; }
    /// <summary>
    /// Extended attribute name
    /// </summary>
    [XmlElement("Name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// Text attribute content
    /// </summary>
    [XmlElement("Text", IsNullable = true)]
    public MemberTextExtend? Text { get; set; }
    /// <summary>
    /// Web attribute content
    /// </summary>
    [XmlElement("Web", IsNullable = true)]
    public MemberWebExtend? Web { get; set; }
}

/// <summary>
/// Text attribute type, fill in when the extended attribute type is 0
/// </summary>
[XmlRoot("Text")]
public class MemberTextExtend 
{
    /// <summary>
    /// Text attribute content
    /// </summary>
    [XmlElement("Value")]
    public string Value { get; set; } = default!;
}
/// <summary>
/// Web page type attribute, fill in when the extended attribute type is 1
/// </summary>
[XmlRoot("Web")]
public class MemberWebExtend
{
    /// <summary>
    /// Display title of the web page
    /// </summary>
    [XmlElement("Title")]
    public string Title { get; set; } = default!;
    /// <summary>
    /// URL of the web page
    /// </summary>
    [XmlElement("Url")]
    public string Url { get; set; } = default!;
}
