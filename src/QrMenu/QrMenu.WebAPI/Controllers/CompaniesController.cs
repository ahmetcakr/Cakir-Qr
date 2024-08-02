using Core.Application.Requests;
using Core.Application.Responses;
using QrMenu.Application.Features.Companies.Commands.Create;
using QrMenu.Application.Features.Companies.Commands.Delete;
using QrMenu.Application.Features.Companies.Commands.Update;
using QrMenu.Application.Features.Companies.Queries.GetById;
using QrMenu.Application.Features.Companies.Queries.GetList;
using QrMenu.Application.Features.Users.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace QrMenu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : BaseController
    {

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCompanyQuery getByIdCompanyQuery)
        {
            Result<GetByIdCompanyResponse> result = await Mediator.Send(getByIdCompanyQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCompanyQuery getListUserQuery = new() { PageRequest = pageRequest };
            Result<GetListResponse<GetListCompanyResponse>> result = await Mediator.Send(getListUserQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCompanyCommand createCompanyCommand)
        {
            Result<CreatedCompanyResponse> result = await Mediator.Send(createCompanyCommand);
            return Created(uri: "", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyCommand  updateCompanyCommand)
        {
            Result<UpdatedCompanyResponse> result = await Mediator.Send(updateCompanyCommand);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCompanyCommand deleteCompanyCommand)
        {
            Result<DeletedCompanyResponse> result = await Mediator.Send(deleteCompanyCommand);
            return Ok(result);
        }
    }
}
