using Hunarmis.Manager;
using System;
using System.Collections.Generic;
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
    }
}