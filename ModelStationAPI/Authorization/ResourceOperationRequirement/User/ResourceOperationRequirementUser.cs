using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Authorization
{
    public class ResourceOperationRequirementUser : IAuthorizationRequirement
    {
        public ResourceOperationRequirementUser(ResourceOperation resourceOperation)
        {
            ResourceOperation = ResourceOperation;
        }

        public ResourceOperation ResourceOperation { get; }
    }
}
