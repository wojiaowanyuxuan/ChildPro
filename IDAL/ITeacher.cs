using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface ITeacher
    {
        //获取教师信息
        IEnumerable<Teacher> GetTeacher();
        IEnumerable<Teacher> GetTeacher(int? id);
        Teacher GetTeacherById(int? id);
    }
}
