using System;

namespace LINGYUN.Abp.Location;

public class Location
{
    /// <summary>
    /// Radius of the Earth (meters)
    /// </summary>
    public const double EARTH_RADIUS = 6378137;
    /// <summary>
    /// Latitude
    /// </summary>
    public double Latitude { get; set; }
    /// <summary>
    /// Longitude
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Calculate the distance between two locations and return the distance between two points in meters.
    /// This formula is provided by GOOGLE, the error is less than 0.2 meters
    /// </summary>
    /// <param name="location">Location information participating in calculation</param>
    /// <returns>Returns the distance between two positions</returns>
    public double CalcDistance(Location location)
    {
        return CalcDistance(this, location);
    }
    /// <summary>
    /// Calculate the distance between two locations and return the distance between two points in meters.
    /// This formula is provided by GOOGLE, the error is less than 0.2 meters
    /// </summary>
    /// <param name="location1">Location information participating in calculation</param>
    /// <param name="location2">Location information participating in calculation</param>
    /// <returns>Returns the distance between two positions</returns>
    public static double CalcDistance(Location location1, Location location2)
    {
        double radLat1 = Rad(location1.Latitude);
        double radLng1 = Rad(location1.Longitude);
        double radLat2 = Rad(location2.Latitude);
        double radLng2 = Rad(location2.Longitude);
        double a = radLat1 - radLat2;
        double b = radLng1 - radLng2;
        double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
        result *= EARTH_RADIUS;
        return result;
    }
    /// <summary>
    /// Calculate the offset distance of the position
    /// </summary>
    /// <param name="location">Location participating in calculation</param>
    /// <param name="distance">Position offset, unit meter</param>
    /// <returns></returns>
    public static Position CalcOffsetDistance(Location location, double distance)
    {
        double dlng = 2 * Math.Asin(Math.Sin(distance / (2 * EARTH_RADIUS)) / Math.Cos(Rad(location.Latitude)));
        dlng = Deg(dlng);
        double dlat = distance / EARTH_RADIUS;
        dlat = Deg(dlat);
        double leftTopLat = location.Latitude + dlat;
        double leftTopLng = location.Longitude - dlng;

        double leftBottomLat = location.Latitude - dlat;
        double leftBottomLng = location.Longitude - dlng;

        double rightTopLat = location.Latitude + dlat;
        double rightTopLng = location.Longitude + dlng;

        double rightBottomLat = location.Latitude - dlat;
        double rightBottomLng = location.Longitude + dlng;

        return new Position(leftTopLat, leftBottomLat, leftTopLng, leftBottomLng,
            rightTopLat, rightBottomLat, rightTopLng, rightBottomLng);
    }

    /// <summary>
    /// Convert angles to radians
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static double Rad(double d)
    {
        return d * Math.PI / 180d;
    }

    /// <summary>
    /// Convert radians to degrees
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static double Deg(double d)
    {
        return d * (180 / Math.PI);
    }
}
