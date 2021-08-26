using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface ILikedPostService
    {
        int Create(CreateLikedPostDTO dto);
        bool Edit(EditLikedPostDTO dto);
        List<LikedPostDTO> GetAll();
        LikedPostDTO GetById(int id);
        List<LikedPostDTO> GetLikedPostsByUserId(int userId);
        List<UserDTO> GetUsersByPostId(int postId);
    }
}
