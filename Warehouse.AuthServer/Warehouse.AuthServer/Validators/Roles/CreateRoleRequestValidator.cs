using FluentValidation;
using Warehouse.AuthServer.Models;
using Warehouse.AuthServer.Models.Request;

namespace Warehouse.AuthServer.Validators.Roles
{
    public class CreateRoleRequestValidator : AbstractValidator<RegisterRole>
    {
        public CreateRoleRequestValidator()
        {
            RuleFor(role => role.Role).NotEmpty().WithMessage(Constants.RequiredRole);
        }
    }
}
