using System;
using System.IO;

namespace MotoRentAPI.Services
{
    public class ImageService
    {
        private readonly string _basePath;

        public ImageService(string basePath)
        {
            _basePath = basePath;
        }

        public string SaveDriverLicense(string base64, string driverId)
        {
            var bytes = Convert.FromBase64String(base64);

            if (!IsPngOrBmp(bytes))
                throw new ArgumentException("Invalid image format. Only PNG or BMP allowed.");

            var folder = Path.Combine(_basePath, "driver_licenses");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var filePath = Path.Combine(folder, $"{driverId}.png");
            File.WriteAllBytes(filePath, bytes);

            return filePath;
        }

        private bool IsPngOrBmp(byte[] bytes)
        {
            if (
                bytes.Length >= 4
                && bytes[0] == 0x89
                && bytes[1] == 0x50
                && bytes[2] == 0x4E
                && bytes[3] == 0x47
            )
                return true;

            if (bytes.Length >= 2 && bytes[0] == 0x42 && bytes[1] == 0x4D)
                return true;

            return false;
        }
    }
}
