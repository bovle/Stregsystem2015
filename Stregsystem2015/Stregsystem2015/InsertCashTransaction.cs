using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //arver fra Transaction for at gør alle funktioner fra transaction tilgængelige
    public class InsertCashTransaction : Transaction
    {
        //constructor der sender alle oplysninger videre til Transactions constructor
        public InsertCashTransaction(User user, DateTime date, decimal amount)
            : base(user, date, amount) { }

        // overrider ToString til at returnere en string med oplysninger om hvilken bruger der har indbetalt hvad
        public override string ToString()
        {
            return "Deposit by: " + TransactionUser.Username + "\t " + base.ToString();
        }
    }
}
