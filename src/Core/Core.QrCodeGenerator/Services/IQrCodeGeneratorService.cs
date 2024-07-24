namespace Core.QrCodeGenerator.Services;

public interface IQrCodeGeneratorService
{
    Task<byte[]> GenerateQrCodeAsync(string text);
}
