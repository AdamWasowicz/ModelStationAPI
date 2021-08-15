using Microsoft.AspNetCore.Authorization;
using ModelStationAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Authorization
{
    public class HasAccessLevelAtLeastHandler : AuthorizationHandler<HasAccessLevelAtLeast>
    {
        private readonly ModelStationDbContext _dbContext;

        public HasAccessLevelAtLeastHandler(ModelStationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessLevelAtLeast requirement)
        {
            var roleName = context.User.FindFirst(c => c.Type == "Role").Value;
            var accessLevel = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName).AccessLevel;

            if (accessLevel >= requirement.MinimumAccessLevel)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
