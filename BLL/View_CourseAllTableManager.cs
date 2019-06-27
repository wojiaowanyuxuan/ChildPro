using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;
using DALFactory;

namespace BLL
{
    public class View_CourseAllTableManager
    {
        IView_CourseAllTable iview = DataAccess.CreateCourseTableAll();
        //获取课程
        public IEnumerable<View_CourseAllTable> GetCourse()
        {
            var courses = iview.GetCourse();
            return courses;
        }
    }
}
