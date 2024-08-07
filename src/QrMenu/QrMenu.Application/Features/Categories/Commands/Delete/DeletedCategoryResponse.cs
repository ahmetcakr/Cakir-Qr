
using Core.Application.Responses;

namespace QrMenu.Application.Features.Categories.Commands.Delete;
public class DeletedCategoryResponse : IResponse
{
    public int Id { get; set; }

    public DeletedCategoryResponse()
    {
        // Varsayılan değerler burada atanabilir
    }

    public DeletedCategoryResponse(int id)
    {
        Id = id;
    }
}
