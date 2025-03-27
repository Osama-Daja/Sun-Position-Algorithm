using SunPositionAlgorithm.Program;
using SunPositionAlgorithm.Service;

double latitude = 51.59111;
double longitude = -0.45416;
DateTime utcNow = new DateTime(2025, 11, 29, 4, 30, 0);

SunZoneProgram sunTimeZone = new SunZoneProgram(new SunZoneService(), new AttachmentService());
string sunZone = sunTimeZone.StoreImageSunZone(latitude, longitude, utcNow);

Console.WriteLine($"Sun Zone: {sunZone}");