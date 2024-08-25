
using AutoMapper;
using QrMenu.Application.Features.Items.Rules;
using Core.Application.Results;
using MediatR;
using QrMenu.Application.Services.ItemsService;

namespace QrMenu.Application.Features.Items.Queries.GetListByCategoryId
{
    public class GetListByCategoryIdItemQuery : MediatR.IRequest<Result<List<GetListByCategoryIdItemResponse>>>
    {
        public int CategoryId { get; set; }

        internal sealed class GetByIdItemQueryHandler(
            IItemService ıtemService,
            IMapper mapper,
            ItemBusinessRules ıtemBusinessRules) : IRequestHandler<GetListByCategoryIdItemQuery, Result<List<GetListByCategoryIdItemResponse>>>
        {

            public async Task<Result<List<GetListByCategoryIdItemResponse>>> Handle(GetListByCategoryIdItemQuery request, CancellationToken cancellationToken)
            {
                var ıtem = await ıtemService.GetListByCategoryIdAsync(request.CategoryId);

                List<GetListByCategoryIdItemResponse> getByIdItemResponse = mapper.Map<List<GetListByCategoryIdItemResponse>>(ıtem);

                return Result<List<GetListByCategoryIdItemResponse>>.Succeed(getByIdItemResponse);
            }
        }
    }
}
