using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class CreateLikedPostDTO_Validator : AbstractValidator<CreateLikedPostDTO>
    {
        public CreateLikedPostDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var userExist = dbContext
                        .Users
                        .Any(u => u.Id == value);

                    if (!userExist)
                        context.AddFailure("User", "NOT FOUND");
                });


            RuleFor(x => x.PostId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var postExist = dbContext
                        .Posts
                        .Any(p => p.Id == value);

                    if (!postExist)
                        context.AddFailure("Post", "NOT FOUND");
                });

            RuleFor(x => x.Value)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var valueIsValid = (value == -1) || (value == 0) || (value == 1);
                    if (!valueIsValid)
                        context.AddFailure("Value", "INVALID");
                });
        }
    }
}
