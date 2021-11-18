using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface ILikedPostService
    {
        int Create(CreateLikedPostDTO dto);
        bool Edit(EditLikedPostDTO dto);
        bool EditByPostId(EditLikedPostDTO dto, ClaimsPrincipal userClaims);
        List<LikedPostDTO> GetAll();
        LikedPostDTO GetById(int id);
        LikedPostDTO GetByPostId(int id, ClaimsPrincipal userClaims);
        List<LikedPostDTO> GetLikedPostsByUserId(int userId);
        List<UserDTO> GetUsersByPostId(int postId);
    }
}
