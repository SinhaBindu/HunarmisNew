using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Controllers
{
    [Authorize]
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