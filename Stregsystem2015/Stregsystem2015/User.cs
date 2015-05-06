using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stregsystem2015
{
    public class User : IEquatable<User>, IComparable
    {
        static private int NextUserID = 1;
        
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
            get { return _UserID; }
        }

        private string _Firstname;
        public string Firstname
        {
            get { return _Firstname; }
            set
            {
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
                string[] splitEmail = value.Split('@');
                string localPart = splitEmail[0];
                string localPartPattern = "^[a-zA-Z0-9._-]+$";
                string domain = splitEmail[1];
                string domainPattern = "^[a-zA-Z0-9-.]+$";
                string firstAndLast = domain.First().ToString() + domain.Last().ToString();
                if (splitEmail.Length == 2 && Regex.IsMatch(localPart, localPartPattern) && 
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
        }

        private decimal _Balance;

        public decimal Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }

        public override string ToString()
        {
            return Firstname + " " + Lastname + " (" + Email + ")";
        }

        public bool Equals(User other)
        {
            if (other != null && this.Username.Equals(other.Username))
                return true;
            else
                return false;
        }

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

        public override int GetHashCode()
        {
            return this.UserID.GetHashCode();
        }

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
