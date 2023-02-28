using ProjectBasedSystem.Database;
using ProjectBasedSystem.Repositories.Nationality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBasedSystem.Controllers
{
    public class NationalityController : Controller
    {
        private readonly database _db;
        private readonly Repo_Nationality _repoNationality;


        public NationalityController(database db)
        {
            _db = db;
            _repoNationality = new Repo_Nationality(_db);
        }


        // GET: Nationality
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public JsonResult GetNationality()
        {
            return Json(_repoNationality.getNationality(), JsonRequestBehavior.AllowGet);
        }


    }
}