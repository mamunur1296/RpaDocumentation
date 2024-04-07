using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.CustomerFeatures.Commands;
using Project.Application.Features.CustomerFeatures.Queries;


namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCustomerCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllCustomer")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllCustomerQuery()));
        }
        [HttpGet("getCustomer/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetCustomerByIdQuery(id)));
        }
        [HttpGet("getCustomerByEmai/{email}")]
        public async Task<IActionResult> getCustomerByEmail(string email)
        {
            return Ok(await _mediator.Send(new GetCustomerByEmailQuery(email)));
        }
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCustomerCommand(id)));
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
