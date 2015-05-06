using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, DateTime date, decimal amount)
            : base(user, date, amount) { }

        public override string ToString()
        {
            return "Deposit by: " + TransactionUser.Username + "\t " + base.ToString();
        }
    }
}
