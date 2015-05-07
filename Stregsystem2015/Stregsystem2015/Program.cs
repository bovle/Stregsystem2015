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

            User bovle = new User("Frederik", "Lund", "bovle", "klaver1@hotmail.com");
            User goejsen = new User("Maia", "Norgaard", "goejsen", "maianorgaard@hotmail.com");
            User harder = new User("Jakob", "Harder", "sirharder", "harder@hotmail.com");

            stregsystem.AddUser(bovle);
            stregsystem.AddUser(goejsen);
            stregsystem.AddUser(harder);

            stregsystem.ExecuteTransaction(new InsertCashTransaction(bovle, DateTime.Now, 3000));
            stregsystem.ExecuteTransaction(new InsertCashTransaction(goejsen, DateTime.Now, 2500));
            stregsystem.ExecuteTransaction(new InsertCashTransaction(harder, DateTime.Now, 1000000000000));

            StregsystemCLI cli = new StregsystemCLI(stregsystem);
            StregsystemCommandParser parser = new StregsystemCommandParser(cli, stregsystem);
            cli.Start(parser);

            Console.ReadKey();
        }
    }
}
