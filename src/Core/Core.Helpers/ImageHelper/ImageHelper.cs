using Microsoft.AspNetCore.Http;

namespace Core.Helpers.ImageHelper
{
    public static class ImageHelper
    {
        // ConvertToByteArrayAsync
        public static async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        // ConvertToBase64String
        public static string ConvertToBase64String(byte[] image)
        {
            return Convert.ToBase64String(image);
        }

        public static async Task<string> ConvertToBase64String(IFormFile file)
        {
            byte[] image = await ConvertToByteArrayAsync(file);
            return ConvertToBase64String(image);
        }

    }
}
