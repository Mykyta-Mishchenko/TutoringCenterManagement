﻿using backend.DTO.AuthDTO;
using FluentValidation;

namespace JwtBackend.Validators
{
    public class SignUpDTOValidator : AbstractValidator<SignUpDTO>
    {
        public SignUpDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .Matches("^[a-zA-Z][a-zA-Z0-9_-]{3,}$")
                .WithMessage("First Name must start with a letter and be at least 4 characters long");

            RuleFor(x => x.LastName)
                .Matches("^[a-zA-Z][a-zA-Z0-9_-]{3,}$")
                .WithMessage("Last Name must start with a letter and be at least 4 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required")
                .IsInEnum().WithMessage("Invalid role specified");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[_-])[A-Za-z\\d_-]{8,}$")
                .WithMessage("Password must be at least 8 characters long and contain uppercase, lowercase, number, and special character");

            RuleFor(x => x.ConfirmedPassword)
                .NotEmpty().WithMessage("Confirm Password is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
