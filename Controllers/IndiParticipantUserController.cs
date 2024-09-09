using Hunarmis.Manager;
using Hunarmis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Controllers
{
    public class IndiParticipantUserController : Controller
    {
        // GET: IndiParticipantUser
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            //Session.Clear();
            //if (!string.IsNullOrWhiteSpace(RandomValue))
            //{
            //    Session["RV"] = RandomValue;
            //}
            //else
            //{
            //    Session["RV"] = null;
            //}
            IndiParticipantModels model = new IndiParticipantModels();
            return View(model);
        }

        //[SessionCheckPart]
        //[Authorize(Roles = "User")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string RandomValue, IndiParticipantModels model)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            try
            {
              //  System.IO.File.AppendAllText(Server.MapPath("~/logLoginUser.txt"), $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}:
                if (model != null)
                {
                    DataTable dt = SPManager.SP_LoginForIndiParticipantCheck(model);
                    if (dt.Rows.Count > 0)
                    {
                        var loginmsg = "Login Success : UserID_Fk :" + dt.Rows[0]["UserID_Fk"].ToString() + " RegID :" + dt.Rows[0]["RegID"].ToString();
                        //  System.IO.File.AppendAllText(Server.MapPath("~/logIndiLoginUser.txt"), $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}: {loginmsg}{Environment.NewLine}");
                        Session["CUser"] = dt.Rows[0]["UserID_Fk"].ToString();
                        Session["IndiUserID_Fk"] = dt.Rows[0]["UserID_Fk"].ToString();
                        Session["IndiRegID"] = dt.Rows[0]["RegID"].ToString();
                        Session["Name"] = dt.Rows[0]["Name"].ToString();
                        Session["EmailID"] = dt.Rows[0]["EmailID"].ToString();
                        return RedirectToAction("IndiPartiDashBaord", "IndiParticipantUser");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Email ID and Password Invalid !!");
                        model.RandomValue = RandomValue;
                        return View(model);
                    }
                }
                ModelState.AddModelError("", "Please Enter Email ID and Password !!");
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        public ActionResult IndiPartiDashBaord(string UserID)
        {
            DataSet ds = SPManager.GetIndiParticipantDetailsByID(UserID);
            return View(ds);
        }
    }
}