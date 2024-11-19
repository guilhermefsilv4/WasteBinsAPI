using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WasteBinsAPI.Models;
using WasteBinsAPI.Services;
using WasteBinsAPI.ViewModel;

namespace WasteBinsAPI.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IUserService userService, IMapper mapper)
    {
        _authService = authService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginViewModel user)
    {
        var token = _authService.Authenticate(user);
        if (token.Result == null)
        {
            return Unauthorized();
        }

        return Ok(new TokenViewModel(token.Result));
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserViewModel>> Register([FromBody] UserCreateViewModel viewModel)
    {
        var userModel = _mapper.Map<UserModel>(viewModel);
        await _userService.AddUserAsync(userModel);

        var userViewModel = _mapper.Map<UserViewModel>(userModel);
        var uri = Url.Action("GetUser", "User", new { id = userViewModel.UserId }, Request.Scheme);
        return Created(uri, userViewModel);
    }
}