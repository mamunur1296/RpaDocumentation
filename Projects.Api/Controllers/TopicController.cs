using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.TopicFeatures.Commands;
using Project.Application.Features.TopicFeatures.Queries;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateTopicCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllTopic")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllTopicQuery()));
        }
        [HttpGet("getTopic/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetTopicByIdQuery(id)));
        }
        [HttpDelete("DeleteTopic/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteTopicCommand(id)));
        }
        [HttpPut("UpdateTopic/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTopicCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
