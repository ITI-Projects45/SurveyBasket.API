
using Microsoft.AspNetCore.Identity;
using SurveyBasket.API.Authentication;

namespace SurveyBasket.API.Services;

public class AuthService(UserManager<ApplicationUser> userManager, IJWTProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManger = userManager;
    private readonly IJWTProvider jWTProvider = jwtProvider;

    public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManger.FindByEmailAsync(email);
        if (user is null) { return null; }
        // Check if the user exists and if the password is valid
        var isValidPassword = await _userManger.CheckPasswordAsync(user, password);
        if (isValidPassword) { return null; }
        // If the password is valid, generate a JWT token
        var (token, expiresIn) = jWTProvider.GenerateToken(user);
        // Return the AuthResponse with user details and token
        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);
    }
}
