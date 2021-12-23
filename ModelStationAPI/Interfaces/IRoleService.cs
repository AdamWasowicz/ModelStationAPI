using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface IRoleService
    {
        int ChangeUserRole(ChangeUserRoleDTO dto, ClaimsPrincipal userClaims);
    }
}
