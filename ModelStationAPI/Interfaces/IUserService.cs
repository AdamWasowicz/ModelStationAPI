using ModelStationAPI.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace ModelStationAPI.Interfaces
{
    public interface IUserService
    {
        bool BanUserByUserId(int id, ClaimsPrincipal userClaims);
        bool ChangeActiveStateByUserId(int id, ClaimsPrincipal userClaims);
        int Create(CreateUserDTO dto);
        bool Delete(int id, ClaimsPrincipal userClaims);
        bool Edit(EditUserDTO dto, ClaimsPrincipal userClaims);
        List<UserDTO> GetAll();
        UserDTO GetById(int id);
        UserDTO GetByUserName(string userName);
        List<UserBannerDTO> SearchUsers_ReturnBanners(string userName);
        bool UnBanUserByUserId(int id, ClaimsPrincipal userClaims);
        bool UploadUserImage(CreateFileStorageDTO dto, ClaimsPrincipal userClaims);
        UserProfileDTO GetUserProfileById(int id);
    }
}