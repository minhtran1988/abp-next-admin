using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.ExceptionHandling.Emailing;

public class AbpEmailExceptionHandlingOptions
{
    /// <summary>
    /// Send stack information
    /// </summary>
    public bool SendStackTrace { get; set; } = false;
    /// <summary>
    /// Default email title
    /// </summary>
    public string? DefaultTitle { get; set; }
    /// <summary>
    /// Default email content header
    /// </summary>
    public string? DefaultContentHeader { get; set; }
    /// <summary>
    /// Default email content
    /// </summary>
    public string? DefaultContentFooter { get; set; }
    /// <summary>
    /// Default exception recipient
    /// </summary>
    public string? DefaultReceiveEmail { get; set; }
    /// <summary>
    /// The exception type specifies the recipient processing mapping list
    /// </summary>
    public IDictionary<Type, string> Handlers { get; set; }
    public AbpEmailExceptionHandlingOptions()
    {
        Handlers = new Dictionary<Type, string>();
    }

    /// <summary>
    /// Add users who need to receive exception notifications to the processing list
    /// </summary>
    /// <typeparam name="TException">Exception type handled</typeparam>
    /// <param name="receivedEmails">User categories for receiving emails, for group sending, separated by symbols</param>
    public void HandReceivedException<TException>(string receivedEmails) where TException : Exception
    {
        HandReceivedException(typeof(TException), receivedEmails);
    }
    /// <summary>
    /// Add users who need to receive exception notifications to the processing list
    /// </summary>
    /// <param name="ex">Exception type handled</param>
    /// <param name="receivedEmails">User categories for receiving emails, for group sending, separated by symbols</param>
    public void HandReceivedException(Type exceptionType, string receivedEmails)
    {
        if (Handlers.ContainsKey(exceptionType))
        {
            Handlers[exceptionType] += receivedEmails;
        }
        else
        {
            Handlers.Add(exceptionType, receivedEmails);
        }
    }

    public string? GetReceivedEmailOrDefault(Type exceptionType)
    {
        if (Handlers.TryGetValue(exceptionType, out var receivedUsers))
        {
            return receivedUsers;
        }
        return DefaultReceiveEmail;
    }
}
