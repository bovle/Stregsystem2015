using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    public class StregsystemCommandParser
    {
        public StregsystemCommandParser(IStregsystemUI sui, IStregsystem ssystem)
        {
            SSystem = ssystem;
            SUI = sui;
        }

        IStregsystem SSystem;
        IStregsystemUI SUI;

        public void ParseCommand(string command)
        {
            if (command.First() == ':')
            {
                ParseAdminCommand(command);
            }
            else
            {
                string[] Commands = command.Split(' ');
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
                        SUI.DisplayTooManyArgumentsError();
                        break;
                }
            }
        }

        void oneArgumentCommand(string Command)
        {
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

        void twoArgumentsCommand(string[] Commands)
        {
            int productID;
            if (int.TryParse(Commands[1], out productID))
            {
                try
                {
                    User user = SSystem.GetUser(Commands[0]);
                    Product product = SSystem.GetProduct(productID);
                    BuyTransaction BT = SSystem.BuyProduct(user, product);
                    SUI.DisplayUserBuysProduct(BT);
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

        void threeArgumentsCommand(string[] Commands)
        {
            int amount;
            if (int.TryParse(Commands[1], out amount))
            {
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

        void ParseAdminCommand(string command)
        {
            string Command = command.Remove(1, 1);
            switch (Command)
            {
                case "q":
                case "quit":
                    SUI.Close();
                    break;
                default:
                    break;
            }
        }
    }
}
