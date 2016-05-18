using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class StudentsController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /Students/

        public ActionResult Index(AcademicYear AY = null)
        {
            if (AY.Name == null)
                return View(db.Students.ToList());
            else
                return View(db.Students.ToList().Where(f => f.AcademicYear.AcademicYearID.Equals(AY.AcademicYearID)));
        }

        public ActionResult Get_student_from_Dept(Guid id ,Guid Yer)
        {
            return View("Index", db.Students.ToList().Where(f => f.DepartmentID == id));
        }
        public ActionResult Get_courseStudent_From_Courses2(Cours ST)
        {
            var t = db.Students.Include(S => S.StudentCourses).Where(X => X.StudentCourses.Any(V => V.CourseID == ST.CourseID)).ToList();
            return View("Index", t.ToList());
        }

        public ActionResult Student_from_Section(Guid id , Guid Yer, Guid F)
        {
            return View("Index", db.Students.ToList().Where(f => f.DepartmentID == (Yer) && f.CurrentAcademicYearID == (F) && f.SectionID == (id)));
        }

        public ActionResult Get_Student_for_Courses(Guid id)
        {
            Student st = db.Students.Find(id);

            return RedirectToAction("From_student_to_courses", "Courses", new { id });
        }

        public ActionResult Get_student_for_Grades(Guid id)
        {
          // return RedirectToAction("From_student_to_Grades", "StudentGrade", new { id });

            return RedirectToAction("Index", "StudentGrade", new { id });
        }

        //
        // GET: /Students/Details/5

        public ActionResult Details(Guid id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // GET: /Students/Create

        public ActionResult Create()
        {
            ViewBag.CurrentAcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name");
            ViewBag.SectionID = new SelectList(db.AcademicYearSections, "SectionID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        //
        // POST: /Students/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                student.StudentID = Guid.NewGuid();
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentAcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", student.CurrentAcademicYearID);
            ViewBag.SectionID = new SelectList(db.AcademicYearSections, "SectionID", "Name", student.SectionID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        //
        // GET: /Students/Edit/5

        public ActionResult Edit(Guid id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentAcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", student.CurrentAcademicYearID);
            ViewBag.SectionID = new SelectList(db.AcademicYearSections, "SectionID", "Name", student.SectionID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        //
        // POST: /Students/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentAcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", student.CurrentAcademicYearID);
            ViewBag.SectionID = new SelectList(db.AcademicYearSections, "SectionID", "Name", student.SectionID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        //
        // GET: /Students/Delete/5

        public ActionResult Delete(Guid id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /Students/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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