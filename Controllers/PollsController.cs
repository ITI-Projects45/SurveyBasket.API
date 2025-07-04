


using Microsoft.AspNetCore.Authorization;
using SurveyBasket.API.Contracts.Polls;
//using SurveyBasket.API.Contracts.Requests;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController([FromKeyedServices("PollService")] IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var polls = await _pollService.GetAllAsync(cancellationToken);

            var response = polls.Adapt<IEnumerable<PollResponse>>();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            Poll poll = await _pollService.GetAsync(id, cancellationToken);
            if (poll is null) { return NotFound(); }
            var response = poll.Adapt<PollResponse>();
            return response is null ? NotFound() : Ok(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellationToken)
        {
            var newPoll = await _pollService.AddAsync(request.Adapt<Poll>(), cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken = default)
        {

            var IsUpdated = await _pollService.UpdateAsync(id, request.Adapt<Poll>(), cancellationToken);
            return IsUpdated ? NoContent() : NotFound();

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var IsDeleted = await _pollService.DeleteAsync(id, cancellationToken);
            return IsDeleted ? NoContent() : NotFound();
        }
        [HttpPut("{id:int}/togglePublish")]
        public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken = default)
        {

            var IsUpdated = await _pollService.TogglePublishStatusAsync(id, cancellationToken);
            return IsUpdated ? NoContent() : NotFound();

        }
    }
}
