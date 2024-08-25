
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

namespace QrMenu.Application.Features.MenuQrCodes.Commands.Delete
{
    public class DeleteMenuQrCodeCommand : IRequest<Result<DeletedMenuQrCodeResponse>>, ISecuredRequest, ICacheRemoverRequest
    {
        public int Id { get; set; }

        public string[] Roles => new [] 
        {
            MenuQrCodeOperationClaims.Admin,
            MenuQrCodeOperationClaims.Delete, 
            MenuQrCodeOperationClaims.Write
        };

        public bool BypassCache => false;
        public string? CacheKey => "";
        public string? CacheGroupKey => "GetMenuQrCodes";

        internal sealed class DeleteMenuQrCodeCommandHandler : IRequestHandler<DeleteMenuQrCodeCommand, Result<DeletedMenuQrCodeResponse>>
        {
            private readonly IMenuQrCodeService _menuqrcodeService;
            private readonly IMapper _mapper;
            private readonly MenuQrCodeBusinessRules _menuqrcodeBusinessRules;

            public DeleteMenuQrCodeCommandHandler(IMenuQrCodeService menuqrcodeService, IMapper mapper,MenuQrCodeBusinessRules menuqrcodeBusinessRules)
            {
                _menuqrcodeService = menuqrcodeService;
                _mapper = mapper;
                _menuqrcodeBusinessRules = menuqrcodeBusinessRules;    
            }

            public async Task<Result<DeletedMenuQrCodeResponse>> Handle(DeleteMenuQrCodeCommand request, CancellationToken cancellationToken)
            {
                await _menuqrcodeBusinessRules.MenuQrCodeIdShouldBeExist(request.Id);

                MenuQrCode? menuqrcode = await _menuqrcodeService.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (menuqrcode is null)
                    throw new BusinessException("Entity does not exist.");

                await _menuqrcodeService.DeleteAsync(menuqrcode);

                DeletedMenuQrCodeResponse response = _mapper.Map<DeletedMenuQrCodeResponse>(menuqrcode);

                return Result<DeletedMenuQrCodeResponse>.Succeed(response);
            }
        }
    }
}
