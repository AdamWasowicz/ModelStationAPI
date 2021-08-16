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

        public EditUserDTO_Validator()
        {
            RuleFor(u => u.Name)
                .MaximumLength(64);


            RuleFor(u => u.Surname)
                .MaximumLength(64);


            RuleFor(u => u.Gender)
                .Custom((value, context) =>
                {
                    var validGender = Genders.Contains(value);

                    if (!validGender)
                        context.AddFailure("Gendr", "NOT VALID");
                });


            RuleFor(u => u.Description)
                .MaximumLength(256);
        }
    }
}
