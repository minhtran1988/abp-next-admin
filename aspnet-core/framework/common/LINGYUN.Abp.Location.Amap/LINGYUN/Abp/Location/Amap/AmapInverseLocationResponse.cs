namespace LINGYUN.Abp.Location.Amap
{
    /// <summary>
    /// Reverse geocoding result
    /// </summary>
    public class AmapInverseLocationResponse : AmapHttpResponse
    {
        public AmapRegeocode Regeocode { get; set; }
        public AmapInverseLocationResponse()
        {
            Regeocode = new AmapRegeocode();
        }
    }
}
