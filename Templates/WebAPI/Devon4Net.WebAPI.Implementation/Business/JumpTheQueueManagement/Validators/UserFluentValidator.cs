using Devon4Net.Infrastructure.FluentValidation;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using FluentValidation;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Validators
{
    public class UserFluentValidator : CustomFluentValidator<User>
    {
        public UserFluentValidator(bool launchExceptionWhenError) : base(launchExceptionWhenError)
        {
        }

        public override void CustomValidate()
        {
            RuleFor(User => User.Clientid).NotNull();
            RuleFor(User => User.Clientid).NotEmpty();
            RuleFor(User => User.Role).NotEmpty();
        }
    }
}
