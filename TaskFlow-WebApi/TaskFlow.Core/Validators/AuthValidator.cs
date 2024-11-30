using System;
using FluentValidation;
using static TaskFlow.Core.DTOs.AuthRequestDto;

namespace TaskFlow.Core.Validators;

public class UserLoginAuthValidator : AbstractValidator<UserLoginAuthRequestDto>
{
    public UserLoginAuthValidator()
    {
        RuleFor(usr => usr.Email).NotEmpty().WithMessage("Email is Required");
        RuleFor(usr => usr.Password).NotEmpty().WithMessage("Password is Required");
    }
}
