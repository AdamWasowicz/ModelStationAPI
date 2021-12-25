using ModelStationAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public char? Gender { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Description { get; set; }


        //Photo
        public FileStorageDTO File { get; set; }


        //Role
        public Role Role { get; set; }


        //Stats
        public int AmountOfPosts { get; set; }
        public int AmountOfComments { get; set; }
    }
}
