using ProjectBasedSystem.Database;
using ProjectBasedSystem.Repositories.Barangay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBasedSystem.Controllers
{
    public class BarangayController : Controller
    {
        private readonly database _db;
        private readonly Repo_Barangay _repoBarangay ;

        public BarangayController(database db)
        {
            _db = db;
            _repoBarangay = new Repo_Barangay(_db);
        }

        // GET: Barangay
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetBarangay()
        {
            var getData = _repoBarangay.GetBarangay();

            return  Json(getData, JsonRequestBehavior.AllowGet);
        }
    }
}