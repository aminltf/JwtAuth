#nullable disable

using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UserRepository(AppDbContext context, IConfiguration configuration)
    {
        _context = context; _configuration = configuration;
    }

    private async Task<User> FindUserByEmail(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<LoginResponse> Login(Login login)
    {
        var getUser = await FindUserByEmail(login.Email);
        if (getUser == null) 
            return new LoginResponse(false, "User Not Found.");

        bool checkPassword = BCrypt.Net.BCrypt.Verify(login.Password, getUser.Password);
        if (checkPassword) 
            return new LoginResponse(true, "Login Succeeded.", GenerateJwtToken(getUser));
        else 
            return new LoginResponse(false, "Login Failed");
    }

    public async Task<SignupResponse> Signup(Signup signup)
    {
        var getUser = await FindUserByEmail(signup.Email);
        if (getUser == null)
            return new SignupResponse(false, "User Already Exists.");

        _context.Users.Add(new User()
        {
            Username = signup.Username,
            Email = signup.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(signup.Password)
        });
        await _context.SaveChangesAsync();
        return new SignupResponse(true, "Signup Completed.");
    }
}
