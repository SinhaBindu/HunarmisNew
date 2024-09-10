using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Hunarmis.Manager;
using Hunarmis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        [SessionCheckPart]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            Session.Clear();
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
                        Session["PartUserId"] = dt.Rows[0]["UserID_Fk"].ToString();
                        Session["IndiUserID_Fk"] = dt.Rows[0]["UserID_Fk"].ToString();
                        Session["IndiRegID"] = dt.Rows[0]["RegID"].ToString();
                        Session["Name"] = dt.Rows[0]["Name"].ToString();
                        Session["EmailID"] = dt.Rows[0]["EmailID"].ToString();
                        return RedirectToAction("UserDashBaord", "UserProfile", new { UserID = dt.Rows[0]["UserID_Fk"].ToString() });

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
        [SessionCheckPart]
        public ActionResult UserDashBaord(string UserID)
        {
            DataSet ds = SPManager.GetIndiParticipantDetailsByID(UserID);
            return View(ds);
        }
        [SessionCheckPart]
        public ActionResult DownLoadCretificate(string UId, int CId, int BId)
        {
            Hunar_DBEntities dBEntities = new Hunar_DBEntities();
            CertificateModel model = new CertificateModel();
            string bodydata = string.Empty;
            DataSet ds = SPManager.GetSP_DownloadCertificate(UId, CId, BId);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    var IsCertf = dt.Rows[0]["IsCertificateApplicable"].ToString();
                    model.IsCertificate = IsCertf;
                    model.ScorePercentage = dt.Rows[0]["Percentage"].ToString();
                    if (dt.Rows[0]["IsCertificateApplicable"].ToString() == "1")
                    {
                        string bodyTemplate = string.Empty;//Server.MapPath("~/Certificate/ParticipantCertificate.html")
                        using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/CertificateIssuePart.html")))
                        {
                            bodyTemplate = reader.ReadToEnd();
                        }
                        if (!string.IsNullOrWhiteSpace(bodyTemplate))
                        {
                         
                            decimal ASCP = Convert.ToDecimal(dt.Rows[0]["Percentage"].ToString());
                            var AScore = Math.Round(ASCP, MidpointRounding.ToEven);
                            bodydata = bodyTemplate.Replace("{Name}", dt.Rows[0]["ReportedBy"].ToString())
                       .Replace("{CourseName}", dt.Rows[0]["CourseName"].ToString())
                       //.Replace("{Completion}", "yes")//dt.Rows[0][""].ToString()
                       .Replace("{ConductedBy}", dt.Rows[0]["TrainerName"].ToString())
                       .Replace("{BatchS}", dt.Rows[0]["BatchStartDate"].ToString())
                       .Replace("{BatchE}", dt.Rows[0]["BatchEndDate"].ToString())
                       .Replace("{TrainingCenter}", dt.Rows[0]["TrainingCenter"].ToString())
                       .Replace("{IsIssueDt}", dt.Rows[0]["IsIssueDt"].ToString())
                       .Replace("{AssessmentScore}", AScore.ToString());
                            model.HrmlData = bodydata;
                        }
                    }
                }
            }
            return View(model);
        }
        public ActionResult DownLoadCretificateIF(string CPath)
        {
            Hunar_DBEntities dBEntities = new Hunar_DBEntities();
            var tblc = dBEntities.Courses_Master.Find(Convert.ToInt32(CPath));
            return View(tblc);
        }
        public ActionResult DownLoadSoftSkill()
        {
            return View();
        }
    }
}