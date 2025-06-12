
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
            return Ok(_pollService.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            Poll poll = _pollService.Get(id);
            return poll is null ? NotFound() : Ok(poll);
        }

        [HttpPost("")]
        public IActionResult Add([FromBody] Poll Request)
        {
            Poll newPoll = _pollService.Add(Request);
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Poll request)
        {

            var IsUpdated = _pollService.Update(id, request);
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
