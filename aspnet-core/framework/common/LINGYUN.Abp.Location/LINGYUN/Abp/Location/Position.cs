namespace LINGYUN.Abp.Location;

/// <summary>
/// Position quantity
/// </summary>
public class Position
{
    /// <summary>
    /// Upper left latitude
    /// </summary>
    public double LeftTopLatitude { get; }
    /// <summary>
    /// Upper left longitude
    /// </summary>
    public double LeftTopLongitude { get; }
    /// <summary>
    /// Lower left latitude
    /// </summary>
    public double LeftBottomLatitude { get; }
    /// <summary>
    /// Lower left longitude
    /// </summary>
    public double LeftBottomLongitude { get; }

    /// <summary>
    /// Upper right latitude
    /// </summary>
    public double RightTopLatitude { get; }
    /// <summary>
    /// Upper right longitude
    /// </summary>
    public double RightTopLongitude { get; }
    /// <summary>
    /// Lower right latitude
    /// </summary>
    public double RightBottomLatitude { get; }
    /// <summary>
    /// Lower right longitude
    /// </summary>
    public double RightBottomLongitude { get; }

    internal Position(double leftTopLat, double leftBottomLat, double leftTopLng, double leftBottomLng,
        double rightTopLat, double rightBottomLat, double rightTopLng, double rightBottomLng)
    {
        LeftTopLatitude = leftTopLat;
        LeftBottomLatitude = leftBottomLat;
        LeftTopLongitude = leftTopLng;
        LeftBottomLongitude = leftBottomLng;
        RightTopLatitude = rightTopLat;
        RightTopLongitude = rightTopLng;
        RightBottomLatitude = rightBottomLat;
        RightBottomLongitude = rightBottomLng;
    }
}
