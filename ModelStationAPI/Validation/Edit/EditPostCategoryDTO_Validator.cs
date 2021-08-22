using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class EditPostCategoryDTO_Validator : AbstractValidator<EditPostCategoryDTO>
    {
        public EditPostCategoryDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(pc => pc.Id)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var postCategoryExist = dbContext
                        .PostCategories
                        .Any(pc => pc.Id == value);

                    if (!postCategoryExist)
                        context.AddFailure("PostCategory", "NOT FOUND");
                });


            RuleFor(pc => pc.Name)
                .MaximumLength(32);


            RuleFor(pc => pc.Description)
                .MaximumLength(256);
        }
    }
}
