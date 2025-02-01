using Hunarmis.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Controllers
{
    [Authorize(Roles = CommonModel.RoleNameCont.Admin + "," +CommonModel.RoleNameCont.Viewer + "," + CommonModel.RoleNameCont.Verifier + "," + CommonModel.RoleNameCont.State + "," + CommonModel.RoleNameCont.Trainer + "," + CommonModel.RoleNameCont.District)]
    //[Authorize]
    public class TainerController : Controller
    {
        // GET: Tainer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TrainerDashboard()
        {
            return View();
        }
        public ActionResult GetTrainerDashboardList(int CId = 0, int BId = 1)
        {
            DataSet ds = new DataSet();
            DataTable tbllist = new DataTable();
            try
            {
                ds = SPManager.SP_TainerDashoard_List(CId,BId);
                if (ds.Tables.Count > 0)
                {
                    tbllist = (ds.Tables[0]);
                    var html = ConvertViewToString("_TrainerDashboardListData", tbllist);
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