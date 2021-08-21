using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class EditPostDTO_Validator : AbstractValidator<EditPostDTO>
    {
        public EditPostDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var postExist = dbContext
                        .Posts
                        .Any(p => p.Id == value);

                    if (!postExist)
                        context.AddFailure("Post", "NOT FOUND");
                });


            RuleFor(x => x.Title)
                .MaximumLength(32);


            RuleFor(x => x.Text)
                .MaximumLength(256);
        }
    }
}
