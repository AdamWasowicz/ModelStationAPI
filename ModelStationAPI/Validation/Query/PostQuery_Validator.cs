using FluentValidation;
using ModelStationAPI.Entities;
using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Validation
{
    public class PostQuery_Validator : AbstractValidator<PostQuery>
    {
        public PostQuery_Validator()
        {
            RuleFor(x => x.CurrentPage)
                .GreaterThan(0);


            RuleFor(x => x.NumberOfPosts)
                .GreaterThan(0)
                .LessThan(20);
        }
    }
}
