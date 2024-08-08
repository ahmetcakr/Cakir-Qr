
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using QrMenu.Domain.Entities;
using QrMenu.Application.Features.Menus.Constants;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;
using MediatR;
using QrMenu.Application.Services.MenusService;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;

namespace QrMenu.Application.Features.Menus.Commands.Create
{
    public class CreateMenuCommand : IRequest<Result<CreatedMenuResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int CompanyId { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }

        public string[] Roles => new [] 
        {
            MenuOperationClaims.Admin,
            MenuOperationClaims.Write, 
            MenuOperationClaims.Add
        };

        public bool BypassCache => false;

        public string CacheKey => "";

        public string? CacheGroupKey => "GetMenus";

        internal sealed class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Result<CreatedMenuResponse>>
        {
            private readonly IMenuService _menuService;
            private readonly IMapper _mapper;

            public CreateMenuCommandHandler(IMenuService menuService, IMapper mapper)
            {
                _menuService = menuService;
                _mapper = mapper;
            }

            public async Task<Result<CreatedMenuResponse>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
            {
                Menu menu = _mapper.Map<Menu>(request);

                Menu createdMenu = await _menuService.AddAsync(menu);

                CreatedMenuResponse response = _mapper.Map<CreatedMenuResponse>(createdMenu);

                return Result<CreatedMenuResponse>.Succeed(response, StatusCodes.Status201Created);
            }
        }
    }
}
