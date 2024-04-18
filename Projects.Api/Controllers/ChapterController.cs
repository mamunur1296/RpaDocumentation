using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.ChapterFeatures.Commands;
using Project.Application.Features.ChapterFeatures.Queries;


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
        [HttpGet("getChapter/{id}")]
        public async Task<IActionResult> getCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new GetChapterByIdQuery(id)));
        }
        [HttpDelete("DeleteChapter/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteChapterCommand(id)));
        }
        [HttpPut("UpdateChapter/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateChapterCommand commend)
        {
            if (id != commend.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(commend));
        }
    }
}
