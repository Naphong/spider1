using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace MVCProject1.Models.Student
{
    public class StudentViewModels
    {
        public List<StudentList> StudentList { get; set; }
        public List<SelectListItem> StatusDropdownList { get; set; }

        public List<StudentList> StudentAllList { get; set; }
    }

    public class StudentList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class StudentReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class StudentReportList
    {
        public List<StudentReport> _StudentReport { get; set; }
    }

  
}
