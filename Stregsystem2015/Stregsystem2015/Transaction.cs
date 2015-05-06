using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    public class Transaction
    {
        static int NextTransactionID = 1;

        public Transaction(User user, DateTime date, decimal amount)
        {
            _TransactionID = NextTransactionID;
            NextTransactionID++;
            _TransactionUser = user;
            _Date = date;
            Amount = amount;
        }

        private int _TransactionID;
        public int TransactionID
        {
            get { return _TransactionID; }
        }

        private User _TransactionUser;
        public User TransactionUser
        {
            get { return _TransactionUser; }
            set
            {
                if (value != null)
                    _TransactionUser = value;
                else
                    throw new ArgumentNullException("TransactionUser cant be null");
            }
        }

        private DateTime _Date;
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public decimal Amount { get; set; }

        public override string ToString()
        {
            return "Amount:" + Amount.ToString() + "kr" + "\t Date:" + Date.ToString() + "\t Transaction ID: " + TransactionID.ToString();
        }

        public virtual void Execute() 
        {
            TransactionUser.Balance += Amount;
        }
    }
}
