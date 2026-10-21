using System.Threading.Tasks;

namespace LINGYUN.Abp.BackgroundTasks;

/// <summary>
/// Define the task executor interface
/// Some restrictions can be implemented through it (such as distributed locks)
/// </summary>
public interface IJobRunnableExecuter
{
    Task ExecuteAsync(JobRunnableContext context);
}
