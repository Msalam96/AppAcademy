using Blahgger.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blahgger.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Password"))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;
              
                // TODO get the user record from the database
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"
                        Select FirstName, LastName
                        From Users
                        Where Email = @Email and Password = @Password
                    ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                    }

                }
                // TODO check if the passwords match
                //if (!(user.Email == "james@smashdev.com" && user.Password == "password"))
                //{
                //    ModelState.AddModelError("", "Login failed.");
                //}
                if (String.IsNullOrEmpty(user.FirstName))
                {
                    ModelState.AddModelError("", "Login failed.");
                }
            }

            if (ModelState.IsValid)
            {
                // Login the user.
                FormsAuthentication.SetAuthCookie(user.Email, false);

                // Send them to the home page.
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Logout()
        {
           FormsAuthentication.SignOut();

           return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {

            User user = new User();
            user.FirstName = collection.Get("FirstName");
            user.LastName = collection.Get("LastName");
            user.Email = collection.Get("Email");
            user.Password = collection.Get("Password");

            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Password") &&
                ModelState.IsValidField("FirstName") && ModelState.IsValidField("LastName"))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"
                        Insert into Users(FirstName, LastName, Email, Password)
                        Values(@FirstName, @LastName, @Email, @Password)
                    ";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    SqlDataReader reader = command.ExecuteReader();

                    return RedirectToAction("Login");
                }
            }
            return View(user);
        }
        // GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Account/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Account/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Account/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Account/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Account/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Account/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Account/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
