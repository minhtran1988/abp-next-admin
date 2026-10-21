using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;

namespace LINGYUN.Abp.Exporter.MagicodesIE.Excel;
public class AbpExporterMagicodesIEExcelOptions
{
    /// <summary>
    /// Data export settings
    /// </summary>
    public IDictionary<Type, Action<ExcelExporterAttribute>> ExportSettingMapping { get; }
    /// <summary>
    /// Data import settings
    /// </summary>
    public IDictionary<Type, Action<ExcelImporterAttribute>> ImportSettingMapping { get; }
    public AbpExporterMagicodesIEExcelOptions()
    {
        ExportSettingMapping = new Dictionary<Type, Action<ExcelExporterAttribute>>();
        ImportSettingMapping = new Dictionary<Type, Action<ExcelImporterAttribute>>();
    }

    public void MapExportSetting(Type dataType, Action<ExcelExporterAttribute> exportSettingsSetup)
    {
        ExportSettingMapping[dataType] = exportSettingsSetup;
    }

    public void MapImportSetting(Type dataType, Action<ExcelImporterAttribute> importSettingsSetup)
    {
        ImportSettingMapping[dataType] = importSettingsSetup;
    }
}
