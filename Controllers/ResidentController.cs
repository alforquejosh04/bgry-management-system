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
    public class ResidentController : Controller
    {
        private readonly database _db;
        private readonly BrgyResidential _repoResidential;

        public ResidentController(database db)
        {
            _db = db;
            _repoResidential = new BrgyResidential(_db);      
        }
        // GET: Resident
        public ActionResult Index()
        {


            return View(_repoResidential.GetResidentials());
        }
        public ActionResult modal()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetResidents()
        {

            return Json(_repoResidential.GetResidentials(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public JsonResult GetResidentById(int ResidentId)

        {
            var results = _repoResidential.GetResidentials().GroupJoin( //inner
                _repoResidential.GetResidentialsImage(), //outer
                res => res.residential_id, // inner 
                images => images.residential_id, //outer

                (res, images) => new
                {
                    residential_id = res.residential_id,
                    first_Name = res.first_Name,
                    last_Name = res.last_Name,
                    Suffix = res.Suffix,
                    birthdate = res.birthdate,
                    gender = res.gender,
                    marital_status = res.marital_status,
                    contact_no = res.contact_no,
                    email_add = res.email_add,
                    household_no = res.household_no,
                    zone_no = res.zone_no,
                    barangay_id = res.barangay_id,
                    household_member = res.household_member,
                    length_of_stay = res.length_of_stay,
                    religion_id = res.religion_id,
                    occupation = res.occupation,
                    nationality_id = res.nationality_id,
                    Philhealth_no = res.Philhealth_no,
                    voters = res.voters,
                    residency_status = res.residency_status,
                    is_Delete = res.is_Delete,
                    Files = images
                }).Where(x => x.residential_id == ResidentId && (x.is_Delete != true));
            //   var resident = _repoResidential.GetResidentials().First(x => x.residential_id == ResidentId && (x.is_Delete != true));
            if (results != null)
            {
                return Json(results, JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult AddUpdateResident(barangayResidential item)
        {
            bool status = false;
            bool Result = false;
            if (ModelState.IsValid)
            {

                Result = _repoResidential.AddUpdate(new barangayResidential()
                {
                    residential_id = item.residential_id,
                    first_Name = item.first_Name,
                    last_Name = item.last_Name,
                    Suffix = item.Suffix,
                    birthdate = item.birthdate,
                    gender = item.gender,
                    marital_status = item.marital_status,
                    contact_no = item.contact_no,
                    email_add = item.email_add,
                    household_no = item.household_no,
                    zone_no = item.zone_no,
                    barangay_id = item.barangay_id,
                    household_member = item.household_member,
                    length_of_stay = item.length_of_stay,
                    religion_id = item.religion_id,
                    occupation = item.occupation,
                    nationality_id = item.nationality_id,
                    Philhealth_no = item.Philhealth_no,
                    voters = item.voters,
                    residency_status = item.residency_status
                }

                      );

                if (Result == true)
                {
                    status = true;

                }

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public ActionResult SaveImages(int ResidentId)
        {
            string FileName, ExtensionName, savePath, OldFileName;
            FileName = ExtensionName = savePath = OldFileName = string.Empty;
            bool status = false;

            if (ModelState.IsValid)
            {
                if (Request.Files != null)
                {
                    var file = Request.Files[0];
                    FileName = file.FileName;
                    ExtensionName = Guid.NewGuid() + Path.GetExtension(FileName);
                    var Size = file.ContentLength;

                    try
                    {
                        string tempPath = Server.MapPath("~/uploadImage");
                        if (System.IO.Directory.Exists(tempPath) == false)
                        {
                            System.IO.Directory.CreateDirectory(tempPath);
                        }
                        savePath = Path.Combine(tempPath, FileName);
                        file.SaveAs(savePath);
                        var getOldFileName = _repoResidential.GetResidentialsImage().Where(x => x.residential_id == ResidentId).Select(x => x.FileName).Distinct();
                        foreach (var item in getOldFileName)
                        {
                            OldFileName = item;

                        }
                        string OldFilepath = Path.Combine(tempPath, OldFileName);
                        if (System.IO.File.Exists(OldFilepath))
                        {

                            System.IO.File.Delete(OldFilepath);

                        }
                        bool Result = _repoResidential.FileSave(new residentialImages()
                        {
                            residential_id = ResidentId,
                            FileName = FileName,
                            Description = "Profile Image",
                            FilePath = savePath,
                            FileSize = Size

                        });

                        status = true;

                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error:" + ex);

                    }
                }
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}