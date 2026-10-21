using Volo.Abp.Data;

namespace LINGYUN.Abp.TaskManagement;

public abstract class BackgroundJobActionCreateOrUpdateDto
{
    /// <summary>
    /// Whether to enable
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Parameters
    /// </summary>
    public ExtraPropertyDictionary Paramters { get; set; }

    public BackgroundJobActionCreateOrUpdateDto()
    {
        Paramters = new ExtraPropertyDictionary();
    }
}
