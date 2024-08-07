
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using QrMenu.Domain.Entities;
using QrMenu.Application.Features.Categories.Constants;
using QrMenu.Application.Features.Categories.Rules;
using MediatR;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Services.CategoriesService;

namespace QrMenu.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<Result<DeletedCategoryResponse>>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => new [] 
        {
            CategoryOperationClaims.Admin,
            CategoryOperationClaims.Delete, 
            CategoryOperationClaims.Write
        };

        internal sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<DeletedCategoryResponse>>
        {
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;
            private readonly CategoryBusinessRules _categoryBusinessRules;

            public DeleteCategoryCommandHandler(ICategoryService categoryService, IMapper mapper,CategoryBusinessRules categoryBusinessRules)
            {
                _categoryService = categoryService;
                _mapper = mapper;
                _categoryBusinessRules = categoryBusinessRules;    
            }

            public async Task<Result<DeletedCategoryResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryIdShouldBeExist(request.Id);

                Category? category = await _categoryService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (category is null)
                    throw new BusinessException("Entity does not exist.");

                await _categoryService.DeleteAsync(category);

                DeletedCategoryResponse response = _mapper.Map<DeletedCategoryResponse>(category);

                return Result<DeletedCategoryResponse>.Succeed(response);
            }
        }
    }
}
