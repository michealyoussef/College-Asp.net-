using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CourseMaterialController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();
        string fileaddress = null;
        public ActionResult Index()
        {
            var coursematerials = db.CourseMaterials.Include(c => c.Cours).Include(c => c.MaterialType);
            return View(coursematerials.ToList());
        }
        public ActionResult Details(Guid id)
        {
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            if (coursematerial == null)
            {
                return HttpNotFound();
            }
            return View(coursematerial);
        }
        public ActionResult Get_material_From_Courses(Guid id)
        {
            return View("Index", db.CourseMaterials.Where(A => A.CourseID == (id)).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseMaterial coursematerial, HttpPostedFileBase FilePath)
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursematerial.CourseID);
            ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name", coursematerial.MaterialTypeID);
            if (ModelState.IsValid)
            {
                var supportedTypes = new[] { "pdf", "doc", "docx", "ppt" };
                var fileExt = System.IO.Path.GetExtension(FilePath.FileName).Substring(1);
                if (FilePath != null && FilePath.ContentLength > 0 && supportedTypes.Contains(fileExt))
                {
                    coursematerial.MaterialID = Guid.NewGuid();
                    string path = Server.MapPath("~/MaterialFiles/" + FilePath.FileName);
                    coursematerial.FilePath = path;
                    FilePath.SaveAs(path);
                    db.CourseMaterials.Add(coursematerial);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { CourseID = coursematerial.CourseID, MaterialTypeID = coursematerial.MaterialTypeID });
                }

            }
            return View(coursematerial);
        }

        // GET: /CourseMaterial/Edit/5

        public ActionResult Edit(Guid id)
        {
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            fileaddress = coursematerial.FilePath;
            if (coursematerial == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursematerial.CourseID);
            ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name", coursematerial.MaterialTypeID);
            return View(coursematerial);
        }

        //
        // POST: /CourseMaterial/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(CourseMaterial coursematerial)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        db.Entry(coursematerial).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursematerial.CourseID);
        //    ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name", coursematerial.MaterialTypeID);
        //    return View(coursematerial);
        //}

        //
        // GET: /CourseMaterial/Delete/5
        public ActionResult Edit(CourseMaterial coursematerial, HttpPostedFileBase FilePath,string id)
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursematerial.CourseID);
            ViewBag.MaterialTypeID = new SelectList(db.MaterialTypes, "MaterialTypeID", "Name", coursematerial.MaterialTypeID);

            if (ModelState.IsValid)
            {

                if (FilePath == null)
                {
                    ////coursematerial.MaterialID = Guid.NewGuid();
                    coursematerial.FilePath = id;
                    string path = coursematerial.FilePath;                    
                    db.Entry(coursematerial).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { CourseID = coursematerial.CourseID, MaterialTypeID = coursematerial.MaterialTypeID });
                }

                else if (FilePath != null && FilePath.ContentLength > 0)
                {
                    var supportedTypes = new[] { "pdf", "doc", "docx", "ppt" };
                    var fileExt = System.IO.Path.GetExtension(FilePath.FileName).Substring(1);

                    coursematerial.MaterialID = Guid.NewGuid();
                    string path = Server.MapPath("~/MaterialFiles/" + FilePath.FileName);
                    coursematerial.FilePath = path;
                    FilePath.SaveAs(path);
                    db.CourseMaterials.Add(coursematerial);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { CourseID = coursematerial.CourseID, MaterialTypeID = coursematerial.MaterialTypeID });
                }

            }
            return View(coursematerial);
        }
        public ActionResult Delete(Guid id)
        {
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            if (coursematerial == null)
            {
                return HttpNotFound();
            }
            return View(coursematerial);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            db.CourseMaterials.Remove(coursematerial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}