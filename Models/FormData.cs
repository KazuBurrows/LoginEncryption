using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using LoginEncryption2.Models;


namespace LoginEncryption2.Models
{
    public class FormData
    {
        private string email;
        private string password;
        private string encryptedPassword;


        public string Email
        {
            get { return email; }
            set { email = value; }
        }


        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public string EncryptedPassword
        {
            get { return encryptedPassword; }
            set { encryptedPassword = value; }
        }


    }




}
