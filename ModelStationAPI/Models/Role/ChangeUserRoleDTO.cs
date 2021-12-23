using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models
{
    public class ChangeUserRoleDTO
    {
        public string UserName { get; set; }
        public int NewRoleId { get; set; }
    }
}
