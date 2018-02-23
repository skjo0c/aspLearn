using Idea.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Idea.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }

        [HttpPost]
        public ActionResult Index(User frm)
        {
            string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                //SqlDataAdapter adapter = new SqlDataAdapter();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_checkUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("username", frm.username);
                    cmd.Parameters.AddWithValue("password", frm.password);
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    int count = ds.Tables[0].Rows.Count;
                   if (count>0)
                    {
                        Session["UserId"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Firstname"].ToString();
                        return RedirectToAction("Index","Ideas");          
                    }
                    else
                    {
                        con.Close();
                        return RedirectToAction("Index","Home");
                    }
                }
                catch(Exception ex)
                {
                    
                }
               return View("About");
            }
            
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp( User frm)
        {
            string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                //@firstname, @lastname, @username, @email, @password
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_InsertUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("firstname", frm.firstname);
                cmd.Parameters.AddWithValue("lastname", frm.lastname);
                cmd.Parameters.AddWithValue("username", frm.username);
                cmd.Parameters.AddWithValue("email", frm.email);
                cmd.Parameters.AddWithValue("password", frm.password);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //string username = frm.Keys[0].ToString();
            return View("Index");
        }
    }
}   