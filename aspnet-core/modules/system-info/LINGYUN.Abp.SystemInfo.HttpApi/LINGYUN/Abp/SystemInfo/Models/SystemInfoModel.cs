namespace LINGYUN.Abp.SystemInfo.Models;

public class SystemInfoModel
{
    /// <summary>
    /// Component state collection
    /// </summary>
    public ComponentInfoModel[] Components { get; set; } = default!;
}
