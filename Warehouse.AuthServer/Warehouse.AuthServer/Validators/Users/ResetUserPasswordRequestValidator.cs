using FluentValidation;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Models.Request;

namespace Warehouse.AuthServer.Validators.Users
{
    public class ResetUserPasswordRequestValidator : AbstractValidator<Register>
    {
        public ResetUserPasswordRequestValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Constants.RequriedPassword)
                .MinimumLength(8).WithMessage(Constants.PasswordCharactersLong)
                .Matches("[A-Z]").WithMessage(Constants.PasswordUppercase)
                .Matches("[a-z]").WithMessage(Constants.PasswordLowercase)
                .Matches("[0-9]").WithMessage(Constants.PasswordNumericDigit)
                .Matches("[^a-zA-Z0-9]").WithMessage(Constants.PasswordSpecialCharacter);
        }
    }
}
