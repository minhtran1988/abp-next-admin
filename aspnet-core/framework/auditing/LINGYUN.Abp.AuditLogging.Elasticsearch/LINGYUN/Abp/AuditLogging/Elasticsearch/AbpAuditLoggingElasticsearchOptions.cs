using Elastic.Clients.Elasticsearch.IndexManagement;

namespace LINGYUN.Abp.AuditLogging.Elasticsearch;

public class AbpAuditLoggingElasticsearchOptions
{
    public const string DefaultIndexPrefix = "auditlogging";
    public string IndexPrefix { get; set; }
    /// <summary>
    /// Index scrolling interval
    /// </summary>
    public RollingInterval RollingInterval { get; set; }
    /// <summary>
    /// Index initialization failed and an exception was thrown.
    /// </summary>
    /// <remarks>
    /// Default: true, the application stops running after index initialization fails
    /// </remarks>
    public bool ThrowIfIndexInitFailed { get; set; }
    /// <summary>
    /// Audit log index settings
    /// </summary>
    public IndexSettings AuditLogSettings { get; set; }
    /// <summary>
    /// Security log index settings
    /// </summary>
    public IndexSettings SecurityLogSettings { get; set; }

    public AbpAuditLoggingElasticsearchOptions()
    {
        IndexPrefix = DefaultIndexPrefix;
        ThrowIfIndexInitFailed = true;
        RollingInterval = RollingInterval.Year;

        AuditLogSettings = new IndexSettings()
        {
            NumberOfReplicas = 1,
            NumberOfShards = 3,
            Mapping = new MappingLimitSettings
            {
                TotalFields = new MappingLimitSettingsTotalFields
                {
                    Limit = 1000,
                },
                NestedFields = new MappingLimitSettingsNestedFields
                {
                    Limit = 50,
                },
                Depth = new MappingLimitSettingsDepth
                {
                    Limit = 10,
                },
            }
        };

        SecurityLogSettings = new IndexSettings();
    }
}
