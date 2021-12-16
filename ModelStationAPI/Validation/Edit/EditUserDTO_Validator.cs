using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class EditUserDTO_Validator : AbstractValidator<EditUserDTO>
    {
        private readonly List<char> Genders = new List<char> { 'F', 'M' };

        public EditUserDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(u => u.Name)
                .MaximumLength(64);


            RuleFor(u => u.Surname)
                .MaximumLength(64);


            RuleFor(u => u.Gender)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        var validGender = Genders.Contains(Convert.ToChar(value));

                        if (!validGender)
                            context.AddFailure("Gender", "INVALID");
                    }
                });


            RuleFor(u => u.Description)
                .MaximumLength(256);
        }
    }
}
