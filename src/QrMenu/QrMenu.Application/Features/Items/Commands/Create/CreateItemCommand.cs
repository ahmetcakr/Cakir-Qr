
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using QrMenu.Domain.Entities;
using QrMenu.Application.Features.Items.Constants;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;
using MediatR;
using QrMenu.Application.Services.ItemsService;
using Core.Application.Pipelines.Caching;

namespace QrMenu.Application.Features.Items.Commands.Create
{
    public class CreateItemCommand : IRequest<Result<CreatedItemResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public string[] Roles => new [] 
        {
            ItemOperationClaims.Admin,
            ItemOperationClaims.Write, 
            ItemOperationClaims.Add
        };

        public bool BypassCache => false;

        public string? CacheKey => "";

        public string? CacheGroupKey => "GetItems";

        internal sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<CreatedItemResponse>>
        {
            private readonly IItemService _itemService;
            private readonly IMapper _mapper;

            public CreateItemCommandHandler(IItemService itemService, IMapper mapper)
            {
                _itemService = itemService;
                _mapper = mapper;
            }

            public async Task<Result<CreatedItemResponse>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
            {
                Item item = _mapper.Map<Item>(request);

                Item createdItem = await _itemService.AddAsync(item);

                CreatedItemResponse response = _mapper.Map<CreatedItemResponse>(createdItem);

                return Result<CreatedItemResponse>.Succeed(response, StatusCodes.Status201Created);
            }
        }
    }
}
