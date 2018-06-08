using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationManage.Util
{
    public class Contacts
    {
        private string name;
        private string province;
        private string city;
        private string adress;
        private string postalcode;
        private string email;
        private string phone;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Province
        {
            get { return province; }
            set { province = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }
        public string PostalCode
        {
            get { return postalcode; }
            set { postalcode = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
    }
}
