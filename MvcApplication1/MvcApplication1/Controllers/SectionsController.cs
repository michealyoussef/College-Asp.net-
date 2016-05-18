using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class SectionsController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /Sections/

        public ActionResult Index(AcademicYear AY = null)
        {
            if (AY.Name == null)
            {
                return View(db.AcademicYearSections.ToList());
            }
            else
                return View(db.AcademicYearSections.ToList().Where(f => f.AcademicYear.Name.Equals(AY.Name)));
        }

        public ActionResult GetSection_from_Dept(Guid id, Guid Yer)
        {
            return View("Index", db.AcademicYearSections.ToList().Where(f => f.AcademicYearID == Yer && f.DepartmentID == id));
        }
        public ActionResult GetStudents (Guid id, Guid Yer, Guid F)
        {
            //AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            //AcademicYear Academicyear = db.AcademicYears.Find(F);
            //Department Dep = db.Departments.Find(Yer);
            return RedirectToAction("Student_from_Section", "Students", new { id, Yer, F });
        }

        //
        // GET: /Sections/Details/5

        public ActionResult Details(Guid id)
        {
            AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            if (academicyearsection == null)
            {
                return HttpNotFound();
            }
            return View(academicyearsection);
        }

        //
        // GET: /Sections/Create

        public ActionResult Create()
        {
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        //
        // POST: /Sections/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AcademicYearSection academicyearsection)
        {
            if (ModelState.IsValid)
            {
                academicyearsection.SectionID = Guid.NewGuid();
                db.AcademicYearSections.Add(academicyearsection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", academicyearsection.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicyearsection.DepartmentID);
            return View(academicyearsection);
        }

        //
        // GET: /Sections/Edit/5

        public ActionResult Edit(Guid id)
        {
            AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            if (academicyearsection == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", academicyearsection.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicyearsection.DepartmentID);
            return View(academicyearsection);
        }

        //
        // POST: /Sections/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AcademicYearSection academicyearsection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicyearsection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", academicyearsection.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicyearsection.DepartmentID);
            return View(academicyearsection);
        }

        //
        // GET: /Sections/Delete/5

        public ActionResult Delete(Guid id)
        {
            AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            if (academicyearsection == null)
            {
                return HttpNotFound();
            }
            return View(academicyearsection);
        }

        //
        // POST: /Sections/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AcademicYearSection academicyearsection = db.AcademicYearSections.Find(id);
            db.AcademicYearSections.Remove(academicyearsection);
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