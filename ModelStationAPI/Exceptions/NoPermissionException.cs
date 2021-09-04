using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelStationAPI.Exceptions
{
    public class NoPermissionException : Exception
    {
        public NoPermissionException(string mesage) : base(mesage)
        {

        }
    }
}
