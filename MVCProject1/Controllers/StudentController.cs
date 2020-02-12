using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AccountStorageTest.Models;
using System.Diagnostics;
using System.Collections.Generic;
using MVCProject1.GlobalVariable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using MVCProject1.Data;
using MVCProject1.Models.Student;
using System.Linq.Dynamic;

namespace MVCProject1.Controllers
{

    public class StudentController : Controller
    {
        int numberPerPage = 10;
        int MaxPaging = 5;
        public StudentController()
        {
        }

        public ActionResult Index()
        {
            //List<StudentReport> StudentList = new List<StudentReport>();
            StudentViewModels StudentViewModels = new StudentViewModels();
            List<StudentList> StudentList = new List<StudentList>();
            //using (DataModel1Entities _entity = new DataModel1Entities())
            //{
            //    StudentList = _entity.StudentDetails.Select(x => new StudentList
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Age = x.Age,
            //        City = x.City,
            //        Gender = x.Gender
            //    }).ToList();
            //}



            //CourselogList = (from c1 in CourseResults select new Courselog { Id = c1.Id, Name = c1.Name }).ToList();
            //List<SelectListItem> CourseDropdownList = new List<SelectListItem>();
            //CourseDropdownList.Add(new SelectListItem { Text = "", Value = "", Selected = true });
            //foreach (var CourselogListItem in CourselogList)
            //{
            //    CourseDropdownList.Add(new SelectListItem { Text = CourselogListItem.Id, Value = CourselogListItem.Id });
            //}
            List<SelectListItem> StatusDropdownList = new List<SelectListItem>();
            StatusDropdownList.Add(new SelectListItem { Text = "", Value = "" ,Selected = true });
            StatusDropdownList.Add(new SelectListItem { Text ="Pending", Value = "0" });
            StatusDropdownList.Add(new SelectListItem { Text = "Cancel", Value = "1" });
            StatusDropdownList.Add(new SelectListItem { Text = "Completed", Value = "2" });

            //StudentViewModels.CourseDropdownList = CourseDropdownList;
            StudentViewModels.StatusDropdownList = StatusDropdownList;
            StudentViewModels.StudentList = StudentList;
            return View(StudentViewModels);
        }



    //  return View();
    public PartialViewResult GetStudentFilter(string page,string startdate, string enddate, string name, string city, string email, string email2, string email3,string statusdropdown)
        {
            GlobalVariables.SetUp_StudentData = null;
            GlobalVariables.SetUp_TotalPage = 0;
            ViewBag.NumPerPage = numberPerPage;
            ViewBag.MaxPaging = MaxPaging;
            ViewBag.StartPage = 1;
            ViewBag.CurPage = 1;
            ViewBag.NumRecords = 0;
            int pagenumber;

            StudentViewModels StudentViewModels = new StudentViewModels();
            IList<StudentList> _studentList = new List<StudentList>();


            string condition = "";
            if (name != "")
            {
                condition = "Name = " + name;
                //_studentList = _studentList.Where(condition)
            }

            if (GlobalVariables.SetUp_StudentData != null)
            {
                _studentList = GlobalVariables.SetUp_StudentData;
            }
            else
            {
                using (DataModel1Entities _entity = new DataModel1Entities())
                {
                    _studentList = _entity.StudentDetails.Select(x => new StudentList
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Age = x.Age,
                        City = x.City,
                        Gender = x.Gender
                    }).ToList();
                }
            }

            GlobalVariables.SetUp_StudentData = null;
            GlobalVariables.SetUp_TotalPage = 0;
            ViewBag.NumPerPage = numberPerPage;
            ViewBag.MaxPaging = MaxPaging;
            ViewBag.StartPage = 1;
            ViewBag.CurPage = 1;
            ViewBag.NumRecords = 0;

            email = email.Trim();
            email2 = email2.Trim();
            email3 = email3.Trim();
            name = name.Trim();
            statusdropdown = statusdropdown.Trim();
            //int pagenumber;
            if (page == null)
            {
                pagenumber = 1;
            }
            else
            {
                pagenumber = Convert.ToInt16(page);
            }

            object[] paramValues = new object[10];// new object[] { name, city};
            int i = 0;
            if (name != "")
            {
                
                condition = "Name == @0 and ";
                paramValues[i] = name;
                i = i + 1;
                //Values. = name;
            }
            if (city != "")
            {
                condition += "city == @1";
                paramValues[i] = city;
                i = i + 1;
            }


            if (condition.Length > 0)
            {
                _studentList = _studentList.Where(condition, paramValues).ToList();
            }



            _studentList = _studentList.OrderBy(d => d.Name).ToList();

            GlobalVariables.SetUp_StudentData = _studentList.ToList();
            if (pagenumber > 1)
            {
                StudentViewModels.StudentList = _studentList.Skip(numberPerPage * pagenumber).Take(numberPerPage).ToList();
            }
            else
            {
                StudentViewModels.StudentList = _studentList.Take(numberPerPage).ToList();
            }

            StudentViewModels.StudentAllList = _studentList.ToList();
            GlobalVariables.SetUp_TotalRec = _studentList.Count();
            int totalPages = Convert.ToInt16(_studentList.Count() / (decimal)numberPerPage);
            GlobalVariables.SetUp_TotalPage = totalPages;
            ViewBag.NumPerPage = numberPerPage;
            ViewBag.MaxPaging = MaxPaging;
            ViewBag.StartPage = 1;
            ViewBag.CurPage = 1;
            ViewBag.NumRecords = _studentList.Count();
            return PartialView("result", StudentViewModels);
        }


        public PartialViewResult GetStudentFilterPaging(string skip,string page)
        {
            int StartPage = 1;
            int skipnum;
            int CurPage = 0;
            CurPage = Convert.ToInt16(page);
            if (skip == null)
            {
                skipnum = 1;
            }
            else
            {
                skipnum = Convert.ToInt16(skip);
                //1 >skip 0 //2 >skip 3 //3> skip 6 (3x2) //4> skip 12 (4x3)
            }
            if (CurPage >= GlobalVariables.SetUp_TotalPage)
            {
                 StartPage = (GlobalVariables.SetUp_TotalPage - (MaxPaging))+2;
            }
            if (CurPage >= MaxPaging && CurPage < GlobalVariables.SetUp_TotalPage)
            {
                if (GlobalVariables.SetUp_TotalPage < (CurPage + MaxPaging)-1)
                {
                    if ((GlobalVariables.SetUp_TotalPage % MaxPaging) > 0)
                    {
                        StartPage = (CurPage - (MaxPaging / 2));
                    }
                    else
                    {
                        if (GlobalVariables.SetUp_TotalPage == MaxPaging)
                        {
                            StartPage = GlobalVariables.SetUp_TotalPage - (MaxPaging - 1);
                        }
                        else
                        {
                            if (GlobalVariables.SetUp_TotalPage - CurPage <= 5)
                            {
                                StartPage = GlobalVariables.SetUp_TotalPage - (MaxPaging-1);
                            }
                            else
                            {
                                StartPage = (CurPage - (MaxPaging / 2));
                            }
                            //StartPage = GlobalVariables.SetUp_TotalPage - (MaxPaging);
                        }
                    }
                }
                else
                {
                    StartPage = (CurPage - (MaxPaging / 2));
                }
            }
            if (CurPage < 5)
            {
                StartPage =1;
            }

            IEnumerable<StudentList> StudentInfo=null;
            StudentViewModels StudentViewModels = new StudentViewModels();
            if (GlobalVariables.SetUp_StudentData != null)
            {
                StudentInfo = GlobalVariables.SetUp_StudentData;
                StudentInfo = StudentInfo.OrderBy(d => d.Name).ToList();
            }

            if (skipnum > 1)
            {
                StudentViewModels.StudentList = StudentInfo.Skip(skipnum).Take(numberPerPage).ToList();
            }
            else
            {
                StudentViewModels.StudentList = StudentInfo.Take(numberPerPage).ToList();
            }
            StudentViewModels.StudentAllList = StudentInfo.ToList();
            ViewBag.NumPerPage = numberPerPage;
            ViewBag.MaxPaging = MaxPaging;
            ViewBag.StartPage = StartPage;
            ViewBag.CurPage = CurPage;
            ViewBag.NumRecords = StudentInfo.Count();
            return PartialView("result", StudentViewModels);
        }

    }
}