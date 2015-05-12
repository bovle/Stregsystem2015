using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //class der tager komandoer fra brugeren og kalder de rigtige metoder
    public class StregsystemCommandParser
    {
        //contructor der tager et stregsystem og et stregsystem ui
        //Interfaces så de kan udskiftes nemt
        public StregsystemCommandParser(IStregsystemUI sui, IStregsystem ssystem)
        {
            SSystem = ssystem;
            SUI = sui;
            InitializeAdminCommands();
        }

        IStregsystem SSystem;
        IStregsystemUI SUI;
        //her defineres et dictionary der har en string som key og en action som value
        Dictionary<string, Action<string[]>> AdminCommands = new Dictionary<string, Action<string[]>>();

        //metode der af gøre hvilken slags kommando der er blevet sendt
        public void ParseCommand(string command)
        {
            //hvis kommandoen starter med ':' er det en admincommand
            if (command.First() == ':')
            {
                string[] Commands = command.Split(' ');
                adminCommand(Commands);
            }
            else
            {
                string[] Commands = command.Split(' ');
                //afhængig af hvor mange strings der er i commands sendes kommandoen videre til den passende metode
                switch (Commands.Count())
                {
                    case 0:
                        break;
                    case 1:
                        oneArgumentCommand(Commands[0]);
                        break;
                    case 2:
                        twoArgumentsCommand(Commands);
                        break;
                    case 3:
                        threeArgumentsCommand(Commands);
                        break;
                    default:
                        //fejl hvis der er over 3 argumenter i kommandoen
                        SUI.DisplayTooManyArgumentsError();
                        break;
                }
            }
        }

        //metode der håndtere admin kommandoer
        void adminCommand(string[] Commands)
        {
            Action<string[]> action;
            //der tjekkes om dictionariet indeholder en key der passer til kommandoen
            //hvis ikke udskrives en fejlbesked
            if (AdminCommands.ContainsKey(Commands[0]))
            {
                //action'en læses fra dictionariet
                AdminCommands.TryGetValue(Commands[0], out action);
                //action'en forsøges udført, og exceptions fanges
                try
                {
                    action.Invoke(Commands);
                }
                catch (UserNotFoundException e)
                {
                    SUI.DisplayUserNotFound(e.Message);
                }
                catch (ProductNotFoundException e)
                {
                    SUI.DisplayProductNotFound(e.Message);
                }
                catch (FormatException)
                {
                    SUI.DisplayGeneralError("Input was not in correct format");
                }
                catch (Exception e)
                {
                    SUI.DisplayGeneralError("Unexpected error occurred with message: " + e.Message);
                }
            }
            else
                SUI.DisplayGeneralError("Admin command not valid");
        }

        //hvis kommanden har et argument skal der vises information om brugeren med brugernavn magen til kommandoen
        void oneArgumentCommand(string Command)
        {
            //udskrivning af brugeroplysninger forsøges, og exceptions fanges
            try
            {
                SUI.DisplayUserInfo(SSystem.GetUser(Command));
            }
            catch (UserNotFoundException e)
            {
                SUI.DisplayUserNotFound(e.Message);
            }
            catch (Exception e)
            {
                SUI.DisplayGeneralError(e.Message);
            }
        }

        //to argumenter betyder at brugeren forsøger at købe et produkt
        // første argument er brugernavnet, andet er produkt ID'et
        void twoArgumentsCommand(string[] Commands)
        {
            int productID;
            //argument to forsøges parset til en int
            //fejlbesked printes hvis dette mislykkedes
            if (int.TryParse(Commands[1], out productID))
            {
                //her forsøget af finde bruger og produkt samt derefter at udføre en buytransaction
                //exceptions håndteres
                try
                {
                    User user = SSystem.GetUser(Commands[0]);
                    Product product = SSystem.GetProduct(productID);
                    BuyTransaction BT = SSystem.BuyProduct(user, product);
                    SUI.DisplayUserBuysProduct(BT);
                    //hvis brugerens konto er på under 50 kroner advares brugeren om dette
                    if (user.Balance < 50)
                    {
                        SUI.DisplayLowBalance(user);
                    }
                }
                catch (UserNotFoundException e)
                {
                    SUI.DisplayUserNotFound(e.Message);
                }
                catch (ProductNotFoundException e)
                {
                    SUI.DisplayProductNotFound(e.Message);
                }
                catch (InsufficientCreditsException e)
                {
                    SUI.DisplayInsufficientCash(e.Message);
                }
                catch (Exception e)
                {
                    SUI.DisplayGeneralError(e.Message);
                }
            }
            else
            {
                SUI.DisplayGeneralError("productID not a valid number");
            }
        }

        //tre argumenter betyder at brugeren forsøger at købe flere produkter på en gang
        //første argument er brugernavn, andet er antal ønskede køb og trejde er produkt ID for ønskede produkt
        void threeArgumentsCommand(string[] Commands)
        {
            int amount;
            //argument to forsøges parset til en int
            //fejlbesked printes hvis dette mislykkedes
            if (int.TryParse(Commands[1], out amount))
            {
                //bruger og produktID argumenter samles til en kommando med 2 argumenter
                //herefter køres enkelt købs metoden det ønskede antal gange
                string[] newCommands = { Commands[0], Commands[2] };
                for (int i = 0; i < amount; i++)
                {
                    twoArgumentsCommand(newCommands);
                }
            }
            else
            {
                SUI.DisplayGeneralError("Amount not a valid number");
            }
        }

        //admincommands initialiseres
        void InitializeAdminCommands()
        {
            //admincommands er hardcodede

            //q og quit resultere i at programmet lukker
            AdminCommands.Add(":q", x => { SUI.Close(); });
            AdminCommands.Add(":quit", x => { SUI.Close(); });
            //activate og deactivate bruger andet argument til at finde og sætte et bestemt produkt til at være aktivt eller ej
            AdminCommands.Add(":activate", x => { SSystem.SetProductActive(int.Parse(x[1]), true); });
            AdminCommands.Add(":deactivate", x => { SSystem.SetProductActive(int.Parse(x[1]), false); });
            //crediton og creditoff bruger andet argument til at finde og sætte et bestemt produkt
            //til at være muligt at købe på credit eller ej
            AdminCommands.Add(":crediton", x => { SSystem.SetProductCredit(int.Parse(x[1]), true); });
            AdminCommands.Add(":creditoff", x => { SSystem.SetProductCredit(int.Parse(x[1]), false); });
            //addcredits bruger andet argument til at finde en bruger og indsætter værdien af trejde argument ind på vedkommendes konto
            AdminCommands.Add(":addcredits", x => { SSystem.AddCreditsToAccount(SSystem.GetUser(x[1]), int.Parse(x[2])); });
        }
    }
}
