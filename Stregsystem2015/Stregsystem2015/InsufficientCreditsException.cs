using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    public class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(User user, Product product) 
            : base(string.Format("User {0} cant afford item: {1}", user.Username, product.Name)) { }
    }
}
