﻿using Antlr.Runtime.Misc;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.EMMA;
using Hunarmis.Manager;
using Hunarmis.Models;
using Newtonsoft.Json;
using NLog.Fluent;
using SubSonic.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Hunarmis.Manager.CommonModel;

namespace Hunarmis.Controllers
{
    //[Authorize(Roles = "Admin,State,Trainer,Verifier")]
    public class AssessmentController : Controller
    {
        // GET: Assessment
        Hunar_DBEntities db = new Hunar_DBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetIndex(string User = "all", int FormId = 1, int BatchId = 0)
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                User = CommonModel.IsRoleLogin();
                ds = SPManager.GetSPScoreMarkAnswer(User, FormId, BatchId);
                bool IsCheck = false;
                if (ds.Tables.Count > 0)
                {
                    tbllist = (ds.Tables[0]);
                    IsCheck = true;
                    var DList = JsonConvert.SerializeObject(tbllist);
                    //html = ConvertViewToString("_Index", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = DList }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult ScorerSummary()
        {
            return View();
        }
        public ActionResult GetScorerSummary(string User = "all", int FormId = 1, int BatchId = 0)
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            try
            {
                User = CommonModel.IsRoleLogin();
                ds = SPManager.GetSP_ScorersSummaryMarks(User, FormId, BatchId);
                if (ds.Tables.Count > 0)
                {
                    tbllist = (ds.Tables[0]);
                    var html = ConvertViewToString("_ScorerSummaryData", tbllist);
                    var res = tbllist.Rows.Count > 0 ? Json(new { IsSuccess = true, reshtml = html }, JsonRequestBehavior.AllowGet)
                        : Json(new { IsSuccess = false, reshtml = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = false, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        public ActionResult Summary()
        {
            return View();
        }
        public ActionResult GetSummary(string User = "all", int FormId = 1, int BatchId = 0)
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                User = CommonModel.IsRoleLogin();
                ds = SPManager.GetQuestionSummaryMarks(User, FormId, BatchId);
                bool IsCheck = false;
                if (ds.Tables.Count > 0)
                {
                    tbllist = (ds.Tables[0]);
                    IsCheck = true;
                    var DList = JsonConvert.SerializeObject(tbllist);
                    //html = ConvertViewToString("_Index", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = DList }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        //Trainer
        [Authorize(Roles = "Admin,State,Verifier,Trainer")]
        public ActionResult AddRole(int? Id, int FId)
        {
            var m = GetAdd(Id, FId, "");
            return View(m);
        }
        // [Authorize(Roles = "User")]
        [AllowAnonymous]
        [SessionCheckPart]
        public ActionResult Add(int? Id, int FId, string Rdmkeyno)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            if (Session["AssessmentSendLinkPartId_pk"] != null && Session["AssessmentSendLinkPartId_pk"].ToString() != "")
            {

                ParticipantLoginModel filter = new ParticipantLoginModel();
                filter.EmailID = Session["EmailID"].ToString();

                filter.RandomValue = Session["RandomValue"].ToString();
                filter.ParticipantId_fk = Guid.Parse(Session["PartUserId"].ToString());

                DataTable dt = SPManager.SP_LoginForParticipantCheck(filter);
                if (dt.Rows.Count > 0)
                {
                    Session["IsExam"] = dt.Rows[0]["IsExam"].ToString();
                    Session["SurveyId"] = dt.Rows[0]["SurveyId"].ToString();

                    Session["AssessmentSendLinkPartId_pk"] = dt.Rows[0]["AssessmentSendLinkPartId_pk"].ToString();
                    Session["AssessmentScheduleId_fk"] = dt.Rows[0]["AssessmentScheduleId_fk"].ToString();
                    Session["PartUserId"] = dt.Rows[0]["PartUserId"].ToString();
                    Session["EmailID"] = dt.Rows[0]["EmailID"].ToString();

                    Session["RandomValue"] = dt.Rows[0]["RandomValue"].ToString();
                    Session["TrainingCenterId"] = dt.Rows[0]["TrainingCenterId"].ToString();
                    Session["CourseId"] = dt.Rows[0]["CourseId"].ToString();
                    Session["FormId"] = !string.IsNullOrWhiteSpace(dt.Rows[0]["FormId"].ToString()) ? dt.Rows[0]["FormId"].ToString() : FId.ToString();
                    Session["BatchId"] = dt.Rows[0]["BatchId"].ToString();

                    Session["AssessmentCurStatus"] = dt.Rows[0]["AssessmentCurStatus"].ToString();
                    Session["IsAssessmentExpired"] = dt.Rows[0]["IsAssessmentExpired"].ToString();
                    Session["StartTime"] = dt.Rows[0]["StartTime"].ToString();
                    Session["EndTime"] = dt.Rows[0]["EndTime"].ToString();
                    Session["StartDateTime"] = dt.Rows[0]["StartDateTime"].ToString();
                    Session["EndDateTime"] = dt.Rows[0]["EndDateTime"].ToString();
                    Session["ExamDate"] = dt.Rows[0]["ExamDate"].ToString();
                    var assessmentsendid = Guid.Parse(dt.Rows[0]["AssessmentSendLinkPartId_pk"].ToString());
                    var tbl2nd = db_.tbl_AssessmentSendLinkEmail.Find(assessmentsendid);
                    var AssessmentCurStatus = dt.Rows[0]["AssessmentCurStatus"].ToString();
                    var IsAssessmentExpired = dt.Rows[0]["IsAssessmentExpired"].ToString();

                    filter.BatchId = Convert.ToInt32(Session["BatchId"].ToString());
                    filter.ParticipantId = Convert.ToString(Session["PartUserId"]);
                    filter.FormId = Convert.ToInt32(Session["FormId"]);//
                    var tblget = db_.tbl_Survey.Where(x => x.CreatedBy == filter.ParticipantId && x.BatchId == filter.BatchId && x.FormId == filter.FormId).ToList();
                    if (tblget.Any(x => x.IsActive == true))
                    {
                        return RedirectToAction("AssessmentDone", "ParticipantUser");
                    }

                    if (AssessmentCurStatus == "2")
                    {
                        return RedirectToAction("AssessmentExpired", "ParticipantUser");
                    }
                    else if (AssessmentCurStatus == "1")
                    {
                        var assessmentid = Guid.Parse(dt.Rows[0]["AssessmentScheduleId_fk"].ToString());
                        var tbl = db_.tbl_AssessmentSchedule.Find(assessmentid);
                        tbl.AssessmentSchedule = true;
                        tbl.UpdatedOn = DateTime.Now;
                        tbl2nd.AssessmentSchedule = true;
                        tbl2nd.UpdatedOn = DateTime.Now;
                        db_.SaveChanges();
                        // return RedirectToAction("AssessmentDone", "ParticipantUser");
                    }
                    if (dt.Rows[0]["IsFinalDone"].ToString() == "1")
                    {
                        return RedirectToAction("AssessmentDone", "ParticipantUser");
                    }
                    Rdmkeyno = dt.Rows[0]["RandomValue"].ToString();
                    if (AssessmentCurStatus == "0" && dt.Rows[0]["IsExam"].ToString() == "1")
                    {
                        return RedirectToAction("AssessmentExpired", "ParticipantUser");
                    }
                    else if (IsAssessmentExpired == "1" && dt.Rows[0]["IsExam"].ToString() == "1")
                    {
                        if (dt.Rows[0]["IsDraftDone"].ToString() == "1")
                        {
                            return RedirectToAction("AssessmentDone", "ParticipantUser");
                        }
                        return RedirectToAction("AssessmentExpired", "ParticipantUser");
                    }
                }
                else
                {
                    var d = "Add Post Data Session[AssessmentSendLinkPartId_pk]==null:" + "Email ID and Password Invalid Login !!";
                    System.IO.File.AppendAllText(Server.MapPath("~/logLoginUser.txt"), $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}: {d}{Environment.NewLine}");
                    ModelState.AddModelError("", "Email ID and Password Invalid Login !!");
                    return RedirectToAction("Login", "ParticipantUser");
                }

                Id = (Session["SurveyId"] != null && !string.IsNullOrWhiteSpace(Session["SurveyId"].ToString())) ? Convert.ToInt32(Session["SurveyId"].ToString()) : Id;
                var m = GetAdd(Id, FId, Rdmkeyno);
                return View(m);
            }
            return RedirectToAction("Login", "ParticipantUser");
        }
        [AllowAnonymous]
        [SessionCheckPart]
        private QesRes GetAdd(int? Id, int FId, string Rdmkeyno)
        {
            Hunar_DBEntities _db = new Hunar_DBEntities();
            FormModel model;
            List<FormModel> modellist = new List<FormModel>();
            Id = (Session["SurveyId"] != null && !string.IsNullOrWhiteSpace(Session["SurveyId"].ToString())) ? Convert.ToInt32(Session["SurveyId"].ToString()) : Id;
            var tblhr = _db.tbl_Survey.Find(Id);
            var tblhrAns = _db.tbl_SurveyAnswer.Where(x => x.SId_fk == Id).ToList();
            var fid = Convert.ToInt32(FId);
            var tbl = _db.QuestionBooks.Where(x => x.IsActive == true && x.FormId_fk == fid && x.IsQuestionDisplay == true).OrderBy(x => x.QuestionCode).ToList();
            var tbloptionlist = _db.QuestionOptions.Where(x => x.IsActive == true && x.FormId_fk == fid).ToList();
            if (tbl != null)
            {
                foreach (var item in tbl.ToList())
                {
                    model = new FormModel();
                    model.OptionList = new List<QuestOption>();
                    //model.OptionList.Clear();
                    model.FormId = item.FormId_fk.Value;
                    model.QuestionId_pk = item.Id;
                    model.QuestionCode = item.QuestionCode;
                    model.Question = HttpUtility.JavaScriptStringEncode(item.Question);
                    model.ControlType = item.ControlType;
                    model.ParentQuestionCode = item.ParentQuestionCode;
                    model.DependedAnswer = item.DependedAnswer;
                    model.SectionType = item.SectionType;
                    model.HindiQuestion = HttpUtility.JavaScriptStringEncode(item.HindiQuestion);
                    model.OptionTypeValidation = item.QuestTypeValidation;

                    var tbllist = tbloptionlist.Where(x => x.QuestionCode == item.QuestionCode).OrderBy(x => x.QuestionCode).ToList();
                    if (tbllist != null)
                    {
                        for (int i = 0; i < tbllist.Count; i++)
                        {
                            var ans = tblhrAns.FirstOrDefault(x => x.QuestionCode == tbllist[i].QuestionCode && x.QuestionOption_fk == tbllist[i].Id);
                            model.OptionList.Add(new QuestOption
                            {
                                Id = ans == null ? 0 : ans.Id,
                                FormId_fk = tbllist[i].FormId_fk,
                                OptionId_Pk = tbllist[i].Id,
                                QuestionId_fk = tbllist[i].QuestionId_fk.Value,
                                QuestionCode = tbllist[i].QuestionCode,
                                ControlInputType = tbllist[i].ControlInputType,
                                OptionTypeValidation = tbllist[i].OptionTypeValidation,
                                Limit = tbllist[i].Limit,
                                Text = tbllist[i].OptionText,
                                HindiOptionText = HttpUtility.JavaScriptStringEncode(tbllist[i].HindiOptionText),
                                Value = tbllist[i].OptionCode,
                                InputText = ans?.InputValue,

                                LabelName1 = HttpUtility.JavaScriptStringEncode(tbllist[i].LabelName1),
                                LabelName2 = HttpUtility.JavaScriptStringEncode(tbllist[i].LabelName2),
                                ControlInputType1 = tbllist[i].ControlInputType1,
                                InputType1 = tbllist[i].InputType1,
                                InputText1 = ans?.InputValue1,

                                SelectedItem = ans != null
                            });
                            if (ans != null)
                            {
                                model.Answer = tbllist[i].OptionCode;
                            }
                        }
                    }
                    modellist.Add(model);
                }
            }
            var reslist = BuildQuestion(modellist);
            reslist = reslist.OrderBy(x => x.OrderBy).ToList();
            var qList = new List<FormModel>();
            foreach (var item in reslist.ToList())
            {
                qList.Add(item);
                if (item.ChildQuestionList != null && item.ChildQuestionList.Any())
                {
                    qList.AddRange(item.ChildQuestionList.ToList());
                }
            }
            if (tblhr != null)
            {
                if (tblhr.Id > 0)
                {
                    if (!string.IsNullOrWhiteSpace(Rdmkeyno))
                    {
                        tblhr.BatchId = Convert.ToInt32(Session["BatchId"].ToString());
                        tblhr.TrainingCenterId = Convert.ToInt32(Session["TrainingCenterId"].ToString());
                    }
                    return new QesRes
                    {
                        Id = tblhr.Id,
                        Lat = tblhr.Lat,
                        Long = tblhr.Long,
                        FormId = fid,
                        YearId = tblhr.YearId,
                        BatchId = tblhr.BatchId.Value,
                        TrainingCenterId = tblhr.TrainingCenterId.Value,
                        FrequencyId = tblhr.FrequencyId,
                        RandomValue = Rdmkeyno,
                        StartTime = (Session["StartTime"] != null && !string.IsNullOrWhiteSpace(Session["StartTime"].ToString())) ? Session["StartTime"].ToString() : null,
                        EndTime = (Session["EndTime"] != null && !string.IsNullOrWhiteSpace(Session["EndTime"].ToString())) ? Session["EndTime"].ToString() : null,
                        Qlist = qList,
                        IsDraft = tblhr.IsDraft.Value
                    };
                }
            }
            ViewBag.Qlist = qList;
            if (Session["BatchId"] != null && Session["TrainingCenterId"] != null)
            {
                return new QesRes { SchoolId = 0, BatchId = Convert.ToInt32(Session["BatchId"].ToString()), TrainingCenterId = Convert.ToInt32(Session["TrainingCenterId"].ToString()), RandomValue = Session["RandomValue"].ToString(), StartTime = Session["StartTime"].ToString(), EndTime = Session["EndTime"].ToString(), FormId = fid, Qlist = qList, IsDraft = false };

            }
            return new QesRes { SchoolId = 0, BatchId = 0, Lat = "", Long = "", TrainingCenterId = 0, RandomValue = Rdmkeyno, StartTime = "", EndTime = "", FormId = fid, Qlist = qList, IsDraft = false };
        }
        [AllowAnonymous]
        [SessionCheckPart]
        [HttpPost]
        public ActionResult Add(QesRes model)
        {

            var d = "Add Post Data :" + JsonConvert.SerializeObject(model);
            System.IO.File.AppendAllText(Server.MapPath("~/log.txt"), $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}: {d}{Environment.NewLine}");

            Hunar_DBEntities db_ = new Hunar_DBEntities();
            var result = 0; //var fid = Convert.ToInt32(2);
            string localIP = GetLocalIPAddress();
            string publicIP = GetPublicIPAddress();
            try
            {
                if (model.BatchId == 0)
                {
                    return Json(new { IsSuccess = false, htmlData = "Please select Batch.", msg = "Please select Batch." }, JsonRequestBehavior.AllowGet);
                }
                if (model.TrainingCenterId == 0)
                {
                    return Json(new { IsSuccess = false, htmlData = "Please select TrainingCenter.", msg = "Please select TrainingCenter." }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.Lat) && string.IsNullOrWhiteSpace(model.Long))
                {
                    return Json(new { IsSuccess = false, htmlData = "Please Enable Geolocation Latitude and Longitude.", msg = "Please Enable Geolocation Latitude and Longitude." }, JsonRequestBehavior.AllowGet);
                }
                //var resdata = this.Request.Unvalidated.Form.AllKeys;
                if (model != null && Session["PartUserId"] != null)
                {
                    ParticipantLoginModel filter = new ParticipantLoginModel();
                    filter.EmailID = Session["EmailID"].ToString();
                    filter.AssessmentScheduleId_fk = Guid.Parse(Session["AssessmentScheduleId_fk"].ToString());
                    filter.BatchId = model.BatchId;
                    filter.FormId = model.FormId;
                    filter.ParticipantId_fk = Guid.Parse(Session["PartUserId"].ToString());
                    //filter.Password = Session["Password"].ToString();
                    filter.RandomValue = Session["RandomValue"].ToString();

                    Session["FormId"] = model.FormId;
                    Session["BatchId"] = model.BatchId;
                    DataTable dt = SPManager.SP_LoginForParticipantCheck(filter);
                    if (dt.Rows.Count > 0)
                    {
                        Session["StartTime"] = dt.Rows[0]["StartTime"].ToString();
                        Session["EndTime"] = dt.Rows[0]["EndTime"].ToString();
                        Session["StartDateTime"] = dt.Rows[0]["StartDateTime"].ToString();
                        Session["EndDateTime"] = dt.Rows[0]["EndDateTime"].ToString();
                        Session["ExamDate"] = dt.Rows[0]["ExamDate"].ToString();

                        var assessmentsendid = Guid.Parse(dt.Rows[0]["AssessmentSendLinkPartId_pk"].ToString());
                        var tbl2nd = db_.tbl_AssessmentSendLinkEmail.Find(assessmentsendid);
                        var AssessmentCurStatus = Session["AssessmentCurStatus"].ToString();
                        var IsAssessmentExpired = Session["IsAssessmentExpired"].ToString();
                        if (AssessmentCurStatus == "1" || IsAssessmentExpired == "1")
                        {
                            var assessmentid = Guid.Parse(dt.Rows[0]["AssessmentScheduleId_fk"].ToString());
                            var tbl = db_.tbl_AssessmentSchedule.Find(assessmentid);
                            tbl.AssessmentSchedule = true;
                            tbl.UpdatedOn = DateTime.Now;
                            

                            tbl2nd.AssessmentSchedule = true;
                            tbl2nd.UpdatedOn = DateTime.Now;
                            db_.SaveChanges();
                           // return Json(new { IsSuccess = false, type = 1, Redirect = "AssessmentDone", Control = "ParticipantUser" }, JsonRequestBehavior.AllowGet);
                            // return RedirectToAction("AssessmentDone", "ParticipantUser");
                        }

                    }

                    if (model.BatchId == 0)
                    {
                        return Json(new { IsSuccess = false, res = "", msg = "Select School !" }, JsonRequestBehavior.AllowGet);
                    }
                    var assId = Guid.Parse(Session["AssessmentScheduleId_fk"].ToString());
                    var partId = Session["PartUserId"].ToString();
                    var maintbl = model.Id != 0 ? db_.tbl_Survey.Find(model.Id) : new tbl_Survey();
                    var main_tbl = maintbl.Id == 0 ? db_.tbl_Survey.Where(x => x.AssessmentId == assId && x.BatchId == model.BatchId && x.CreatedBy == partId)?.FirstOrDefault() : null;
                    if (main_tbl != null)
                    {
                        maintbl = model.Id == 0 && main_tbl.Id != 0 ? main_tbl : new tbl_Survey();
                        model.Id = maintbl.Id;
                        model.BatchId = maintbl.BatchId.Value;
                        model.FormId = maintbl.FormId.Value;
                    }
                    if (maintbl != null)
                    {
                        maintbl.FormId = Convert.ToInt32(model.FormId);
                        if (model.Id == 0)//&& !(db_.tbl_Survey.Any(x => x.SchoolId == model.Id))
                        {
                            var PartUserId = Session["PartUserId"].ToString();
                            model.BatchId = Convert.ToInt32(Session["BatchId"].ToString());
                            if (db_.tbl_Survey.Any(x => x.CreatedBy == PartUserId && x.FormId == model.FormId && x.BatchId == model.BatchId && x.IsActive == true))
                            {
                                return Json(new { IsSuccess = false, res = "", msg = "This Record Is Already Exists !" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                maintbl.IsDraft = model.IsDraft;
                                if (model.IsDraft && model.IsDraftMode == 1)
                                {
                                    maintbl.IsActive = false;
                                    maintbl.IsDraft = true;
                                }
                                else if (!model.IsDraft && model.IsDraftMode == 0)
                                {
                                    maintbl.IsDraft = false;
                                    maintbl.IsActive = true;

                                }
                                if (!db_.tbl_Survey.Any(x => x.CreatedBy == PartUserId && x.FormId == model.FormId && x.BatchId == model.BatchId))
                                {
                                    maintbl.YearId = model.YearId == null ? DateTime.Now.Year : Convert.ToInt32(model.YearId);
                                    maintbl.FrequencyId = model.FrequencyId == null ? DateTime.Now.Month : Convert.ToInt32(model.FrequencyId);
                                    maintbl.Date = DateTime.Now.Date;
                                    maintbl.AssessmentId = Guid.Parse(Session["AssessmentScheduleId_fk"].ToString());
                                    maintbl.BatchId = model.BatchId;
                                    maintbl.ParticipantId = Guid.Parse(Session["PartUserId"].ToString());
                                    maintbl.Lat = model.Lat;
                                    maintbl.Long = model.Long;
                                    maintbl.TrainingCenterId = model.TrainingCenterId;
                                    maintbl.CreatedBy = Session["PartUserId"].ToString();
                                    maintbl.CreatedOn = DateTime.Now;
                                    maintbl.IsDeleted = false;

                                    if (model.TimeOut == 1)
                                    {
                                        maintbl.IsTimeOut = true;
                                        maintbl.IsActive = true;

                                    }

                                    //maintbl.SurveyStartTime = "";
                                    // maintbl.SurveyEndTime = "";


                                    maintbl.LocalIP = localIP;
                                    maintbl.PublicIP = publicIP;
                                    maintbl.ConLocalIP = localIP;
                                    maintbl.ConPublicIP = publicIP;

                                    db_.tbl_Survey.Add(maintbl);


                                }
                            }
                        }
                        else if (model.Id > 0)
                        {
                            maintbl.UpdatedBy = Session["PartUserId"].ToString();
                            maintbl.UpdatedOn = DateTime.Now;
                            maintbl.IsDraft = model.IsDraft;

                            maintbl.ConLocalIP = maintbl.LocalIP == localIP ? localIP : maintbl.ConLocalIP + "@/" + localIP;
                            maintbl.ConPublicIP = maintbl.ConPublicIP == publicIP ? publicIP : maintbl.ConPublicIP + "@/" + publicIP;
                            maintbl.LocalIP = localIP;
                            maintbl.PublicIP = publicIP;

                            if (!model.IsDraft && model.IsDraftMode == 0)
                            {
                                maintbl.IsActive = true;
                            }
                            if (model.TimeOut == 1)
                            {
                                maintbl.IsTimeOut = true;
                                maintbl.IsActive = true;

                            }
                            //maintbl.Lat = model.Lat;
                            //maintbl.Long = model.Long;
                        }
                        result += db_.SaveChanges();

                        if (model.Qlist != null && maintbl.Id > 0)
                        {
                            var mid = maintbl.Id;
                            var tblHRAnsMain = db_.tbl_SurveyAnswer.Where(x => x.SId_fk == maintbl.Id).ToList();
                            foreach (var item in model.Qlist.ToList())
                            {
                                if (item.OptionList != null)
                                {
                                    var asnlist = tblHRAnsMain.Where(x => x.QuestionId_fk == item.QuestionId_pk && x.SId_fk == maintbl.Id).ToList();
                                    for (int i = 0; i < item.OptionList.Count; i++)
                                    {
                                        var isSaveChild = false;
                                        var Resid = item.OptionList[i].Id;
                                        var IsMainRes = false;
                                        if (Resid != 0 && mid != 0)
                                        {
                                            IsMainRes = true;
                                        }
                                        var Resptbl = IsMainRes == true ? tblHRAnsMain.Where(c => c.SId_fk == mid && c.Id == Resid)?.FirstOrDefault() : new tbl_SurveyAnswer();
                                        if (Resptbl != null)
                                        {
                                            Resptbl.Id = item.OptionList[i].Id;

                                            Resptbl.SId_fk = mid;
                                            Resptbl.QuestionId_fk = item.QuestionId_pk;
                                            Resptbl.QuestionCode = item.QuestionCode;
                                            Resptbl.QuestionOption_fk = item.OptionList[i].OptionId_Pk;

                                            if (item.OptionList[i].SelectedItem == true && item.ControlType.ToLower() == "checkbox")
                                            {
                                                Resptbl.ResponseCode = item.OptionList[i].Value;
                                                Resptbl.InputValue = item.OptionList[i].InputText;
                                                Resptbl.InputValue1 = item.OptionList[i].InputText1;
                                                isSaveChild = true;
                                            }
                                            if (!string.IsNullOrWhiteSpace(item.OptionList[i].AnswerValue) && item.OptionList[i].AnswerValue != "0" && item.ControlType.ToLower() == "radio")
                                            {
                                                Resptbl.ResponseCode = item.OptionList[i].AnswerValue;
                                                //Resptbl.InputValue = item.OptionList[i].AnswerValue;
                                                //Resptbl.InputValue1 = item.OptionList[i].AnswerValue;
                                                Resptbl.InputValue = item.OptionList[i].InputText;
                                                isSaveChild = true;
                                            }
                                            else if (!string.IsNullOrWhiteSpace(item.Answer) && item.ControlType.ToLower() == "radiobutton")
                                            {
                                                if (item.Answer == item.OptionList[i].Value)
                                                {
                                                    Resptbl.ResponseCode = item.Answer;
                                                    Resptbl.InputValue = item.OptionList[i].InputText;
                                                    isSaveChild = true;
                                                }
                                                else
                                                {
                                                    var ans = asnlist.FirstOrDefault(x => x.QuestionCode == item.QuestionCode && x.QuestionOption_fk == item.OptionList[i].OptionId_Pk);
                                                    if (ans != null)
                                                    {
                                                        db_.tbl_SurveyAnswer.Remove(ans);
                                                        db_.SaveChanges();
                                                    }
                                                }
                                            }
                                            else if (item.OptionList[i].SelectedItem != true && item.ControlType.ToLower() == "textbox")
                                            {
                                                if (!string.IsNullOrWhiteSpace(item.OptionList[i].InputText))
                                                {
                                                    Resptbl.ResponseCode = item.OptionList[i].Value;
                                                    Resptbl.InputValue = item.OptionList[i].InputText;
                                                    isSaveChild = true;
                                                }
                                                else
                                                {
                                                    var ans = asnlist.FirstOrDefault(x => x.QuestionCode == item.QuestionCode && x.QuestionOption_fk == item.OptionList[i].OptionId_Pk);
                                                    if (ans != null)
                                                    {
                                                        db_.tbl_SurveyAnswer.Remove(ans);
                                                        db_.SaveChanges();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var ans = asnlist.FirstOrDefault(x => x.QuestionCode == item.QuestionCode && x.QuestionOption_fk == item.OptionList[i].OptionId_Pk);
                                                if (ans != null)
                                                {
                                                    db_.tbl_SurveyAnswer.Remove(ans);
                                                    db_.SaveChanges();
                                                }
                                            }

                                            if (Resptbl.Id == 0 && isSaveChild)
                                            {
                                                var partid = Session["PartUserId"].ToString();
                                                if (!tblHRAnsMain.Any(x => x.CreatedBy == partid
                                                && x.SId_fk == maintbl.Id
                                                && x.QuestionId_fk == item.QuestionId_pk
                                                && x.QuestionOption_fk == item.OptionList[i].OptionId_Pk))
                                                {
                                                    Resptbl.PartId_fk = Guid.Parse(Session["PartUserId"].ToString());
                                                    Resptbl.CreatedBy = Session["PartUserId"].ToString();
                                                    Resptbl.CreatedOn = DateTime.Now;
                                                    Resptbl.IsActive = maintbl.IsActive;
                                                    Resptbl.IsDeleted = false;
                                                    db_.tbl_SurveyAnswer.Add(Resptbl);
                                                    result += db_.SaveChanges();
                                                }
                                            }
                                            else
                                            {
                                                Resptbl.IsActive = maintbl.IsActive;
                                                Resptbl.UpdatedOn = DateTime.Now;
                                                result += db_.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (result > 0)
                    {
                        //Success($"{action} successfully!", true);
                        // return RedirectToAction("Add", "Infra");
                        //Success("HR save and modified successfully !", true);
                        // return RedirectToAction("Index", "HR");
                        if (maintbl.IsTimeOut == true || maintbl.IsActive == true)
                        {
                            var assessmentsendid = Guid.Parse(Session["AssessmentSendLinkPartId_pk"].ToString());
                            var tbl2nd = db_.tbl_AssessmentSendLinkEmail.Find(assessmentsendid);
                            var assessmentid = Guid.Parse(Session["AssessmentScheduleId_fk"].ToString());
                            var tbls = db_.tbl_AssessmentSchedule.Find(assessmentid);
                            tbls.AssessmentSchedule = true;
                            tbls.UpdatedOn = DateTime.Now;
                            tbl2nd.AssessmentSchedule = true;
                            tbl2nd.UpdatedOn = DateTime.Now;
                            db_.SaveChanges();
                        }

                        ModelState.Clear();
                        model.Id = maintbl.Id;
                        model.BatchId = maintbl.BatchId.Value;
                        model.FormId = maintbl.FormId.Value;
                        model.TrainingCenterId = maintbl.TrainingCenterId.Value;
                        model.RandomValue = model.RandomValue;
                        var res = GetAdd(maintbl.Id, maintbl.FormId.Value, model.RandomValue);
                        var html = ConvertViewToString("add", res);
                        var action = maintbl.Id == 0 ? "Saved" : "Modified";
                        var msg = model.IsDraft ? "Save in draft" : "Final Submit Successfully";
                        var resResponse = Json(new { IsSuccess = true, htmlData = html, msg = action }, JsonRequestBehavior.AllowGet);
                        resResponse.MaxJsonLength = int.MaxValue;
                        Session["SurveyId"] = maintbl.Id;
                        Session["FormId"] = maintbl.FormId.Value;
                        Session["BatchId"] = maintbl.BatchId;
                        Session["Assessement_Id"] = maintbl.AssessmentId;



                        GetParticipantSaveUpdateScore();
                        return resResponse;
                        //Success("HR save and modified successfully !", true);
                        // return Json(new { IsSuccess = true, res = html, MSG = "HR save and modified successfully !" }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { IsSuccess = false, htmlData = "Something went wrong.", msg = "Something went wrong." }, JsonRequestBehavior.AllowGet);

                //if (result > 0)
                //{
                //    Success("HR save and modified successfully !", true);
                //    return RedirectToAction("Add", "HR");
                //}
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                //Danger("Something went wrong..", true);
                return Json(new { IsSuccess = false, htmlData = "Something went wrong.", msg = "Something went wrong." }, JsonRequestBehavior.AllowGet);
            }
            // return View(model);
        }

        private int GetParticipantSaveUpdateScore()
        {
            int reslt = 0;
            Hunar_DBEntities dBEntities = new Hunar_DBEntities();
            CertificateModel model = new CertificateModel();
            string bodydata = string.Empty;
            if (Session["SurveyId"] != null && Session["SurveyId"].ToString() != "")
            {
                DataSet ds = SPManager.GetSP_ScorersSummaryMarks(Session["PartUserId"].ToString(), Convert.ToInt32(Session["FormId"].ToString()), Convert.ToInt32(Session["BatchId"].ToString()));
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        var bid = Convert.ToInt32(Session["BatchId"]);
                        var btch = dBEntities.Batch_Master.Find(bid);
                        if (btch != null)
                        {
                            if (btch.IsAssessmentDone != true)
                            {
                                btch.IsAssessmentDone = true;
                                dBEntities.SaveChanges();
                            }
                        }
                        var IsCertf = dt.Rows[0]["IsCertificate"].ToString();
                        model.IsCertificate = IsCertf;
                        model.ScorePercentage = dt.Rows[0]["Percentage"].ToString();
                        //if (dt.Rows[0]["IsCertificate"].ToString() == "1")
                        //{
                        model.BatchId = bid;
                        model.FormId = Convert.ToInt32(Session["FormId"]);
                        model.ParticipantId = Guid.Parse(Session["PartUserId"].ToString());
                        var tblget = dBEntities.tbl_Survey.Where(x => x.CreatedBy == model.ParticipantId.ToString() && x.BatchId == bid && x.FormId == model.FormId).ToList();
                        if (tblget.Any())
                        {
                            var tblu = tblget.FirstOrDefault();
                            tblu.AssessmentScoreNo = !string.IsNullOrWhiteSpace(model.ScorePercentage) ? Convert.ToDecimal(model.ScorePercentage) : 0;
                            tblu.IsCertificate = model.IsCertificate == "1" ? true : false;

                            var tblpartu = dBEntities.tbl_Participant.Find(model.ParticipantId);
                            tblpartu.AssessmentScore = model.ScorePercentage;
                            tblpartu.IsAssessmentDone = model.IsCertificate == "1" ? true : false;
                            reslt = dBEntities.SaveChanges();
                            return reslt;
                        }
                        //}
                    }
                }
            }
            return reslt;
        }
        public ActionResult GetViewData(int? Id)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            var html = "";
            try
            {
                var tblhr = db_.tbl_Survey.Find(Id);
                bool IsCheck = false;
                if (tblhr != null && tblhr.Id > 0)
                {
                    FormModel model;
                    List<FormModel> modellist = new List<FormModel>();

                    var tblhrAns = db_.tbl_SurveyAnswer.Where(x => x.SId_fk == Id).ToList();
                    var fid = Convert.ToInt32(1);
                    var tbl = db_.QuestionBooks.Where(x => x.IsActive == true && x.FormId_fk == fid).OrderBy(x => x.QuestionCode).ToList();
                    if (tbl != null)
                    {
                        foreach (var item in tbl.ToList())
                        {
                            model = new FormModel();
                            model.OptionList = new List<QuestOption>();
                            //model.OptionList.Clear();
                            model.QuestionId_pk = item.Id;
                            model.QuestionCode = item.QuestionCode;
                            model.Question = item.Question;
                            model.HindiQuestion = item.HindiQuestion;
                            model.ControlType = item.ControlType;
                            model.ParentQuestionCode = item.ParentQuestionCode;
                            model.DependedAnswer = item.DependedAnswer;
                            model.SectionType = item.SectionType;
                            var tbllist = db_.QuestionOptions.Where(x => x.QuestionCode == item.QuestionCode).OrderBy(x => x.QuestionCode).ToList();
                            if (tbllist != null)
                            {
                                for (int i = 0; i < tbllist.Count; i++)
                                {
                                    if (tblhrAns != null)
                                    {
                                        var ans = tblhrAns.FirstOrDefault(x => x.QuestionCode == tbllist[i].QuestionCode && x.QuestionOption_fk == tbllist[i].Id);
                                        if (ans != null && ans.Id > 0)
                                        {
                                            model.OptionList.Add(new QuestOption
                                            {
                                                Id = ans == null ? 0 : ans.Id,
                                                OptionId_Pk = tbllist[i].Id,
                                                QuestionId_fk = tbllist[i].QuestionId_fk.Value,
                                                QuestionCode = tbllist[i].QuestionCode,
                                                ControlInputType = tbllist[i].ControlInputType,
                                                OptionTypeValidation = tbllist[i].OptionTypeValidation,
                                                Limit = tbllist[i].Limit,
                                                Text = tbllist[i].OptionText,
                                                HindiOptionText = tbllist[i].HindiOptionText,
                                                Value = tbllist[i].OptionCode,
                                                InputText = ans?.InputValue,
                                                InputText1 = ans?.InputValue1,

                                                LabelName1 = tbllist[i].LabelName1,
                                                LabelName2 = tbllist[i].LabelName2,
                                                SelectedItem = ans != null
                                            });
                                            if (ans != null)
                                            {
                                                model.Answer = tbllist[i].OptionCode;
                                            }
                                        }
                                    }
                                }
                            }
                            modellist.Add(model);
                        }
                    }
                    var reslist = BuildQuestion(modellist);
                    if (tblhr != null)
                    {
                        if (tblhr.Id > 0)
                        {
                            var m1 = new QesRes { Id = tblhr.Id, FrequencyId = tblhr.FrequencyId, YearId = tblhr.YearId, Qlist = reslist };
                            // var m = new QesRes { Qlist = reslist };
                            IsCheck = true;
                            html = ConvertViewToString("_ViewData", m1);
                            var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                            res.MaxJsonLength = int.MaxValue;
                            return res;
                        }
                    }
                    var res1 = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                else
                {
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }


        #region Recursion
        public static List<FormModel> BuildQuestion(List<FormModel> source)
        {
            var groups = source.GroupBy(i => i.ParentQuestionCode);

            var roots = groups.FirstOrDefault(g => string.IsNullOrWhiteSpace(g.Key)).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => !string.IsNullOrWhiteSpace(g.Key)).ToDictionary(g => g.Key, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChild(roots[i], dict);
            }

            return roots;
        }

        private static void AddChild(FormModel node, Dictionary<string, List<FormModel>> source)
        {
            if (source.ContainsKey(node.QuestionCode))
            {
                node.ChildQuestionList = source[node.QuestionCode];
                for (int i = 0; i < node.ChildQuestionList.Count; i++)
                    AddChild(node.ChildQuestionList[i], source);
            }
            else
            {
                node.ChildQuestionList = new List<FormModel>();
            }
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
        #endregion

    }
}