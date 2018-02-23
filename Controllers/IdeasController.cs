using Idea.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
        }

        //[HttpPost]
        //public JsonResult Index(int id)
        //{
        //    //return Json("success");
        //    var userIdeas = { Title: "Chelsea vs Barcelona", Description: "They played 1-1 draw as Barca had more possession but Chelsea had more shots on target." };
        //    return Json();
        //}

        [HttpPost]
        public JsonResult Index(int id)
        {
            string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();
            User um = new User();
            List<Ideator> useridea = new List<Ideator>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_selectUserIdea", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userID", id);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    um.UserIdea = new List<Ideator>();
                    int numRows = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < numRows; i++)
                    {
                        Ideator idea = new Ideator();
                        idea.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                        idea.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                        idea.IdeasID = Convert.ToInt32(ds.Tables[0].Rows[i]["IdeasID"]);
                        um.UserIdea.Add(idea);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                return Json(um, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString.ToString();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_deleteIdeas", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ideaID", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }            
            }
            return RedirectToAction("Index", "Ideas");
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