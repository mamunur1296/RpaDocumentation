using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.QuestionsFeatures.Commands;
using Project.Application.Features.QuestionsFeatures.Queries;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateQuestionsCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllQuestions")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllQuestionsQuery()));
        }
        //[HttpGet("getQuestions/{id}")]
        //public async Task<IActionResult> getCustomer(Guid id)
        //{
        //    return Ok(await _mediator.Send(new GetCustomerByIdQuery(id)));
        //}
        //[HttpGet("getCustomerByEmai/{email}")]
        //public async Task<IActionResult> getCustomerByEmail(string email)
        //{
        //    return Ok(await _mediator.Send(new GetCustomerByEmailQuery(email)));
        //}
        [HttpDelete("DeleteQuestions/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteQuestionsCommand(id)));
        }
        [HttpPut("UpdateQuestions/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateQuestionsCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
