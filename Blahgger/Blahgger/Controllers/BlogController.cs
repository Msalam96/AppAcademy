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
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        // GET: Blog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            Blog blog = new Blog();
            return View(blog);
        }

        // POST: Blog/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Blog blog = new Blog();
            blog.UsersId = int.Parse(collection.Get("UsersId"));
            blog.Text = collection.Get("Text");
            blog.CreatedOn = DateTime.Now;

            //try
            //{
                if (ModelState.IsValidField("UsersId") && ModelState.IsValidField("Text"))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = @"
                        Insert into Blogs(UsersId, Text, CreatedOn)
                        Values(@UsersId, @Text, @CreatedOn)
                    ";
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@UsersId", blog.UsersId);
                        command.Parameters.AddWithValue("@Text", blog.Text);
                        command.Parameters.AddWithValue("@CreatedOn", blog.CreatedOn);
                        command.ExecuteNonQuery();
                    }
                return RedirectToAction("Index", "Home");
            }
                
            //}
            //catch
            //{
            return View(blog);
            //}
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Blog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
