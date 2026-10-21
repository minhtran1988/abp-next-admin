using System.ComponentModel.DataAnnotations;

namespace LINGYUN.Abp.Account.Dto;
/// <summary>
/// Associated user Dto
/// </summary>
public class LinkUserInput : LinkUserBaseDto
{
    /// <summary>
    /// Associated user Token
    /// </summary>
    [Required]
    public string Token { get; set; } = default!;
}
