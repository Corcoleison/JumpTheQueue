using Devon4Net.Infrastructure.FluentValidation;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using FluentValidation;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Validators
{
    public class AccessCodeFluentValidator : CustomFluentValidator<AccessCode>
    {
        public AccessCodeFluentValidator(bool launchExceptionWhenError) : base(launchExceptionWhenError)
        {
        }

        public override void CustomValidate()
        {
            RuleFor(AccessCode => AccessCode.Id).NotNull();
            RuleFor(AccessCode => AccessCode.Id).NotEmpty();
            RuleFor(AccessCode => AccessCode.Code).NotEmpty();
        }
    }
}
