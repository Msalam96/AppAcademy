using Blahgger.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blahgger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.CurrentUserEmail = User.Identity.Name;

            string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;

            List<Blog> blogs = new List<Blog>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"
                    SELECT Id, Text, CreatedOn
                    FROM Blogs
                    Where UsersId = @UsersId
                    ORDER BY CreatedOn DESC
                ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UsersId", 1);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Blog blog = new Blog();
                    blog.Id = reader.GetInt32(0);
                    blog.Text = reader.GetString(1);
                    blog.CreatedOn = reader.GetDateTime(2);
                    blogs.Add(blog);
                }
            }
            return View(blogs);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Directory()
        {
            return View();
        }

        //// GET: Home/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Home/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Home/Create
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

        //// GET: Home/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Home/Edit/5
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

        //// GET: Home/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Home/Delete/5
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
