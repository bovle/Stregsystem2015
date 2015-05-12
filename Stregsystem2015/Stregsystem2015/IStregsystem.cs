using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //interface som beskriver hvilke metoder stregsystem skal have.
    //dette gør det muligt at udskifte hvilket stregsystem der bruges
    //uden at skulle ændre alle andre dele af koden
    public interface IStregsystem
    {
        BuyTransaction BuyProduct(User user, Product product);
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        void ExecuteTransaction(Transaction transaction);
        Product GetProduct(int productID);
        User GetUser(string username);
        List<Transaction> GetTransactionList(int numberOfTransactions, User username);
        List<Product> GetActiveProducts();
        void SetProductActive(int productID, bool active);
        void SetProductCredit(int productID, bool credit);
    }
}
