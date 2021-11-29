using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreateOrEditLikedCommentDTO
    {
        public int CommentId { get; set; }
        public int Value { get; set; }
    }
}
