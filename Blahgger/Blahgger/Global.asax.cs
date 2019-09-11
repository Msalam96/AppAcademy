using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blahgger
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var user = HttpContext.Current.User;



            // TODO query the database for the user's
            // name and roles
            string connectionString = ConfigurationManager.ConnectionStrings["BlahggerDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = @"
                        Select Id, FirstName, LastName
                        From Users
                        Where Email = @Email and Password = @Password
                    ";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", user.Email);
            }
            // TODO customize user principal and identity




        }
    }
}
