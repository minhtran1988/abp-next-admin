using LINGYUN.Abp.WeChat.Common.Messages;
using System.Collections.Generic;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Template card event push
/// </summary>
[EventName("template_card_event")]
public class TemplateCardPushEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// The same as the task_id specified when sending the template card message
    /// </summary>
    [XmlElement("TaskId")]
    public string TaskId { get; set; } = default!;
    /// <summary>
    /// The type of universal template card, the types are
    /// "text_notice", 
    /// "news_notice",
    /// "button_interaction",
    /// "vote_interaction", 
    /// "multiple_interaction"
    /// Five kinds
    /// </summary>
    [XmlElement("CardType")]
    public string CardType { get; set; } = default!;
    /// <summary>
    /// ResponseCode used to call the update card interface, valid within 72 hours, and can only be used once
    /// </summary>
    [XmlElement("ResponseCode")]
    public string ResponseCode { get; set; } = default!;
    /// <summary>
    /// The same as the button btn:key value specified when sending the template card message
    /// </summary>
    [XmlElement("EventKey")]
    public string EventKey { get; set; } = default!;
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("SelectedItems")]
    public TemplateCardQuestionInfo QuestionInfo { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<TemplateCardPushEvent>(this);
    }
}

public class TemplateCardQuestionInfo
{
    /// <summary>
    /// Event KEY value
    /// </summary>
    [XmlElement("SelectedItem")]
    public List<TemplateCardQuestion>? Items { get; set; }
}

public class TemplateCardQuestion
{
    /// <summary>
    /// The key value of the question
    /// </summary>
    [XmlElement("QuestionKey")]
    public string QuestionKey { get; set; } = default!;
    /// <summary>
    /// The key value of the question
    /// </summary>
    [XmlArray("OptionIds")]
    public List<TemplateCardQuestionOption>? OptionIds { get; set; }
}

public class TemplateCardQuestionOption
{
    /// <summary>
    /// The key value of the question
    /// </summary>
    [XmlElement("OpitonId")]
    public List<string>? Items { get; set; }
}
