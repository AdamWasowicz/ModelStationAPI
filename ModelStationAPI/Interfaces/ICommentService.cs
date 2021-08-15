using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface ICommentService
    {
        int Create(CreateCommentDTO dto);
        bool Delete(int id);
        List<CommentDTO> GetAll();
        CommentDTO GetById(int id);
        List<CommentDTO> GetCommentsByPostId(int postId);
        List<CommentDTO> GetCommentsByUserId(int userId);
    }
}
