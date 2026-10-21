using System.ComponentModel.DataAnnotations;

namespace LINGYUN.Abp.Account.Dto;
/// <summary>
/// Verify associated user TokenDto
/// </summary>
public class VerifyLinkTokenInput : LinkUserBaseDto
{
    /// <summary>
    /// Associated user Token
    /// </summary>
    [Required]
    public string Token { get; set; } = default!;
}
