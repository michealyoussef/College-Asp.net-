using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class AcademicYearsController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /AcademicYears/

        public ActionResult Index()
        {
            return View(db.AcademicYears.ToList());
        }


        public ActionResult Getsemetester(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            return RedirectToAction("index", "Semseters", academicyear);
        }
        public ActionResult GetSections(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            return RedirectToAction("index", "Sections", academicyear);
        }

        public ActionResult GetDepartment(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            return RedirectToAction("index", "Department", academicyear);
        }
        public ActionResult GetCourses(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            return RedirectToAction("index", "Courses", academicyear);
        }

        public ActionResult GetStudents(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            return RedirectToAction("index", "Students", academicyear);
        }
        //
        // GET: /AcademicYears/Details/5

        public ActionResult Details(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            if (academicyear == null)
            {
                return HttpNotFound();
            }
            return View(academicyear);
        }

        //
        // GET: /AcademicYears/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AcademicYears/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AcademicYear academicyear)
        {
            if (ModelState.IsValid)
            {
                academicyear.AcademicYearID = Guid.NewGuid();
                db.AcademicYears.Add(academicyear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academicyear);
        }

        //
        // GET: /AcademicYears/Edit/5

        public ActionResult Edit(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            if (academicyear == null)
            {
                return HttpNotFound();
            }
            return View(academicyear);
        }

        //
        // POST: /AcademicYears/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AcademicYear academicyear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicyear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academicyear);
        }

        //
        // GET: /AcademicYears/Delete/5

        public ActionResult Delete(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            if (academicyear == null)
            {
                return HttpNotFound();
            }
            return View(academicyear);
        }

        //
        // POST: /AcademicYears/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AcademicYear academicyear = db.AcademicYears.Find(id);
            db.AcademicYears.Remove(academicyear);
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