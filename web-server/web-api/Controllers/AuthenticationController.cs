using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Home_API.Controllers;

using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Home_API.Models.Dtos;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        IUserService userService,
        IMapper mapper)
    {
        _logger = logger;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task Login(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
    }

    [HttpPost]
    public IResult Logout() => Results.Ok();

    [HttpPost]
    public async Task<EntityState> Register(
        User user, 
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _userService.AddUser(user, cancellationToken);
    }

    [HttpPatch]
    public IResult Update() => Results.Ok();

    [HttpGet]
    public async Task<List<UserDto>> Users(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var users = await _userService.GetAllUsers(cancellationToken);

        return this._mapper.Map<List<UserDto>>(users);
    }
}
