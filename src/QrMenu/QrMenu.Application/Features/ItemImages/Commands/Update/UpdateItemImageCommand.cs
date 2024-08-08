
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using QrMenu.Domain.Entities;
using QrMenu.Application.Features.ItemImages.Constants;
using QrMenu.Application.Features.ItemImages.Rules;
using MediatR;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Services.ItemImagesService;
using Core.Application.Pipelines.Caching;

namespace QrMenu.Application.Features.ItemImages.Commands.Update
{
    public class UpdateItemImageCommand : IRequest<Result<UpdatedItemImageResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Byte[] Image { get; set; }
        public string Description { get; set; }

        public string[] Roles => new [] 
        {
            ItemImageOperationClaims.Admin,
            ItemImageOperationClaims.Write, 
            ItemImageOperationClaims.Update
        };

        public bool BypassCache => false;
        public string? CacheKey => "";
        public string? CacheGroupKey => "GetItemImages";

        internal sealed class UpdateItemImageCommandHandler(
            IItemImageService _itemimageService,
            IMapper _mapper,
            ItemImageBusinessRules _itemimageBusinessRules) : IRequestHandler<UpdateItemImageCommand, Result<UpdatedItemImageResponse>>
        {

            public async Task<Result<UpdatedItemImageResponse>> Handle(UpdateItemImageCommand request, CancellationToken cancellationToken)
            {
                await _itemimageBusinessRules.ItemImageIdShouldBeExist(request.Id);

                ItemImage? itemimage = await _itemimageService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (itemimage is null)
                    throw new BusinessException("Entity does not exist.");

                _mapper.Map(request, itemimage);

                ItemImage updatedItemImage = await _itemimageService.UpdateAsync(itemimage);

                UpdatedItemImageResponse response = _mapper.Map<UpdatedItemImageResponse>(updatedItemImage);

                return Result<UpdatedItemImageResponse>.Succeed(response);
            }
        }
    }
}
