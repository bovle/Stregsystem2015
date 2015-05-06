using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    class SeasonalProduct : Product
    {

        public SeasonalProduct(int productID, string name, int price, DateTime startDate, DateTime endDate) 
            : base(productID, name, price, false)
        {
            SeasonStartDate = startDate;
            SeasonEndDate = endDate;
        }

        public DateTime SeasonStartDate { get; set; }

        public DateTime SeasonEndDate { get; set; }

        public override bool Active
        {
            get
            {
                if (DateTime.Now > SeasonStartDate && DateTime.Now < SeasonEndDate)
                    return true;
                else
                    return false;
            }
        }
    }
}
