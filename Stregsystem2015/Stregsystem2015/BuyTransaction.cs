using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, DateTime date, Product product)
            : base(user, date, product.Price * -1) 
        {
            ProductToBuy = product;
        }

        public Product ProductToBuy { get; set; }

        public override string ToString()
        {
            return TransactionUser.Username + " bought the item: " + ProductToBuy.Name + "\t" + base.ToString();
        }

        public override void Execute()
        {
            if (TransactionUser.Balance + Amount < 0 && !ProductToBuy.CanBeBoughtOnCredit)
                throw new InsufficientCreditsException(TransactionUser, ProductToBuy);
            else if (!ProductToBuy.Active)
                throw new ArgumentException("Product is not active", "ProductToBuy");
            else
                base.Execute();
        }
    }
}
