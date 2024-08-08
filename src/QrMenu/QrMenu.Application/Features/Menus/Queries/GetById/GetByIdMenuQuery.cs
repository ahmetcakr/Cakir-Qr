
using AutoMapper;
using QrMenu.Application.Features.Menus.Rules;
using Core.Application.Results;
using MediatR;
using QrMenu.Application.Services.MenusService;

namespace QrMenu.Application.Features.Menus.Queries.GetById
{
    public class GetByIdMenuQuery : MediatR.IRequest<Result<GetByIdMenuResponse>>
    {
        public int Id { get; set; }

        internal sealed class GetByIdMenuQueryHandler(
            IMenuService menuService,
            IMapper mapper,
            MenuBusinessRules menuBusinessRules) : IRequestHandler<GetByIdMenuQuery, Result<GetByIdMenuResponse>>
        {

            public async Task<Result<GetByIdMenuResponse>> Handle(GetByIdMenuQuery request, CancellationToken cancellationToken)
            {
                var menu = await menuService.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

                GetByIdMenuResponse getByIdMenuResponse = mapper.Map<GetByIdMenuResponse>(menu);

                return Result<GetByIdMenuResponse>.Succeed(getByIdMenuResponse);
            }
        }
    }
}
