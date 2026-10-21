namespace LINGYUN.Abp.PushPlus.Message;
/// <summary>
/// Template enumeration. Use html template by default
/// </summary>
public enum PushPlusMessageTemplate
{
    /// <summary>
    /// Default template, supports html text
    /// </summary>
    Html = 0,
    /// <summary>
    /// Plain text display, no escaping html
    /// </summary>
    Text = 1,
    /// <summary>
    /// Content is displayed based on json format
    /// </summary>
    Json = 2,
    /// <summary>
    /// Content is displayed based on markdown format
    /// </summary>
    Markdown = 3,
    /// <summary>
    /// Alibaba Cloud Monitoring Alarm Customization Template
    /// </summary>
    CloudMonitor = 4,
    /// <summary>
    /// jenkins plug-in custom template
    /// </summary>
    Jenkins = 5,
    /// <summary>
    /// Router plug-in custom template
    /// </summary>
    Route = 6
}
