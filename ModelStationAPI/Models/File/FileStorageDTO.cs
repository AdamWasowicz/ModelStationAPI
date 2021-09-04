using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class FileStorageDTO
    {
        public int Id { get; set; }
        public string FileType { get; set; }
        public string UserGivenName { get; set; }
        public string StorageName { get; set; }
    }
}
