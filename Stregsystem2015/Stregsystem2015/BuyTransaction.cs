using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //arver fra Transaction for at gør alle funktioner fra transaction tilgængelige
    public class BuyTransaction : Transaction
    {
        //constructor der sender nogle oplysninger videre til Transactions constructor
        //product prisen sættes til negativ da de skal trækkes fra brugerens konto
        public BuyTransaction(User user, DateTime date, Product product)
            : base(user, date, product.Price * -1) 
        {
            ProductToBuy = product;
        }

        private Product ProductToBuy { get; set; }

        // overrider ToString til at returnere en string med oplysninger om hvilken bruger der har købt hvad
        public override string ToString()
        {
            return TransactionUser.Username + " bought the item: " + ProductToBuy.Name + "\t" + base.ToString();
        }

        // overrider Execute fra Transaction. her tjekkes om brugeren har penge nok på kontoen og 
        // om product er aktivt. er alt okay, bliver buytransaction udført ved at kalde transactions excute
        // som trækker produktets pris fra brugerens konto.
        // der bliver kastet exceptions hvis noget fejler.
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
