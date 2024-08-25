
using Core.Application.Responses;

namespace QrMenu.Application.Features.MenuQrCodes.Commands.Delete;
public class DeletedMenuQrCodeResponse : IResponse
{
    public int Id { get; set; }

    public DeletedMenuQrCodeResponse()
    {
        Id = 0;
    }

    public DeletedMenuQrCodeResponse(int id)
    {
        Id = id;
    }
}
