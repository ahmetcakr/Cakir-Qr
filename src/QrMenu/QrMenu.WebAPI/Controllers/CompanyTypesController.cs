using Core.Application.Requests;
using Core.Application.Responses;
using QrMenu.Application.Features.CompanyTypes.Commands.Create;
using QrMenu.Application.Features.CompanyTypes.Commands.Delete;
using QrMenu.Application.Features.CompanyTypes.Commands.Update;
using QrMenu.Application.Features.CompanyTypes.Queries.GetById;
using QrMenu.Application.Features.CompanyTypes.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace QrMenu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTypesController : BaseController
    {

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdCompanyTypeQuery getByIdCompanyTypeQuery)
        {
            Result<GetByIdCompanyTypeResponse> result = await Mediator.Send(getByIdCompanyTypeQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCompanyTypeQuery getListCompanyTypeQuery = new() { PageRequest = pageRequest };
            Result<GetListResponse<GetListCompanyTypeResponse>> result = await Mediator.Send(getListCompanyTypeQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCompanyTypeCommand createCompanyTypeCommand)
        {
            Result<CreatedCompanyTypeResponse> result = await Mediator.Send(createCompanyTypeCommand);
            return Created(uri: "", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyTypeCommand updateCompanyTypeCommand)
        {
            Result<UpdatedCompanyTypeResponse> result = await Mediator.Send(updateCompanyTypeCommand);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCompanyTypeCommand deleteCompanyTypeCommand)
        {
            Result<DeletedCompanyTypeResponse> result = await Mediator.Send(deleteCompanyTypeCommand);
            return Ok(result);
        }
    }
}
