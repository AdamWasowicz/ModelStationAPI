using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface IPostService
    {
        int Create(CreatePostDTO dto, int userId);
        bool Delete(int id, int userId);
        bool Edit(EditPostDTO dto, int userId);

        List<PostDTO> GetAll();
        PostDTO GetById(int id);

        //To be implemented
        bool UnBanPostByPostId(int postId);
        bool BanPostByPostId(int postId);
        bool BanPostsByUserId(int userId);
        bool ChangeActiveStateByPostId(int postId);


        List<PostDTO> GetPostsByPostCategoryId(int categoryId);
        List<PostDTO> GetPostsByPostCategoryName(string categoryName);
        List<PostDTO> GetPostsByUserId(int userId);
        List<PostDTO> GetPostsByUserName(string userName);
    }
}
