using Core.Application.Responses;

namespace QrMenu.Application.Features.Companies.Commands.Delete;

public class DeletedCompanyResponse : IResponse
{
    public int Id { get; set; }
}
