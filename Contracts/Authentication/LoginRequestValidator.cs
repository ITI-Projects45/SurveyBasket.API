namespace SurveyBasket.API.Contracts.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(v => v.Password)
            .NotEmpty();
    }
}
