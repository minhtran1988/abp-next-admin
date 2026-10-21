using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace LINGYUN.Abp.Account;

public class PhoneResetPasswordDto
{
    [Required]
    [Phone]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
    // If the Dto attribute is inconsistent with the localized content, you need to specify the localized name as follows
    // [Display(Name = "DisplayName:RequiredPhoneNumber")] //The json localization file must have the same format: DisplayName:RequiredPhoneNumber
    //[DisplayName("DisplayName:RequiredPhoneNumber")] //Both methods are available
    // If the Dto attribute is consistent with the localized content, there is no need to display the specified name, but the localized file must have text in the corresponding format: DisplayName:PhoneNumber
    public string PhoneNumber { get; set; } = default!;

    [Required]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPasswordLength))]
    [DataType(DataType.Password)]
    [DisableAuditing]
    public string NewPassword { get; set; } = default!;

    [Required]
    [StringLength(6)]
    [DisableAuditing]
    [Display(Name = "DisplayName:SmsVerifyCode")]
    public string Code { get; set; } = default!;
}
