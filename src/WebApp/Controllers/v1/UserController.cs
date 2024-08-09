#nullable disable

using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.v1;

public class UserController : BaseController
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) => this._userRepository = userRepository;

    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> Login(Login login)
    {
        var result = await _userRepository.Login(login);
        return Ok(result);
    }

    [HttpPost("Signup")]
    public async Task<ActionResult<SignupResponse>> Signup(Signup signup)
    {
        var result = await _userRepository.Signup(signup);
        return Ok(result);
    }
}
