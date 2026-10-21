using System;
using System.Diagnostics;

namespace LINGYUN.Abp.SystemInfo.Utils;

public class MemoryMetrics
{
    /// <summary>
    /// Total physical memory
    /// </summary>
    public long TotalMemory { get; set; }
    /// <summary>
    /// Working set memory
    /// </summary>
    public long WorkingSet { get; set; }
    /// <summary>
    /// Private memory
    /// </summary>
    public long PrivateMemory { get; set; }
    /// <summary>
    /// virtual memory
    /// </summary>
    public long VirtualMemory { get; set; }
    /// <summary>
    /// GC heap size
    /// </summary>
    public long GCHeapSize { get; set; }
}

public class MemoryMonitor
{
    // Convert to MB
    const long MB = 1024 * 1024;

    public MemoryMetrics GetMemoryMetrics()
    {
        var process = Process.GetCurrentProcess();

        return new MemoryMetrics
        {
            TotalMemory = GC.GetTotalMemory(false) / MB,  
            WorkingSet = process.WorkingSet64 / MB,
            PrivateMemory = process.PrivateMemorySize64 / MB,
            VirtualMemory = process.VirtualMemorySize64 / MB,
            GCHeapSize = GetGCHeapSize() / MB
        };
    }

    private long GetGCHeapSize()
    {
        var gcInfo = GC.GetGCMemoryInfo();
        return (long)gcInfo.HeapSizeBytes;
    }
}
