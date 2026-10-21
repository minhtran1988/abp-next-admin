using System.Threading.Tasks;

namespace LINGYUN.Abp.BackgroundTasks;

/// <summary>
/// The task class needs to implement this interface
/// </summary>
public interface IJobRunnable
{
    Task ExecuteAsync(JobRunnableContext context);
}
