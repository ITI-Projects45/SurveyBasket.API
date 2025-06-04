
namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController([FromKeyedServices("PollService")]IPollService pollService,ILogger<Poll> logger) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;
        private readonly ILogger<Poll> _logger = logger;

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var tmp = _pollService.GetAll();
            foreach (var item in tmp) {
                _logger.LogError(item.Title);
            }
           
            return Ok(_pollService.GetAll());
        }

        [HttpGet("Get/{id:int}")]
        public IActionResult Get(int id)
        {
            Poll poll = _pollService.Get(id);
            return poll is null ? NotFound() : Ok(poll);
        }

    }
}
