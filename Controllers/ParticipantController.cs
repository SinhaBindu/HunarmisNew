﻿using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Hunarmis.Helpers;
using Hunarmis.Manager;
using Hunarmis.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
//using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using static Hunarmis.Manager.Enums;

namespace Hunarmis.Controllers
{
    [Authorize]
    [SessionCheck]
    public class ParticipantController : Controller
    {
        Hunar_DBEntities db = new Hunar_DBEntities();
        int results = 0; int results2nd = 0;
        public ActionResult Index()
        {
            //var physicalFilePath = Server.MapPath("~/upload/template.xlsx");
            //ExcelUtility excelut = new ExcelUtility();
            //DataTable dt = excelut.GetData(physicalFilePath);
            return View();
        }

        #region Dashboard and Add Participant
        public ActionResult ParticipantProfile()
        {
            return View();
        }
        public ActionResult ParticipantProfile2()
        {
            return View();
        }
        public ActionResult ParticipantProfile3()
        {
            return View();
        }
        public ActionResult ParticipantProfile4()
        {
            return View();
        }
        public ActionResult ParticipantProfile5()
        {
            return View();
        }
        public ActionResult GetDashboard(int mode)
        {
            bool IsCheck = false;
            try
            {
                DataTable dt = SPManager.SP_Dashboard_Graphs(mode);
                if (dt.Rows.Count > 0)
                {
                    IsCheck = true;
                    var data = JsonConvert.SerializeObject(dt);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = data }, JsonRequestBehavior.AllowGet);
                    res1.MaxJsonLength = int.MaxValue;
                    return res1;
                }
                var datares = JsonConvert.SerializeObject(dt);
                var res = Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.RecordNotFound), resData = "" }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = IsCheck, Data = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult ParticipantList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPartList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_RawParticipantList(model);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_PartData", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddParticipant(Guid? Id)
        {
            ParticipantModel model = new ParticipantModel();
            if (Id != Guid.Empty && Id != null)
            {
                var tbl = db.tbl_Participant.Find(Id);
                if (tbl != null)
                {
                    model.ID = tbl.ID;
                    model.RegID = tbl.RegID;
                    model.FirstName = tbl.FirstName;
                    model.MiddleName = tbl.MiddleName;
                    model.LastName = tbl.LastName;
                    model.FullName = tbl.FullName;
                    model.Gender = tbl.Gender;
                    model.Age = tbl.Age;
                    model.AadharCardNo = tbl.AadharCardNo;
                    model.EmailID = tbl.EmailID;
                    model.PhoneNo = tbl.PhoneNo;
                    model.AlternatePhoneNo = tbl.AlternatePhoneNo;
                    model.AssessmentScore = !string.IsNullOrWhiteSpace(tbl.AssessmentScore) ? ((int)(Math.Round(Convert.ToDecimal(tbl.AssessmentScore), 2))).ToString() : null;
                    model.BatchId = tbl.BatchId;
                    model.EduQualificationID = tbl.EduQualificationID;
                    model.EduQualOther = tbl.EduQualOther;
                    model.CourseEnrolledID = tbl.CourseEnrolledID;
                    model.StateID = tbl.StateID;
                    model.DistrictID = tbl.DistrictID;
                    model.TrainingAgencyID = tbl.TrainingAgencyID;
                    model.TrainingCenterID = tbl.TrainingCenterID;
                    model.TrainerName = tbl.TrainerName;
                    model.TrainerId = tbl.TrainerId;
                    model.Is_Placed = tbl.IsPlaced == true ? "1" : "0";
                    model.Is_Offered = tbl.IsOffered == true ? "1" : "0";

                    model.MaritalStatus = tbl.MaritalStatus;
                    model.NoofFamilyMembers = tbl.NoofFamilyMembers;
                    model.AnnualHouseholdincome = tbl.AnnualHouseholdincome;
                    model.PreTrainingStatus = tbl.PreTrainingStatus;
                    model.TargetGroup = tbl.TargetGroup;

                }
            }
            return View(model);
        }
        [HttpPost]
        //[EnableCors("*")]
        public ActionResult AddParticipant(ParticipantModel model)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (ModelState.IsValid && MvcApplication.CUser != null)
                {
                    var getdt = db_.tbl_Participant.Where(x => x.IsActive == true).ToList();
                    if (getdt.Any(x => x.PhoneNo == model.PhoneNo.Trim() && model.ID == Guid.Empty))//&& x.BatchId == model.BatchId
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Already Exists Registration.<br /> <span> Reg ID : <strong> " + getdt?.FirstOrDefault().RegID + " </strong>  </span>", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                    if (getdt.Any(x => x.PhoneNo == model.PhoneNo.Trim() && x.ID != model.ID))
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Already Exists Registration.<br /> <span> Reg ID : <strong> " + getdt?.FirstOrDefault().RegID + " </strong>  </span>", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                    var tbl = model.ID != Guid.Empty ? db_.tbl_Participant.Find(model.ID) : new tbl_Participant();
                    if (tbl != null)
                    {
                        var fstname = !(string.IsNullOrWhiteSpace(model.FirstName)) ? model.FirstName.Trim() : string.Empty;
                        var mname = !(string.IsNullOrWhiteSpace(model.MiddleName)) ? " " + (model.MiddleName.Trim()) : string.Empty;
                        var lname = !(string.IsNullOrWhiteSpace(model.LastName)) ? " " + model.LastName.Trim() : string.Empty;
                        tbl.FirstName = fstname;
                        tbl.MiddleName = mname;
                        tbl.LastName = lname;
                        tbl.FullName = fstname + mname + lname;
                        //l.FirstName + tbl.MiddleName + tbl.LastName;
                        //  tbl.StateId = db.State_Mast.Where(x => x.ID == sid).FirstOrDefault().ID.ToString();
                        tbl.Gender = !(string.IsNullOrWhiteSpace(model.Gender)) ? model.Gender.Trim() : model.Gender;
                        tbl.Age = !(string.IsNullOrWhiteSpace(model.Age)) ? model.Age.Trim() : model.Age;
                        tbl.PhoneNo = !(string.IsNullOrWhiteSpace(model.PhoneNo)) ? model.PhoneNo.Trim() : model.PhoneNo;
                        tbl.AlternatePhoneNo = !(string.IsNullOrWhiteSpace(model.AlternatePhoneNo)) ? model.AlternatePhoneNo.Trim() : model.AlternatePhoneNo;
                        tbl.EmailID = !(string.IsNullOrWhiteSpace(model.EmailID)) ? model.EmailID.Trim() : model.EmailID;
                        tbl.AadharCardNo = !(string.IsNullOrWhiteSpace(model.AadharCardNo)) ? model.AadharCardNo.Trim() : model.AadharCardNo;
                        tbl.AssessmentScore = !(string.IsNullOrWhiteSpace(model.AssessmentScore)) ? model.AssessmentScore.Trim() : model.AssessmentScore;
                        tbl.BatchId = model.BatchId;
                        tbl.EduQualificationID = model.EduQualificationID;
                        tbl.EduQualOther = model.EduQualificationID == 4 ? model.EduQualOther : null;
                        tbl.CourseEnrolledID = model.CourseEnrolledID;
                        tbl.StateID = model.StateID;
                        tbl.DistrictID = model.DistrictID;
                        tbl.TrainingAgencyID = model.TrainingAgencyID;
                        tbl.TrainingCenterID = model.TrainingCenterID;
                        tbl.TrainerName = !(string.IsNullOrWhiteSpace(model.TrainerName)) ? model.TrainerName.Trim() : model.TrainerName;
                        tbl.TrainerId = model.TrainerId != Guid.Empty ? model.TrainerId : null;
                        tbl.IsActive = true;
                        tbl.IsPlaced = model.Is_Placed == "1" ? true : false;
                        tbl.IsOffered = model.Is_Offered == "1" ? true : false;

                        tbl.MaritalStatus = model.MaritalStatus;
                        tbl.NoofFamilyMembers = model.NoofFamilyMembers;
                        tbl.AnnualHouseholdincome = model.AnnualHouseholdincome;
                        tbl.PreTrainingStatus = model.PreTrainingStatus;
                        tbl.TargetGroup = model.TargetGroup;

                        if (model.ID == Guid.Empty)
                        {
                            tbl.CreatedBy = MvcApplication.CUser.UserId;
                            tbl.ID = Guid.NewGuid();
                            tbl.CreatedOn = DateTime.Now;
                            tbl.CallTempStatus = (int)Enums.eTempCallStatus.CallNot;
                            tbl.CallTempStatusDate = DateTime.Now;
                            tbl.CallTempReportedBy = MvcApplication.CUser.UserId;
                            //tbl.FixedCallLimitMonth = model.IsPlaced == true ? (int)Enums.eCallLimitDuration.FixedCallLimit : 0;
                            // tbl.AtPresentCallStatus = model.IsPlaced == true ? 0 : 0;
                            tbl.FixedCallLimitMonth = (int)Enums.eCallLimitDuration.FixedCallLimit;
                            tbl.AtPresentCallStatus = 0;

                            db_.tbl_Participant.Add(tbl);
                        }
                        else
                        {
                            tbl.UpdatedBy = MvcApplication.CUser.UserId;
                            tbl.UpdatedOn = DateTime.Now;
                        }
                        results = db_.SaveChanges();
                    }
                    if (results > 0)
                    {
                        // var taskres = CU_RegLogin(model, tbl.ID);
                        var reg_id = db_.tbl_Participant.Where(x => x.PhoneNo == model.PhoneNo.Trim() && x.EmailID == model.EmailID.Trim())?.FirstOrDefault().RegID; //if (case_id != null) { }
                        if (model.ID != Guid.Empty)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + reg_id + " </strong> </span>", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully registered! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + reg_id + " </strong> </span>", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !!", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                };
            }
            catch (Exception ex)
            {
                CommonModel.ExpSubmit("tbl_Participant", "Participant", "AddParticipant", "AddParticipant", ex.Message);
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
            return View();
        }
        #endregion

        #region Placement Tracker 
        public ActionResult AddPlacementTracker(Guid PartId, Guid? Id)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            PlacementTracker_Model model = new PlacementTracker_Model();
            if (PartId != Guid.Empty && PartId != null)
            {
                var tbl = db_.tbl_Participant.Find(PartId);
                if (tbl != null)
                {
                    model.ParticipantId_fk = tbl.ID;
                    model.modelbasicpart.RegID = tbl.RegID;
                    model.modelbasicpart.FullName = tbl.FullName;
                    model.modelbasicpart.PhoneNo = tbl.PhoneNo;
                    var batch = db_.Batch_Master.Where(x => x.Id == tbl.BatchId)?.FirstOrDefault();
                    model.modelbasicpart.BatchName = batch != null ? batch.BatchName : null;
                    model.modelbasicpart.SBatchDt = batch != null ? CommonModel.FormateDtDMY(batch.BatchStartDate.ToString()) : null;
                    model.modelbasicpart.EBatchDt = batch != null ? CommonModel.FormateDtDMY(batch.BatchEndDate.ToString()) : null;
                    var TrainAcy = db_.TrainingAgency_Master.Where(x => x.Id == tbl.TrainingAgencyID)?.FirstOrDefault();
                    var TrainCen = db_.TrainingCenter_Master.Where(x => x.Id == tbl.TrainingCenterID
                    && x.TrainingAgencyID_fk == tbl.TrainingAgencyID && x.DistrictID_fk == tbl.DistrictID)?.FirstOrDefault();
                    model.modelbasicpart.TrainingAgencyName = TrainAcy != null ? TrainAcy.TrainingAgencyName : null;
                    model.modelbasicpart.TrainingCenter = TrainCen != null ? TrainCen.TrainingCenter : null;
                    var Cname = db_.Courses_Master.Where(x => x.Id == tbl.CourseEnrolledID)?.FirstOrDefault();
                    model.modelbasicpart.CourseName = Cname != null ? Cname.CourseName : null;

                    var tblPlaceTrackerId = db_.tbl_PlacementTracker.Find(Id);
                    if (tblPlaceTrackerId != null && Id != Guid.Empty)
                    {
                        model.ParticipantId_fk = tblPlaceTrackerId.ParticipantId_fk;
                        model.PlacementTrackerId_pk = tblPlaceTrackerId.PlacementTrackerId_pk;

                        model.EmployeeTypeId = tblPlaceTrackerId.EmployeeTypeId;
                        model.IndustryId = tblPlaceTrackerId.IndustryId;
                        model.CompanyName = tblPlaceTrackerId.CompanyName;
                        model.Designation = tblPlaceTrackerId.Designation;
                        model.Salary = tblPlaceTrackerId.Salary;
                        model.StateId = tblPlaceTrackerId.StateId;
                        model.DistrictId = tblPlaceTrackerId.DistrictId;
                        model.PinCode = tblPlaceTrackerId.PinCode;
                        model.DateofOffer = tblPlaceTrackerId.DateofOffer;
                        model.DOJStartDate = tblPlaceTrackerId.DOJStartDate;
                        model.DOJEndDate = tblPlaceTrackerId.DOJEndDate;

                        model.FilterModel.DistrictId = tblPlaceTrackerId.DistrictId.ToString();
                    }

                }
            }
            return PartialView("_AddPlacementTracker", model);
        }
        public ActionResult PlacementTrackerDetail(FilterModel filtermodel)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            DataTable dt = new DataTable();
            if (!string.IsNullOrWhiteSpace(filtermodel.ParticipantId) && !string.IsNullOrWhiteSpace(filtermodel.PlacementTrackerId))
            {
                dt = SPManager.SP_PlacementTrackerDetail(filtermodel);
            }
            return PartialView("_ViewPlacementTracker", dt);
        }
        [HttpPost]
        public ActionResult PostPlacementTracker(PlacementTracker_Model model)
        {
            JsonResponseData response = new JsonResponseData();
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            try
            {
                if (model.DateofOffer > model.DOJStartDate)
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Date of offer greater than date of joining.", Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                if (model.DOJEndDate != null)
                {
                    if (model.DateofOffer > model.DOJEndDate)
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Date of offer greater than date of joining end date.", Data = null };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                    }
                }
                if (model.DOJStartDate != null)
                {
                    if (model.DateofOffer >= model.DOJStartDate)
                    {
                        response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Date of offer greater than date of joining start date.", Data = null };
                        var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse3.MaxJsonLength = int.MaxValue;
                        return resResponse3;
                    }
                    if (model.DOJStartDate != null && model.DOJEndDate != null)
                    {
                        if (model.DOJStartDate > model.DOJEndDate)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Date of offer greater than date of joining start date.", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                    }
                }

                var tbl = (model.ParticipantId_fk != Guid.Empty && model.PlacementTrackerId_pk != Guid.Empty) ? db_.tbl_PlacementTracker.Find(model.PlacementTrackerId_pk) : new tbl_PlacementTracker();
                if (tbl != null && MvcApplication.CUser != null)
                {
                    if (model.ParticipantId_fk != Guid.Empty)
                    {
                        tbl.EmployeeTypeId = model.EmployeeTypeId;
                        tbl.IndustryId = model.IndustryId;
                        tbl.CompanyName = !string.IsNullOrWhiteSpace(model.CompanyName) ? model.CompanyName.Trim().Replace("+", " ") : null;
                        tbl.Designation = !string.IsNullOrWhiteSpace(model.Designation) ? model.Designation.Trim().Replace("+", " ") : null;
                        tbl.Salary = model.Salary;
                        tbl.StateId = model.StateId;
                        tbl.DistrictId = model.DistrictId;
                        tbl.PinCode = model.PinCode;
                        tbl.DateofOffer = model.DateofOffer;
                        tbl.DOJStartDate = model.DOJStartDate;
                        tbl.DOJEndDate = model.DOJEndDate;

                        if (model.PlacementTrackerId_pk == Guid.Empty)
                        {
                            tbl.PlacementTrackerId_pk = Guid.NewGuid();
                        }
                        if (model.UploadOfferLetter != null)
                        {
                            var filePathUOL = CommonModel.SaveSingleFile(model.UploadOfferLetter, tbl.PlacementTrackerId_pk.ToString(), "UploadOfferLetter");
                            var physicalFilePathhUOL = Server.MapPath(filePathUOL);
                            tbl.UploadOfferLetterPath = filePathUOL;
                        }
                        if (model.UploadAppointmentLetter != null)
                        {
                            var filePathUAL = CommonModel.SaveSingleFile(model.UploadAppointmentLetter, tbl.PlacementTrackerId_pk.ToString(), "UploadAppointmentLetter");
                            var physicalFilePathhUAL = Server.MapPath(filePathUAL);
                            tbl.UploadAppointmentLetterPath = filePathUAL;
                        }

                        if (model.PlacementTrackerId_pk == Guid.Empty)
                        {
                            tbl.ParticipantId_fk = model.ParticipantId_fk;
                            tbl.IsActive = true;
                            tbl.CreatedBy = MvcApplication.CUser.UserId;
                            tbl.CreatedOn = DateTime.Now;
                            db_.tbl_PlacementTracker.Add(tbl);
                        }
                        else
                        {
                            tbl.UpdatedBy = MvcApplication.CUser.UserId;
                            tbl.UpdatedOn = DateTime.Now;
                        }
                        results = db_.SaveChanges();
                        if (results > 0)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Submit successfully ! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + model.modelbasicpart.RegID + " </strong> </span>", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonModel.ExpSubmit("tbl_PlacementTracker", "Participant", "AddPlacementTracker", "AddPlacementTracker", ex.Message);
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError), Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired), Data = null };
            var resResponsejs = Json(response, JsonRequestBehavior.AllowGet);
            resResponsejs.MaxJsonLength = int.MaxValue;
            return resResponsejs;
        }
        #endregion

        #region Calling Method Post Data
        public ActionResult GetTempStatus()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = SPManager.SP_PartTempStatus();
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                }
                var resdata = JsonConvert.SerializeObject(tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = resdata }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult ParticipantCallList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPartCallList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_ParticipantList(model);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_PartCallData", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
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
                return Json(new { IsSuccess = false, Data = "There are communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult AddPartCalling(Guid? Id, Guid? PartId)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            ParticipantChildModel model = new ParticipantChildModel();
            if (PartId != Guid.Empty && PartId != null)
            {
                if (PartId != Guid.Empty)
                {
                    var tbl = db_.tbl_Participant.Find(PartId);
                    if (tbl != null)
                    {
                        tbl.CallTempStatus = (int)Enums.eTempCallStatus.CallOnProgress;
                        tbl.CallTempStatusDate = DateTime.Now;
                        tbl.CallTempReportedBy = MvcApplication.CUser.UserId;
                        int r = db_.SaveChanges();
                    }
                }
                model.ParticipantId_fk = PartId;
                var getdt = db_.tbl_Participant.Where(x => x.IsActive == true && x.ID == PartId)?.FirstOrDefault();
                if (getdt != null)
                {
                    var tblremakHist = db_.tbl_ParticipantRemarks_History.Where(x => x.ID == PartId).OrderByDescending(x => x.RemarkDate).Take(1)?.FirstOrDefault();
                    model.RegID = getdt.RegID;
                    model.FullName = !string.IsNullOrEmpty(getdt.FullName) ? getdt.FullName : getdt.FirstName + " " + getdt.MiddleName + " " + getdt.LastName;
                    model.PhoneNo = getdt.PhoneNo;
                    var batch = db_.Batch_Master.Where(x => x.Id == getdt.BatchId)?.FirstOrDefault();
                    model.BatchName = batch != null ? batch.BatchName : null;
                    model.SBatchDt = batch != null ? CommonModel.FormateDtDMY(batch.BatchStartDate.ToString()) : null;
                    model.EBatchDt = batch != null ? CommonModel.FormateDtDMY(batch.BatchEndDate.ToString()) : null;
                    var TrainAcy = db_.TrainingAgency_Master.Where(x => x.Id == getdt.TrainingAgencyID)?.FirstOrDefault();
                    var TrainCen = db_.TrainingCenter_Master.Where(x => x.Id == getdt.TrainingCenterID
                    && x.TrainingAgencyID_fk == getdt.TrainingAgencyID && x.DistrictID_fk == getdt.DistrictID)?.FirstOrDefault();
                    model.TrainingAgencyName = TrainAcy != null ? TrainAcy.TrainingAgencyName : null;
                    model.TrainingCenter = TrainCen != null ? TrainCen.TrainingCenter : null;
                    if (tblremakHist != null)
                    {
                        model.PrvRemark = tblremakHist.CallTempStatus != null ? tblremakHist.CallRemark : null;//CallTempStatus == 2
                        model.PrvRemarkStatus = tblremakHist.CallTempStatus != null ? tblremakHist.RemarkStatus : null;//CallTempStatus == 2
                    }
                    else
                    {
                        model.PrvRemark = getdt.CallTempStatus != null ? getdt.CallRemark : null;//CallTempStatus == 2
                        model.PrvRemarkStatus = getdt.CallTempStatus != null ? getdt.RemarkStatus : null;//CallTempStatus == 2
                    }
                    var AspUser = db_.AspNetUsers.Where(x => x.Id == getdt.CreatedBy)?.FirstOrDefault();
                    model.ReportedBy = AspUser != null ? AspUser.EmpName : null;
                    model.ReportedOn = AspUser != null ? CommonModel.FormateDtDMY(getdt.RemarkDate.ToString()) : null;

                }
            }
            if (Id != Guid.Empty && Id != null)
            {
            }
            return View(model);
        }
        private int GetUpdateRemarks(tbl_Participant tbl, Guid guid)
        {
            Hunar_DBEntities dBEntities = new Hunar_DBEntities();
            tbl_ParticipantRemarks_History tblRm1 = new tbl_ParticipantRemarks_History();
            int res = 0;
            if (tbl.ID != Guid.Empty && guid != Guid.Empty)
            {
                tblRm1.RemarkHistoryID = guid;
                tblRm1.ID = tbl.ID;
                tblRm1.RegID = tbl.RegID;
                tblRm1.FirstName = tbl.FirstName;
                tblRm1.MiddleName = tbl.MiddleName;
                tblRm1.LastName = tbl.LastName;
                tblRm1.FullName = tbl.FullName;
                tblRm1.Gender = tbl.Gender;
                tblRm1.Age = tbl.Age;
                tblRm1.AadharCardNo = tbl.AadharCardNo;
                tblRm1.EmailID = tbl.EmailID;
                tblRm1.PhoneNo = tbl.PhoneNo;
                tblRm1.AlternatePhoneNo = tbl.AlternatePhoneNo;
                tblRm1.AssessmentScore = tbl.AssessmentScore;
                tblRm1.BatchId = tbl.BatchId;
                tblRm1.EduQualificationID = tbl.EduQualificationID;
                tblRm1.EduQualOther = tbl.EduQualOther;
                tblRm1.CourseEnrolledID = tbl.CourseEnrolledID;
                tblRm1.StateID = tbl.StateID;
                tblRm1.DistrictID = tbl.DistrictID;
                tblRm1.TrainingAgencyID = tbl.TrainingAgencyID;
                tblRm1.TrainingCenterID = tbl.TrainingCenterID;
                tblRm1.TrainerName = tbl.TrainerName;
                tblRm1.IsOffered = tbl.IsOffered;
                tblRm1.IsPlaced = tbl.IsPlaced.Value;
                tblRm1.CreatedBy = tbl.CreatedBy;
                tblRm1.CreatedOn = tbl.CreatedOn;

                tblRm1.FixedCallLimitMonth = tbl.FixedCallLimitMonth;
                tblRm1.AtPresentCallStatus = tbl.AtPresentCallStatus;

                tblRm1.CallTempStatus = tbl.CallTempStatus;
                tblRm1.CallTempStatusDate = tbl.CallTempStatusDate;
                tblRm1.CallTempReportedBy = tbl.CallTempReportedBy;
                tblRm1.CallYear = tbl.CallYear;
                tblRm1.CallMonth = tbl.CallMonth;
                tblRm1.CallRemark = tbl.CallRemark.Trim();
                tblRm1.RemarkStatus = tbl.RemarkStatus;
                tblRm1.RemarkDate = tbl.RemarkDate;
                tblRm1.RemarksHistoryDate = DateTime.Now;
                tblRm1.RemarkReportedBy = MvcApplication.CUser.UserId;
                dBEntities.tbl_ParticipantRemarks_History.Add(tblRm1);
                res = dBEntities.SaveChanges();

            }
            return res;
        }

        [HttpPost]
        public ActionResult AddPartCalling(ParticipantChildModel model)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (MvcApplication.CUser != null)
                {
                    if (model != null)
                    {
                        var getdt = db_.tbl_Participant.Where(x => x.IsActive == true && x.ID == model.ParticipantId_fk)?.FirstOrDefault();
                        if (model.CallType == Enums.GetEnumDescription(Enums.eTypeCall.No)
                            && !string.IsNullOrWhiteSpace(model.Remark))
                        {
                            if (db_.tbl_Participant_Calling.Any(x => x.ParticipantId_fk == model.ParticipantId_fk && model.ID == Guid.Empty && (x.QuesMonth == model.QuesMonth && x.QuesYear == model.QuesYear)))
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "This Participant Is Already Exists.<br /> <span> Reg ID : <strong>" + getdt.RegID + " </strong>  </span>", Data = null };
                                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse1.MaxJsonLength = int.MaxValue;
                                return resResponse1;
                            }
                            //var tbl = new tbl_Participant_Calling();
                            if (model.ID == Guid.Empty)
                            {
                                //tbl.ID = Guid.NewGuid();
                                //tbl.QuesMonth = model.QuesMonth;
                                //tbl.QuesYear = model.QuesYear;
                                //tbl.IsCalling = model.CallType;
                                //tbl.Remark = model.Remark.Trim();
                                //tbl.IsActive = true;
                                //tbl.CreatedBy = MvcApplication.CUser.UserId;
                                //tbl.CreatedOn = DateTime.Now;
                                //db_.tbl_Participant_Calling.Add(tbl);

                                var tblpart = db_.tbl_Participant.Find(model.ParticipantId_fk);
                                tblpart.CallTempStatus = (int)Enums.eTempCallStatus.CallNotPick;
                                tblpart.CallTempReportedBy = MvcApplication.CUser.UserId;
                                tblpart.CallTempStatusDate = DateTime.Now;
                                tblpart.CallYear = model.QuesYear;
                                tblpart.CallMonth = model.QuesMonth;
                                tblpart.CallRemark = model.Remark.Trim();
                                tblpart.RemarkStatus = model.RemarkStatus;
                                tblpart.RemarkDate = DateTime.Now;
                                tblpart.RemarkReportedBy = MvcApplication.CUser.UserId;

                                results = db_.SaveChanges();

                                if (tblpart != null && results > 0)
                                {
                                    var gudidremarks = Guid.NewGuid();
                                    results = GetUpdateRemarks(tblpart, gudidremarks);
                                    tblpart.RemarksID_fk = gudidremarks;
                                    results = db_.SaveChanges();
                                }

                                response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Congratulations, you have been Submitted successfully Reg ID : <strong>" + getdt.RegID + " </strong>  </span>", Data = null };
                                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse1.MaxJsonLength = int.MaxValue;
                                return resResponse1;

                            }
                        }
                        else if (model.CallType == Enums.GetEnumDescription(Enums.eTypeCall.Yes))
                        {
                            if (ModelState.IsValid)
                            {
                                if (db_.tbl_Participant_Calling.Any(x => x.ParticipantId_fk == model.ParticipantId_fk && model.ID == Guid.Empty && (x.QuesMonth == model.QuesMonth && x.QuesYear == model.QuesYear)))
                                {
                                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "This Participant Is Already Exists.<br /> <span> Reg ID : <strong>" + getdt.RegID + " </strong>  </span>", Data = null };
                                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                                    resResponse1.MaxJsonLength = int.MaxValue;
                                    return resResponse1;
                                }
                                var tbl = model.ID != Guid.Empty ? db.tbl_Participant_Calling.Find(model.ID) : new tbl_Participant_Calling();
                                if (tbl != null)
                                {
                                    tbl.IsAssessmentCert = model.IsAssessmentCert;
                                    tbl.IsPresent = model.IsPresent;
                                    model.PlacedStatus = model.IsPresent;
                                    //tbl.IsPresent = model.IsPresent.ToString() == Enums.GetEnumDescription(Enums.eTypeCall.Yes) ? model.IsPresent : model.IsPresent;
                                    if (model.IsPresent == Enums.GetEnumDescription(Enums.eTypeCall.Yes))
                                    {
                                        tbl.IsComfortable = model.IsComfortable;
                                        tbl.EmpCompany = model.EmpCompany;
                                        tbl.FirstJobTraining = model.FirstJobTraining;
                                        tbl.DOJ = model.DOJ;
                                        tbl.CurrentEmployer = model.CurrentEmployer;
                                        tbl.Designation = model.Designation;
                                        tbl.SalaryPackage = model.SalaryPackage;
                                        tbl.CurrentlyWorking = model.CurrentlyWorking;
                                        tbl.WorkingKM = model.WorkingKM;
                                        tbl.WorkingKMOther = model.WorkingKMOther;
                                        tbl.Traininghelp = model.Traininghelp;
                                        tbl.SalaryWages = model.SalaryWages;
                                        tbl.ExpectationJobRole = model.ExpectationJobRole;
                                        tbl.WorkPlaceSafe = model.WorkPlaceSafe;
                                        tbl.IsMSBenefit = model.IsMSBenefit;
                                        tbl.MSBenefitId = model.MSBenefitId;
                                        tbl.MSOther = model.MSOther;
                                        tbl.AnyChallenges = model.AnyChallenges;
                                        tbl.AreaSupport = model.AreaSupport;
                                    }
                                    tbl.EmployedId = model.IsPresent.ToString() == Enums.GetEnumDescription(Enums.eTypeCall.Yes) ? model.EmployedId : null;
                                    tbl.EmployedOther = model.EmployedId == 3 ? model.EmployedOther : null;
                                    tbl.IsGettingjob = model.IsGettingjob;
                                    tbl.PlacedStatus = model.PlacedStatus;
                                    tbl.IsActive = true;
                                    if (model.ID == Guid.Empty)
                                    {
                                        tbl.ID = Guid.NewGuid();
                                        tbl.QuesMonth = model.QuesMonth;
                                        tbl.QuesYear = model.QuesYear;
                                        tbl.CallingDate = DateTime.Now.Date;
                                        tbl.IsCalling = Enums.GetEnumDescription(Enums.eTypeCall.Yes);
                                        tbl.ParticipantId_fk = model.ParticipantId_fk;
                                        tbl.CreatedBy = MvcApplication.CUser.UserId;
                                        tbl.CreatedOn = DateTime.Now;
                                        db_.tbl_Participant_Calling.Add(tbl);
                                        results += db_.SaveChanges();
                                        var tblpart = db_.tbl_Participant.Find(model.ParticipantId_fk);
                                        var tblcal = db_.tbl_Participant_Calling.Where(x => x.ParticipantId_fk == model.ParticipantId_fk);
                                        var noofcall = tblcal != null ? tblcal.Count() + 1 : 1;
                                        tblpart.CallTempStatus = (int)Enums.eTempCallStatus.CallDone;
                                        tblpart.CallTempReportedBy = MvcApplication.CUser.UserId;
                                        tblpart.CallTempStatusDate = tbl.CreatedOn;
                                        tblpart.IsPlaced = model.PlacedStatus == Enums.GetEnumDescription(Enums.ePlaced.Yes) ? true : false;
                                        tblpart.CallMonth = model.QuesMonth;
                                        tblpart.CallYear = model.QuesYear;
                                        tblpart.AtPresentCallStatus = noofcall;
                                        tblpart.CallingID_fk = tbl.ID;
                                        results += db_.SaveChanges();
                                    }
                                    else if (model.ID != Guid.Empty)
                                    {
                                        tbl.UpdatedBy = MvcApplication.CUser.UserId;
                                        tbl.UpdatedOn = DateTime.Now;
                                        results += db_.SaveChanges();
                                    }
                                    if (results > 0)
                                    {
                                        var reg_id = db_.tbl_Participant.Where(x => x.ID == model.ParticipantId_fk)?.FirstOrDefault().RegID; //if (case_id != null) { }
                                        if (model.ID != Guid.Empty)
                                        {
                                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + reg_id + " </strong> </span>", Data = null };
                                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                                            resResponse3.MaxJsonLength = int.MaxValue;
                                            return resResponse3;
                                        }
                                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully Questionnaire! \r\nPlease Note Your <br /> <span> Reg ID : <strong>" + reg_id + " </strong> </span>", Data = null };
                                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                                        resResponse1.MaxJsonLength = int.MaxValue;
                                        return resResponse1;
                                    }
                                }
                            }
                            else
                            {
                                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !!", Data = null };
                                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                                resResponse1.MaxJsonLength = int.MaxValue;
                                return resResponse1;
                            };
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                CommonModel.ExpSubmit("tbl_Participant_Calling", "Participant", "AddPartCalling", "AddPartCalling", ex.Message);
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !", Data = null };
            var resResponse = Json(response, JsonRequestBehavior.AllowGet);
            resResponse.MaxJsonLength = int.MaxValue;
            return resResponse;
        }
        #endregion
        public ActionResult PartQuestionList()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetPartQuestionList(FilterModel model)
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_PartQuesList(model);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_PartQuesData", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
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
        public ActionResult QuesResponse(string PartQuestId, string PartId, int M, int Y)
        {
            FilterModel model = new FilterModel();
            model.ParticipantQuestionId = PartQuestId;
            //model.YearId = Y;
            //model.MonthId = M;
            model.ParticipantId = PartId;
            DataTable dt = SPManager.SP_PartQuesList(model);
            return View(dt);
        }

        #region File Excel Upload Future 
        public ActionResult ExcelFileUpload()
        {
            FileExcelModel model = new FileExcelModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ExcelFileUpload(FileExcelModel model)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            var strmobile = "";
            var strAadharNo = "";
            var resupdated = 0;
            if (model != null)
            {
                try
                {
                    if (ModelState.IsValid && model.FileUpload != null)
                    {
                        if (db_.tbl_FileUpload.Any(x => x.FileName == model.FileName.Trim()))
                        {
                            var res = Json(new { IsSuccess = false, Message = Enums.GetEnumDescription(Enums.eReturnReg.Already) }, JsonRequestBehavior.AllowGet);
                            res.MaxJsonLength = int.MaxValue;
                            return res;
                        }
                        var maxid = db_.tbl_FileUpload.Count() != 0 ? db_.tbl_FileUpload?.Max(x => x.Id) : 0;
                        var lastaddmaxid = maxid == 0 ? 1 : maxid + 1;
                        var filePath = CommonModel.SaveSingleExcelFile(model.FileUpload, lastaddmaxid.ToString(), "ParticipantFile");
                        var physicalFilePath = Server.MapPath(filePath);
                        ExcelUtility excelut = new ExcelUtility();
                        DataTable dt = excelut.GetData(physicalFilePath);
                        if (dt.Rows.Count > 0)
                        {
                            tbl_FileUpload tbl = new tbl_FileUpload();
                            tbl.UploadDate = model.UploadDate;
                            tbl.FileName = model.FileName.Trim();
                            tbl.FilePath = filePath;
                            tbl.TotalData = dt.Rows.Count;
                            tbl.CreatedBy = MvcApplication.CUser.UserId;
                            tbl.CreatedOn = DateTime.Now;
                            tbl.IsActive = true;
                            db.tbl_FileUpload.Add(tbl);

                            List<tbl_ExcelParticipantData> listexcepart =
                                dt.AsEnumerable().Select(x => new tbl_ExcelParticipantData
                                {
                                    ID = Guid.NewGuid(),
                                    StateName = Convert.ToString(x["StateName"]),
                                    DistrictName = Convert.ToString(x["DistrictName"]),
                                    Name = Convert.ToString(x["Name"]),
                                    Gender = Convert.ToString(x["Gender"]),
                                    DateofBirth = Convert.ToString(x["DateofBirth"]),
                                    Age = Convert.ToString(x["Age"]),
                                    PhoneNo = Convert.ToString(x["PhoneNo"]),
                                    EmailID = Convert.ToString(x["EmailID"]),
                                    AadharCardNo = Convert.ToString(x["AadharCardNo"]),
                                    BatchName = Convert.ToString(x["BatchName"]),
                                    //  BatchStartDate = Convert.ToString(x["BatchStartDate"]),
                                    //  BatchEndDate = Convert.ToString(x["BatchEndDate"]),
                                    AssessmentScore = Convert.ToString(x["AssessmentScore"]),
                                    QualificationName = Convert.ToString(x["QualificationName"]),
                                    CourseName = Convert.ToString(x["CourseName"]),
                                    TrainingAgencyName = Convert.ToString(x["TrainingAgencyName"]),
                                    TrainingCenter = Convert.ToString(x["TrainingCenter"]),
                                    TrainerName = Convert.ToString(x["TrainerName"]),
                                    TrainerMobileNo = Convert.ToString(x["TrainerMobileNo"]),
                                    IsPlaced = Convert.ToString(x["IsPlaced"]),
                                    DOBDD = Convert.ToString(x["DOBDD"]),
                                    DOBMM = Convert.ToString(x["DOBMM"]),
                                    DOBYYYY = Convert.ToString(x["DOBYYYY"]),
                                    CreatedBy = MvcApplication.CUser.UserId,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true,

                                    MaritalStatus = Convert.ToString(x["MaritalStatus"]),
                                    NoofFamilyMembers = Convert.ToString(x["NoofFamilyMembers"]),
                                    AnnualHouseholdincome = !string.IsNullOrWhiteSpace(Convert.ToString(x["AnnualHouseholdincome"])) ? Convert.ToDecimal(x["AnnualHouseholdincome"]) : 0,
                                    PreTrainingStatus = Convert.ToString(x["PreTrainingStatus"]),
                                    TargetGroup = Convert.ToString(x["TargetGroup"]),

                                }).ToList();
                            db_.tbl_ExcelParticipantData.AddRange(listexcepart);
                            db_.SaveChanges();

                            /* Inserted Data */
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!string.IsNullOrWhiteSpace(dr["PhoneNo"].ToString()))
                                {
                                    var moblie = dr["PhoneNo"].ToString();
                                    var AadharCardNo = dr["AadharCardNo"].ToString();
                                    if (db_.tbl_Participant.Any(x => x.PhoneNo == moblie))
                                    {

                                        strmobile += ", " + dr["PhoneNo"].ToString();
                                        var tblu = db_.tbl_Participant.Where(x => x.PhoneNo == moblie)?.FirstOrDefault();
                                        var fulllist = dr["Name"].ToString().Split(' ');
                                        if (fulllist != null || fulllist.Length != 0)
                                        {
                                            if (fulllist.Length >= 1)
                                            {
                                                tblu.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                            }
                                            if (fulllist.Length >= 2)
                                            {
                                                tblu.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                tblu.LastName = !(string.IsNullOrWhiteSpace(fulllist[1])) ? fulllist[1].Trim().ToUpper() : null;
                                            }
                                            else if (fulllist.Length >= 3)
                                            {
                                                tblu.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                tblu.MiddleName = !(string.IsNullOrWhiteSpace(fulllist[1])) ? fulllist[1].Trim().ToUpper() : null;
                                                tblu.LastName = !(string.IsNullOrWhiteSpace(fulllist[2])) ? fulllist[2].Trim().ToUpper() : null;
                                            }
                                            tblu.FullName = !(string.IsNullOrWhiteSpace(dr["Name"].ToString())) ? dr["Name"].ToString().Trim() : null;
                                        }

                                        tblu.EmailID = !(string.IsNullOrWhiteSpace(dr["EmailID"].ToString())) ? dr["EmailID"].ToString().Trim() : null;

                                        tblu.Gender = !(string.IsNullOrWhiteSpace(dr["Gender"].ToString())) ? dr["Gender"].ToString().Trim() : null;
                                        tblu.Age = !(string.IsNullOrWhiteSpace(dr["Age"].ToString())) ? dr["Age"].ToString().Trim() : null;
                                        tblu.PhoneNo = !(string.IsNullOrWhiteSpace(dr["PhoneNo"].ToString())) ? dr["PhoneNo"].ToString().Trim() : null;
                                        // tblpart.AlternatePhoneNo = !(string.IsNullOrWhiteSpace(dr["AlternatePhoneNo"].ToString())) ? dr["AlternatePhoneNo"].ToString().Trim() : null;
                                        tblu.AadharCardNo = !(string.IsNullOrWhiteSpace(dr["AadharCardNo"].ToString())) ? dr["AadharCardNo"].ToString().Trim() : null;
                                        tblu.AssessmentScore = !(string.IsNullOrWhiteSpace(dr["AssessmentScore"].ToString())) ? dr["AssessmentScore"].ToString().Trim() : null;

                                        var dobirth = !(string.IsNullOrWhiteSpace(dr["DateofBirth"].ToString())) ?
                                         dr["DOBDD"].ToString() + "-" + dr["DOBMM"].ToString() + "-" + dr["DOBYYYY"].ToString() : null;
                                        tblu.DateOfBirth = dobirth;
                                        tblu.Gender = !(string.IsNullOrWhiteSpace(dr["Gender"].ToString())) ? dr["Gender"].ToString().Trim() : null;
                                        tblu.Age = !(string.IsNullOrWhiteSpace(dr["Age"].ToString())) ? dr["Age"].ToString().Trim() : null;
                                        tblu.PhoneNo = !(string.IsNullOrWhiteSpace(dr["PhoneNo"].ToString())) ? dr["PhoneNo"].ToString().Trim() : null;
                                        // tblpart.AlternatePhoneNo = !(string.IsNullOrWhiteSpace(dr["AlternatePhoneNo"].ToString())) ? dr["AlternatePhoneNo"].ToString().Trim() : null;
                                        tblu.EmailID = !(string.IsNullOrWhiteSpace(dr["EmailID"].ToString())) ? dr["EmailID"].ToString().Trim() : null;
                                        tblu.AadharCardNo = !(string.IsNullOrWhiteSpace(dr["AadharCardNo"].ToString())) ? dr["AadharCardNo"].ToString().Trim() : null;
                                        tblu.AssessmentScore = !(string.IsNullOrWhiteSpace(dr["AssessmentScore"].ToString())) ? dr["AssessmentScore"].ToString().Trim() : null;

                                        var bName = Convert.ToString(dr["BatchName"]);
                                        var GetBatchdata = !(string.IsNullOrWhiteSpace(bName)) ? db.Batch_Master.Where(x => x.BatchName == bName.Trim()).FirstOrDefault() : null;
                                        tblu.BatchId = GetBatchdata.Id;
                                        tblu.TrainerId = GetBatchdata.TrainerId;

                                        var qName = Convert.ToString(dr["QualificationName"]);
                                        var EducationId = !(string.IsNullOrWhiteSpace(qName)) ? db.Educational_Master.Where(x => x.QualificationName == qName.Trim()).FirstOrDefault()?.Id : null;
                                        tblu.EduQualificationID = EducationId;
                                        //tblpart.EduQualOther = EducationId == 4 && !(string.IsNullOrWhiteSpace(dr["EduQualOther"].ToString())) ? dr["EduQualOther"].ToString().Trim() : "NA";
                                        tblu.EduQualOther = dr["EduQualOther"].ToString();
                                        var cName = Convert.ToString(dr["CourseName"]);
                                        var CourseEnrolledId = !(string.IsNullOrWhiteSpace(cName)) ? db.Courses_Master.Where(x => x.CourseName == cName.Trim()).FirstOrDefault()?.Id : null;
                                        tblu.CourseEnrolledID = CourseEnrolledId;

                                        var trainingAgencyName = Convert.ToString(dr["TrainingAgencyName"]);
                                        var TrainingAgencyId = !(string.IsNullOrWhiteSpace(trainingAgencyName)) ? db.TrainingAgency_Master.Where(x => x.TrainingAgencyName == trainingAgencyName.Trim()).FirstOrDefault()?.Id : null;
                                        tblu.TrainingAgencyID = TrainingAgencyId;
                                        var trainingCenter = Convert.ToString(dr["TrainingCenter"]);
                                        var TrainingCenterId = !(string.IsNullOrWhiteSpace(trainingCenter)) ? db.TrainingCenter_Master.Where(x => x.TrainingCenter == trainingCenter.Trim()).FirstOrDefault()?.Id : null;
                                        tblu.TrainingCenterID = TrainingCenterId;
                                        tblu.TrainerName = !(string.IsNullOrWhiteSpace(dr["TrainerName"].ToString())) ? dr["TrainerName"].ToString().Trim() : null;
                                        tblu.TrainerMobileNo = !(string.IsNullOrWhiteSpace(dr["TrainerMobileNo"].ToString())) ? dr["TrainerMobileNo"].ToString().Trim() : null;
                                        tblu.IsPlaced = !(string.IsNullOrWhiteSpace(dr["IsPlaced"].ToString())) && dr["IsPlaced"].ToString().ToLower() == Enums.ePlaced.Yes.ToString().ToLower() ? true : false;

                                        var MaritalStatus = Convert.ToString(dr["MaritalStatus"]);
                                        var MaritalStatusId = !(string.IsNullOrWhiteSpace(MaritalStatus)) ? CommonModel.GetMaritalStatus().Where(x => x.Text == MaritalStatus.Trim()).FirstOrDefault()?.Value : null;
                                        tblu.MaritalStatus = MaritalStatusId;
                                        tblu.NoofFamilyMembers = !(string.IsNullOrWhiteSpace(dr["NoofFamilyMembers"].ToString())) ? Convert.ToInt32(dr["NoofFamilyMembers"]) : 0;
                                        tblu.AnnualHouseholdincome = !(string.IsNullOrWhiteSpace(dr["AnnualHouseholdincome"].ToString())) ? Convert.ToDecimal(dr["AnnualHouseholdincome"]) : 0;
                                        var PreTrainingStatus = Convert.ToString(dr["PreTrainingStatus"]);
                                        var PreTrainingStatusId = !(string.IsNullOrWhiteSpace(PreTrainingStatus)) ? db_.PreTraining_Master.Where(x => x.PreTrainingName == PreTrainingStatus.Trim()).FirstOrDefault()?.PreTrainingId_pk : null;
                                        tblu.PreTrainingStatus = PreTrainingStatusId;
                                        var TargetGroup = Convert.ToString(dr["TargetGroup"]);
                                        var TargetGroupId = !(string.IsNullOrWhiteSpace(TargetGroup)) ? db_.TargetGroup_Master.Where(x => x.TargetGroupName == TargetGroup.Trim()).FirstOrDefault()?.TargetGroupId_pk : null;
                                        tblu.TargetGroup = TargetGroupId;
                                        tblu.IsPlaced = !(string.IsNullOrWhiteSpace(dr["IsPlaced"].ToString())) && dr["IsPlaced"].ToString().ToLower() == Enums.ePlaced.Yes.ToString().ToLower() ? true : false;

                                        resupdated += db_.SaveChanges();
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(AadharCardNo))
                                        {
                                            if (db_.tbl_Participant.Any(x => x.AadharCardNo == AadharCardNo))
                                            {

                                                strAadharNo += ", " + dr["AadharCardNo"].ToString();
                                                var tblu = db_.tbl_Participant.Where(x => x.AadharCardNo == AadharCardNo)?.FirstOrDefault();
                                                var fulllist = dr["Name"].ToString().Split(' ');
                                                if (fulllist != null || fulllist.Length != 0)
                                                {
                                                    if (fulllist.Length >= 1)
                                                    {
                                                        tblu.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                    }
                                                    if (fulllist.Length >= 2)
                                                    {
                                                        tblu.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                        tblu.LastName = !(string.IsNullOrWhiteSpace(fulllist[1])) ? fulllist[1].Trim().ToUpper() : null;
                                                    }
                                                    else if (fulllist.Length >= 3)
                                                    {
                                                        tblu.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                        tblu.MiddleName = !(string.IsNullOrWhiteSpace(fulllist[1])) ? fulllist[1].Trim().ToUpper() : null;
                                                        tblu.LastName = !(string.IsNullOrWhiteSpace(fulllist[2])) ? fulllist[2].Trim().ToUpper() : null;
                                                    }
                                                    tblu.FullName = !(string.IsNullOrWhiteSpace(dr["Name"].ToString())) ? dr["Name"].ToString().Trim() : null;
                                                }

                                                tblu.EmailID = !(string.IsNullOrWhiteSpace(dr["EmailID"].ToString())) ? dr["EmailID"].ToString().Trim() : null;

                                                tblu.Gender = !(string.IsNullOrWhiteSpace(dr["Gender"].ToString())) ? dr["Gender"].ToString().Trim() : null;
                                                tblu.Age = !(string.IsNullOrWhiteSpace(dr["Age"].ToString())) ? dr["Age"].ToString().Trim() : null;
                                                tblu.PhoneNo = !(string.IsNullOrWhiteSpace(dr["PhoneNo"].ToString())) ? dr["PhoneNo"].ToString().Trim() : null;
                                                // tblpart.AlternatePhoneNo = !(string.IsNullOrWhiteSpace(dr["AlternatePhoneNo"].ToString())) ? dr["AlternatePhoneNo"].ToString().Trim() : null;
                                                tblu.AadharCardNo = !(string.IsNullOrWhiteSpace(dr["AadharCardNo"].ToString())) ? dr["AadharCardNo"].ToString().Trim() : null;
                                                tblu.AssessmentScore = !(string.IsNullOrWhiteSpace(dr["AssessmentScore"].ToString())) ? dr["AssessmentScore"].ToString().Trim() : null;

                                                var dobirth = !(string.IsNullOrWhiteSpace(dr["DateofBirth"].ToString())) ?
                                                 dr["DOBDD"].ToString() + "-" + dr["DOBMM"].ToString() + "-" + dr["DOBYYYY"].ToString() : null;
                                                tblu.DateOfBirth = dobirth;
                                                tblu.Gender = !(string.IsNullOrWhiteSpace(dr["Gender"].ToString())) ? dr["Gender"].ToString().Trim() : null;
                                                tblu.Age = !(string.IsNullOrWhiteSpace(dr["Age"].ToString())) ? dr["Age"].ToString().Trim() : null;
                                                tblu.PhoneNo = !(string.IsNullOrWhiteSpace(dr["PhoneNo"].ToString())) ? dr["PhoneNo"].ToString().Trim() : null;
                                                // tblpart.AlternatePhoneNo = !(string.IsNullOrWhiteSpace(dr["AlternatePhoneNo"].ToString())) ? dr["AlternatePhoneNo"].ToString().Trim() : null;
                                                tblu.EmailID = !(string.IsNullOrWhiteSpace(dr["EmailID"].ToString())) ? dr["EmailID"].ToString().Trim() : null;
                                                tblu.AadharCardNo = !(string.IsNullOrWhiteSpace(dr["AadharCardNo"].ToString())) ? dr["AadharCardNo"].ToString().Trim() : null;
                                                tblu.AssessmentScore = !(string.IsNullOrWhiteSpace(dr["AssessmentScore"].ToString())) ? dr["AssessmentScore"].ToString().Trim() : null;

                                                var bName = Convert.ToString(dr["BatchName"]);
                                                var GetBatchdata = !(string.IsNullOrWhiteSpace(bName)) ? db.Batch_Master.Where(x => x.BatchName == bName.Trim()).FirstOrDefault() : null;
                                                tblu.BatchId = GetBatchdata.Id;
                                                tblu.TrainerId = GetBatchdata.TrainerId;

                                                var qName = Convert.ToString(dr["QualificationName"]);
                                                var EducationId = !(string.IsNullOrWhiteSpace(qName)) ? db.Educational_Master.Where(x => x.QualificationName == qName.Trim()).FirstOrDefault()?.Id : null;
                                                tblu.EduQualificationID = EducationId;
                                                //tblpart.EduQualOther = EducationId == 4 && !(string.IsNullOrWhiteSpace(dr["EduQualOther"].ToString())) ? dr["EduQualOther"].ToString().Trim() : "NA";
                                                tblu.EduQualOther = dr["EduQualOther"].ToString();
                                                var cName = Convert.ToString(dr["CourseName"]);
                                                var CourseEnrolledId = !(string.IsNullOrWhiteSpace(cName)) ? db.Courses_Master.Where(x => x.CourseName == cName.Trim()).FirstOrDefault()?.Id : null;
                                                tblu.CourseEnrolledID = CourseEnrolledId;

                                                var trainingAgencyName = Convert.ToString(dr["TrainingAgencyName"]);
                                                var TrainingAgencyId = !(string.IsNullOrWhiteSpace(trainingAgencyName)) ? db.TrainingAgency_Master.Where(x => x.TrainingAgencyName == trainingAgencyName.Trim()).FirstOrDefault()?.Id : null;
                                                tblu.TrainingAgencyID = TrainingAgencyId;
                                                var trainingCenter = Convert.ToString(dr["TrainingCenter"]);
                                                var TrainingCenterId = !(string.IsNullOrWhiteSpace(trainingCenter)) ? db.TrainingCenter_Master.Where(x => x.TrainingCenter == trainingCenter.Trim()).FirstOrDefault()?.Id : null;
                                                tblu.TrainingCenterID = TrainingCenterId;
                                                tblu.TrainerName = !(string.IsNullOrWhiteSpace(dr["TrainerName"].ToString())) ? dr["TrainerName"].ToString().Trim() : null;
                                                tblu.TrainerMobileNo = !(string.IsNullOrWhiteSpace(dr["TrainerMobileNo"].ToString())) ? dr["TrainerMobileNo"].ToString().Trim() : null;

                                                var MaritalStatus = Convert.ToString(dr["MaritalStatus"]);
                                                var MaritalStatusId = !(string.IsNullOrWhiteSpace(MaritalStatus)) ? CommonModel.GetMaritalStatus().Where(x => x.Text == MaritalStatus).FirstOrDefault()?.Value : null;
                                                tblu.MaritalStatus = MaritalStatusId;
                                                tblu.NoofFamilyMembers = !(string.IsNullOrWhiteSpace(dr["NoofFamilyMembers"].ToString())) ? Convert.ToInt32(dr["NoofFamilyMembers"]) : 0;
                                                tblu.AnnualHouseholdincome = !(string.IsNullOrWhiteSpace(dr["AnnualHouseholdincome"].ToString())) ? Convert.ToDecimal(dr["AnnualHouseholdincome"]) : 0;
                                                var PreTrainingStatus = Convert.ToString(dr["PreTrainingStatus"]);
                                                var PreTrainingStatusId = !(string.IsNullOrWhiteSpace(PreTrainingStatus)) ? db_.PreTraining_Master.Where(x => x.PreTrainingName == PreTrainingStatus.Trim()).FirstOrDefault()?.PreTrainingId_pk : null;
                                                tblu.PreTrainingStatus = PreTrainingStatusId;
                                                var TargetGroup = Convert.ToString(dr["TargetGroup"]);
                                                var TargetGroupId = !(string.IsNullOrWhiteSpace(TargetGroup)) ? db_.TargetGroup_Master.Where(x => x.TargetGroupName == TargetGroup.Trim()).FirstOrDefault()?.TargetGroupId_pk : null;
                                                tblu.TargetGroup = TargetGroupId;

                                                tblu.IsPlaced = !(string.IsNullOrWhiteSpace(dr["IsPlaced"].ToString())) && dr["IsPlaced"].ToString().ToLower() == Enums.ePlaced.Yes.ToString().ToLower() ? true : false;
                                                resupdated += db_.SaveChanges();
                                            }
                                        }

                                        if (!db_.tbl_Participant.Any(x => x.PhoneNo == moblie || x.AadharCardNo == AadharCardNo))
                                        {
                                            var tblpart = new tbl_Participant();
                                            if (tblpart != null)
                                            {
                                                var fulllist = dr["Name"].ToString().Split(' ');
                                                if (fulllist != null || fulllist.Length != 0)
                                                {
                                                    if (fulllist.Length >= 1)
                                                    {
                                                        tblpart.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                    }
                                                    if (fulllist.Length >= 2)
                                                    {
                                                        tblpart.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                        tblpart.LastName = !(string.IsNullOrWhiteSpace(fulllist[1])) ? fulllist[1].Trim().ToUpper() : null;
                                                    }
                                                    else if (fulllist.Length >= 3)
                                                    {
                                                        tblpart.FirstName = !(string.IsNullOrWhiteSpace(fulllist[0])) ? fulllist[0].Trim().ToUpper() : null;
                                                        tblpart.MiddleName = !(string.IsNullOrWhiteSpace(fulllist[1])) ? fulllist[1].Trim().ToUpper() : null;
                                                        tblpart.LastName = !(string.IsNullOrWhiteSpace(fulllist[2])) ? fulllist[2].Trim().ToUpper() : null;
                                                    }
                                                    tblpart.FullName = !(string.IsNullOrWhiteSpace(dr["Name"].ToString())) ? dr["Name"].ToString().Trim() : null;
                                                }

                                                var sn = Convert.ToString(dr["StateName"]);
                                                var StateId = db.State_Master.Where(x => x.StateName == sn).FirstOrDefault()?.Id;
                                                tblpart.StateID = StateId;
                                                var dname = Convert.ToString(dr["DistrictName"]);
                                                var DistrictID = db.District_Master.Where(x => x.DistrictName == dname && x.StateId == StateId)?.FirstOrDefault().Id;
                                                tblpart.DistrictID = DistrictID;


                                                //tblpart.DateOfBirth = !(string.IsNullOrWhiteSpace(dr["DateofBirth"].ToString())) ? dr["DateofBirth"].ToString().Trim() : null;
                                                var dobirth = !(string.IsNullOrWhiteSpace(dr["DateofBirth"].ToString())) ?
                                                    dr["DOBDD"].ToString() + "-" + dr["DOBMM"].ToString() + "-" + dr["DOBYYYY"].ToString() : null;
                                                tblpart.DateOfBirth = dobirth;
                                                tblpart.Gender = !(string.IsNullOrWhiteSpace(dr["Gender"].ToString())) ? dr["Gender"].ToString().Trim() : null;
                                                tblpart.Age = !(string.IsNullOrWhiteSpace(dr["Age"].ToString())) ? dr["Age"].ToString().Trim() : null;
                                                tblpart.PhoneNo = !(string.IsNullOrWhiteSpace(dr["PhoneNo"].ToString())) ? dr["PhoneNo"].ToString().Trim() : null;
                                                // tblpart.AlternatePhoneNo = !(string.IsNullOrWhiteSpace(dr["AlternatePhoneNo"].ToString())) ? dr["AlternatePhoneNo"].ToString().Trim() : null;
                                                tblpart.EmailID = !(string.IsNullOrWhiteSpace(dr["EmailID"].ToString())) ? dr["EmailID"].ToString().Trim() : null;
                                                tblpart.AadharCardNo = !(string.IsNullOrWhiteSpace(dr["AadharCardNo"].ToString())) ? dr["AadharCardNo"].ToString().Trim() : null;
                                                tblpart.AssessmentScore = !(string.IsNullOrWhiteSpace(dr["AssessmentScore"].ToString())) ? dr["AssessmentScore"].ToString().Trim() : null;

                                                var bName = Convert.ToString(dr["BatchName"]);
                                                var GetBatchdata = !(string.IsNullOrWhiteSpace(bName)) ? db.Batch_Master.Where(x => x.BatchName == bName.Trim()).FirstOrDefault() : null;
                                                tblpart.BatchId = GetBatchdata != null ? GetBatchdata.Id : tblpart.BatchId;
                                                tblpart.TrainerId = GetBatchdata != null ? GetBatchdata.TrainerId : tblpart.TrainerId;

                                                var qName = Convert.ToString(dr["QualificationName"]);
                                                var EducationId = !(string.IsNullOrWhiteSpace(qName)) ? db.Educational_Master.Where(x => x.QualificationName == qName.Trim()).FirstOrDefault()?.Id : null;
                                                tblpart.EduQualificationID = EducationId;
                                                //tblpart.EduQualOther = EducationId == 4 && !(string.IsNullOrWhiteSpace(dr["EduQualOther"].ToString())) ? dr["EduQualOther"].ToString().Trim() : "NA";
                                                tblpart.EduQualOther = dr["EduQualOther"].ToString();
                                                var cName = Convert.ToString(dr["CourseName"]);
                                                var CourseEnrolledId = !(string.IsNullOrWhiteSpace(cName)) ? db.Courses_Master.Where(x => x.CourseName == cName.Trim()).FirstOrDefault()?.Id : null;
                                                tblpart.CourseEnrolledID = CourseEnrolledId;

                                                var trainingAgencyName = Convert.ToString(dr["TrainingAgencyName"]);
                                                var TrainingAgencyId = !(string.IsNullOrWhiteSpace(trainingAgencyName)) ? db.TrainingAgency_Master.Where(x => x.TrainingAgencyName == trainingAgencyName.Trim()).FirstOrDefault()?.Id : null;
                                                tblpart.TrainingAgencyID = TrainingAgencyId;
                                                var trainingCenter = Convert.ToString(dr["TrainingCenter"]);
                                                var TrainingCenterId = !(string.IsNullOrWhiteSpace(trainingCenter)) ? db.TrainingCenter_Master.Where(x => x.TrainingCenter == trainingCenter).FirstOrDefault()?.Id : null;
                                                tblpart.TrainingCenterID = TrainingCenterId;
                                                tblpart.TrainerName = !(string.IsNullOrWhiteSpace(dr["TrainerName"].ToString())) ? dr["TrainerName"].ToString().Trim() : null;
                                                tblpart.TrainerMobileNo = !(string.IsNullOrWhiteSpace(dr["TrainerMobileNo"].ToString())) ? dr["TrainerMobileNo"].ToString().Trim() : null;

                                                var MaritalStatus = Convert.ToString(dr["MaritalStatus"]);
                                                var MaritalStatusId = !(string.IsNullOrWhiteSpace(MaritalStatus)) ? CommonModel.GetMaritalStatus().Where(x => x.Text == MaritalStatus.Trim()).FirstOrDefault()?.Value : null;
                                                tblpart.MaritalStatus = MaritalStatusId;
                                                tblpart.NoofFamilyMembers = !(string.IsNullOrWhiteSpace(dr["NoofFamilyMembers"].ToString())) ? Convert.ToInt32(dr["NoofFamilyMembers"]) : 0;
                                                tblpart.AnnualHouseholdincome = !(string.IsNullOrWhiteSpace(dr["AnnualHouseholdincome"].ToString())) ? Convert.ToDecimal(dr["AnnualHouseholdincome"]) : 0;
                                                var PreTrainingStatus = Convert.ToString(dr["PreTrainingStatus"]);
                                                var PreTrainingStatusId = !(string.IsNullOrWhiteSpace(PreTrainingStatus)) ? db_.PreTraining_Master.Where(x => x.PreTrainingName == PreTrainingStatus.Trim()).FirstOrDefault()?.PreTrainingId_pk : null;
                                                tblpart.PreTrainingStatus = PreTrainingStatusId;
                                                var TargetGroup = Convert.ToString(dr["TargetGroup"]);
                                                var TargetGroupId = !(string.IsNullOrWhiteSpace(TargetGroup)) ? db_.TargetGroup_Master.Where(x => x.TargetGroupName == TargetGroup.Trim()).FirstOrDefault()?.TargetGroupId_pk : null;
                                                tblpart.TargetGroup = TargetGroupId;
                                                //if (!string.IsNullOrEmpty(dr["DateofBirth"].ToString()))
                                                //{
                                                //    tblpart.DOB = Convert.ToDateTime(dr["DateofBirth"].ToString());
                                                //}
                                                tblpart.IsActive = true;
                                                if (tblpart.ID == Guid.Empty)
                                                {
                                                    if (User.Identity.IsAuthenticated)
                                                    {
                                                        tblpart.CreatedBy = MvcApplication.CUser.UserId;
                                                    }
                                                    tblpart.ID = Guid.NewGuid();
                                                    tblpart.CreatedOn = DateTime.Now;
                                                    tblpart.CallTempStatus = (int)Enums.eTempCallStatus.CallNot;
                                                    tblpart.IsPlaced = !(string.IsNullOrWhiteSpace(dr["IsPlaced"].ToString())) && dr["IsPlaced"].ToString().ToLower() == Enums.ePlaced.Yes.ToString().ToLower() ? true : false;
                                                    //tblpart.FixedCallLimitMonth = tblpart.IsPlaced == true ? (int)Enums.eCallLimitDuration.FixedCallLimit : 0;
                                                    // tblpart.FixedCallLimitMonth = tblpart.IsPlaced == true ? (int)Enums.eCallLimitDuration.FixedCallLimit : 0;
                                                    tblpart.FixedCallLimitMonth = (int)Enums.eCallLimitDuration.FixedCallLimit;
                                                    tblpart.AtPresentCallStatus = 0;
                                                    tblpart.IsAssessmentDone = false;
                                                    //tblpart.IsOffered = !(string.IsNullOrWhiteSpace(dr["IsOffered"].ToString())) && dr["IsOffered"].ToString().ToLower() == Enums.ePlaced.Yes.ToString().ToLower() ? true : false;
                                                    db.tbl_Participant.Add(tblpart);
                                                }
                                                //tbl.NoofInserted = results - 1;
                                                results += db.SaveChanges();
                                            }
                                        }
                                    }
                                }

                            }
                            if (!string.IsNullOrEmpty(strmobile))
                            {
                                strmobile += " " + Enums.GetEnumDescription(Enums.eReturnReg.Already);
                            }
                            if (results > 0)
                            {
                                tbl.NoofInserted = results - 1;
                                results += db.SaveChanges();
                                var res = Json(new { IsSuccess = true, Message = Enums.GetEnumDescription(Enums.eReturnReg.Insert) + strmobile + " Updated No :  " + resupdated }, JsonRequestBehavior.AllowGet);
                                res.MaxJsonLength = int.MaxValue;
                                return res;
                            }
                            else if (results == 0 && resupdated > 0)
                            {
                                //tbl.NoofInserted = results - 1;
                                //results += db.SaveChanges();
                                var res = Json(new { IsSuccess = true, Message = Enums.GetEnumDescription(Enums.eReturnReg.Update) + strmobile + " Updated No :  " + resupdated }, JsonRequestBehavior.AllowGet);
                                res.MaxJsonLength = int.MaxValue;
                                return res;
                            }
                        }
                    }
                    else
                    {
                        var res = Json(new { IsSuccess = false, Message = Enums.GetEnumDescription(Enums.eReturnReg.AllFieldsRequired) }, JsonRequestBehavior.AllowGet);
                        res.MaxJsonLength = int.MaxValue;
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    CommonModel.ExpSubmit("tbl_FileUpload ,tbl_Participant"
                         , "Participant", "ExcelFileUpload", "ExcelFileUpload"
                         , ex.Message);
                    string er = ex.Message;
                    //Tbl_ExceptionHandle tbl = new Tbl_ExceptionHandle();
                    //tbl.Id_pk = Guid.NewGuid(); 
                    //tbl.Action = "ExcelFileUpload";
                    //tbl.Controller = "Participant";
                    //tbl.Table = "Participant,tbl_ExcelParticipantData";
                    //tbl.E_Exception = er;
                    //tbl.CreatedBy = MvcApplication.CUser.UserId;
                    //tbl.CreatedOn= DateTime.Now;
                    //tbl.IsActive = true;
                    //db_.Tbl_ExceptionHandle.Add(tbl);
                    //db_.SaveChanges();
                    return Json(new { IsSuccess = false, Message = Enums.GetEnumDescription(Enums.eReturnReg.ExceptionError) }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { IsSuccess = false, Message = Enums.GetEnumDescription(Enums.eReturnReg.Error) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GeExcelFileUploadList()
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SPManager.SP_GetFileUpload();
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_ExcelData", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
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
        #endregion
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
        public ActionResult CallSummary()
        {
            return View();
        }
        public ActionResult GetCallSummary(string BatchId = "",string Course="",string CallStatus="",string User="")
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            try
            {
                User = CommonModel.IsRoleLogin();
                ds = SPManager.GetSP_ParticipantCallMonthWisematrix(User, BatchId,Course,CallStatus);
                if (ds.Tables.Count > 0)
                {
                    tbllist = (ds.Tables[0]);
                    var html = ConvertViewToString("_CallSummaryData", tbllist);
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
    }
}