using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class EditLikedCommentDTO_Validator : AbstractValidator<EditLikedCommentDTO>
    {
        public EditLikedCommentDTO_Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();


            RuleFor(x => x.Value)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var valueValid = (value == -1) || (value == 0) || (value == 1);

                    if (!valueValid)
                        context.AddFailure("Value", "INVALID");
                });
        }
    }
}
