using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office.CustomXsn;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Hunarmis.Manager;
using Hunarmis.Models;
using MariGold.OpenXHTML;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static Hunarmis.Manager.Enums;
using Run = DocumentFormat.OpenXml.Spreadsheet.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;

namespace Hunarmis.Controllers
{

    public class UserProfileController : Controller
    {
        // GET: UserProfile
        Hunar_DBEntities db = new Hunar_DBEntities();
        JsonResponseData response = new JsonResponseData();
        int result = 0;
        [SessionCheckPart]
        public ActionResult Index()
        {
            return View();
        }
        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
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
        [SessionCheckPart]
        public ActionResult DownLoadSoftSkill()
        {
            return View();
        }

        #region  Resume Template
        [SessionCheckPart]
        public ActionResult ResumeTemplate(Guid? ID)
        {
            PartResumeMode model = new PartResumeMode();
            if (Session["PartUserId"] != null && !string.IsNullOrWhiteSpace(Session["PartUserId"].ToString()))
            {
                model.PartId = Guid.Parse(Session["PartUserId"].ToString());
                if (ID == null)
                {
                    DataSet ds =SPManager.GetIndiParticipantDetailsByID(Session["PartUserId"].ToString());
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    dt = ds.Tables[0]; dt1 = ds.Tables[1];
                    string bodyTemplate = string.Empty;//Server.MapPath("~/Certificate/ParticipantCertificate.html")
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Shared/ResumeFormat.html")))
                    {
                        bodyTemplate = reader.ReadToEnd();
                    }
                    var bodydata = bodyTemplate.Replace("{Name}", dt.Rows[0]["Name"].ToString())
                          .Replace("{EmailID}", dt.Rows[0]["EmailID"].ToString())
                          .Replace("{PhoneNo}", dt.Rows[0]["PhoneNo"].ToString())
                          .Replace("{Age}", dt.Rows[0]["Age"].ToString());
                    model.ResumeTemplate = bodydata;
                }
            }

            if (ID != null)
            {
                var tbl = db.tbl_ParticipantResumeTemplate.Find(ID);
                model.PartResumeId_pk = tbl.PartResumeId_pk;
                model.PartId = Guid.Parse(Session["PartUserId"].ToString());
                model.ResumeTemplate = tbl.ResumeTemplate;
                //var toReplace = "src=\"" + CommonModel.GetWebUrl() + "/Uploads/";
                //var toPicUSReplace = CommonModel.GetHeaderUSLogo(tbl.ResumeTemplate);
                //var toPicCareReplace = CommonModel.GetHeaderCareLogo(toPicUSReplace);
                //model.ResumeTemplate = toPicCareReplace.Replace("src=\"..//Uploads/", toReplace);

               


            }
            return View(model);
        }
        [SessionCheckPart]
        [HttpPost]
        public ActionResult ResumeTemplate(PartResumeMode model)
        {
            result = 0;

            if (string.IsNullOrEmpty(model.ResumeTemplate))
            {
                ModelState.AddModelError("HtmlContent", "Please provide HTML content.");
                return View("ResumeTemplate");
            }
            if (ModelState.IsValid)
            {
                if (db.tbl_ParticipantResumeTemplate.Any(x => x.PartId != model.PartId))
                {
                    GlobalUtilityManager.MessageToaster(this, "Already Exists Resume Format.", Enums.GetEnumDescription(Enums.eReturnReg.Already), eAlertType.error.ToString());
                    return View(model);
                }
                var tbl = model.PartResumeId_pk != Guid.Empty ? db.tbl_ParticipantResumeTemplate.Find(model.PartResumeId_pk) : new tbl_ParticipantResumeTemplate();
                if (tbl != null)
                {
                    tbl.ResumeTemplate = model.ResumeTemplate;
                    if (model.PartResumeId_pk == Guid.Empty)
                    {
                        tbl.PartResumeId_pk = Guid.NewGuid();
                        tbl.PartId = model.PartId;
                        tbl.CreatedBy = model.PartId.ToString();
                        tbl.CreatedOn = DateTime.Now;
                        tbl.IsActive = true;
                        db.tbl_ParticipantResumeTemplate.Add(tbl);
                    }
                    else
                    {
                        tbl.UpdateBy = model.PartId.ToString();
                        tbl.UpdateByOn = DateTime.Now;
                    }
                    result = db.SaveChanges();

                    if (result > 0)
                    {
                        //Load an existing HTML file.
                        using (MemoryStream mem = new MemoryStream())
                        {
                            WordDocument doc = new WordDocument(mem);
                            doc.Process(new HtmlParser(model.ResumeTemplate));
                            doc.Save();

                            return File(mem.ToArray(), "application/msword", "ResumeFormat.docx");
                        }
                        // GlobalUtilityManager.MessageToaster(this, "Resume Format", Enums.GetEnumDescription(Enums.eReturnReg.Insert), eAlertType.success.ToString());
                        // return RedirectToAction("ResumeTemplate");
                    }
                }
            }
            return View(model);
        }

        #endregion
    }
}