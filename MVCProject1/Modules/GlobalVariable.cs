using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using MVCProject1.Data;
using MVCProject1.Models;
using MVCProject1.Models.Student;

namespace MVCProject1.GlobalVariable
{
    public static class GlobalVariables
    {
        public static IList<StudentList> StudentData;
        public static int TotalRec;
        public static int TotalPage;

        public static IList<StudentList> SetUp_StudentData
        {
            get { return StudentData; }
            set { StudentData = value; }
        }
        public static int SetUp_TotalRec
        {
            get { return TotalRec; }
            set { TotalRec = value; }
        }
        public static int SetUp_TotalPage
        {
            get { return TotalPage; }
            set { TotalPage = value; }
        }

    }
}
//    namespace GlobalVariables
//{
//    public static class Globals
//    {
//        // parameterless constructor required for static class
//        static Globals() { GlobalInt = 1234; } // default value

//        // public get, and private set for strict access control
//        public static int GlobalInt { get; private set; }

//        // GlobalInt can be changed only via this method
//        public static void SetGlobalInt(int newInt)
//        {
//            GlobalInt = newInt;
//        }
//    }
//}
