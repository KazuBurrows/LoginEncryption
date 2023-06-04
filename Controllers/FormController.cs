
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginEncryption2.Models;

namespace LoginEncryption2.Controllers
{
    public class FormController : Controller
    {


        // Strongly-typed sychnronous form
        //[HttpPost]
        //public ActionResult FormTwo(Models.FormData formData)
        //{

        //    formData.encrytPassword(formData.Password);


        //    return View(formData);
        //}



        ///ActionResult RegisterForm
        [HttpPost]
        public ActionResult RegisterForm(Models.FormData formData)
        {

            string formEmail = formData.Email;
            string formPassword = formData.Password;


            ////if email exists in db, redirect
            //if (!(f.emailExists(formEmail)))
            //{
            //    return RedirectToAction("", "", new { msg = ErrorMessage.user_exists });
            //}



            Tuple<string, string> shaPasswordTuple = Hash.EncodePassword(formData.Password);

            string passwordHash = shaPasswordTuple.Item1;
            string salt = shaPasswordTuple.Item2;

            // add to db

            bool a = FileHandler.registerUser(formEmail, passwordHash, salt);


            return View(formData);

        }



        ///ActionResult LoginForm
        [HttpPost]
        public ActionResult LoginForm(Models.FormData formData)
        {

            //check if email exists
            Tuple<bool, string, string> userData = FileHandler.dbFetchUser(formData);
            bool userExists = userData.Item1;
            string userHashedPassword = userData.Item2;
            string userSalt = userData.Item3;


            if (!(userExists))
            {
                return RedirectToAction("Index", "Home");
            }



            //check if password matches


            bool passwordIsValid = Hash.AuthenticatePassword(formData.Password, userHashedPassword, userSalt);

            // if not input password does not match db's password
            if (!passwordIsValid)
            {
                return RedirectToAction("Index", "Home");

            }



            return View(formData);

        }







        /////Testing ajax for login
        //[HttpPost]
        //public ActionResult LoginForm(string passedEmail, string passedPassword)
        //{

        //    FormData formData = new FormData();
        //    formData.Email = passedEmail;
        //    formData.Password = passedPassword;

        //    //check if email exists
        //    Tuple<bool, string, string> userData = FileHandler.dbFetchUser(formData);
        //    bool userExists = userData.Item1;
        //    string userHashedPassword = userData.Item2;
        //    string userSalt = userData.Item3;


        //    if (!(userExists))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }



        //    //check if password matches


        //    bool passwordIsValid = Hash.AuthenticatePassword(formData.Password, userHashedPassword, userSalt);

        //    // if not input password does not match db's password
        //    if (!passwordIsValid)
        //    {
        //        return RedirectToAction("Index", "Home");

        //    }



        //    return View(formData);

        //}











    }
}