using Microsoft.AspNetCore.Authorization;
using ModelStationAPI.Entities;
using ModelStationAPI.Exceptions;
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
            var roleId = Convert.ToInt32(context.User.FindFirst(c => c.Type == "Role").Value);
            var Role = _dbContext.Roles.FirstOrDefault(r => r.Id == roleId);
            if (Role != null)
                throw new NotFoundException("Role with this Id does not exist");

            var accessLevel = Role.AccessLevel;
            if (accessLevel >= requirement.MinimumAccessLevel)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
