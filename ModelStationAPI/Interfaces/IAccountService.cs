using ModelStationAPI.Models;
using System.Collections.Generic;

namespace ModelStationAPI.Interfaces
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDTO dto);
    }
}
