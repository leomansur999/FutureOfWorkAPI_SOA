using FutureOfWorkAPI.Models.Dtos;
using FutureOfWorkAPI.Models.Shared;
using FutureOfWorkAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureOfWorkAPI.Controllers;

[ApiController]
[Route("api/" + ApiVersions.V1 + "/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<LoginResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LoginResponseDto>), StatusCodes.Status401Unauthorized)]
    public ActionResult<ApiResponse<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        var user = _authService.ValidateUser(request.Username, request.Password);
        if (user is null)
        {
            return Unauthorized(ApiResponse<LoginResponseDto>.Falha("Usuário ou senha inválidos."));
        }

        var token = _authService.GenerateJwtToken(user);

        var response = new LoginResponseDto
        {
            Username = user.Username,
            Role = user.Role,
            Token = token
        };

        return Ok(ApiResponse<LoginResponseDto>.Ok(response, "Login realizado com sucesso."));
    }
}
