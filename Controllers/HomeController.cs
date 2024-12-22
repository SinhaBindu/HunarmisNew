using Hunarmis.Manager;
using Hunarmis.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Controllers
{
    [Authorize]
    [SessionCheck]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult GetDashboard(int mode, string startDate = "", string endDate = "")
        {
            bool IsCheck = false;
            try
            {
                DataTable dt = SPManager.SP_Dashboard_Graphs(mode, startDate, endDate);
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
        public ActionResult GetIndex(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = SPManager.SP_Dashboard(model);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    dt1 = ds.Tables[1];
                    dt2 = ds.Tables[2];
                    dt3 = ds.Tables[3];
                    IsCheck = true;
                    var datares1 = JsonConvert.SerializeObject(dt);
                    var datares2 = JsonConvert.SerializeObject(dt1);
                    var datares3 = JsonConvert.SerializeObject(dt2);
                    var datares4 = JsonConvert.SerializeObject(dt3);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = datares1, Data2 = datares2, Data3 = datares3, Data4 = datares4 }, JsonRequestBehavior.AllowGet);
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
        public ActionResult TrainingCenterWise()
        {
            return View();
        }
        public ActionResult TrainingCenterWisePartDetail(FilterModel filtermodel)
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            DataTable dt = new DataTable();
            dt = SPManager.SP_RawPartList_TrainCentchart(filtermodel);
            return PartialView("_ParticipantPOPData_TC", dt);
        }
        public ActionResult CallingDashboard()
        {
            return View();
        }
        public ActionResult GetCallingDashboard(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = SPManager.Sp_DashboardPartCalling(model);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    //dt1 = ds.Tables[1];
                    //dt2 = ds.Tables[2];
                    //dt3 = ds.Tables[3];
                    IsCheck = true;
                    var datares1 = JsonConvert.SerializeObject(dt);
                    var datares2 = JsonConvert.SerializeObject(dt1);
                    var datares3 = JsonConvert.SerializeObject(dt2);
                    var datares4 = JsonConvert.SerializeObject(dt3);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = datares1, Data2 = datares2, Data3 = datares3, Data4 = datares4 }, JsonRequestBehavior.AllowGet);
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
        public ActionResult PlacementTracker()
        {
            return View();
        }

        #region
        public ActionResult CallingChart()
        {
            return View();
        }
        public ActionResult GetCallingChartMonth(FilterModel model)
        {
            bool IsCheck = false;
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = SPManager.SP_CallChartWiseMonth(model);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    dt1 = ds.Tables[1];
                    dt2 = ds.Tables[2];
                    dt3 = ds.Tables[3];
                    IsCheck = true;
                    var datares1 = JsonConvert.SerializeObject(dt);
                    var datares2 = JsonConvert.SerializeObject(dt1);
                    var datares3 = JsonConvert.SerializeObject(dt2);
                    var datares4 = JsonConvert.SerializeObject(dt3);
                    var res1 = Json(new { IsSuccess = IsCheck, Data = datares1, Data2 = datares2, Data3 = datares3, Data4 = datares4 }, JsonRequestBehavior.AllowGet);
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
        public ActionResult CallStatus()
        {
            FilterModel model = new FilterModel();
            return View(model);
        }
        public ActionResult GetCallStatus(FilterModel model)
        {
            DataSet ds = new DataSet();
            var html = "";
            try
            {
                ds = SPManager.sp_callstatus(model);
                bool IsCheck = false;
                if (ds.Tables.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_CallStatusData", ds);
                    var resDt = JsonConvert.SerializeObject(ds);
                    var res = Json(new { IsSuccess = IsCheck, Data = html, resData = resDt }, JsonRequestBehavior.AllowGet);
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
                return Json(new { IsSuccess = false, Data = "These exceptions are caused by the programmer." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult CallStatustDetails(string ReportedBy = "", int Flag = 0,string MaxDate="",string SD = "", string ED = "")
        {
            Hunar_DBEntities db_ = new Hunar_DBEntities();
            DataTable dt = new DataTable();
            dt = SPManager.SP_GetCallStatusDetails(ReportedBy, Flag,MaxDate,SD,ED);
            TempData["Flag"] = Flag;
            return PartialView("_CallStatusDetails", dt);
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
    }
}