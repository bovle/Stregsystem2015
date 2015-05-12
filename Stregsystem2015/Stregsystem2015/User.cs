using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    //klasse til at beskrive brugerere
    //arver fra 2 interfaces der gør det muligt for andre klasser og metoder at bruge equals, gethashcode og compareto
    public class User : IEquatable<User>, IComparable
    {
        //statisk tæller der sørger for at hver bruger får et unikt ID
        static private int NextUserID = 1;
        
        //constructor der intialisere næsten alle variabler
        public User(string firstname, string lastname, string username, string email)
        {
            _UserID = NextUserID;
            NextUserID++;

            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Balance = 0;
        }

        private int _UserID;
        public int UserID
        {
            //userID er read only
            get { return _UserID; }
        }

        private string _Firstname;
        public string Firstname
        {
            get { return _Firstname; }
            set
            {
                //firsname må ikke være null
                if (value != null)
                    _Firstname = value;
                else
                    throw new ArgumentNullException("Firstname");
            }
        }

        private string _Lastname;
        public string Lastname
        {
            get { return _Lastname; }
            set
            {
                //lastname må ikke være null
                if (value != null)
                    _Lastname = value;
                else
                    throw new ArgumentNullException("Lastname");
            }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                //username må kun indeholde små bogstaver, 0-9 og '_'
                string pattern = "^[a-z0-9_]+$";
                if (Regex.IsMatch(value, pattern))
                    _Username = value;
                else
                    throw new ArgumentException("Invalid Username");                
            }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                //email skal indeholde @ og have noget på begge sider
                string[] splitEmail = value.Split('@');
                if (splitEmail.Length == 2)
                {
                    //på venstresiden af @ er localpart
                    //localpart må kun indeholde bogstaver, 0-9 og tegnene '._-'
                    string localPart = splitEmail[0];
                    string localPartPattern = "^[a-zA-Z0-9._-]+$";
                    //på højresiden af @ er domain
                    //domain må kun indeholde bogstaver, 0-9 og tegnene '-.'
                    //derudover skal der være et punktum og domain må ikke starte eller slutte med et tegn
                    string domain = splitEmail[1];
                    string domainPattern = "^[a-zA-Z0-9-.]+$";
                    string firstAndLast = domain.First().ToString() + domain.Last().ToString();
                    if (Regex.IsMatch(localPart, localPartPattern) &&
                        Regex.IsMatch(domain, domainPattern) && !Regex.IsMatch(firstAndLast, "[.-]") &&
                        Regex.IsMatch(domain, "[.]"))
                    {
                        _Email = value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid Email");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid Email");
                }
                
                
            }
        }

        public decimal Balance { get; set; }

        //overrider tostring to at returnere en string med information om fornavn efternavn og email
        public override string ToString()
        {
            return Firstname + " " + Lastname + " (" + Email + ")";
        }

        //Equals metode der returnere true hvis brugerneavnene er ens
        public bool Equals(User other)
        {
            if (other != null && this.Username.Equals(other.Username))
                return true;
            else
                return false;
        }

        //Equals metode der kalder overstående hvis objektet i parameteren er en User
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            User other = obj as User;

            if (other != null)
                return this.Equals(other);
            else
                return false;
        }

        //returnere hashcoden for brugerID'et
        public override int GetHashCode()
        {
            return this.UserID.GetHashCode();
        }

        //sammenligner brugerID'er hvis objektet sendt med er en User
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            User other = obj as User;

            if (other != null)
                return this.UserID.CompareTo(other.UserID);
            else
                throw new ArgumentException("Object is not a User");
        }
    }
    

}
