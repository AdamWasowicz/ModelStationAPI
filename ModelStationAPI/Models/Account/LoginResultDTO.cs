using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Models.Account
{
    public class LoginResultDTO
    {
        public UserDTO user { get; set; }
        public string jwt { get; set; }
    }
}
