using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ModelStationAPI.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public char? Gender { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
        public string PasswordHash { get; set; }


        public int? FileStorageId { get; set; }


        public int RoleId { get; set; }
        public virtual Role Role { get; set;}

        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public virtual List<LikedPost> LikedPosts { get; set; }
        public virtual List<LikedComment> LikedComments { get; set; }
    }
}
