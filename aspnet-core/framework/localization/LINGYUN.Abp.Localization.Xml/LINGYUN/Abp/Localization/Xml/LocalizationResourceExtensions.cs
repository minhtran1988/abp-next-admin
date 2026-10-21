using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Localization;

namespace LINGYUN.Abp.Localization.Xml;

public static class LocalizationResourceExtensions
{
    /// <summary>
    /// Add Xml virtual file system support
    /// </summary>
    /// <param name="localizationResource"></param>
    /// <param name="virtualPath"></param>
    /// <returns></returns>
    public static LocalizationResource AddVirtualXml(
        [NotNull] this LocalizationResource localizationResource,
        [NotNull] string virtualPath)
    {
        Check.NotNull(localizationResource, nameof(localizationResource));
        Check.NotNull(virtualPath, nameof(virtualPath));

        localizationResource.Contributors.Add(new XmlVirtualFileLocalizationResourceContributor(
            virtualPath.EnsureStartsWith('/')
        ));

        return localizationResource;
    }
    /// <summary>
    /// Add Xml local disk file support
    /// </summary>
    /// <param name="localizationResource"></param>
    /// <param name="xmlFilePath"></param>
    /// <returns></returns>
    public static LocalizationResource AddPhysicalXml(
        [NotNull] this LocalizationResource localizationResource,
        [NotNull] string xmlFilePath)
    {
        Check.NotNull(localizationResource, nameof(localizationResource));
        Check.NotNull(xmlFilePath, nameof(xmlFilePath));

        localizationResource.Contributors.Add(new XmlPhysicalFileLocalizationResourceContributor(xmlFilePath));

        return localizationResource;
    }
}
