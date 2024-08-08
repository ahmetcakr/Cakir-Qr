
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

namespace QrMenu.Application.Features.ItemImages.Commands.Delete
{
    public class DeleteItemImageCommand : IRequest<Result<DeletedItemImageResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }

        public string[] Roles => new [] 
        {
            ItemImageOperationClaims.Admin,
            ItemImageOperationClaims.Delete, 
            ItemImageOperationClaims.Write
        };

        public bool BypassCache => false;
        public string? CacheKey => "";
        public string? CacheGroupKey => "GetItemImages";

        internal sealed class DeleteItemImageCommandHandler : IRequestHandler<DeleteItemImageCommand, Result<DeletedItemImageResponse>>
        {
            private readonly IItemImageService _itemimageService;
            private readonly IMapper _mapper;
            private readonly ItemImageBusinessRules _itemimageBusinessRules;

            public DeleteItemImageCommandHandler(IItemImageService itemimageService, IMapper mapper,ItemImageBusinessRules itemimageBusinessRules)
            {
                _itemimageService = itemimageService;
                _mapper = mapper;
                _itemimageBusinessRules = itemimageBusinessRules;    
            }

            public async Task<Result<DeletedItemImageResponse>> Handle(DeleteItemImageCommand request, CancellationToken cancellationToken)
            {
                await _itemimageBusinessRules.ItemImageIdShouldBeExist(request.Id);

                ItemImage? itemimage = await _itemimageService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (itemimage is null)
                    throw new BusinessException("Entity does not exist.");

                await _itemimageService.DeleteAsync(itemimage);

                DeletedItemImageResponse response = _mapper.Map<DeletedItemImageResponse>(itemimage);

                return Result<DeletedItemImageResponse>.Succeed(response);
            }
        }
    }
}
