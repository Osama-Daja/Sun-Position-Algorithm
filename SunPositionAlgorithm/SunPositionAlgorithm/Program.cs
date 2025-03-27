using SunPositionAlgorithm.Program;
using SunPositionAlgorithm.Service;

double latitude = 31.94338;
double longitude = 35.94989;
DateTime utcNow = DateTime.Now;

SunZoneProgram sunTimeZone = new SunZoneProgram(new SunZoneService(), new AttachmentService());
string sunZone = sunTimeZone.StoreImageSunZone(latitude, longitude, utcNow);

Console.WriteLine($"Sun Zone: {sunZone}");