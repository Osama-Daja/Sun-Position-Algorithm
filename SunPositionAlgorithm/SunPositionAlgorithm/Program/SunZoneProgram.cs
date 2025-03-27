using SunPositionAlgorithm.IService;
using SunPositionAlgorithm.Service;

namespace SunPositionAlgorithm.Program
{
    class SunZoneProgram
    {
        private readonly ISunZoneService SunZoneService;
        private readonly IAttachmentService AttachmentService;
        public SunZoneProgram(SunZoneService sunZoneService, AttachmentService attachmentService)
        {
            SunZoneService = sunZoneService;
            AttachmentService = attachmentService;
        }

        public string StoreImageSunZone(double latitude, double longitude, DateTime dateTime)
        {
            string imageSunZoneName = Calculate(latitude, longitude, dateTime);

            byte[] imageFile = AttachmentService.GetImage(imageSunZoneName.ToLower());

            if(imageFile != null)
            {
                AttachmentService.StoreImage(imageFile);
            }

            return imageSunZoneName;
        }

        private string Calculate(double latitude, double longitude, DateTime dateTime)
        {
            string sunZone = SunZoneService.GetSunZone(latitude, longitude, dateTime);
            return sunZone;
        }
    }
}
