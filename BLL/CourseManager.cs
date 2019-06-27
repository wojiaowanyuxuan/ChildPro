using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DALFactory;
using IDAL;

//业务逻辑层：分离数据与实现,利用抽象工厂直接调用DAL实现代码
namespace BLL 
{
    public class CourseManager
    {
        ICourse icourse = DataAccess.CreateCourse();
        //获取课程
        public IEnumerable<Course> GetCourse()
        {
            var courses = icourse.GetCourse();
            return courses;
        }
     
        //获取课程id集合
        public IEnumerable<Course> GetCourseById(int? id)
        {
            var courseid = icourse.GetCourseById(id);
            return courseid;
        }

        //获取课程id
        public Course GetCourseById1(int? id)
        {
            var courseid1 = icourse.GetCourseById1(id);
            return courseid1;
        }
        //通过课程来获取类别id
        public IEnumerable<Course> GetTypeById(int? id)
        {
            var typeid = icourse.GetTypeById(id);
            return (typeid);
        }
        //通过课程来获取教师id
        public IEnumerable<Course> GetTeacherById(int? id)
        {
            var teacherid = icourse.GetTeacherById(id);
            return (teacherid);
        }

        //获取评论最多的课程
        public IEnumerable<Course> GetMostCourse()
        {
            var mostcourse = icourse.GetMostCourse();
            return mostcourse;
        }
        //获取最新课程
        public IEnumerable<Course> GetNewCourse()
        {
            var newcourse = icourse.GetNewCourse();
            return newcourse;
        }
        public IEnumerable<Course> GetHotCourse()
        {
            var hotcourse = icourse.GetHotCourse();
            return hotcourse;
        }
    }
}
