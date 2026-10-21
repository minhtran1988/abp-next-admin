using System;
using Volo.Abp.Validation;

namespace LINGYUN.Platform.Portal;

public abstract class EnterpriseCreateOrUpdateDto 
{
    /// <summary>
    /// Associated tenants
    /// </summary>
    public Guid? TenantId { get; set; }
    /// <summary>
    /// English name
    /// </summary>
    [DynamicStringLength(typeof(EnterpriseConsts), nameof(EnterpriseConsts.MaxEnglishNameLength))]
    public string? EnglishName { get; set; }
    /// <summary>
    /// Logo
    /// </summary>
    [DynamicStringLength(typeof(EnterpriseConsts), nameof(EnterpriseConsts.MaxLogoLength))]
    public string? Logo { get; set; }
    /// <summary>
    /// address
    /// </summary>
    [DynamicStringLength(typeof(EnterpriseConsts), nameof(EnterpriseConsts.MaxAddressLength))]
    public string? Address { get; set; }
    /// <summary>
    /// Legal representative
    /// </summary>
    [DynamicStringLength(typeof(EnterpriseConsts), nameof(EnterpriseConsts.MaxLegalManLength))]
    public string? LegalMan { get; set; }
    /// <summary>
    /// Tax registration number
    /// </summary>
    [DynamicStringLength(typeof(EnterpriseConsts), nameof(EnterpriseConsts.MaxTaxCodeLength))]
    public string? TaxCode { get; set; }
    /// <summary>
    /// Organization code
    /// </summary>
    [DynamicStringLength(typeof(EnterpriseConsts), nameof(EnterpriseConsts.MaxOrganizationCodeLength))]
    public string? OrganizationCode { get; set; }
    /// <summary>
    /// Registration code
    /// </summary>
    [DynamicStringLength(typeof(EnterpriseConsts), nameof(EnterpriseConsts.MaxRegistrationCodeLength))]
    public string? RegistrationCode { get; set; }
    /// <summary>
    /// Registration date
    /// </summary>
    public DateTime? RegistrationDate { get; set; }
    /// <summary>
    /// Expiration date
    /// </summary>
    public DateTime? ExpirationDate { get; set; }
}
