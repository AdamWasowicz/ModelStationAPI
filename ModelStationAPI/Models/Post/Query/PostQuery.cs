using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class PostQuery
    {
        public string PostCategory { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public int CurrentPage { get; set; }
        public int NumberOfPosts { get; set; }


        public SortDirection OrderByDirection { get; set; }
        public SortAtribute OrderByAtribute { get; set; }
    }
}
