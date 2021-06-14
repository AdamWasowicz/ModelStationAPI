using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class CreateUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string? SecondName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public char? Gender { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
    }
}
