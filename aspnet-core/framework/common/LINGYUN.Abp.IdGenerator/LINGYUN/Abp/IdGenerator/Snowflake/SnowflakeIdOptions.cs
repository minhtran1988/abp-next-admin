namespace LINGYUN.Abp.IdGenerator.Snowflake;

public class SnowflakeIdOptions
{
    /// <summary>
    /// Machine ID
    /// </summary>
    public int WorkerId { get; set; }
    /// <summary>
    /// Machine ID length
    /// </summary>
    public int WorkerIdBits { get; set; }
    /// <summary>
    /// Data center ID
    /// </summary>
    public int DatacenterId { get; set; }
    /// <summary>
    /// Data center ID length
    /// </summary>
    public int DatacenterIdBits { get; set; }
    /// <summary>
    /// 12bit serial number
    /// </summary>
    public long Sequence { get; set; }

    public int SequenceBits { get; set; }
    /// <summary>
    /// Use the previous ID when time rollback occurs
    /// </summary>
    public bool UsePreviousInTimeRollback { get; set; }

    public SnowflakeIdOptions()
    {
        WorkerIdBits = 5;
        DatacenterIdBits = 5;
        Sequence = 0L;
        SequenceBits = 12;
        UsePreviousInTimeRollback = true;
    }
}
