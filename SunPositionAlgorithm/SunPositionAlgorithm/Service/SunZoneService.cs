using SunPositionAlgorithm.IService;
using SunPositionAlgorithm.Model.Enum;

namespace SunPositionAlgorithm.Service
{
    public class SunZoneService : ISunZoneService
    {
        public string GetSunZone(double latitude, double longitude, DateTime utcNow)
        {
            // Step 1: Determine local time
            int timeZoneOffset = EstimateTimeZoneOffset(latitude, longitude);
            DateTime localTime = utcNow.AddHours(timeZoneOffset);

            // Step 2: Add DST'Daylight Saving Time'
            if (IsDaylightSavingTime(latitude, longitude, localTime))
            {
                localTime = localTime.AddHours(1);
            }

            // Step 3: Calculate sunrise and sunset times dynamically
            DateTime sunrise, sunset;
            CalculateSunriseSunset(latitude, longitude, localTime, out sunrise, out sunset);

            // Step 4: Determine sun zone
            // cucloation for noon time
            DateTime noon = sunrise.AddHours((sunset - sunrise).TotalHours / 2); 

            if (localTime >= sunrise.AddHours(-1) && localTime < sunrise.AddHours(1))
                return ImagesNamesEnum.Sunrise.ToString();
            else if (localTime >= sunrise.AddHours(1) && localTime < noon)
                return ImagesNamesEnum.Morning.ToString();
            else if (localTime >= noon && localTime <= noon.AddHours(1))
                return ImagesNamesEnum.Noon.ToString();
            else if (localTime > noon.AddHours(1) && localTime < sunset.AddHours(-1))
                return ImagesNamesEnum.Evening.ToString();
            else if (localTime >= sunset.AddHours(-1) && localTime <= sunset.AddHours(1))
                return ImagesNamesEnum.Sunset.ToString();
            else
                return ImagesNamesEnum.Night.ToString();
        }

        private int EstimateTimeZoneOffset(double latitude, double longitude)
        {
            return (int)Math.Round(longitude / 15.0);
        }

        bool IsDaylightSavingTime(double latitude, double longitude, DateTime localTime)
        {
            if ((latitude > 30 && longitude < -30) || (latitude > 30 && longitude > 30))
            {
                return (localTime.Month >= 3 && localTime.Month <= 11); // March - November (common DST period)
            }
            else if (latitude < -30 && longitude > 30)
            {
                return (localTime.Month >= 10 || localTime.Month <= 3); // October - March (southern hemisphere DST)
            }
            return false;
        }

        void CalculateSunriseSunset(double latitude, double longitude, DateTime localTime, out DateTime sunrise, out DateTime sunset)
        {
            int dayOfYear = localTime.DayOfYear;
            double declination = 23.44 * Math.Cos((360.0 / 365.0) * (dayOfYear + 10) * Math.PI / 180.0);
            double hourAngle = Math.Acos(-Math.Tan(latitude * Math.PI / 180.0) * Math.Tan(declination * Math.PI / 180.0)) * 180.0 / Math.PI;

            double solarNoon = 12.0 + (longitude / 15.0);
            double sunriseTime = ((solarNoon - (hourAngle / 15.0)) + 24) % 24;
            double sunsetTime = ((solarNoon + (hourAngle / 15.0)) + 24) % 24;

            sunrise = new DateTime(localTime.Year, localTime.Month, localTime.Day, (int)sunriseTime, (int)((sunriseTime % 1) * 60), 0);
            sunset = new DateTime(localTime.Year, localTime.Month, localTime.Day, (int)sunsetTime, (int)((sunsetTime % 1) * 60), 0);
        }
    }
}
