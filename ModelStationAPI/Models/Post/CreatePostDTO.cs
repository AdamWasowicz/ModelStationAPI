using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
