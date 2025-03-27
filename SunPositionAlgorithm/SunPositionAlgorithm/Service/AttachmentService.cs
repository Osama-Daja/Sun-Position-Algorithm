using SunPositionAlgorithm.IService;

namespace SunPositionAlgorithm.Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string DirectoryPath = "";
        public AttachmentService()
        {
            DirectoryPath = Directory.GetCurrentDirectory() + "/Assets/SuntimeZoneImages";
        }
        public byte[] GetImage(string imageName)
        {
            byte[] imageBytes = null;
            string fullPath = $"{DirectoryPath}/{imageName}.png";

            if (File.Exists(fullPath))
            {
                imageBytes = File.ReadAllBytes(fullPath);
            }

            return imageBytes;
        }

        public void StoreImage(byte[] imageBytes)
        {
            string storePath = $"{DirectoryPath}/Result/stdout.png";

            if (File.Exists(storePath))
            {
                File.Delete(storePath);
            }

            File.WriteAllBytes(storePath, imageBytes);
        }
    }
}
