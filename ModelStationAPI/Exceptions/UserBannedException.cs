using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Exceptions
{
    public class UserBannedException : Exception
    {
        public UserBannedException(string msg) : base(msg)
        {

        }
    }
}
