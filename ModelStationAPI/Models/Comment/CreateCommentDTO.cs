using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreateCommentDTO
    {
        public string Text { get; set; }
        public int? ParentCommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
