using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class AcademicStaffController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /AcademicStaff/

        public ActionResult Index()
        {
            var academicstaffs = db.AcademicStaffs.Include(a => a.AcademicStaffRole).Include(a => a.Department);
            return View(academicstaffs.ToList());
        }
        public ActionResult academicstaffroleindex(AcademicStaffRole academicstaffrole)
        {
            return View("Index",db.AcademicStaffs.ToList().Where(A=>A.AcademicStaffRole.Name.Equals(academicstaffrole.Name)));
        }


        public ActionResult Get_courseStudent_From_Courses(Cours ST)
        {
            return View("Index", null);
        }


        public ActionResult GetStaff_From_Courses(Guid id)
        {
            var d=db.AcademicStaffs.Include(J => J.Courses).Where(K => K.Courses.Any(S => S.CourseID == id)).ToList();

            // var courses = db.Courses.Include(c => c.StudentCourses).Where(c => c.StudentCourses.Any(s => s.StudentID == ST.StudentID)).ToList();
            return View("Index",d );
        }








        public ActionResult Get_Academic_staff_From_Dept(Guid id)
        {
            return View("Index", db.AcademicStaffs.ToList().Where(A => A.DepartmentID==(id)));
        }


        public ActionResult Get_courses_from_academic(Guid id)
        {
          

            return RedirectToAction("Courses_from_academicstaff", "Courses", new { id }); ;
        }





        //
        // GET: /AcademicStaff/Details/5

        public ActionResult Details(Guid id)
        {
            AcademicStaff academicstaff = db.AcademicStaffs.Find(id);
            if (academicstaff == null)
            {
                return HttpNotFound();
            }
            return View(academicstaff);
        }

        //
        // GET: /AcademicStaff/Create

        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.AcademicStaffRoles, "RoleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        //
        // POST: /AcademicStaff/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AcademicStaff academicstaff)
        {
            if (ModelState.IsValid)
            {
                academicstaff.InstructorID = Guid.NewGuid();
                db.AcademicStaffs.Add(academicstaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.AcademicStaffRoles, "RoleID", "Name", academicstaff.RoleID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicstaff.DepartmentID);
            return View(academicstaff);
        }

        //
        // GET: /AcademicStaff/Edit/5

        public ActionResult Edit(Guid id )
        {
            AcademicStaff academicstaff = db.AcademicStaffs.Find(id);
            if (academicstaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.AcademicStaffRoles, "RoleID", "Name", academicstaff.RoleID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicstaff.DepartmentID);
            return View(academicstaff);
        }

        //
        // POST: /AcademicStaff/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AcademicStaff academicstaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicstaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.AcademicStaffRoles, "RoleID", "Name", academicstaff.RoleID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", academicstaff.DepartmentID);
            return View(academicstaff);
        }

        //
        // GET: /AcademicStaff/Delete/5

        public ActionResult Delete(Guid id)
        {
            AcademicStaff academicstaff = db.AcademicStaffs.Find(id);
            if (academicstaff == null)
            {
                return HttpNotFound();
            }
            return View(academicstaff);
        }

        //
        // POST: /AcademicStaff/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AcademicStaff academicstaff = db.AcademicStaffs.Find(id);
            db.AcademicStaffs.Remove(academicstaff);
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