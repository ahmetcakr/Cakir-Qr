
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

namespace QrMenu.Application.Features.Menus.Commands.Update
{
    public class UpdateMenuCommand : IRequest<Result<UpdatedMenuResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
public string MenuName { get; set; }
public string Description { get; set; }

        public string[] Roles => new [] 
        {
            MenuOperationClaims.Admin,
            MenuOperationClaims.Write, 
            MenuOperationClaims.Update
        };

        public bool BypassCache => false;

        public string? CacheKey => "";

        public string? CacheGroupKey => "GetMenus";

        internal sealed class UpdateMenuCommandHandler(
            IMenuService _menuService,
            IMapper _mapper,
            MenuBusinessRules _menuBusinessRules) : IRequestHandler<UpdateMenuCommand, Result<UpdatedMenuResponse>>
        {

            public async Task<Result<UpdatedMenuResponse>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
            {
                await _menuBusinessRules.MenuIdShouldBeExist(request.Id);

                Menu? menu = await _menuService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (menu is null)
                    throw new BusinessException("Entity does not exist.");

                _mapper.Map(request, menu);

                Menu updatedMenu = await _menuService.UpdateAsync(menu);

                UpdatedMenuResponse response = _mapper.Map<UpdatedMenuResponse>(updatedMenu);

                return Result<UpdatedMenuResponse>.Succeed(response);
            }
        }
    }
}
