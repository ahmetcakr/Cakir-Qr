
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using QrMenu.Domain.Entities;
using QrMenu.Application.Features.Menus.Constants;
using QrMenu.Application.Features.Menus.Rules;
using MediatR;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Services.MenusService;
using Core.Application.Pipelines.Caching;

namespace QrMenu.Application.Features.Menus.Commands.Delete
{
    public class DeleteMenuCommand : IRequest<Result<DeletedMenuResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }

        public string[] Roles => new [] 
        {
            MenuOperationClaims.Admin,
            MenuOperationClaims.Delete, 
            MenuOperationClaims.Write
        };

        public bool BypassCache => false;

        public string? CacheKey => "";

        public string? CacheGroupKey => "GetMenus";

        internal sealed class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, Result<DeletedMenuResponse>>
        {
            private readonly IMenuService _menuService;
            private readonly IMapper _mapper;
            private readonly MenuBusinessRules _menuBusinessRules;

            public DeleteMenuCommandHandler(IMenuService menuService, IMapper mapper,MenuBusinessRules menuBusinessRules)
            {
                _menuService = menuService;
                _mapper = mapper;
                _menuBusinessRules = menuBusinessRules;    
            }

            public async Task<Result<DeletedMenuResponse>> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
            {
                await _menuBusinessRules.MenuIdShouldBeExist(request.Id);

                Menu? menu = await _menuService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (menu is null)
                    throw new BusinessException("Entity does not exist.");

                await _menuService.DeleteAsync(menu);

                DeletedMenuResponse response = _mapper.Map<DeletedMenuResponse>(menu);

                return Result<DeletedMenuResponse>.Succeed(response);
            }
        }
    }
}
