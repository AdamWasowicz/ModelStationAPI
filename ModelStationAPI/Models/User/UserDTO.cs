using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public char? Gender { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
        public int? FileStorageId { get; set; }
    }
}
