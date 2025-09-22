using Application.Features.Auth.DTOs;

namespace Application.Common.Abstractions.Services;

public interface IAuthService
{
    Task<SignupResponse> Signup(SignupRequest signup);
    Task<LoginResponse> Login(LoginRequest login);
}
