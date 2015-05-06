using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    public class Product
    {
        public Product(int productID, string name, decimal price, bool active)
        {
            _ProductID = productID;
            _Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = false;
        }

        private int _ProductID;
        public int ProductID
        {
            get { return _ProductID; }
            set
            {
                if (value > 0)
                    _ProductID = value;
                else
                    throw new ArgumentException("Invalid ProductID");
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (value != null)
                    _Name = value;
                else
                    throw new ArgumentNullException("Name");
            }
        }

        public decimal Price { get; set; }

        public bool CanBeBoughtOnCredit { get; set; }

        public virtual bool Active { get; set; }
        
    }
}
