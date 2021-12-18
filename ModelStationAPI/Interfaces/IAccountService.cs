using ModelStationAPI.Models;
using ModelStationAPI.Models.Account;
using System.Collections.Generic;
using System.Security.Claims;

namespace ModelStationAPI.Interfaces
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDTO dto);
        LoginResultDTO Login(LoginDTO dto);
        int ChangePassword(ChangePasswordDTO dto, ClaimsPrincipal userClaims);
        int DeleteAccount(DeleteAccountDTO dto, ClaimsPrincipal userClaims);
    }
}
