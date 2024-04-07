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
    }
}
