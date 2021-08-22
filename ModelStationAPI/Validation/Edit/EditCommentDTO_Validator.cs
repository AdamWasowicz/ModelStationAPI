using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class EditCommentDTO_Validator : AbstractValidator<EditCommentDTO>
    {
        public EditCommentDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var commentExist = dbContext
                        .Comments
                        .Any(c => c.Id == value);

                    if (!commentExist)
                        context.AddFailure("Comment", "NOT FOUND");
                });


            RuleFor(x => x.Text)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(256);
        }
    }
}
