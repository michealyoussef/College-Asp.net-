using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CourseGradesController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /CourseGrades/

        public ActionResult Index()
        {
            var coursegrades = db.CourseGrades.Include(c => c.Cours);
            return View(coursegrades.ToList());
        }

        //
        // GET: /CourseGrades/Details/5

        public ActionResult Get_Grade_From_Courses(Guid id)
        {
            var res = db.CourseGrades.Where(D => D.CourseID == id).ToList();
            return View("index", res);
        }
        public ActionResult Details(Guid id)
        {
            CourseGrade coursegrade = db.CourseGrades.Find(id);
            if (coursegrade == null)
            {
                return HttpNotFound();
            }
            return View(coursegrade);
        }
      

        public ActionResult Get_coursesGrade_From_Courses(Cours ST)
        {

            return View("Index", db.CourseGrades.Where(A => A.CourseID.Equals(ST.CourseID)).ToList());
        }





        //
        // GET: /CourseGrades/Create

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            return View();
        }

        //
        // POST: /CourseGrades/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseGrade coursegrade)
        {
            if (ModelState.IsValid)
            {
                coursegrade.GradeID = Guid.NewGuid();
                db.CourseGrades.Add(coursegrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursegrade.CourseID);
            return View(coursegrade);
        }

        //
        // GET: /CourseGrades/Edit/5

        public ActionResult Edit(Guid id)
        {
            CourseGrade coursegrade = db.CourseGrades.Find(id);
            if (coursegrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursegrade.CourseID);
            return View(coursegrade);
        }

        //
        // POST: /CourseGrades/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseGrade coursegrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursegrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", coursegrade.CourseID);
            return View(coursegrade);
        }

        //
        // GET: /CourseGrades/Delete/5

        public ActionResult Delete(Guid id)
        {
            CourseGrade coursegrade = db.CourseGrades.Find(id);
            if (coursegrade == null)
            {
                return HttpNotFound();
            }
            return View(coursegrade);
        }

        //
        // POST: /CourseGrades/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CourseGrade coursegrade = db.CourseGrades.Find(id);
            db.CourseGrades.Remove(coursegrade);
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