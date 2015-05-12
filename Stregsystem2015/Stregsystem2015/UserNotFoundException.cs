using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //arver fra exception
    public class UserNotFoundException : Exception
    {
        //constructor der sender en string med oplysninger om hvilken bruger der ikke blev fundet
        public UserNotFoundException(string username)
            : base(string.Format("User {0} not found", username)) { }

    }
}
