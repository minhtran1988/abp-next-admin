using System.ComponentModel;

namespace LINGYUN.Platform.Feedbacks;
public enum FeedbackStatus
{
    /// <summary>
    /// Add
    /// </summary>
    [Description("FeedbackStatus:Created")]
    Created = 1,
    /// <summary>
    /// Processing
    /// </summary>
    [Description("FeedbackStatus:InProgress")]
    InProgress = 2,
    /// <summary>
    /// Closed
    /// </summary>
    [Description("FeedbackStatus:Closed")]
    Closed = 3,
    /// <summary>
    /// Resolved
    /// </summary>
    [Description("FeedbackStatus:Resolved")]
    Resolved = 4
}
