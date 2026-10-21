using System;
using System.Collections.Generic;
using System.Text;

namespace LINGYUN.Abp.OpenObserve.Utils;

/// <summary>
/// OpenObserve query string construction
/// </summary>
internal static class OpenObserveQueryString
{
    /// <summary>
    /// Construct query string
    /// </summary>
    /// <returns>Such as "?type=logs&delete_all=true", returns an empty string if there are no parameters</returns>
    public static string Build(params (string Name, object? Value)[] parameters)
    {
        var builder = new StringBuilder();

        foreach (var (name, value) in parameters)
        {
            if (value == null)
            {
                continue;
            }

            var text = value switch
            {
                bool boolean => boolean ? "true" : "false",
                _ => value.ToString(),
            };

            if (text.IsNullOrWhiteSpace())
            {
                continue;
            }

            builder.Append(builder.Length == 0 ? '?' : '&');
            builder.Append(name);
            builder.Append('=');
            builder.Append(Uri.EscapeDataString(text!));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Construct query string
    /// </summary>
    public static string Build(IDictionary<string, object?> parameters)
    {
        var builder = new StringBuilder();

        foreach (var (name, value) in parameters)
        {
            if (value == null)
            {
                continue;
            }

            var text = value switch
            {
                bool boolean => boolean ? "true" : "false",
                _ => value.ToString(),
            };

            if (text.IsNullOrWhiteSpace())
            {
                continue;
            }

            builder.Append(builder.Length == 0 ? '?' : '&');
            builder.Append(name);
            builder.Append('=');
            builder.Append(Uri.EscapeDataString(text!));
        }

        return builder.ToString();
    }
}
