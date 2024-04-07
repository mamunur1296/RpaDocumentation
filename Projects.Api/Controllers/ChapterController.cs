using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.ChapterFeatures.Commands;
using Project.Application.Features.ChapterFeatures.Queries;
using Project.Application.Features.TopicFeatures.Commands;
using Project.Application.Features.TopicFeatures.Queries;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChapterController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateChapterCommand commend)
        {
            return Ok(await _mediator.Send(commend));
        }
        [HttpGet("getAllChapter")]
        public async Task<IActionResult> getAllCustomer()
        {
            return Ok(await _mediator.Send(new GetAllChapterQuery()));
        }
    }
}
