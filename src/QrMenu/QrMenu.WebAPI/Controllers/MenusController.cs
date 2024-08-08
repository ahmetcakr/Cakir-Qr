
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using Microsoft.AspNetCore.Mvc;
using QrMenu.Application.Features.Menus.Commands.Create;
using QrMenu.Application.Features.Menus.Commands.Delete;
using QrMenu.Application.Features.Menus.Commands.Update;
using QrMenu.Application.Features.Menus.Queries.GetById;
using QrMenu.Application.Features.Menus.Queries.GetList;

namespace QrMenu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenusController : BaseController
{

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdMenuQuery getByIdMenuQuery)
    {
        Result<GetByIdMenuResponse> result = await Mediator.Send(getByIdMenuQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMenuQuery getListMenuQuery = new() { PageRequest = pageRequest };
        Result<GetListResponse<GetListMenuResponse>> result = await Mediator.Send(getListMenuQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMenuCommand createMenuCommand)
    {
        Result<CreatedMenuResponse> result = await Mediator.Send(createMenuCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMenuCommand  updateMenuCommand)
    {
        Result<UpdatedMenuResponse> result = await Mediator.Send(updateMenuCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteMenuCommand deleteMenuCommand)
    {
        Result<DeletedMenuResponse> result = await Mediator.Send(deleteMenuCommand);
        return Ok(result);
    }


}
