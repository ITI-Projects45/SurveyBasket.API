//using SurveyBasket.API.Contracts.Requests;

namespace SurveyBasket.API.Contracts.Polls;

public class PollRequestValidator :AbstractValidator<PollRequest>
{
    public PollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .Length(3,100)
            .WithMessage("{PropertyName} Should Be at least {MinLength} and Maximum {MaxLength} You Entered [{TotalLength}] Characters");
        RuleFor(x => x.Summary)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.StartsAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
        RuleFor(x => x.EndsAt)
           .NotEmpty();

        RuleFor(x => x)
            .Must(hasValideDate)
            .WithMessage("EndsAt must be greater than StartsAt.");

    }

    private bool hasValideDate(PollRequest pollRequest) {

        return pollRequest.EndsAt > pollRequest.StartsAt;
    }
}
