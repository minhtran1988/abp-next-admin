using System;

namespace LINGYUN.Abp.SystemInfo.Utils;

public class GCMetrics
{
    /// <summary>
    /// Gen0 recycling times
    /// </summary>
    public int Gen0Collections { get; set; }
    /// <summary>
    /// Gen1 recycling times
    /// </summary>
    public int Gen1Collections { get; set; }
    /// <summary>
    /// Gen2 recycling times
    /// </summary>
    public int Gen2Collections { get; set; }
    /// <summary>
    /// Total available memory
    /// </summary>
    public long TotalMemory { get; set; }
    /// <summary>
    /// Memory load
    /// </summary>
    public long MemoryLoad { get; set; }
    /// <summary>
    /// Memory fragment size
    /// </summary>
    public long FragmentedBytes { get; set; }
}

public class GCMonitor
{
    private int _lastGen0;
    private int _lastGen1;
    private int _lastGen2;

    public GCMetrics GetGCMetrics()
    {
        var currentGen0 = GC.CollectionCount(0);
        var currentGen1 = GC.CollectionCount(1);
        var currentGen2 = GC.CollectionCount(2);

        var metrics = new GCMetrics
        {
            Gen0Collections = currentGen0 - _lastGen0,
            Gen1Collections = currentGen1 - _lastGen1,
            Gen2Collections = currentGen2 - _lastGen2
        };

        var gcInfo = GC.GetGCMemoryInfo();
        metrics.TotalMemory = gcInfo.TotalAvailableMemoryBytes;
        metrics.MemoryLoad = gcInfo.MemoryLoadBytes;
        metrics.FragmentedBytes = gcInfo.FragmentedBytes;

        _lastGen0 = currentGen0;
        _lastGen1 = currentGen1;
        _lastGen2 = currentGen2;

        return metrics;
    }
}
