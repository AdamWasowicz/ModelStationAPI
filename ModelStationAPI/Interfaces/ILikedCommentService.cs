using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface ILikedCommentService
    {
        int Create(CreateLikedCommentDTO dto, ClaimsPrincipal userClaims);
        bool Edit(EditLikedCommentDTO dto, ClaimsPrincipal userClaims);
        bool CreateOrEdit(CreateOrEditLikedCommentDTO dto, ClaimsPrincipal userClaims);

        List<LikedCommentDTO> GetAll();
        LikedCommentDTO GetById(int id);
        LikedCommentDTO GetByCommentId(int id, ClaimsPrincipal userClaims);

        List<LikedCommentDTO> GetLikedCommentsByUserId(int userId);
        List<UserDTO> GetUsersByCommentId(int commentId);
    }
}
