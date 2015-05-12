using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //stregsystemCLI arver fra IstregsystemUI så andre klasser og metoder kan bruge den som UI til systemet
    class StregsystemCLI : IStregsystemUI
    {
        //constructor der tager et stregsystem som parameter
        public StregsystemCLI(IStregsystem ssystem)
        {
            Ssystem = ssystem;
        }

        IStregsystem Ssystem { get; set; }
        StregsystemCommandParser Parser { get; set; }
        bool running { get; set; }

        //metode der kaldes når UI'et skal starte
        //tager en parser som parameter
        public void Start(StregsystemCommandParser parser)
        {
            Parser = parser;
            //programmet køre indtil running bliver sat til false af metoden Close()
            running = true;
            while (running)
            {
                //kald til metoden der viser interfacet
                DisplayInterface();
                //kommando indlæses
                string Command = Console.ReadLine();
                //og sendes til parseren
                Parser.ParseCommand(Command);
                Console.WriteLine("press any key to continue");
                Console.ReadKey();
            }
        }

        //sætter running til false og stopper programet
        public void Close()
        {
            running = false;
            Console.WriteLine("Bye!");
        }

        //viser main interfacet
        public void DisplayInterface()
        {
            //listen med aktive produkter hentes
            List<Product> ActiveProducts = new List<Product>();
            ActiveProducts = Ssystem.GetActiveProducts().OrderBy(p => p.ProductID).ToList();
            //længden af det længste navn findes
            int width = ActiveProducts.Max(p => p.Name.Length) + 1;
            //'ID', 'Product' og 'Price' bliver skrevet ud med passende mellemrum
            Console.WriteLine("{0, -5}| {1, " + -width + "}| {2, -10}", "ID", "Product", "Price");
            //linje der seperere overskrifterne med produkterne skrives ud
            Console.Write("-------");
            for (int i = 0; i < width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("------------");
            //produkterne udskrives med passende format og mellemrum
            foreach (Product product in ActiveProducts)
            {
                Console.WriteLine("{0, -5}| {1, " + -width + "}| {2, 7 : 0.00} kr", product.ProductID, product.Name, product.Price);
            }
            Console.WriteLine();
            Console.WriteLine("Write your Username below to see info about your transactions and balance");
            Console.WriteLine("Write your Username and a product ID (seperated with 'space') to buy product");
            Console.WriteLine("Write your Username, a number and a product ID (all seperated with 'space') to buy multiple products");
            Console.WriteLine();
            
        }

        //metoder til udskrivning af forskellig information er defineret herunder
        public void DisplayLowBalance(User user)
        {
            Console.WriteLine("Warning {0}! Balance below 50kr", user.Username);
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
            Console.WriteLine(message);
        }
    }
}
