using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface ILikedCommentService
    {
        int Create(CreateLikedCommentDTO dto);
        bool Edit(EditLikedCommentDTO dto);
        List<LikedCommentDTO> GetAll();
        LikedCommentDTO GetById(int id);
        List<LikedCommentDTO> GetLikedCommentsByUserId(int userId);
        List<UserDTO> GetUserByCommentId(int commentId);
    }
}
