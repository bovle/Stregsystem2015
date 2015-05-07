using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    class StregsystemCLI : IStregsystemUI
    {
        public StregsystemCLI(IStregsystem ssystem)
        {
            Ssystem = ssystem;
        }

        IStregsystem Ssystem { get; set; }
        StregsystemCommandParser Parser { get; set; }
        bool running { get; set; }

        public void Start(StregsystemCommandParser parser)
        {
            Parser = parser;
            running = true;
            while (running)
            {
                DisplayInterface();
                string Command = Console.ReadLine();
                Parser.ParseCommand(Command);
                Console.WriteLine("press any key to continue");
                Console.ReadKey();
                DisplayInterface();
            }
        }

        public void Close()
        {
            running = false;
        }

        public void DisplayInterface()
        {
            List<Product> ActiveProducts = new List<Product>();
            ActiveProducts = Ssystem.GetActiveProducts();
            int width = ActiveProducts.Max(p => p.Name.Length) + 1;
            Console.WriteLine("{0, -5}| {1, " + -width + "}| {2, -10}", "ID", "Product", "Price");
            Console.Write("-------");
            for (int i = 0; i < width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("------------");
            foreach (Product product in ActiveProducts)
            {
                Console.WriteLine("{0, -5}| {1, " + -width + "}| {2, 7:0.00} kr", product.ProductID, product.Name, product.Price);
            }
            Console.WriteLine();
            Console.WriteLine("Write your Username below to see info about your transactions and balance");
            Console.WriteLine("Write your Username and a product ID (seperated with 'space') to buy product");
            Console.WriteLine("Write your Username, a number and a product ID (all seperated with 'space') to buy multiple products");
            Console.WriteLine();
            
        }

        public void DisplayUserNotFound(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayProductNotFound(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user.ToString());
            Console.WriteLine("Username: {0} \t Balance: {1}", user.Username, user.Balance);
            Console.WriteLine("Transactions:");
            foreach (Transaction transaction in Ssystem.GetTransactionList(10, user))
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        public void DisplayTooManyArgumentsError()
        {
            Console.WriteLine("a maximun of 3 arguments is allowed");
        }

        public void DisplayAdminCommandNotFoundMessage(string command)
        {
            Console.WriteLine("Command: {0} not found", command);
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayInsufficientCash(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayGeneralError(string message)
        {
            Console.WriteLine("Unknown error accured with message: {0}", message);
        }
    }
}
