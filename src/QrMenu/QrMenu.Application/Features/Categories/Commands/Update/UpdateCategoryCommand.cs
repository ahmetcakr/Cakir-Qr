
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

namespace QrMenu.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<Result<UpdatedCategoryResponse>>, ISecuredRequest
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
public string CategoryName { get; set; }
public string Description { get; set; }

        public string[] Roles => new [] 
        {
            CategoryOperationClaims.Admin,
            CategoryOperationClaims.Write, 
            CategoryOperationClaims.Update
        };

        internal sealed class UpdateCategoryCommandHandler(
            ICategoryService _categoryService,
            IMapper _mapper,
            CategoryBusinessRules _categoryBusinessRules) : IRequestHandler<UpdateCategoryCommand, Result<UpdatedCategoryResponse>>
        {

            public async Task<Result<UpdatedCategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                await _categoryBusinessRules.CategoryIdShouldBeExist(request.Id);

                Category? category = await _categoryService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (category is null)
                    throw new BusinessException("Entity does not exist.");

                _mapper.Map(request, category);

                Category updatedCategory = await _categoryService.UpdateAsync(category);

                UpdatedCategoryResponse response = _mapper.Map<UpdatedCategoryResponse>(updatedCategory);

                return Result<UpdatedCategoryResponse>.Succeed(response);
            }
        }
    }
}
