using System;
using System.Data;
using FluentValidation;
using TaskFlow.Core.DTOs;

namespace TaskFlow.Core.Validators;

public class UserLoginAuthValidator : AbstractValidator<UserRegisterAuthRequestDto>
{
    public UserLoginAuthValidator()
    {
        RuleFor(usr => usr.Email).NotEmpty().WithMessage("Email is Required");
        RuleFor(usr => usr.Password).NotEmpty().WithMessage("Password is Required");
    }
}

public class UserRegisterAuthValidator : AbstractValidator<UserRegisterAuthRequestDto>
{
    public UserRegisterAuthValidator()
    {
        RuleFor(usr => usr.Username).NotEmpty().WithMessage("Username is empty");
        RuleFor(usr => usr.Password).Must(CheckValidPassword)
            .WithMessage((usr, password) => GetPasswordErrorMessage(password));
        RuleFor(usr => usr.Email).EmailAddress().WithMessage("Email address is not valid");
        RuleFor(usr => usr.Email).NotEmpty().WithMessage("Email address cannot be empty");
    }

    private bool CheckValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return false;

        bool hasSpecialChar = ContainSpecialCharacter(password);
        bool hasUpperCase = ContainUpperCaseLetter(password);
        bool hasDigit = ContainDigit(password);

        return hasSpecialChar && hasUpperCase && hasDigit;
    }

    private string GetPasswordErrorMessage(string password)
    {
        if (string.IsNullOrEmpty(password)) return "Password field is empty.";

        string errorMessage = "Password must contain";

        if (!ContainSpecialCharacter(password)) errorMessage += " at least one special character,";
        if (!ContainUpperCaseLetter(password)) errorMessage += " at least one uppercase letter,";
        if (!ContainDigit(password)) errorMessage += " at least one digit,";
        if (errorMessage.EndsWith(","))
            errorMessage = errorMessage.Remove(errorMessage.Length - 1) + ".";

        return errorMessage;
    }

    private bool ContainSpecialCharacter(string password)
    {
        return password.Any(ch => !char.IsLetterOrDigit(ch));
    }

    private bool ContainUpperCaseLetter(string password)
    {
        return password.Any(char.IsUpper);
    }

    private bool ContainDigit(string password)
    {
        return password.Any(char.IsDigit);
    }
}