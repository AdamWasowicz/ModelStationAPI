using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class EditLikedPostDTO_Validator : AbstractValidator<EditLikedPostDTO>
    {
        public EditLikedPostDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var likedPostExist = dbContext
                        .LikedPosts
                        .Any(lp => lp.Id == value);

                    if (!likedPostExist)
                        context.AddFailure("LikedPost", "NOT FOUND");
                });


            RuleFor(x => x.Value)
                .Custom((value, context) =>
                {
                    var valueValid = (value == -1) || (value == 0) || (value == 1);

                    if (!valueValid)
                        context.AddFailure("Value", "INVALID");
                });
        }
    }
}
