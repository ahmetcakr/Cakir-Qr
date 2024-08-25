
using Core.Application.Responses;

namespace QrMenu.Application.Features.MenuQrCodes.Queries.GetById;

public class GetByIdMenuQrCodeResponse : IResponse
{
    public int Id { get; set; }
    public int MenuId { get; set; }
    public string QrCodeText { get; set; }
    public Byte[] QrCode { get; set; }

    public GetByIdMenuQrCodeResponse()
    {
        Id = 0;
        MenuId = 0;
        QrCodeText = string.Empty;
        QrCode = new Byte[0];
    }

    public GetByIdMenuQrCodeResponse(int id, int menuId, string qrCodeText, Byte[] qrCode)
    {
        this.Id = id;
        this.MenuId = menuId;
        this.QrCodeText = qrCodeText;
        this.QrCode = qrCode;
    }
}
