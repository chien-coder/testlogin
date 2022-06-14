using FluentValidation;
using System.Text.RegularExpressions;
using UserAPI_NetCore6.Models;

namespace UserAPI_NetCore6.Validation
{
    public class UserValidation: AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(u => u.username).NotEmpty().MinimumLength(3)
                .MaximumLength(15);
            RuleFor(u => u.username)
                .Must(un => Regex.IsMatch(un, "^[a-zA-Z0-9_]*$"))
                .WithMessage("Username does not contain special characters");
            RuleFor(u => u.password).NotEmpty().MaximumLength(22);
        }
    }
}
