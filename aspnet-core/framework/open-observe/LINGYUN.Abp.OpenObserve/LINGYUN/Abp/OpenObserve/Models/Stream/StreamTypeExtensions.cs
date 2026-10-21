using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Stream;

public static class StreamTypeExtensions
{
    /// <summary>
    /// Convert to the string value required by the interface
    /// </summary>
    public static string AsString(this StreamType streamType)
    {
        return streamType switch
        {
            StreamType.Metrics => OpenObserveConstants.StreamTypes.Metrics,
            StreamType.Traces => OpenObserveConstants.StreamTypes.Traces,
            _ => OpenObserveConstants.StreamTypes.Logs,
        };
    }
}
