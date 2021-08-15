using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class CreateUserDTO_Validator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                        context.AddFailure("Email", "TAKEN");
                });


            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);


            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);

            RuleFor(x => x.UserName).Custom((value, context) =>
            {
                var userNameInUse = dbContext.Users.Any(u => u.UserName == value);
                if (userNameInUse)
                    context.AddFailure("UserName", "TAKEN");
            });
        }
    }
}
