using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{

    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productID)
            : base(string.Format("Product with ID {0} not found", productID)) { }
    }
}
