using System.Text;

namespace MotoRentAPI.Services
{
    public class LocalStorageService
    {
        private readonly string _basePath = "Storage";

        public LocalStorageService()
        {
            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public string SaveBase64Image(string base64, string fileName)
        {
            if (string.IsNullOrWhiteSpace(base64))
                throw new ArgumentException("Image cannot be null or empty");

            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(base64);
            }
            catch
            {
                throw new ArgumentException("Invalid Base64 string");
            }

            string extension;
            if (bytes.Length > 2 && bytes[0] == 0x42 && bytes[1] == 0x4D)
            {
                extension = ".bmp";
            }
            else if (
                bytes.Length > 8
                && bytes[0] == 0x89
                && bytes[1] == 0x50
                && bytes[2] == 0x4E
                && bytes[3] == 0x47
                && bytes[4] == 0x0D
                && bytes[5] == 0x0A
                && bytes[6] == 0x1A
                && bytes[7] == 0x0A
            )
            {
                extension = ".png";
            }
            else
            {
                throw new ArgumentException("Only PNG or BMP images are allowed");
            }

            string path = Path.Combine(_basePath, fileName + extension);

            File.WriteAllBytes(path, bytes);

            return path;
        }
    }
}
