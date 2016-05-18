using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class SemsetersController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /Semseters/

        public ActionResult Index(AcademicYear AY = null)
        {

            if (AY.Name == null)
            {
                return View(db.Semesters.ToList());
            }
            else
            {
                var semesters = db.Semesters.Include(s => s.AcademicYear);
                ViewBag.Name = null;
                return View(db.Semesters.ToList().Where(F => F.AcademicYear.AcademicYearID.Equals(AY.AcademicYearID)));
            }
        }
        public ActionResult Getacademicsemester(Guid id, Guid Yer)
        {
       
            return RedirectToAction("academicsemester", "Courses", new { id, Yer });
            
        }

        //
        // GET: /Semseters/Details/5

        public ActionResult Details(Guid id)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        //
        // GET: /Semseters/Create

        public ActionResult Create()
        {
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name");
            return View();
        }

        //
        // POST: /Semseters/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Semester semester)
        {
            if (ModelState.IsValid)
            {
                semester.SemesterID = Guid.NewGuid();
                db.Semesters.Add(semester);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", semester.AcademicYearID);
            return View(semester);
        }

        //
        // GET: /Semseters/Edit/5

        public ActionResult Edit(Guid id)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", semester.AcademicYearID);
            return View(semester);
        }

        //
        // POST: /Semseters/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Semester semester)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semester).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", semester.AcademicYearID);
            return View(semester);
        }

        //
        // GET: /Semseters/Delete/5

        public ActionResult Delete(Guid id)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        //
        // POST: /Semseters/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Semester semester = db.Semesters.Find(id);
            db.Semesters.Remove(semester);
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