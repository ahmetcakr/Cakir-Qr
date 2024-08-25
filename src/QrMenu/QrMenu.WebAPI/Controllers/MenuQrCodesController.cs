
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using Microsoft.AspNetCore.Mvc;
using QrMenu.Application.Features.MenuQrCodes.Commands.Create;
using QrMenu.Application.Features.MenuQrCodes.Commands.Delete;
using QrMenu.Application.Features.MenuQrCodes.Commands.Update;
using QrMenu.Application.Features.MenuQrCodes.Queries.GetById;
using QrMenu.Application.Features.MenuQrCodes.Queries.GetList;

namespace QrMenu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuQrCodesController : BaseController
{

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdMenuQrCodeQuery getByIdMenuQrCodeQuery)
    {
        Result<GetByIdMenuQrCodeResponse> result = await Mediator.Send(getByIdMenuQrCodeQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMenuQrCodeQuery getListMenuQrCodeQuery = new() { PageRequest = pageRequest };
        Result<GetListResponse<GetListMenuQrCodeResponse>> result = await Mediator.Send(getListMenuQrCodeQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMenuQrCodeCommand createMenuQrCodeCommand)
    {
        Result<CreatedMenuQrCodeResponse> result = await Mediator.Send(createMenuQrCodeCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMenuQrCodeCommand  updateMenuQrCodeCommand)
    {
        Result<UpdatedMenuQrCodeResponse> result = await Mediator.Send(updateMenuQrCodeCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteMenuQrCodeCommand deleteMenuQrCodeCommand)
    {
        Result<DeletedMenuQrCodeResponse> result = await Mediator.Send(deleteMenuQrCodeCommand);
        return Ok(result);
    }


}
