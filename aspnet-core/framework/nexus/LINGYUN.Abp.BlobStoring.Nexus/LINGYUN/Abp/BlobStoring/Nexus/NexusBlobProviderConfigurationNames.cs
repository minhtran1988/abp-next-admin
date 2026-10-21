namespace LINGYUN.Abp.BlobStoring.Nexus;
public static class NexusBlobProviderConfigurationNames
{
    /// <summary>
    /// Base path
    /// </summary>
    public const string BasePath = "Sonatype:Nexus:Raw:BasePath";
    /// <summary>
    /// Add container name to base path
    /// </summary>
    public const string AppendContainerNameToBasePath = "Sonatype:Nexus:Raw:AppendContainerNameToBasePath";
    /// <summary>
    /// Nexus raw warehouse name
    /// </summary>
    public const string Repository = "Sonatype:Nexus:Raw:Repository";
}
