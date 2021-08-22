using ModelStationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Interfaces
{
    public interface IPostCategoryService
    {
        int Create(CreatePostCategoryDTO dto);
        bool Delete(int id);
        bool Edit(EditPostCategoryDTO dto);
        List<PostCategoryDTO> GetAll();
        PostCategoryDTO GetById(int id);
    }
}
