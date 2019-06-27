using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DALFactory;
using IDAL;

namespace BLL
{
    public class Course_ClassManager
    {
        ICourse_Class iclass = DataAccess.CreateClass();

        public IEnumerable<Course_Class> GetClassById(int? id)
        {
            var classid = iclass.GetClassById(id);
            return (classid);
        }

        public IEnumerable<Course_Class> GetCourseById(int? id)
        {
            var Courseid = iclass.GetCourseById(id);
            return (Courseid);
        }

        public Course_Class GetClassId(int? id)
        {
            var classid1 = iclass.GetClassId(id);
            return (classid1);
        }
    }
}
