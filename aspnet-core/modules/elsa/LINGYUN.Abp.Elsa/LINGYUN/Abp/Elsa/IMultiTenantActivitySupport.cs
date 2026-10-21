using System;

namespace LINGYUN.Abp.Elsa;
/// <summary>
/// Multi-tenant support
/// </summary>
/// <remarks>
/// Is elsa compatible with abp, it is better to specify multi-tenant fields
/// </remarks>
public interface IMultiTenantActivitySupport
{
    Guid? TenantId { get; set; }
}
