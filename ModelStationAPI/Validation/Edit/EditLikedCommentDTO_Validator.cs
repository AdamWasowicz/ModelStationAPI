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
        public EditLikedCommentDTO_Validator(ModelStationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var likedCommentExist = dbContext
                        .LikedComments
                        .Any(lc => lc.Id == value);

                    if (!likedCommentExist)
                        context.AddFailure("LikedComment", "NOT FOUND");
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
