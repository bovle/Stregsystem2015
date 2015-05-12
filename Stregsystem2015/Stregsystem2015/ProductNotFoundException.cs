using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //arver fra exception
    public class ProductNotFoundException : Exception
    {
        //constructor der sender en string med oplysninger om hvilket product der ikke blev fundet
        public ProductNotFoundException(int productID)
            : base(string.Format("Product with ID {0} not found", productID)) { }
    }
}
