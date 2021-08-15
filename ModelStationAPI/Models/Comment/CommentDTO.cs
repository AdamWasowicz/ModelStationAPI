using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
        public string Text { get; set; }
        public int? ParentCommentId { get; set; }


        public int UserId { get; set; }
        public string UserName { get; set; }

        public int PostId { get; set; }
    }
}
