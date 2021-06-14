using ModelStationAPI.Models;
using System.Collections.Generic;

namespace ModelStationAPI.Services
{
    public interface IUserService
    {
        int Create(CreateUserDTO dto);
        List<UserDTO> GetAll();
        UserDTO GetById(int id);
        bool Delete(int id);
    }
}