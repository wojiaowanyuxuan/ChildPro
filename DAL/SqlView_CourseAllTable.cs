using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using IDAL;

namespace DAL
{
    public class SqlView_CourseAllTable:IView_CourseAllTable
    {
        LessProEntities dbContext = new LessProEntities();
        //获取课程
        public IEnumerable<View_CourseAllTable> GetCourse()
        {
            var courses = dbContext.View_CourseAllTable.ToList();
            return courses;
        }
    }
}
