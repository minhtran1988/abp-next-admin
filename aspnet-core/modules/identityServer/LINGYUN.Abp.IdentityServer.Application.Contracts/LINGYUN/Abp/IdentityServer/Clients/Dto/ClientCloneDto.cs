using System.ComponentModel.DataAnnotations;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.Validation;

namespace LINGYUN.Abp.IdentityServer.Clients;

public class ClientCloneDto
{
    /// <summary>
    /// Client ID
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(ClientConsts), nameof(ClientConsts.ClientIdMaxLength))]
    public string ClientId { get; set; } = default!;
    /// <summary>
    /// Client name
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(ClientConsts), nameof(ClientConsts.ClientNameMaxLength))]
    public string ClientName { get; set; } = default!;
    /// <summary>
    /// Description
    /// </summary>
    [DynamicStringLength(typeof(ClientConsts), nameof(ClientConsts.DescriptionMaxLength))]
    public string? Description { get; set; }
    /// <summary>
    /// Copy client authorization type
    /// </summary>
    public bool CopyAllowedGrantType { get; set; }
    /// <summary>
    /// Copy client redirect Uri
    /// </summary>
    public bool CopyRedirectUri { get; set; }
    /// <summary>
    /// Copy client scope
    /// </summary>
    public bool CopyAllowedScope { get; set; }
    /// <summary>
    /// Copy client statement
    /// </summary>
    public bool CopyClaim { get; set; }
    /// <summary>
    /// Copy client key
    /// </summary>
    public bool CopySecret { get; set; }
    /// <summary>
    /// Copy client cross-domain source
    /// </summary>
    public bool CopyAllowedCorsOrigin { get; set; }
    /// <summary>
    /// Copy client logout redirect Uri
    /// </summary>
    public bool CopyPostLogoutRedirectUri { get; set; }
    /// <summary>
    /// Copy client properties
    /// </summary>
    public bool CopyPropertie { get; set; }
    /// <summary>
    /// Copy client IdP restrictions
    /// </summary>
    public bool CopyIdentityProviderRestriction { get; set; }
    public ClientCloneDto()
    {
        CopyAllowedCorsOrigin = true;
        CopyAllowedGrantType = true;
        CopyAllowedScope = true;
        CopyClaim = true;
        CopyIdentityProviderRestriction = true;
        CopyPostLogoutRedirectUri = true;
        CopyPropertie = true;
        CopyRedirectUri = true;
        CopySecret = true;
    }
}
