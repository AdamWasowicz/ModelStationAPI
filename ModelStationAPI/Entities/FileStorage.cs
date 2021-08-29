using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Entities
{
    public class FileStorage
    {
        public int Id { get; set; }
        public string ContentType { get; set; }  //USER or POST
        public string FileType { get; set; }
        public string UserGivenName { get; set; }
        public string StorageName { get; set; }
        public string FullName { get; set; }
        public string FullPath { get; set; }
        public DateTime UploadDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int? PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
