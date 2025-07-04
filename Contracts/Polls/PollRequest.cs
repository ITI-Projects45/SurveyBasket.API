//namespace SurveyBasket.API.Contracts.Requests;
namespace SurveyBasket.API.Contracts.Polls;

public record PollRequest(
    string Title,
    string Summary,
     bool IsPublished,
    DateOnly StartsAt,
    DateOnly EndsAt
    );
