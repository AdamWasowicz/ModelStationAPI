using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string FileType { get; set; }
        public string OriginalName { get; set; }
        public string StorageName { get; set; }
        public DateTime UploadDate { get; set; }


        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
