using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class CreateCommentDTO_Validator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(c => c.Text)
                .NotEmpty();

            RuleFor(c => c.UserId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var userExist = dbContext.Users.Any(u => u.Id == value);

                    if (!userExist)
                        context.AddFailure("User", "NOT FOUND");
                });


            RuleFor(c => c.PostId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var postExist = dbContext.Posts.Any(p => p.Id == value);

                    if (!postExist)
                        context.AddFailure("Post", "NOT FOUND");
                });
        }
    }
}
