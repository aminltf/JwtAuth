#nullable disable

using Application.Dtos;
using Domain.Common;

namespace Application.Interfaces;

public interface IUserRepository : IGenericRepository
{
    Task<SignupResponse> Signup(Signup signup);
    Task<LoginResponse> Login(Login login);
}
