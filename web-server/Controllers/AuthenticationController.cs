using Microsoft.AspNetCore.Mvc;

namespace web_server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController: ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(
        ILogger<AuthenticationController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IResult Login() => Results.Ok();

    [HttpPost]
    public IResult Logout() => Results.Ok();

    [HttpPost]
    public IResult Register() => Results.Ok();

    [HttpPatch]
    public IResult Update() => Results.Ok();
}