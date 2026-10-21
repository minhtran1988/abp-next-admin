using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.OpenObserve.Models.Function;

/// <summary>
/// Functional language type
/// </summary>
/// <remarks>
/// Request body field transType, 0 = VRL, 1 = JavaScript
/// </remarks>
public enum FunctionTransType
{
    Vrl = 0,
    JavaScript = 1,
}
