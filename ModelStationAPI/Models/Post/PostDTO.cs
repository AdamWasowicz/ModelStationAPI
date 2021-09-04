using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class PostDTO
    {
        //Basic
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }

        //Photos
        public List<FileStorageDTO> Files { get; set; }

        //Comments
        public List<CommentDTO> Comments { get; set; }

        //User
        public int UserId { get; set; }
        public string UserName { get; set; }

        //PostCategory
        public int PostCategoryId { get; set; }
        public string PostCategoryName { get; set; }
    }
}
