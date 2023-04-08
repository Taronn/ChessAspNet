using FluentValidation;
using Chess.DTOs;

namespace Chess.Validators
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator()
        {

            RuleFor(form => form.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(6).WithMessage("Username must be at least 6 characters")
                .MaximumLength(15).WithMessage("Username is too long. It should have 15 characters or fewer")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username must be alphanumeric");

            RuleFor(form => form.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("First name must be alphanumeric");

            RuleFor(form => form.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("Last name must be alphanumeric");

            RuleFor(form => form.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(form => form.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .MaximumLength(32).WithMessage("Password is too long. It should have 32 characters or fewer");

            RuleFor(form => form.PasswordConfirmation)
                .Must((form, PasswordConfirmation) => PasswordConfirmation == form.Password)
                .WithMessage("Passwords do not match");
        }


    }
}
