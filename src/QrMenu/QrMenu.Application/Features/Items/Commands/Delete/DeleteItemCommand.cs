
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

namespace QrMenu.Application.Features.Items.Commands.Delete
{
    public class DeleteItemCommand : IRequest<Result<DeletedItemResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }

        public string[] Roles => new [] 
        {
            ItemOperationClaims.Admin,
            ItemOperationClaims.Delete, 
            ItemOperationClaims.Write
        };

        public bool BypassCache => false;

        public string? CacheKey => "";

        public string? CacheGroupKey => "GetItems";

        internal sealed class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Result<DeletedItemResponse>>
        {
            private readonly IItemService _itemService;
            private readonly IMapper _mapper;
            private readonly ItemBusinessRules _itemBusinessRules;

            public DeleteItemCommandHandler(IItemService itemService, IMapper mapper,ItemBusinessRules itemBusinessRules)
            {
                _itemService = itemService;
                _mapper = mapper;
                _itemBusinessRules = itemBusinessRules;    
            }

            public async Task<Result<DeletedItemResponse>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
            {
                await _itemBusinessRules.ItemIdShouldBeExist(request.Id);

                Item? item = await _itemService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (item is null)
                    throw new BusinessException("Entity does not exist.");

                await _itemService.DeleteAsync(item);

                DeletedItemResponse response = _mapper.Map<DeletedItemResponse>(item);

                return Result<DeletedItemResponse>.Succeed(response);
            }
        }
    }
}
