using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;

namespace LINGYUN.Abp.Gdpr;

/// <summary>
/// Represents a GDPR request made by the user
/// See: https://abp.io/docs/latest/modules/gdpr#gdprrequest
/// </summary>
public class GdprRequest : AggregateRoot<Guid>, IHasCreationTime
{
    ///// TODO: Abp Framework GDPR module does not support multi-tenancy
    ///// <summary>
    ///// TenantId
    ///// </summary>
    ///// public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// The ID of the user making the request
    /// </summary>
    public virtual Guid UserId { get; protected set; }
    /// <summary>
    /// Creation time
    /// </summary>
    public virtual DateTime CreationTime { get; protected set; }
    /// <summary>
    /// The end time of the data preparation process
    /// </summary>
    public virtual DateTime ReadyTime { get; protected set; }
    /// <summary>
    /// User personal data collected
    /// </summary>
    public virtual ICollection<GdprInfo> Infos { get; protected set; }
    protected GdprRequest()
    {
        Infos = new Collection<GdprInfo>();
    }

    public GdprRequest(
        Guid id, 
        Guid userId,
        DateTime readyTime)
      : base(id)
    {
        UserId = userId;
        ReadyTime = readyTime;
        Infos = new Collection<GdprInfo>();
    }

    public void AddData(
        IGuidGenerator guidGenerator, 
        string data, 
        string provider)
    {
        Infos.Add(
            new GdprInfo(
                guidGenerator.Create(), 
                Id, 
                data,
                provider));
    }
}
