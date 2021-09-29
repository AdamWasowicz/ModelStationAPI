using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ModelStationAPI.Entities;

namespace ModelStationAPI.Authorization
{
    public class ResourceOperationRequirementUserHandler
        : AuthorizationHandler<ResourceOperationRequirementUser, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirementUser requirement, User user)
        {
            var accessLevel = Convert.ToInt32(context.User.FindFirst(c => c.Type == "AccessLevel").Value);
            var userId = Convert.ToInt32(context.User.FindFirst(c => c.Type == "UserId").Value);

            if (accessLevel == 10)
                context.Succeed(requirement);

            else if (accessLevel >= 6)
            {
                if (requirement.ResourceOperation != ResourceOperation.Delete)
                    context.Succeed(requirement);
            }
            else if (accessLevel >= 3)
            {
                if (requirement.ResourceOperation == ResourceOperation.Ban)
                    context.Fail();

                else if (user.Id == userId)
                    context.Succeed(requirement);
            }
            else
            {
                if (requirement.ResourceOperation == ResourceOperation.Read)
                    context.Succeed(requirement);

                else
                    context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
