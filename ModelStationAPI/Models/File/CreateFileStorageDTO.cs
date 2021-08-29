using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreateFileStorageDTO
    {
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public string ContentType { get; set; }
        public string UserGivenName { get; set; }

        public IFormFile File { get; set; }
    }
}
