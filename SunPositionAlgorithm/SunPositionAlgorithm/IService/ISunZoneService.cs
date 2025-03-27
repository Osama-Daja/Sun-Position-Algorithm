namespace SunPositionAlgorithm.IService
{
    public interface ISunZoneService
    {
        public string GetSunZone(double latitude, double longitude, DateTime utcNow);
    }
}
