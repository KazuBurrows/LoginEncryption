using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;  // include the System.IO namespace
using System.Text;

namespace LoginEncryption2.Models
{
    public static class FileHandler
    {
        private static string FILE_PATH = @"C:\Users\user-000\Desktop\Hobby\vsCommunity_workspace\LoginEncryption2\LoginEncryption2\App_Data\user-logins.txt";


        /// <summary>
        /// Check if file exists
        /// </summary>
        /// <returns>
        /// boolean
        /// </returns>
        public static bool fileExists()
        {

            //var relativePath = HttpContext.Current.Server.MapPath(FILE_PATH);

            if (File.Exists(FILE_PATH))
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// Check if user exists in db
        /// </summary>
        /// <param name="passedEmail"></param>
        /// <returns>
        /// true if email is in db
        /// </returns>
        public static bool emailExists(string passedEmail)
        {
            bool email_exists = dbMatchEmail(passedEmail).Item1;
            
            return email_exists;
        }



        /// <summary>
        /// Search and fetch a match to 'formData.Eamil' from db via 'dbFetchUser'
        /// </summary>
        /// <param name="formdata"></param>
        /// <returns>
        /// Returns tuple of bool, string1, and string2
        /// bool is true if user/ email was found in db
        /// string1 is hashed password with salt
        /// string2 is salt(not hashed)
        /// </returns>
        public static Tuple<bool, string, string> dbFetchUser(FormData formdata)
        {

            return dbMatchEmail(formdata.Email);
        }





        /// <summary>
        /// Parse txt file and fetch a match to 'passedEmail' from db
        /// </summary>
        /// <param name="passedEmail"></param>
        /// <returns>
        /// Returns Tuple of (bool, string, string)
        /// bool is false if 'passedEmail' was not found in txt file
        /// string1 is hashed password with salt
        /// string2 is salt(not hashed)
        /// </returns>
        private static Tuple<bool, string, string> dbMatchEmail(string passedEmail)
        {

            bool email_matched = false;     // if email in line matches 'passedEmail'


            var fileStream = new FileStream(FILE_PATH, FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream);


            string line;            // Current line of file
            string readEmail = String.Empty;
            string userHashedPassword = String.Empty;
            string userSalt = String.Empty;


            // Terminate/ stop at EOF or when 'passedEmail' is matched
            while ((line = streamReader.ReadLine()) != null && email_matched == false)
            {

                Tuple<string, string, string> userData = splitUserLine(line);
                readEmail = userData.Item1;
                userHashedPassword = userData.Item2;
                userSalt = userData.Item3;


                // When email in line matches 'passedEmail', set 'email_matched' to true
                if (String.Equals(passedEmail, readEmail))
                {
                    email_matched = true;
                    break;
                }
            }


            return Tuple.Create(email_matched, userHashedPassword, userSalt);
        }




        /// <summary>
        /// Split a line from db to email, hashed password, and salt
        /// </summary>
        /// <param name="line"></param>
        /// <returns>
        /// Returns tuple of (string, string, string)
        /// string1 is email
        /// string2 is hashed password(salt imbedded in password)
        /// string3 is salt used on hased password
        /// </returns>
        private static Tuple<string, string, string> splitUserLine(string line)
        {
            char[] spearator = { ',', ' ' };    // Split string when ',' and ' '

            string[] userData = line.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

            string email = userData[0];
            string hashedPassword = userData[1];
            string salt = userData[2];


            return Tuple.Create(email, hashedPassword, salt);
        }



        /// <summary>
        /// Create new user to db
        /// </summary>
        /// <param name="formData"></param>
        /// <returns>
        /// true if successful
        /// </returns>
        public static bool registerUser(string email, string password, string hashPassword)
        {

            //var relativePath = HttpContext.Current.Server.MapPath(filePath);
            using (StreamWriter writer = new StreamWriter(FILE_PATH, true))
            {
                string userDataLine = String.Format("{0}, {1}, {2}", email, password, hashPassword);             //NEED TO STORE SALT
                writer.WriteLine(userDataLine);
            }


            return false;
        }










    }
}