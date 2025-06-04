
namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController([FromKeyedServices("PollService")] IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_pollService.GetAll());
        }

        [HttpGet("Get/{id:int}")]
        public IActionResult Get(int id)
        {
            Poll poll = _pollService.Get(id);
            return poll is null ? NotFound() : Ok(poll);
        }

        [HttpPost("")]
        public IActionResult Add(Poll Request) {
            Poll newPoll = _pollService.Add(Request);
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }

    }
}
