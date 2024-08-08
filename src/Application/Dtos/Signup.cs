#nullable disable

using FluentValidation;

namespace Application.Dtos;

public record Signup(string Username, string Email, string Password, string PasswordHash);

public class SignupValidator : AbstractValidator<Signup>
{
    public SignupValidator()
    {
        // Validate that Name is required and not empty
        RuleFor(signup => signup.Username)
            .NotEmpty().WithMessage("Name is required.");

        // Validate that Email is required and is in a valid email format
        RuleFor(signup => signup.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        // Validate that Password is required and meets minimum length requirements
        RuleFor(signup => signup.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        // Validate that PasswordHash matches Password
        RuleFor(signup => signup.PasswordHash)
            .NotEmpty().WithMessage("Password confirmation is required.")
            .Equal(signup => signup.Password).WithMessage("Passwords do not match.");
    }
}
