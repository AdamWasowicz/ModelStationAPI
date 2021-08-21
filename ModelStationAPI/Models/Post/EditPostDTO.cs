using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class EditPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageSource { get; set; }
        public int? PostCategoryId { get; set; }
    }
}
