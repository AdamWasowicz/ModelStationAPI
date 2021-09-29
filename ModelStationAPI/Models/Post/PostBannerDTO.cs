using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class PostBannerDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }

        public int UserId { get; set; }
        public string User_UserName { get; set; }

        public int? PostCategoryId { get; set; }
        public string PostCategory_Name { get; set; }
    }
}
