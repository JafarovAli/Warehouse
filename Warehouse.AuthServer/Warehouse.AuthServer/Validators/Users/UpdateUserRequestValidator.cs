using FluentValidation;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Models.Request;

namespace Warehouse.AuthServer.Validators.Users
{
    public class UpdateUserRequestValidator : AbstractValidator<Register>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(request => request.UserName).NotEmpty().WithMessage(Constants.RequriedUsername);

            RuleFor(request => request.Password).NotEmpty().WithMessage(Constants.RequriedPassword);

            RuleFor(request => request.Email).NotEmpty().WithMessage(Constants.RequriedEmail)
                                             .EmailAddress().WithMessage(Constants.EmailInvalid);

            RuleFor(request => request.FirstName).NotEmpty().WithMessage(Constants.RequriedFirstname);

            RuleFor(request => request.LastName).NotEmpty().WithMessage(Constants.RequriedLastname);
        }
        
    }
}
