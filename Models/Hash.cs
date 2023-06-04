using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LoginEncryption2.Models
{
    public static class Hash
    {

        private static string PEPPER = "RATjIoXzNjyAYOSP%WxI$EGlIXrfZmGw@fciaj%e!!bkAE!%NciClDtfGvLKhId@";


        public static Tuple<string, string> EncodePassword(string password)
        {
            string salt = generateRandomSalt();

            return EncodeSHA251String(password, salt);
        }




        public static bool AuthenticatePassword(string password, string dbHashedPassword, string appliedSalt)
        {
            string shaPassword =  EncodeSHA251String(password, appliedSalt).Item1;


            if (String.Equals(dbHashedPassword, shaPassword))
            {
                return true;
            }


            return false;
        }





        //Encode string and salt to sha512
        private static Tuple<string, string> EncodeSHA251String(string password, string salt)
        {
            
            SHA512 sha512 = SHA512Managed.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(string.Concat(password, PEPPER,  salt));
            byte[] hashByte = sha512.ComputeHash(bytes);
            string hashString = GetStringFromHash(hashByte);

            sha512.Clear();

            return Tuple.Create(hashString, salt);
        }








        //byte array to string
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }



        private static string generateRandomSalt()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&?";
            var random = new Random();

            string salt = "";

            for (int i = 0; i < 64; i++)
            {
                salt += chars[random.Next(chars.Length)];
            }

            return salt;
        }

    }
}