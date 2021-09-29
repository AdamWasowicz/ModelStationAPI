using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface IPostService
    {
        int Create(CreatePostDTO dto, ClaimsPrincipal userClaims);
        bool Delete(int id, ClaimsPrincipal userClaims);
        bool Edit(EditPostDTO dto, ClaimsPrincipal userClaims);


        List<PostDTO> GetAll();
        PostDTO GetById(int id);
        PostQuerryResult GetByQuery(PostQuery query);
        List<PostBannerDTO> SearchPostByTitle_ReturnBanners(string title);
        List<PostBannerDTO> SearchByQuery_ReturnBanners(PostQuery query);


        bool UnBanPostByPostId(int postId, ClaimsPrincipal userClaims);
        bool BanPostByPostId(int postId, ClaimsPrincipal userClaims);
        bool BanPostsByUserId(int id, ClaimsPrincipal userClaims);
        bool UnBanPostsByUserId(int id, ClaimsPrincipal userClaims);
        bool ChangeActiveStateByPostId(int postId, ClaimsPrincipal userClaims);


        List<PostDTO> GetPostsByPostCategoryId(int categoryId);
        List<PostDTO> GetPostsByPostCategoryName(string categoryName);
        List<PostDTO> GetPostsByUserId(int userId);
        List<PostDTO> GetPostsByUserName(string userName);
    }
}
