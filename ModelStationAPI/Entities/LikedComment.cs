using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Entities
{
    public class LikedComment
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int Value { get; set; }
    }
}
