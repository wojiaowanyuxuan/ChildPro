using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface ICourse_Class
    {
       
        IEnumerable<Course_Class> GetClass();
        Course_Class GetClassId(int? id);
        IEnumerable<Course_Class> GetClassById(int? id);

        IEnumerable<Course_Class> GetCourseById(int? id);
    }
}
