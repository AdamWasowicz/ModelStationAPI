using ModelStationAPI.Models;
using ModelStationAPI.Models.Account;
using System.Collections.Generic;

namespace ModelStationAPI.Interfaces
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDTO dto);
        LoginResultDTO Login(LoginDTO dto);
    }
}
