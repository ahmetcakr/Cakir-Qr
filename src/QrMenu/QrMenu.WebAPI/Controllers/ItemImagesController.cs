
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using Core.Helpers.ImageHelper;
using Microsoft.AspNetCore.Mvc;
using QrMenu.Application.Features.ItemImages.Commands.Create;
using QrMenu.Application.Features.ItemImages.Commands.Delete;
using QrMenu.Application.Features.ItemImages.Commands.Update;
using QrMenu.Application.Features.ItemImages.Queries.GetById;
using QrMenu.Application.Features.ItemImages.Queries.GetList;
using QrMenu.Application.Features.ItemImages.Requests;

namespace QrMenu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemImagesController : BaseController
{

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdItemImageQuery getByIdItemImageQuery)
    {
        Result<GetByIdItemImageResponse> result = await Mediator.Send(getByIdItemImageQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListItemImageQuery getListItemImageQuery = new() { PageRequest = pageRequest };
        Result<GetListResponse<GetListItemImageResponse>> result = await Mediator.Send(getListItemImageQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] CreateItemImageCommand createItemImageCommand)
    {
        Result<CreatedItemImageResponse> result = await Mediator.Send(createItemImageCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateItemImageCommand  updateItemImageCommand)
    {
        Result<UpdatedItemImageResponse> result = await Mediator.Send(updateItemImageCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteItemImageCommand deleteItemImageCommand)
    {
        Result<DeletedItemImageResponse> result = await Mediator.Send(deleteItemImageCommand);
        return Ok(result);
    }

}
