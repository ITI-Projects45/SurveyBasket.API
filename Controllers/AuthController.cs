using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.API.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("")]
    public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
         
        var AuthResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);

        return AuthResult is null ? BadRequest("Invalid Email Or Password") : Ok(AuthResult);
    }

}
