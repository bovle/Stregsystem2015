using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    class Program
    {
        static void Main(string[] args)
        {
            Stregsystem stregsystem = new Stregsystem();
            //StregsystemCLI cli = new StregsystemCLI(stregsystem);
            //StregsystemCommandParser parser = new StregsystemCommandParser(cli, stregsystem);
            //cli.Start(parser);
            User Bovle = new User("frederik", "lund", "bovle", "klaver1@hotmail.com");
            User Goejsen = new User("Maia", "Noegaard", "goejsen", "maia.Norgaard@hotmail.com");
            stregsystem.AddUser(Bovle);

            stregsystem.AddUser(Goejsen);

            stregsystem.ExecuteTransaction(new InsertCashTransaction(Bovle, DateTime.Now, 300));
            
            stregsystem.ExecuteTransaction(new InsertCashTransaction(Goejsen, DateTime.Now, 500));

            stregsystem.ExecuteTransaction(new BuyTransaction(Goejsen, DateTime.Now, stregsystem.GetProduct(11)));

            stregsystem.ExecuteTransaction(new BuyTransaction(Bovle, DateTime.Now, stregsystem.GetProduct(14)));

            List<Transaction> myTransactions = new List<Transaction>();

            myTransactions = stregsystem.GetTransactionList(2, Goejsen);

            List<Product> myActiveProducts = new List<Product>();

            myActiveProducts = stregsystem.GetActiveProducts();

            foreach (Product product in myActiveProducts)
            {
                Console.WriteLine(product.Name);
            }

            Console.ReadKey();
        }
    }
}
