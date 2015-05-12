using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //Product klasse som beskriver de produkter der er i systemet
    public class Product
    {
        //contructor som giver værdier til næsten alle variabler
        public Product(int productID, string name, decimal price, bool active)
        {
            _ProductID = productID;
            _Name = name;
            Price = price;
            Active = active;
            //canbeboughtoncredit bliver sat til false i første omgang
            CanBeBoughtOnCredit = false;
        }

        private int _ProductID;
        public int ProductID
        {
            get { return _ProductID; }
            set
            {
                //productID må ikke være under 1
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
                //product name må ikke være null
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
