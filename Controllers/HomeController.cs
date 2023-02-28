using ProjectBasedSystem.Database;
using ProjectBasedSystem.Models;
using ProjectBasedSystem.Repositories.Resedential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBasedSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly database _db;
        private readonly BrgyResidential _repoResidential;

        public HomeController(database db)
        {
            _db = db;
            _repoResidential = new BrgyResidential(_db);
        }

        /*  public string Version()
          {
              return typeof(Controller).Assembly.GetName().Version.ToString();
          }*/
        // GET: Home
        public ActionResult Index()
        {
            
               
            return View();
        }
 
    }
}