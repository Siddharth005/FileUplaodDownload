using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileUplaodDownload.Models;

namespace FileUplaodDownload.Controllers
{
    public class employeeloginsController : Controller
    {
        private FileUploadDownloadEntities db = new FileUploadDownloadEntities();

        // GET: employeelogins/login
        public ActionResult login()
        {
            return View();
        }
        // GET: employeelogins/Details/5
        public ActionResult logins(employeelogin employeelogin2)
        {

            employeelogin employeelogin1 = (employeelogin)(from e in db.employeelogins
                                                           where e.pass == employeelogin2.pass
                                                           select e).First();
            if (employeelogin2 == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index","UploadDownloads");
        }

        // GET: employeelogins/Create
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
