using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreateOrEditLikedPostDTO
    {
        public int PostId { get; set; }
        public int Value { get; set; }
    }
}
