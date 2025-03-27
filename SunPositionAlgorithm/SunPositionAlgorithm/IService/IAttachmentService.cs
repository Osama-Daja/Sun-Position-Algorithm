namespace SunPositionAlgorithm.IService
{
    public interface IAttachmentService
    {
        byte[] GetImage(string imageName);
        void StoreImage(byte[] imageBytes);
    }
}
