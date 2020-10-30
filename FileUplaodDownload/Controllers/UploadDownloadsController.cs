using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FileUplaodDownload.Models;

namespace FileUplaodDownload.Controllers
{
    public class UploadDownloadsController : Controller
    {
        private FileUploadDownloadEntities db = new FileUploadDownloadEntities();

        // GET: UploadDownloads
        public ActionResult Index()
        {
            return View(db.UploadDownloads.ToList());
        }

        // GET: UploadDownloads/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadDownload uploadDownload = db.UploadDownloads.Find(id);

            if (uploadDownload == null)
            {
                return HttpNotFound();
            }
            return View(uploadDownload);
        }

        public FileResult Download(int? id)
        {

            UploadDownload uploadDownload = db.UploadDownloads.Find(id);

            return File(uploadDownload.filedata, uploadDownload.filetype, uploadDownload.filename);
        }



        // GET: UploadDownloads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UploadDownloads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fileId,filename,filetype,filedata,ticketnumber")] UploadDownload uploadDownload, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {

                    uploadDownload.filename = System.IO.Path.GetFileName(upload.FileName);
                    uploadDownload.filetype = upload.ContentType;
                    

                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    uploadDownload.filedata = reader.ReadBytes(upload.ContentLength);
                }

            }
            db.UploadDownloads.Add(uploadDownload);
            db.SaveChanges();

            return RedirectToAction("Index"); ;
        }

        // GET: UploadDownloads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadDownload uploadDownload = db.UploadDownloads.Find(id);
            if (uploadDownload == null)
            {
                return HttpNotFound();
            }
            return View(uploadDownload);
        }

        // POST: UploadDownloads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UploadDownload uploadDownload = db.UploadDownloads.Find(id);
            db.UploadDownloads.Remove(uploadDownload);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
