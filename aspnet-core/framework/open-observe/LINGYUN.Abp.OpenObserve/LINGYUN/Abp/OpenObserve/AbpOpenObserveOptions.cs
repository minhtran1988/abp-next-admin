using LINGYUN.Abp.OpenObserve.Models;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace LINGYUN.Abp.OpenObserve;

public class AbpOpenObserveOptions
{
    /// <summary>
    /// Service address
    /// </summary>
    /// <remarks>
    /// Default: http://localhost:5080
    /// </remarks>
    public string Endpoint { get; set; }
    /// <summary>
    /// Default organization name
    /// </summary>
    /// <remarks>
    /// Default: default, used when the organization is not explicitly specified when calling the interface
    /// </remarks>
    public string Organization { get; set; }
    /// <summary>
    /// Username
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// Password
    /// </summary>
    public string? Password { get; set; }
    /// <summary>
    /// Complete authorization header information
    /// </summary>
    /// <remarks>
    /// For example: "Basic YWRtaW5AYWJwLmlvOnd3MVo1TCU2"
    /// After configuration, it takes precedence over UserName/Password
    /// </remarks>
    public string? AccessToken { get; set; }
    /// <summary>
    /// Request timeout (seconds)
    /// </summary>
    /// <remarks>
    /// Default: 600
    /// </remarks>
    public int TimeoutSeconds { get; set; }

    public AbpOpenObserveOptions()
    {
        Endpoint = OpenObserveConstants.DefaultEndpoint;
        Organization = OpenObserveConstants.DefaultOrganization;
        TimeoutSeconds = OpenObserveConstants.DefaultTimeoutSeconds;
    }

    /// <summary>
    /// Create authorization header
    /// </summary>
    /// <returns>Returns null if credentials are not configured</returns>
    public virtual AuthenticationHeaderValue? CreateAuthorizationHeader()
    {
        if (!AccessToken.IsNullOrWhiteSpace() &&
            AuthenticationHeaderValue.TryParse(AccessToken, out var authenticationHeader))
        {
            return authenticationHeader;
        }

        if (!UserName.IsNullOrWhiteSpace() && !Password.IsNullOrWhiteSpace())
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{UserName}:{Password}"));

            return new AuthenticationHeaderValue("Basic", token);
        }

        return null;
    }
}
