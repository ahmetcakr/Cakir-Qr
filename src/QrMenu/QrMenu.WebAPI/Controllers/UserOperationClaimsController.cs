﻿using QrMenu.Application.Features.UserOperationClaims.Commands.Create;
using QrMenu.Application.Features.UserOperationClaims.Commands.Delete;
using QrMenu.Application.Features.UserOperationClaims.Commands.Update;
using QrMenu.Application.Features.UserOperationClaims.Queries.GetById;
using QrMenu.Application.Features.UserOperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using QrMenu.WebAPI.Controllers;
using Core.Application.Results;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserOperationClaimQuery getByIdUserOperationClaimQuery)
    {
        Result<GetByIdUserOperationClaimResponse> result = await Mediator.Send(getByIdUserOperationClaimQuery);;   
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserOperationClaimQuery getListUserOperationClaimQuery = new() { PageRequest = pageRequest };
        Result<GetListResponse<GetListUserOperationClaimListItemDto>> result = await Mediator.Send(getListUserOperationClaimQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
    {
        Result<CreatedUserOperationClaimResponse> result = await Mediator.Send(createUserOperationClaimCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
    {
        Result<UpdatedUserOperationClaimResponse> result = await Mediator.Send(updateUserOperationClaimCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
    {
        Result<DeletedUserOperationClaimResponse> result = await Mediator.Send(deleteUserOperationClaimCommand);
        return Ok(result);
    }
}
