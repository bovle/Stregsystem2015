using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //arver fra product
    class SeasonalProduct : Product
    {
        //contructor der sender nogle oplysninger videre til product
        //active bliver sat til false, da den alligevel bliver overridet 
        public SeasonalProduct(int productID, string name, decimal price, DateTime startDate, DateTime endDate) 
            : base(productID, name, price, false)
        {
            SeasonStartDate = startDate;
            SeasonEndDate = endDate;
        }

        public DateTime SeasonStartDate { get; set; }

        public DateTime SeasonEndDate { get; set; }

        public override bool Active
        {
            //seasonalproduct er aktivt hvis kaldtidspunktet er imellem startdate og enddate
            get
            {
                if (DateTime.Now > SeasonStartDate && DateTime.Now < SeasonEndDate)
                    return true;
                else
                    return false;
            }
            //seasonalproduct sættes ved at ændre på start og end date
            set
            {
                if (value)
                {
                    SeasonStartDate = DateTime.Now;
                    SeasonEndDate = DateTime.MaxValue;
                }
                else
                {
                    SeasonStartDate = DateTime.MinValue;
                    SeasonEndDate = DateTime.Now;
                }
            }
        }
    }
}
