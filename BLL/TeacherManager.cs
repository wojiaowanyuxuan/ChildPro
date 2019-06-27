using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using DALFactory;
using Models;

namespace BLL
{
    public class TeacherManager
    {
        ITeacher iteacher = DataAccess.CreateTeacher();
        public IEnumerable<Teacher> GetTeacher()
        {
            var teacher = iteacher.GetTeacher();
            return teacher;
        }
        public IEnumerable<Teacher> GetTeacher(int? id)
        {
            var teacher = iteacher.GetTeacher(id);
            return teacher;
        }
        public Teacher GetTeacherById(int? id)
        {
            var teacherid = iteacher.GetTeacherById(id);
            return teacherid;
        }
    }
}
