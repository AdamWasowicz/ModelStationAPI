using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class LikedPostDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int Value { get; set; }
    }
}
