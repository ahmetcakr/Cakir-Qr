
using AutoMapper;
using QrMenu.Application.Features.ItemImages.Rules;
using Core.Application.Results;
using MediatR;
using QrMenu.Application.Services.ItemImagesService;

namespace QrMenu.Application.Features.ItemImages.Queries.GetById
{
    public class GetByIdItemImageQuery : MediatR.IRequest<Result<GetByIdItemImageResponse>>
    {
        public int Id { get; set; }

        internal sealed class GetByIdItemImageQueryHandler(
            IItemImageService ıtemImageService,
            IMapper mapper,
            ItemImageBusinessRules ıtemImageBusinessRules) : IRequestHandler<GetByIdItemImageQuery, Result<GetByIdItemImageResponse>>
        {

            public async Task<Result<GetByIdItemImageResponse>> Handle(GetByIdItemImageQuery request, CancellationToken cancellationToken)
            {
                var ıtemImage = await ıtemImageService.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

                GetByIdItemImageResponse getByIdItemImageResponse = mapper.Map<GetByIdItemImageResponse>(ıtemImage);

                return Result<GetByIdItemImageResponse>.Succeed(getByIdItemImageResponse);
            }
        }
    }
}
