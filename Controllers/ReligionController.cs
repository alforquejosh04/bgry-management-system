using ProjectBasedSystem.Database;
using ProjectBasedSystem.Repositories.Religion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBasedSystem.Controllers
{
    public class ReligionController : Controller
    {
        private readonly database _db;
        private readonly Repo_Religion _repoReligion;


        public ReligionController(database db)
        {
            _db = db;
            _repoReligion = new Repo_Religion(_db);
        }


        // GET: Religion
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public JsonResult GetNationality()
        {
            return Json(_repoReligion.GetReligion(), JsonRequestBehavior.AllowGet);
        }
    }
}