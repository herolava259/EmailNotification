using EmailNotification.Application.Commamds;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotification.Application.Validators
{
    public class RemindChangePasswordValidator: AbstractValidator<RemindChangePasswordCommand>
    {
        public RemindChangePasswordValidator() 
        {
            RuleFor(o => o.ExpireDate)
                    .LessThanOrEqualTo(DateTimeOffset.UtcNow);
        }
    }
}
