using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ModelStationAPI.Validation
{
    public class CreatePostDTO_Validator : AbstractValidator<CreatePostDTO>
    {
        public CreatePostDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(32)
                .Custom((value, context) =>
                {
                    var nameInUse = dbContext.Posts.Any(p => p.Title == value);
                    if (nameInUse)
                        context.AddFailure("Title", "TAKEN");
                });


            RuleFor(p => p.Text)
                .NotEmpty()
                .MaximumLength(256);


            RuleFor(p => p.UserId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var userExist = dbContext.Users.Any(u => u.Id == value);
                    if (!userExist)
                        context.AddFailure("User", "NOT FOUND");
                });


            RuleFor(p => p.PostCategoryId)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        var postCategoryExists = dbContext.PostCategories.Any(pc => pc.Id == value);
                        if (!postCategoryExists)
                            context.AddFailure("PostCategory", "NOT FOUND");
                    }
                });
        }
    }
}
