using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string? ImageSource { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }


        public int UserId { get; set; }
        public string UserName { get; set; }

        public int PostCategoryId { get; set; }
        public string PostCategoryName { get; set; }
    }
}
