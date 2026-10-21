using System;
using System.Collections.Generic;
using System.Net;
using Volo.Abp.Collections;

namespace LINGYUN.Abp.Wrapper;

public class AbpWrapperOptions
{
    /// <summary>
    /// Unhandled exception code
    /// Default: 500
    /// </summary>
    public string CodeWithUnhandled { get; set; }
    /// <summary>
    /// Whether to enable the wrapper
    /// Default: false
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Return code on success
    /// Default: 0
    /// </summary>
    public string CodeWithSuccess { get; set; }
    /// <summary>
    /// Whether to prompt an error when the resource is empty
    /// Default: false
    /// </summary>
    public bool ErrorWithEmptyResult { get; set; }
    /// <summary>
    /// Whether to enable 401 error packaging
    /// Default: false
    /// </summary>
    public bool IsWrapUnauthorizedEnabled { get; set; }
    /// <summary>
    /// Return code when the resource is empty
    /// Default: 404
    /// </summary>
    public Func<IServiceProvider, string> CodeWithEmptyResult { get; set; }
    /// <summary>
    /// Return an error message when the resource is empty
    /// </summary>
    public Func<IServiceProvider, string> MessageWithEmptyResult { get; set; }
    /// <summary>
    /// Return status code after packaging
    /// Default: 200 HttpStatusCode.OK
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }
    /// <summary>
    /// Ignore the URL starting type
    /// </summary>
    public IList<string> IgnorePrefixUrls { get; }
    /// <summary>
    /// Ignore the specified namespace
    /// </summary>
    public IList<string> IgnoreNamespaces { get; }
    /// <summary>
    /// Ignore controller
    /// </summary>
    public ITypeList IgnoreControllers { get; }
    /// <summary>
    /// Ignore return value
    /// </summary>
    public ITypeList IgnoreReturnTypes { get; }
    /// <summary>
    /// Ignore exception
    /// </summary>
    public ITypeList<Exception> IgnoreExceptions { get; }
    /// <summary>
    /// Ignore interface type
    /// </summary>
    public ITypeList IgnoredInterfaces { get; }

    internal IDictionary<Type, IExceptionWrapHandler> ExceptionHandles { get; }

    public AbpWrapperOptions()
    {
        CodeWithUnhandled = "500";
        CodeWithSuccess = "0";
        HttpStatusCode = HttpStatusCode.OK;
        ErrorWithEmptyResult = false;

        IgnorePrefixUrls = new List<string>();
        IgnoreNamespaces = new List<string>();

        IgnoreControllers = new TypeList();
        IgnoreReturnTypes = new TypeList();
        IgnoredInterfaces = new TypeList()
        {
            typeof(IWrapDisabled)
        };
        IgnoreExceptions = new TypeList<Exception>();

        CodeWithEmptyResult = (_) => "404";
        MessageWithEmptyResult = (_) => "Not Found";

        ExceptionHandles = new Dictionary<Type, IExceptionWrapHandler>();
    }

    public void AddHandler<TException>(IExceptionWrapHandler handler)
        where TException : Exception
    {
        AddHandler(typeof(TException), handler);
    }

    public void AddHandler(Type exceptionType, IExceptionWrapHandler handler)
    {
        ExceptionHandles[exceptionType] = handler;
    }

    public IExceptionWrapHandler? GetHandler(Type exceptionType)
    {
        ExceptionHandles.TryGetValue(exceptionType, out var handler);

        return handler;
    }
}
