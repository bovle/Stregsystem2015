using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Resources;

namespace Stregsystem2015
{
    //stregsystem arver fra Istregsystem så det kan bruges af andre classer og metoder der bruger en IStregsystem
    public class Stregsystem : IStregsystem
    {
        //constructer der instansiere nogle lister og kalder metoder der læser produkterne ind fra fil
        public Stregsystem()
        {
            Transactions = new List<Transaction>();
            Users = new List<User>();
            Products = new List<Product>();
            InitializeProducts();
        }

        private List<Transaction> Transactions { get; set; }
        private List<User> Users { get; set; }
        private List<Product> Products { get; set; }

        //udføre en buytransaction med info om user og product fra parametrene og returnere transactionen
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction BT = new BuyTransaction(user, DateTime.Now, product);
            ExecuteTransaction(BT);
            return BT;
        }

        //udføre en insertcashtransaction med info om user og beløb fra parametrene og returnere transactionen
        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction ICT = new InsertCashTransaction(user, DateTime.Now, amount);
            ExecuteTransaction(ICT);
            return ICT;
        }

        //udføre en transaction og tilføjer den til transactionsloggen
        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            Transactions.Add(transaction);
            string path = String.Format("{0}\\TransactionLog.txt", Directory.GetParent(Environment.CurrentDirectory).Parent.FullName);
            string[] TransactionLog = Transactions.OrderByDescending(t => t.TransactionID).Select(t => t.ToString()).ToArray();
            File.WriteAllLines(path, TransactionLog);
        }

        //returnere produktet med produktID fra parameter, ProductNotFoundException hvis dette mislykkedes
        public Product GetProduct(int productID)
        {
            Product result = Products.Find(p => p.ProductID == productID);
            if (result != null)
                return result;
            else
                throw new ProductNotFoundException(productID);
        }

        //returnere Bruger med brugernavn fra parameter, UserNotFoundException hvis dette mislykkedes
        public User GetUser(string username)
        {
            User result = Users.Find(u => u.Username == username);
            if (result != null)
                return result;
            else
                throw new UserNotFoundException(username);
        }

        //returnere en liste med de nyeste transactioner knyttet til en bestem bruger, 
        //bruger og antal transactioner fåes fra parametre
        public List<Transaction> GetTransactionList(int numberOfTransactions, User user)
        {
            List<Transaction> Transactions = new List<Transaction>();

            Transactions = Transactions.Where(t => t.TransactionUser == user).
                OrderByDescending(t => t.TransactionID).Take(numberOfTransactions).ToList();

            return Transactions;
        }

        //returnere alle aktive produkter i systemet
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

        //tilføjer et produkt til product listen hvis ikke den allerede findes
        public void AddProduct(Product product)
        {
            if (Products.All(p => p.ProductID != product.ProductID))
                Products.Add(product);
            else
                throw new ArgumentException("Product Id allredy exists");
        }

        //tilføjer en bruger til user listen hvis ikke den allerede findes
        public void AddUser(User user)
        {
            if (!Users.Contains(user))
                Users.Add(user);
            else
                throw new ArgumentException("Username allredy exists");
        }

        //sætter et specefikt produkt active property til true eller false
        public void SetProductActive(int productID, bool activate)
        {
            GetProduct(productID).Active = activate;
        }

        //sætter et specefikt produkt canbeboughtoncredit property til true eller false
        public void SetProductCredit(int productID, bool credit)
        {
            GetProduct(productID).CanBeBoughtOnCredit = credit;
        }

        //indlæser alle produkterne beskrevet i filen products.csv fundet i resource mappen
        //og ligger dem ind i listen over produkter
        void InitializeProducts()
        {
            //henter filen fra resources og splitter den i linjer
            List<string> lines = Properties.Resources.products.Split('\n').ToList();

            //første linje fjernes da den ikke indeholder informationerne om nogle produkter
            lines.RemoveAt(0);
            //tomme linjer fjernes
            lines.RemoveAll(l => l.Equals(""));

            //hver linje bliver læst og oversat til et product
            foreach (string line in lines)
            {
                //deler linjen ved simikolon
                string[] productInfo = line.Split(';');
                //første element bliver indlæst som product ID'et
                int productID = int.Parse(productInfo[0]);
                //næste element bliver indlæst som navn. tegnene '<' og '>' samt alt ind i mellem bliver fjernet.
                //derefter bliver der trimmet for whitespaces og gåseøjn
                string rawProductName = productInfo[1];
                string productName = Regex.Replace(rawProductName, "<.*?>", "").Trim('"', ' ');
                //tredje element bliver indlæst som prisen
                decimal price = decimal.Parse(productInfo[2]) / 100;
                //fjerde element afgør om produktet er aktivt
                bool active = productInfo[3] == "1" ? true : false;
                //til sidste bliver der tilføjet et nyt produkt med informationen hentet ovenover
                Products.Add(new Product(productID, productName, price, active));  
            }
        }
    }
}
