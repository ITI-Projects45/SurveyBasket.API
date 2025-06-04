
namespace SurveyBasket.API.Services;

public class PollService : IPollService
{
    private readonly List<Poll> _Polls = [
        new Poll{Id=1,Title="Title1",Description="Description1" },
        new Poll{Id=2,Title="Title2",Description="Description2" },
        new Poll{Id=3,Title="Title3",Description="Description3" },
        new Poll{Id=4,Title="Title4",Description="Description4" },
        new Poll{Id=5,Title="Title5",Description="Description5" },
        new Poll{Id=6,Title="Title6",Description="Description6" },
        ];
    public IEnumerable<Poll> GetAll() => _Polls;
    public Poll? Get(int id) => _Polls.SingleOrDefault(p => p.Id == id);
}
