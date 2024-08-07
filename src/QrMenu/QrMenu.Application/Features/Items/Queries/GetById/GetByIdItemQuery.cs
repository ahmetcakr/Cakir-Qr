
using AutoMapper;
using QrMenu.Application.Features.Items.Rules;
using Core.Application.Results;
using MediatR;
using QrMenu.Application.Services.ItemsService;

namespace QrMenu.Application.Features.Items.Queries.GetById
{
    public class GetByIdItemQuery : MediatR.IRequest<Result<GetByIdItemResponse>>
    {
        public int Id { get; set; }

        internal sealed class GetByIdItemQueryHandler(
            IItemService ıtemService,
            IMapper mapper,
            ItemBusinessRules ıtemBusinessRules) : IRequestHandler<GetByIdItemQuery, Result<GetByIdItemResponse>>
        {

            public async Task<Result<GetByIdItemResponse>> Handle(GetByIdItemQuery request, CancellationToken cancellationToken)
            {
                var ıtem = await ıtemService.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

                GetByIdItemResponse getByIdItemResponse = mapper.Map<GetByIdItemResponse>(ıtem);

                return Result<GetByIdItemResponse>.Succeed(getByIdItemResponse);
            }
        }
    }
}
