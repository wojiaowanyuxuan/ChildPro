using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    public class SqlTeacher : ITeacher
    {
        //DAL层中需要添加ef的两个引用

        LessProEntities dbContext = new LessProEntities();
        public IEnumerable<Teacher> GetTeacher()
        {
            var teacher = dbContext.Teacher.ToList();
            return teacher;
        }

        public IEnumerable<Teacher> GetTeacher(int? id)
        {
            var teacher = from t in dbContext.Teacher
                          where t.TeacherID == id
                          select t;
            return teacher;
        }

        public Teacher GetTeacherById(int? id)
        {
            var teacherid = dbContext.Teacher.Include("Course").Single(c => c.TeacherID== id);
            return teacherid;
        }
    }
}
