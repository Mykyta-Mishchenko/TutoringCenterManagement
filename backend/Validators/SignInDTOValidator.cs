﻿using backend.DTO.AuthDTO;
using FluentValidation;

namespace JwtBackend.Validators
{
    public class SignInDTOValidator : AbstractValidator<SignInDTO>
    {
        public SignInDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[_-])[A-Za-z\\d_-]{8,}$")
                .WithMessage("Password must be at least 8 characters long and contain uppercase, lowercase, number, and special character");
        }
    }
}
