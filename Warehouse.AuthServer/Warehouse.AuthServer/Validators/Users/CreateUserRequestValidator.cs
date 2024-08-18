using FluentValidation;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Models.Request;
using Warehouse.AuthServer.Services.Users;

namespace Warehouse.AuthServer.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<Register>
    {
        public CreateUserRequestValidator(IUserService userService, Register register)
        {
            RuleFor(user => user.UserName).NotEmpty()
                                          .MustAsync(async (Username,CancellationToken) =>
                                          {
                                              var users = await userService.CreateUserAsync(register);
                                              return users is null;
                                          })
                                        .WithMessage(Constants.UserAlready);

            RuleFor(user => user.Email).NotEmpty().WithMessage(Constants.RequriedEmail).EmailAddress()
                                       .WithMessage(Constants.EmailInvalid);

            RuleFor(user => user.FirstName).NotEmpty()
                                           .WithMessage(Constants.RequriedFirstname);

            RuleFor(user => user.LastName).NotEmpty()
                                          .WithMessage(Constants.RequriedLastname);

            RuleFor(user => user.Password).NotEmpty()
                                          .WithMessage(Constants.RequriedPassword)
                                          .Length(8, 100)
                                          .WithMessage(Constants.PasswordMessage);
        }
    }
}
