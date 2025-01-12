using Microsoft.AspNetCore.Http;
using SkiaSharp;

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

        public static byte[] ScaleImage(byte[] imageBytes, int maxWidth, int maxHeight)
        {
            SKBitmap image = SKBitmap.Decode(imageBytes);

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var info = new SKImageInfo(newWidth, newHeight);
            image = image.Resize(info, SKFilterQuality.High);

            using var ms = new MemoryStream();
            image.Encode(ms, SKEncodedImageFormat.Png, 100);
            return ms.ToArray();
        }

    }
}
