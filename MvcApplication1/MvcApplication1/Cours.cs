//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cours
    {
        public Cours()
        {
            this.CourseGrades = new HashSet<CourseGrade>();
            this.CourseMaterials = new HashSet<CourseMaterial>();
            this.StudentCourses = new HashSet<StudentCours>();
            this.AcademicStaffs = new HashSet<AcademicStaff>();
        }
    
        public System.Guid CourseID { get; set; }
        public string Name { get; set; }
        public System.Guid SemesterID { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }
        public System.Guid AcademicYearID { get; set; }
    
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual ICollection<CourseGrade> CourseGrades { get; set; }
        public virtual ICollection<CourseMaterial> CourseMaterials { get; set; }
        public virtual Department Department { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual ICollection<StudentCours> StudentCourses { get; set; }
        public virtual ICollection<AcademicStaff> AcademicStaffs { get; set; }
    }
}
