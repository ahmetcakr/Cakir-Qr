
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Results;
using Core.Helpers.ImageHelper;
using MediatR;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Features.CompanyTypes.Constants;
using QrMenu.Application.Features.ItemImages.Constants;
using QrMenu.Application.Services.ItemImagesService;
using QrMenu.Domain.Entities;
using System.Text.Json.Serialization;

namespace QrMenu.Application.Features.ItemImages.Commands.Create;

public class CreateItemImageCommand : IRequest<Result<CreatedItemImageResponse>>, ISecuredRequest, ICacheRemoverRequest
{
    public int ItemId { get; set; }
    public IFormFile Image { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public string[] Roles => new[]
    {
        ItemImageOperationClaims.Admin,
        ItemImageOperationClaims.Write,
        ItemImageOperationClaims.Add
    };

    public bool BypassCache => false;

    public string? CacheKey => "";

    public string? CacheGroupKey => "GetItemImages";

    internal sealed class CreateItemImageCommandHandler : IRequestHandler<CreateItemImageCommand, Result<CreatedItemImageResponse>>
    {
        private readonly IItemImageService _itemimageService;
        private readonly IMapper _mapper;

        public CreateItemImageCommandHandler(IItemImageService itemimageService, IMapper mapper)
        {
            _itemimageService = itemimageService;
            _mapper = mapper;
        }

        public async Task<Result<CreatedItemImageResponse>> Handle(CreateItemImageCommand request, CancellationToken cancellationToken)
        {
            ItemImage itemImage = new()
            {
                Image = await ImageHelper.ConvertToByteArrayAsync(request.Image),
                Description = request.Description,
                ItemId = request.ItemId
            };

            ItemImage createdItemImage = await _itemimageService.AddAsync(itemImage);

            CreatedItemImageResponse response = _mapper.Map<CreatedItemImageResponse>(createdItemImage);

            return Result<CreatedItemImageResponse>.Succeed(response, StatusCodes.Status201Created);
        }
    }
}
