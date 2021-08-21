using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreateLikedPostDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Value { get; set; }
    }
}
