
using AutoMapper;
using QrMenu.Application.Features.Categories.Rules;
using Core.Application.Results;
using MediatR;
using QrMenu.Application.Services.CategoriesService;

namespace QrMenu.Application.Features.Categories.Queries.GetById
{
    public class GetByIdCategoryQuery : MediatR.IRequest<Result<GetByIdCategoryResponse>>
    {
        public int Id { get; set; }

        internal sealed class GetByIdCategoryQueryHandler(
            ICategoryService categoryService,
            IMapper mapper,
            CategoryBusinessRules categoryBusinessRules) : IRequestHandler<GetByIdCategoryQuery, Result<GetByIdCategoryResponse>>
        {

            public async Task<Result<GetByIdCategoryResponse>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
            {
                var category = await categoryService.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

                GetByIdCategoryResponse getByIdCategoryResponse = mapper.Map<GetByIdCategoryResponse>(category);

                return Result<GetByIdCategoryResponse>.Succeed(getByIdCategoryResponse);
            }
        }
    }
}
