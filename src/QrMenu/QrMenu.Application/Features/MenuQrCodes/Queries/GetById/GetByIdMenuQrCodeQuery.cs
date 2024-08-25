
using AutoMapper;
using QrMenu.Application.Features.MenuQrCodes.Rules;
using Core.Application.Results;
using MediatR;
using QrMenu.Application.Services.MenuQrCodesService;

namespace QrMenu.Application.Features.MenuQrCodes.Queries.GetById
{
    public class GetByIdMenuQrCodeQuery : MediatR.IRequest<Result<GetByIdMenuQrCodeResponse>>
    {
        public int Id { get; set; }

        internal sealed class GetByIdMenuQrCodeQueryHandler(
            IMenuQrCodeService menuQrCodeService,
            IMapper mapper,
            MenuQrCodeBusinessRules menuQrCodeBusinessRules) : IRequestHandler<GetByIdMenuQrCodeQuery, Result<GetByIdMenuQrCodeResponse>>
        {

            public async Task<Result<GetByIdMenuQrCodeResponse>> Handle(GetByIdMenuQrCodeQuery request, CancellationToken cancellationToken)
            {
                var menuQrCode = await menuQrCodeService.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

                GetByIdMenuQrCodeResponse getByIdMenuQrCodeResponse = mapper.Map<GetByIdMenuQrCodeResponse>(menuQrCode);

                return Result<GetByIdMenuQrCodeResponse>.Succeed(getByIdMenuQrCodeResponse);
            }
        }
    }
}
