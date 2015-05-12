using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //klasse til transaktioner
    public class Transaction
    {
        //static tæller der sørger for at hver transaktion får et unikt ID
        static int NextTransactionID = 1;
        //constructor der initialisere variabler
        public Transaction(User user, DateTime date, decimal amount)
        {
            _TransactionID = NextTransactionID;
            NextTransactionID++;
            _TransactionUser = user;
            Date = date;
            Amount = amount;
        }

        private int _TransactionID;
        public int TransactionID
        {
            //transaktionsID er read only
            get { return _TransactionID; }
        }

        private User _TransactionUser;
        public User TransactionUser
        {
            get { return _TransactionUser; }
            set
            {
                //transaktionsUser må ikke være null
                if (value != null)
                    _TransactionUser = value;
                else
                    throw new ArgumentNullException("TransactionUser cant be null");
            }
        }


        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        //tostring overrides til at returnere en string med information om transaktionen
        public override string ToString()
        {
            return "Amount:" + Amount.ToString() + "kr" + "\t Date:" + Date.ToString() + "\t Transaction ID: " + TransactionID.ToString();
        }

        //når transaktionen udføres lægges det specifiserede beløb til brugerens konto
        public virtual void Execute() 
        {
            TransactionUser.Balance += Amount;
        }
    }
}
