using System.Diagnostics;
using System.Threading;

namespace LINGYUN.Abp.SystemInfo.Utils;

public class ThreadMetrics
{
    /// <summary>
    /// Number of available worker threads
    /// </summary>
    public int AvailableWorkerThreads { get; set; }
    /// <summary>
    /// Number of available I/O threads
    /// </summary>
    public int AvailableCompletionPortThreads { get; set; }
    /// <summary>
    /// Maximum number of worker threads
    /// </summary>
    public int MaxWorkerThreads { get; set; }
    /// <summary>
    /// Maximum number of I/O threads
    /// </summary>
    public int MaxCompletionPortThreads { get; set; }
    /// <summary>
    /// Total number of threads in the process
    /// </summary>
    public int ActiveThreads { get; set; }
    /// <summary>
    /// Number of active threads in the thread pool
    /// </summary>
    public int ThreadPoolThreadCount { get; set; }
}

public class ThreadMonitor
{
    public ThreadMetrics GetThreadMetrics()
    {
        ThreadPool.GetAvailableThreads(
            out var availableWorkerThreads,
            out var availableCompletionPortThreads);
        ThreadPool.GetMaxThreads(
            out var maxWorkerThreads,
            out var maxCompletionPortThreads);

        // Get the number of process threads (approximate number of active threads)
        var process = Process.GetCurrentProcess();
        var threads = process.Threads;

        return new ThreadMetrics
        {
            AvailableWorkerThreads = availableWorkerThreads,
            AvailableCompletionPortThreads = availableCompletionPortThreads,
            MaxWorkerThreads = maxWorkerThreads,
            MaxCompletionPortThreads = maxCompletionPortThreads,
            ActiveThreads = threads.Count,
            ThreadPoolThreadCount = ThreadPool.ThreadCount
        };
    }
}
