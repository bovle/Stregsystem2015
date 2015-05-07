using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    public interface IStregsystem
    {
        BuyTransaction BuyProduct(User user, Product product);
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        void ExecuteTransaction(Transaction transaction);
        Product GetProduct(int productID);
        User GetUser(string username);
        List<Transaction> GetTransactionList(int numberOfTransactions, User username);
        List<Product> GetActiveProducts();
    }
}
