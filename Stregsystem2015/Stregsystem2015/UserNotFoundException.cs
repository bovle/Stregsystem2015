using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username)
            : base(string.Format("User {0} not found", username)) { }

    }
}
