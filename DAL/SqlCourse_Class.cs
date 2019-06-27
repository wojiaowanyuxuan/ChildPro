using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using IDAL;

namespace DAL
{
    public class SqlCourse_Class : ICourse_Class
    {
        LessProEntities dbContext = new LessProEntities();
        public IEnumerable<Course_Class> GetClass()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course_Class> GetClassById(int? id)
        {
            var classid = from c in dbContext.Course_Class
                          where c.ClassID == id
                          select c;
            return (classid);
        }

        public Course_Class GetClassId(int? id)
        {
            var classid1 = dbContext.Course_Class.Include("Course").Single(c => c.ClassID == id);
            return (classid1);
        }

        public IEnumerable<Course_Class> GetCourseById(int? id)
        {
            var Courseid = from c in dbContext.Course_Class
                           where c.CourseId == id
                           select c;
            return (Courseid);
        }
    }
}
