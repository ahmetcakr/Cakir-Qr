
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using Microsoft.AspNetCore.Mvc;
using QrMenu.Application.Features.Categories.Commands.Create;
using QrMenu.Application.Features.Categories.Commands.Delete;
using QrMenu.Application.Features.Categories.Commands.Update;
using QrMenu.Application.Features.Categories.Queries.GetById;
using QrMenu.Application.Features.Categories.Queries.GetList;

namespace QrMenu.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCategoryQuery getByIdCategoryQuery)
    {
        Result<GetByIdCategoryResponse> result = await Mediator.Send(getByIdCategoryQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCategoryQuery getListCategoryQuery = new() { PageRequest = pageRequest };
        Result<GetListResponse<GetListCategoryResponse>> result = await Mediator.Send(getListCategoryQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
    {
        Result<CreatedCategoryResponse> result = await Mediator.Send(createCategoryCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand  updateCategoryCommand)
    {
        Result<UpdatedCategoryResponse> result = await Mediator.Send(updateCategoryCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand deleteCategoryCommand)
    {
        Result<DeletedCategoryResponse> result = await Mediator.Send(deleteCategoryCommand);
        return Ok(result);
    }


}
