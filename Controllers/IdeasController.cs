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
    public class IdeasController : Controller
    {
        // GET: Ideas
        public ActionResult Index()
        {
            string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();
            User um = new User();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_getIdeas", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    um.AllIdea = new List<Ideator>();
                    int numRows = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < numRows; i++)
                    {
                        Ideator idea = new Ideator();
                        idea.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                        idea.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                        um.AllIdea.Add(idea);
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Error");
                }
            }
            return View(um);
            //return RedirectToAction("Index", "Ideas");
        }

        [HttpPost]
        public ActionResult createIdea(User idea)
        {
            string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_insertIdeas", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userID", idea.Idea1.UserID);
                    cmd.Parameters.AddWithValue("title", idea.Idea1.Title);
                    cmd.Parameters.AddWithValue("description", idea.Idea1.Description);
                    cmd.ExecuteNonQuery();
                    con.Close();
            }
            return RedirectToAction("Index", "Ideas");
            //return View("Index");
        }
    }
}