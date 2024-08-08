#nullable disable

using FluentValidation;

namespace Application.Dtos;

public record Login(string Email, string Password);

public class LoginValidator : AbstractValidator<Login>
{
    public LoginValidator()
    {
        // Validate that the Email is not empty and is a valid email format
        RuleFor(login => login.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        // Validate that the Password is not empty and meets minimum length requirements
        RuleFor(login => login.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}
