using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //interface som beskriver hvilke metoder stregsystem interfacet skal have.
    //dette gør det muligt at udskifte hvilket stregsystem interface der bruges
    //uden at skulle ændre alle andre dele af koden
    public interface IStregsystemUI
    {
        void Start(StregsystemCommandParser parser);
        void Close();
        void DisplayLowBalance(User user);
        void DisplayUserNotFound(string message);
        void DisplayProductNotFound(string message);
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError();
        void DisplayAdminCommandNotFoundMessage(string command);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayInsufficientCash(string message);
        void DisplayGeneralError(string message);
        void DisplayInterface();
    }
}
