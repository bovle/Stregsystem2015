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

            stregsystem.Users.Add(new User("frederik", "lund", "bovle", "klaver1@hotmail.com"));

            User myUser = stregsystem.GetUser("bovle");

            Console.WriteLine(myUser.Username);

            Console.ReadKey();
        }
    }
}
