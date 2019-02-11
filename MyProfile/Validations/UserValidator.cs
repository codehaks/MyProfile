using FluentValidation;
using MyProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProfile.Validations
{
    public class MyUserValidator : AbstractValidator<User>
    {
        public MyUserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("User name is required.");
            RuleFor(u => u.Age).NotEmpty().WithMessage("User age is required.")
                .InclusiveBetween(12,99).WithMessage("Age must be more than 12.");
        }
    }
}
