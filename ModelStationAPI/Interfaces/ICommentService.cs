using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface ICommentService
    {
        int Create(CreateCommentDTO dto, ClaimsPrincipal userClaims);
        bool Delete(int id, ClaimsPrincipal userClaims);
        bool Edit(EditCommentDTO dto, ClaimsPrincipal userClaims);
        List<CommentDTO> GetAll();
        CommentDTO GetById(int id);
        List<CommentDTO> GetCommentsByPostId(int postId);
        List<CommentDTO> GetCommentsByUserId(int userId);
    }
}
