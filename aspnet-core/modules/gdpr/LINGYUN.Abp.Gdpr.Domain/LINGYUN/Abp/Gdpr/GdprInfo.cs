using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace LINGYUN.Abp.Gdpr;

/// <summary>
/// Represents the user's personal data
/// See: https://abp.io/docs/latest/modules/gdpr#gdprinfo
/// </summary>
public class GdprInfo : Entity<Guid>
{
    /// <summary>
    /// ID of GDPR request
    /// </summary>
    public virtual Guid RequestId { get; protected set; }

    /// <summary>
    /// Used to store personal data
    /// </summary>
    public virtual string Data { get; protected set; } = default!;

    /// <summary>
    /// Represents the module that collects personal data
    /// </summary>
    public virtual string Provider { get; protected set; } = default!;

    protected GdprInfo()
    {
    }

    public GdprInfo(
        Guid id,
        Guid requestId,
        string data, 
        string provider)
      : base(id)
    {
        RequestId = requestId;
        Data = Check.NotNullOrWhiteSpace(data, nameof(data));
        Provider = Check.NotNullOrWhiteSpace(provider, nameof(provider), GdprInfoConsts.MaxProviderLength);
    }
}
