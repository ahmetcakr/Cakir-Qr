
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using QrMenu.Domain.Entities;
using QrMenu.Application.Features.MenuQrCodes.Constants;
using QrMenu.Application.Features.MenuQrCodes.Rules;
using MediatR;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Results;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Services.MenuQrCodesService;

namespace QrMenu.Application.Features.MenuQrCodes.Commands.Update
{
    public class UpdateMenuQrCodeCommand : IRequest<Result<UpdatedMenuQrCodeResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
public string QrCodeText { get; set; }
public Byte[] QrCode { get; set; }

        public string[] Roles => new [] 
        {
            MenuQrCodeOperationClaims.Admin,
            MenuQrCodeOperationClaims.Write, 
            MenuQrCodeOperationClaims.Update
        };

        public bool BypassCache => false;
        public string? CacheKey => "";
        public string? CacheGroupKey => "GetMenuQrCodes";

        internal sealed class UpdateMenuQrCodeCommandHandler(
            IMenuQrCodeService _menuqrcodeService,
            IMapper _mapper,
            MenuQrCodeBusinessRules _menuqrcodeBusinessRules) : IRequestHandler<UpdateMenuQrCodeCommand, Result<UpdatedMenuQrCodeResponse>>
        {

            public async Task<Result<UpdatedMenuQrCodeResponse>> Handle(UpdateMenuQrCodeCommand request, CancellationToken cancellationToken)
            {
                await _menuqrcodeBusinessRules.MenuQrCodeIdShouldBeExist(request.Id);

                MenuQrCode? menuqrcode = await _menuqrcodeService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (menuqrcode is null)
                    throw new BusinessException("Entity does not exist.");

                _mapper.Map(request, menuqrcode);

                MenuQrCode updatedMenuQrCode = await _menuqrcodeService.UpdateAsync(menuqrcode);

                UpdatedMenuQrCodeResponse response = _mapper.Map<UpdatedMenuQrCodeResponse>(updatedMenuQrCode);

                return Result<UpdatedMenuQrCodeResponse>.Succeed(response);
            }
        }
    }
}
