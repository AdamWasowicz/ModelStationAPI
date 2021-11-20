using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreatePostWithPostCategoryNameDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }

        //Files
        public IFormFile[] Files { get; set; }

        public string PostCategoryName { get; set; }
    }
}
