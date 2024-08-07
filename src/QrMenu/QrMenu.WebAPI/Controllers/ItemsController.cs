
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using Microsoft.AspNetCore.Mvc;
using QrMenu.Application.Features.Items.Commands.Create;
using QrMenu.Application.Features.Items.Commands.Delete;
using QrMenu.Application.Features.Items.Commands.Update;
using QrMenu.Application.Features.Items.Queries.GetById;
using QrMenu.Application.Features.Items.Queries.GetList;

namespace QrMenu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : BaseController
{

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdItemQuery getByIdItemQuery)
    {
        Result<GetByIdItemResponse> result = await Mediator.Send(getByIdItemQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListItemQuery getListItemQuery = new() { PageRequest = pageRequest };
        Result<GetListResponse<GetListItemResponse>> result = await Mediator.Send(getListItemQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateItemCommand createItemCommand)
    {
        Result<CreatedItemResponse> result = await Mediator.Send(createItemCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateItemCommand  updateItemCommand)
    {
        Result<UpdatedItemResponse> result = await Mediator.Send(updateItemCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteItemCommand deleteItemCommand)
    {
        Result<DeletedItemResponse> result = await Mediator.Send(deleteItemCommand);
        return Ok(result);
    }


}
