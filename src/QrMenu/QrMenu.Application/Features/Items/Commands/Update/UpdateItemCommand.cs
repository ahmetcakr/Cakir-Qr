
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using QrMenu.Domain.Entities;
using QrMenu.Application.Features.Items.Constants;
using QrMenu.Application.Features.Items.Rules;
using MediatR;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Services.ItemsService;
using Core.Application.Pipelines.Caching;

namespace QrMenu.Application.Features.Items.Commands.Update
{
    public class UpdateItemCommand : IRequest<Result<UpdatedItemResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public string[] Roles => new [] 
        {
            ItemOperationClaims.Admin,
            ItemOperationClaims.Write, 
            ItemOperationClaims.Update
        };

        public bool BypassCache => false;

        public string? CacheKey => "";

        public string? CacheGroupKey => "GetItems";

        internal sealed class UpdateItemCommandHandler(
            IItemService _itemService,
            IMapper _mapper,
            ItemBusinessRules _itemBusinessRules) : IRequestHandler<UpdateItemCommand, Result<UpdatedItemResponse>>
        {

            public async Task<Result<UpdatedItemResponse>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
            {
                await _itemBusinessRules.ItemIdShouldBeExist(request.Id);

                Item? item = await _itemService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (item is null)
                    throw new BusinessException("Entity does not exist.");

                _mapper.Map(request, item);

                Item updatedItem = await _itemService.UpdateAsync(item);

                UpdatedItemResponse response = _mapper.Map<UpdatedItemResponse>(updatedItem);

                return Result<UpdatedItemResponse>.Succeed(response);
            }
        }
    }
}
