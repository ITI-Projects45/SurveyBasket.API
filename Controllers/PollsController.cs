


namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController([FromKeyedServices("PollService")] IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var polls = _pollService.GetAll();
            var response = polls.Adapt<IEnumerable<PollResponse>>();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            Poll poll = _pollService.Get(id);
            if (poll is null) { return NotFound(); }
            var response = poll.Adapt<PollResponse>();
            return response is null ? NotFound() : Ok(response);
        }

        [HttpPost("")]
        public IActionResult Add([FromBody] CreatePollRequest request)
        {
            var newPoll = _pollService.Add(request.Adapt<Poll>());
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CreatePollRequest request)
        {

            var IsUpdated = _pollService.Update(id, request.Adapt<Poll>());
            return IsUpdated ? NoContent() : NotFound();

        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var IsDeleted = _pollService.Delete(id);
            return IsDeleted ? NoContent() : NotFound();
        }

    }
    }
