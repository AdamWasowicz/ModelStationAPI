using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }

        //Files
        public List<IFormFile> Files { get; set; }

        public int? PostCategoryId { get; set; }
    }
}
