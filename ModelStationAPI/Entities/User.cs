using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name  { get; set; }
        public string Surname { get; set; }
        public char? Gender { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
        public string? ImageSource { get; set; }
        public string PasswordHash { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
