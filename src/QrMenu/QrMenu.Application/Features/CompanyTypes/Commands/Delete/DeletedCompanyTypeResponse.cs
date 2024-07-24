using Core.Application.Responses;

namespace QrMenu.Application.Features.CompanyTypes.Commands.Delete;

public class DeletedCompanyTypeResponse : IResponse
{
    public int Id { get; set; }
}
