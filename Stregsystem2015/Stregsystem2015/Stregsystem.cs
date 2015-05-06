using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Stregsystem2015
{
    public class Stregsystem : IStregsystem
    {
        public Stregsystem()
        {
            TransactionLog = new List<Transaction>();
            Users = new List<User>();
            Products = new List<Product>();
            InitializeProducts();
        }

        private List<Transaction> TransactionLog { get; set; }
        private List<User> Users { get; set; }
        private List<Product> Products { get; set; }

        public void BuyProduct(User user, Product product)
        {
            BuyTransaction BT = new BuyTransaction(user, DateTime.Now, product);
            ExecuteTransaction(BT);
        }

        public void AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction ICT = new InsertCashTransaction(user, DateTime.Now, amount);
            ExecuteTransaction(ICT);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            TransactionLog.Add(transaction);
        }

        public Product GetProduct(int productID)
        {
            return Products.Find(p => p.ProductID == productID);
        }

        public User GetUser(string username)
        {
            return Users.Find(u => u.Username == username);
        }

        public List<Transaction> GetTransactionList(int numberOfTransactions, User user)
        {
            List<Transaction> Transactions = new List<Transaction>();

            Transactions = TransactionLog.Where(t => t.TransactionUser == user).
                OrderByDescending(t => t.TransactionID).Take(numberOfTransactions).ToList();

            return Transactions;
        }

        public List<Product> GetActiveProducts()
        {
            List<Product> activeProducts = new List<Product>();

            foreach (Product product in Products)
            {
                if (product.Active)
                    activeProducts.Add(product);
            }

            return activeProducts;
        }

        public void AddUser(User user)
        {
            if (!Users.Contains(user))
                Users.Add(user);
            else
                throw new ArgumentException("Username alredy exists");
        }

        void InitializeProducts()
        {
            List<string> lines = File.ReadAllLines(@"C:\Users\frederik\Documents\Visual Studio 2013\products.csv", Encoding.Default).ToList();
            lines.RemoveAt(0);
            foreach (string line in lines)
            {
                string[] productInfo = line.Split(';');
                int productID = int.Parse(productInfo[0]);
                string rawProductName = productInfo[1];
                string productName = Regex.Replace(rawProductName, "<.*?>", "").Trim('"', ' ');
                decimal price = decimal.Parse(productInfo[2]) / 100;
                bool active = productInfo[3] == "1" ? true : false;

                Products.Add(new Product(productID, productName, price, active));
            }
        }
    }
}
