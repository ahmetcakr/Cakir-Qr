using Core.Persistence.Repositories;

namespace QrMenu.Domain.Entities;

public class MenuQrCode : Entity<int>
{
    public int MenuId { get; set; }
    public string QrCodeText { get; set; }
    public Byte[] QrCode { get; set; }
    public virtual Menu Menu { get; set; }

    public MenuQrCode()
    {
        Id = 0;
        MenuId = 0;
        QrCodeText = string.Empty;
        QrCode = new Byte[0];
    }

    public MenuQrCode(int id, int menuId, string qrCodeText, Byte[] qrCode)
    {
        Id = id;
        MenuId = menuId;
        QrCodeText = qrCodeText;
        QrCode = qrCode;
    }
}
