using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Results;
using Core.Helpers.QrHelper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using QrMenu.Application.Features.MenuQrCodes.Constants;
using QrMenu.Application.Services.MenuQrCodesService;
using QrMenu.Domain.Entities;

namespace QrMenu.Application.Features.MenuQrCodes.Commands.Create;

public class CreateMenuQrCodeCommand : IRequest<Result<CreatedMenuQrCodeResponse>>, ISecuredRequest, ICacheRemoverRequest
{
    public int MenuId { get; set; }
    public string QrCodeText { get; set; }
    //public Byte[] QrCode { get; set; }

    public string[] Roles => new [] 
    {
        MenuQrCodeOperationClaims.Admin,
        MenuQrCodeOperationClaims.Write, 
        MenuQrCodeOperationClaims.Add
    };

    public bool BypassCache => false;
    public string? CacheKey => "";
    public string? CacheGroupKey => "GetMenuQrCodes";

    internal sealed class CreateMenuQrCodeCommandHandler : IRequestHandler<CreateMenuQrCodeCommand, Result<CreatedMenuQrCodeResponse>>
    {
        private readonly IMenuQrCodeService _menuqrcodeService;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CreateMenuQrCodeCommandHandler(IMenuQrCodeService menuqrcodeService, IMapper mapper, ISendEndpointProvider sendEndpointProvider)
        {
            _menuqrcodeService = menuqrcodeService;
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<Result<CreatedMenuQrCodeResponse>> Handle(CreateMenuQrCodeCommand request, CancellationToken cancellationToken)
        {

            MenuQrCode menuQrCode = _mapper.Map<MenuQrCode>(request);

            MenuQrCode createdMenuQrCode = await _menuqrcodeService.AddAsync(menuQrCode);

            CreatedMenuQrCodeResponse response = _mapper.Map<CreatedMenuQrCodeResponse>(createdMenuQrCode);

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:created-menu-qr-code"));

            await sendEndpoint.Send<CreatedMenuQrCodeResponse>(response);
            // burada rabbitmq tarafýna gönderilip qr code oluþturulabilir.

            return Result<CreatedMenuQrCodeResponse>.Succeed(response, StatusCodes.Status201Created);
        }
    }
}
