using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace MvcApplication1.Controllers
{
    public class StudentGradeController : Controller
    {
        private CollegeSystemEntities db = new CollegeSystemEntities();

        //
        // GET: /StudentGrade/

        public ActionResult From_student_to_Grades(Guid id)
        {
            return View("index", db.StudentGrades.Where(y => y.StudentID == id));
        }


        public ActionResult Index(Guid id)
        {
            ViewBag.StudentID = id;
            List<StudentGrade> StudentGrade = db.StudentGrades.Where(x => x.StudentID == id).ToList();

            return View(StudentGrade);
        }

        //
        // GET: /StudentGrades/Details/5

        public ActionResult Details(Guid id)
        {
            StudentGrade studentgrade = db.StudentGrades.Find(id);
            
            if (studentgrade == null)
            {
                return HttpNotFound();
            }
            return View(studentgrade);
        }

        public ActionResult Courses(string CourseID)
        {
            SelectList CourseGrade = new SelectList(db.CourseGrades.Where(x => x.CourseID == new Guid(CourseID)).ToList(), "GradeID", "Name");
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(CourseGrade, Formatting.Indented, jsonSerializerSettings);
            return Content(json, "application/json");
        }
        public ActionResult Grades(string CourseID)
        {
            List<Guid> GradeIDS = db.CourseGrades.Where(x => x.CourseID == new Guid(CourseID)).Select(x => x.GradeID).ToList();
            List<string> Marks = new List<string>();
            Guid GradID = new Guid();
            for (int i = 0; i < GradeIDS.Count; i++)
            {
                GradID = GradeIDS[i];
                Marks.Add(db.StudentGrades.Single(x => x.GradeID == GradID).Mark.ToString() + " " + GradID.ToString());
            }
            Marks.Add(db.StudentGrades.Single(x => x.GradeID == GradID).Year.ToString());
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(Marks, Formatting.Indented, jsonSerializerSettings);
            return Content(json, "application/json");
        }
        public ActionResult Create(Guid id)
        {
            List<StudentCours> studentcourses = db.StudentCourses.Where(x => x.StudentID == id).ToList();
            List<Cours> Courses = new List<Cours>();
            for (int i = 0; i < studentcourses.Count; i++)
            {
                Courses.Add(studentcourses[i].Cours);
            }
            SelectList CourseID = new SelectList(Courses, "CourseID", "Name");
            ViewBag.CourseID = CourseID;
            ViewBag.StudentID = id;
            return View();
        }

        //
        // POST: /StudentGrades/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentGrade studentgrade, FormCollection formcollection)
        {

            if (ModelState.IsValid)
            {
                for (int i = 3; i < formcollection.Keys.Count; i++)
                {
                    StudentGrade StudentGrade = new StudentGrade();
                    Guid GradeID = new Guid(formcollection.AllKeys[i]);
                    try
                    {
                        StudentGrade = db.StudentGrades.Single(x => x.GradeID == GradeID);
                        StudentGrade.StudentID = studentgrade.StudentID;
                        StudentGrade.Year = int.Parse(formcollection[formcollection.AllKeys[2]]);
                        StudentGrade.GradeID = GradeID;
                        StudentGrade.Mark = Convert.ToDecimal(formcollection[formcollection.AllKeys[i]]);
                        StudentGrade.Student = db.Students.Single(x => x.StudentID == studentgrade.StudentID);
                        db.Entry(StudentGrade).State = EntityState.Modified;
                    }
                    catch
                    {
                        StudentGrade.StudentID = studentgrade.StudentID;
                        StudentGrade.Year = int.Parse(formcollection[formcollection.AllKeys[2]]);
                        StudentGrade.GradeID = GradeID;
                        StudentGrade.Mark = Convert.ToDecimal(formcollection[formcollection.AllKeys[i]]);
                        StudentGrade.Student = db.Students.Single(x => x.StudentID == studentgrade.StudentID);
                        db.StudentGrades.Add(StudentGrade);
                    }



                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { id = studentgrade.StudentID });

        }

        //
        // GET: /StudentGrades/Edit/5

        public ActionResult Edit(Guid StudentID)
        {
            List<StudentCours> studentcourses = db.StudentCourses.Where(x => x.StudentID == StudentID).ToList();
            List<Cours> Courses = new List<Cours>();
            for (int i = 0; i < studentcourses.Count; i++)
            {
                Courses.Add(studentcourses[i].Cours);
            }
            SelectList CourseID = new SelectList(Courses, "CourseID", "Name");
            ViewBag.CourseID = CourseID;
            ViewBag.StudentID = StudentID;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentGrade id, FormCollection formcollection)
        {
            if (ModelState.IsValid)
            {

                for (int i = 3; i < formcollection.Keys.Count; i++)
                {
                    StudentGrade StudentGrade = new StudentGrade();
                    StudentGrade.StudentID = id.StudentID;
                    StudentGrade.Year = int.Parse(formcollection[formcollection.AllKeys[2]]);
                    StudentGrade.GradeID = new Guid(formcollection.AllKeys[i]);
                    StudentGrade.Mark = Convert.ToDecimal(formcollection[formcollection.AllKeys[i]]);
                    StudentGrade.Student = db.Students.Single(x => x.StudentID == id.StudentID);
                    db.Entry(StudentGrade).State = EntityState.Modified;

                }


                db.SaveChanges();
                return RedirectToAction("Index", new { StudentID = id.StudentID });
            }
            ViewBag.GradeID = new SelectList(db.CourseGrades, "GradeID", "Name", id.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", id.StudentID);
            return View(id);
        }

        //
        // GET: /StudentGrades/Delete/5

        public ActionResult Delete(Guid id)
        {
          
            List<StudentGrade> StudentGrades = db.StudentGrades.Where(x => x.StudentID == id).ToList();
            List<Cours> Courses = new List<Cours>();
            for (int i = 0; i < StudentGrades.Count; i++)
            {
                Guid temp = StudentGrades[i].GradeID;
                Guid ID = db.CourseGrades.Single(x => x.GradeID == temp).CourseID;
                Courses.Add(db.Courses.Single(x => x.CourseID == ID));
            }
            Courses = Courses.Distinct<Cours>().ToList();
            SelectList CourseID = new SelectList(Courses, "CourseID", "Name");
            ViewBag.CourseID = CourseID;
            ViewBag.StudentID = id;
            return View();
        }

        //
        // POST: /StudentGrades/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(FormCollection formcollection, Guid StudentID)
        {
            string ID = formcollection["CourseID"];
            List<CourseGrade> CourseGrade = db.CourseGrades.Where(x => x.CourseID == new Guid(ID)).ToList();
            for (int i = 0; i < CourseGrade.Count; i++)
            {
                Guid GradeID = CourseGrade[i].GradeID;
                StudentGrade studentgrade = db.StudentGrades.Single(x => x.GradeID == GradeID);
                db.StudentGrades.Remove(studentgrade);
            }

            db.SaveChanges();
            return RedirectToAction("Index", new { StudentID = StudentID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}