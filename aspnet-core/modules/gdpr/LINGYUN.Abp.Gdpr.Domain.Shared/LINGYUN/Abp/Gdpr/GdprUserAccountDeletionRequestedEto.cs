using System;

namespace LINGYUN.Abp.Gdpr;

/// <summary>
/// User account deletion request event transfer object
/// </summary>
[Serializable]
public class GdprUserAccountDeletionRequestedEto
{
    public Guid UserId { get; set; }
}
