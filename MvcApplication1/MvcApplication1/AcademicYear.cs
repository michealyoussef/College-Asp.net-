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
    
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            this.Semesters = new HashSet<Semester>();
            this.AcademicYearSections = new HashSet<AcademicYearSection>();
            this.Courses = new HashSet<Cours>();
            this.Departments = new HashSet<Department>();
            this.Students = new HashSet<Student>();
        }
    
        public System.Guid AcademicYearID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<AcademicYearSection> AcademicYearSections { get; set; }
        public virtual ICollection<Cours> Courses { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}