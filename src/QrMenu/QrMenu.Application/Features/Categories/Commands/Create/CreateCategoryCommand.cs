
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Features.Categories.Constants;
using QrMenu.Application.Services.CategoriesService;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest<Result<CreatedCategoryResponse>>, ISecuredRequest
    {
        public int CompanyId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public string[] Roles => new [] 
        {
            CategoryOperationClaims.Admin,
            CategoryOperationClaims.Write, 
            CategoryOperationClaims.Add
        };

        internal sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CreatedCategoryResponse>>
        {
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;

            public CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
            {
                _categoryService = categoryService;
                _mapper = mapper;
            }

            public async Task<Result<CreatedCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                Category category = _mapper.Map<Category>(request);

                Category createdCategory = await _categoryService.AddAsync(category);

                CreatedCategoryResponse response = _mapper.Map<CreatedCategoryResponse>(createdCategory);

                return Result<CreatedCategoryResponse>.Succeed(response, StatusCodes.Status201Created);
            }
        }
    }
}
