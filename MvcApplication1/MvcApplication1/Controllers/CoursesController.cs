using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CoursesController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /Courses/

        public ActionResult Index(AcademicYear AY = null)
        {
            if (AY.Name == null)
            {
                return View(db.Courses.ToList());
            }
            else
                return View(db.Courses.ToList().Where(f => f.AcademicYear.AcademicYearID.Equals(AY.AcademicYearID)));

        }
        public ActionResult Getcourses_from_Dept(Guid id, Guid Yer)
        {
            return View("Index", db.Courses.ToList().Where(f => f.DepartmentID == (id) && f.AcademicYearID == (Yer)));
        }

        public ActionResult Courses_from_academicstaff(Guid id)
        {

            AcademicStaff ASF = db.AcademicStaffs.Find(id);

            return View("Index", db.Courses.Include(c => c.AcademicStaffs).Where(c => c.AcademicStaffs.Any(s => s.InstructorID == id)).ToList());
        }

        public ActionResult academicsemester(Guid id, Guid Yer)
        {
            return View("Index", db.Courses.ToList().Where(f => f.SemesterID == (id) && f.AcademicYearID == (Yer)));
            ;
        }

        public ActionResult From_student_to_courses(Guid id)
        {
            Student ST = db.Students.Find(id);
           
            var courses = db.Courses.Include(c => c.StudentCourses).Where(c => c.StudentCourses.Any(s => s.StudentID == ST.StudentID)).ToList();
            return View( "Index",courses);
        }

        public ActionResult Get_coursesGrade_From_Courses(Guid id)
        {
            return RedirectToAction("Get_Grade_From_Courses", "CourseGrades", new { id });
        }
        public ActionResult Get_coursesmaterial_From_Courses(Guid id)
        {
            return RedirectToAction("Get_material_From_Courses", "CourseMaterial", new { id });
        }
        public ActionResult Get_courseStudent_From_Courses(Guid id)
        {
            Cours ST = db.Courses.Find(id);
            return RedirectToAction("Get_courseStudent_From_Courses2", "Students", ST);
        }


        public ActionResult Get_courseStaff_From_Courses(Guid id)
        {
            return RedirectToAction("GetStaff_From_Courses", "AcademicStaff", new { id });
        }

        //
        // GET: /Courses/Details/5

        public ActionResult Details(Guid id)
        {
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        //
        // GET: /Courses/Create

        public ActionResult Create()
        {
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name");
            return View();
        }

        //
        // POST: /Courses/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cours cours)
        {
            if (ModelState.IsValid)
            {
                cours.CourseID = Guid.NewGuid();
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", cours.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", cours.DepartmentID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", cours.SemesterID);
            return View(cours);
        }

        //
        // GET: /Courses/Edit/5

        public ActionResult Edit(Guid id)
        {
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", cours.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", cours.DepartmentID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", cours.SemesterID);
            return View(cours);
        }

        //
        // POST: /Courses/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcademicYearID = new SelectList(db.AcademicYears, "AcademicYearID", "Name", cours.AcademicYearID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", cours.DepartmentID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", cours.SemesterID);
            return View(cours);
        }

        //
        // GET: /Courses/Delete/5

        public ActionResult Delete(Guid id)
        {
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        //
        // POST: /Courses/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
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