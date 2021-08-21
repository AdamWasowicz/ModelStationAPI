using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface IPostService
    {
        int Create(CreatePostDTO dto);
        bool Delete(int id);
        bool Edit(EditPostDTO dto);
        List<PostDTO> GetAll();
        PostDTO GetById(int id);
        List<PostDTO> GetPostsByPostCategoryId(int categoryId);
        List<PostDTO> GetPostsByPostCategoryName(string categoryName);
        List<PostDTO> GetPostsByUserId(int userId);
        List<PostDTO> GetPostsByUserName(string userName);
    }
}
