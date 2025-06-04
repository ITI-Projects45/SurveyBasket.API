namespace SurveyBasket.API.Services;

public interface IPollService
{
    IEnumerable<Poll> GetAll();
    Poll? Get(int id);
}
