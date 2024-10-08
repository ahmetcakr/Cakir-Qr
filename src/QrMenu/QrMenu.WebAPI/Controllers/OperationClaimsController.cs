﻿using QrMenu.Application.Features.OperationClaims.Commands.Create;
using QrMenu.Application.Features.OperationClaims.Commands.Delete;
using QrMenu.Application.Features.OperationClaims.Commands.Update;
using QrMenu.Application.Features.OperationClaims.Queries.GetById;
using QrMenu.Application.Features.OperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using QrMenu.WebAPI.Controllers;
using Core.Application.Results;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdOperationClaimQuery getByIdOperationClaimQuery)
    {
        Result<GetByIdOperationClaimResponse> result = await Mediator.Send(getByIdOperationClaimQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
        Result<GetListResponse<GetListOperationClaimListItemDto>> result = await Mediator.Send(getListOperationClaimQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
    {
        Result<CreatedOperationClaimResponse> result = await Mediator.Send(createOperationClaimCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
    {
        Result<UpdatedOperationClaimResponse> result = await Mediator.Send(updateOperationClaimCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
    {
        Result<DeletedOperationClaimResponse> result = await Mediator.Send(deleteOperationClaimCommand);
        return Ok(result);
    }
}
