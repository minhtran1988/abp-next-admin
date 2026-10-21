using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LINGYUN.Abp.DataProtection;
public class AbpDataProtectionOptions
{
    /// <summary>
    /// Whether to enable data auditing
    /// Default: true
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Data permission policy
    /// </summary>
    public IList<IDataAccessStrategyContributor> StrategyContributors { get; }
    /// <summary>
    /// Authorization subject
    /// </summary>
    public IList<IDataAccessSubjectContributor> SubjectContributors { get; }
    /// <summary>
    /// Filter field keywords
    /// </summary>
    public IDictionary<string, IDataAccessKeywordContributor> KeywordContributors { get; }
    /// <summary>
    /// Data operations
    /// </summary>
    public IDictionary<DataAccessFilterOperate, IDataAccessOperateContributor> OperateContributors { get; }
    /// <summary>
    /// Default entity filtering
    /// </summary>
    public IDictionary<Type, Func<IServiceProvider, Type, DataAccessOperation, LambdaExpression>> DefaultEntityFilters { get; }
    /// <summary>
    /// Entity ignores fields
    /// </summary>
    public IDictionary<Type, string[]> EntityIgnoreProperties { get; }
    /// <summary>
    /// Globally ignore the field list (do not participate in field-level permission verification)
    /// </summary>
    public IList<string> GlobalIgnoreProperties { get; set; }
    /// <summary>
    /// Audit field list (these fields are written into the attribute list of the protected entity for front-end filtering)
    /// </summary>
    public IList<string> AuditedObjectProperties { get; set; }
    public AbpDataProtectionOptions()
    {
        IsEnabled = true;
        SubjectContributors = new List<IDataAccessSubjectContributor>();
        StrategyContributors = new List<IDataAccessStrategyContributor>();
        KeywordContributors = new Dictionary<string, IDataAccessKeywordContributor>();
        OperateContributors = new Dictionary<DataAccessFilterOperate, IDataAccessOperateContributor>();
        DefaultEntityFilters = new Dictionary<Type, Func<IServiceProvider, Type, DataAccessOperation, LambdaExpression>>();
        EntityIgnoreProperties = new Dictionary<Type, string[]>();

        GlobalIgnoreProperties = new List<string>
        {
            nameof(IEntity<Guid>.Id),
            nameof(IAuditedObject.LastModifierId),
            nameof(IAuditedObject.LastModificationTime),
            nameof(IAuditedObject.CreatorId),
            nameof(IAuditedObject.CreationTime),
            nameof(IDeletionAuditedObject.IsDeleted),
            nameof(IDeletionAuditedObject.DeleterId),
            nameof(IDeletionAuditedObject.DeletionTime),
            nameof(IMultiTenant.TenantId),
            nameof(IHasEntityVersion.EntityVersion),
            nameof(IHasConcurrencyStamp.ConcurrencyStamp),
            nameof(IHasExtraProperties.ExtraProperties),
        };

        AuditedObjectProperties = new List<string>
        {
            nameof(IAuditedObject.LastModifierId),
            nameof(IAuditedObject.LastModificationTime),
            nameof(IAuditedObject.CreatorId),
            nameof(IAuditedObject.CreationTime),
        };
    }
}
