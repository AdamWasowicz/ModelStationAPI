using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string? ImageSource { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
        public int Likes { get; set; }


        public int UserId { get; set; }
        public virtual User User {get; set;}

        public int PostCategoryId { get; set; }
        public virtual PostCategory PostCategory { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
