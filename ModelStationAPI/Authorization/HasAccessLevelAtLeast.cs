using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Authorization
{
    public class HasAccessLevelAtLeast : IAuthorizationRequirement
    {
        public int MinimumAccessLevel { get; }

        public HasAccessLevelAtLeast(int minimumAccessLevel)
        {
            MinimumAccessLevel = minimumAccessLevel;
        }
    }
}
