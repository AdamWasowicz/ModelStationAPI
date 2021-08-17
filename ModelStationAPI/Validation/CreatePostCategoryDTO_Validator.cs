using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class CreatePostCategoryDTO_Validator : AbstractValidator<CreatePostCategoryDTO>
    {
        public CreatePostCategoryDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(pc => pc.Name)
                .NotEmpty()
                .MaximumLength(32)
                .Custom((value, context) =>
                {
                    var nameInUse = dbContext.PostCategories.Any(pc => pc.Name == value);
                    if (nameInUse)
                        context.AddFailure("Name", "TAKEN");
                });

            RuleFor(pc => pc.Description)
                .MaximumLength(64);
        }
    }
}
