using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /Department/

        public ActionResult Index(AcademicYear AY = null)
        {
            if (AY.Name == null)
            {
                return View(db.Departments.ToList());
            }
            else
                return View(db.Departments.ToList().Where(f => f.AcademicYear.AcademicYearID == AY.AcademicYearID));
        }

        public ActionResult GetAcademicStaff(Guid id)
        {
            return RedirectToAction("Get_Academic_staff_From_Dept", "AcademicStaff", new { id });
        }
        public ActionResult GetCourses(Guid id, Guid Yer)
        {

            return RedirectToAction("Getcourses_from_Dept", "Courses", new { id, Yer });

        }
        public ActionResult GetSections(Guid id, Guid Yer)
        {
            return RedirectToAction("GetSection_from_Dept", "Sections", new { id, Yer });

        }
        public ActionResult GetStudents(Guid id, Guid Yer)
        {
            return RedirectToAction("Get_student_from_Dept", "Students", new { id, Yer });

        }

        public ActionResult Details(Guid id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name");
            return View();
        }

        //
        // POST: /Department/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                department.DepartmentID = Guid.NewGuid();
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", department.AcademicYearID);
            return View(department);
        }

        //
        // GET: /Department/Edit/5

        public ActionResult Edit(Guid id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", department.AcademicYearID);
            return View(department);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", department.AcademicYearID);
            return View(department);
        }

        //
        // GET: /Department/Delete/5

        public ActionResult Delete(Guid id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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