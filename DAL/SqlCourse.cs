using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using IDAL;

namespace DAL
{
    //课程
    public class SqlCourse:ICourse
    {
        LessProEntities dbContext = new LessProEntities();
        //获取课程
        public IEnumerable<Course> GetCourse()
        {
            var courses = dbContext.Course.ToList();
            return courses;
        }
        //获取课程id
        public Course GetCourseById1(int? id)
        {
            var course1 = dbContext.Course.Include("Course_Class").Single(c => c.CourseID == id);
            return course1;
        }
        //获取课程id集合
        public IEnumerable<Course> GetCourseById(int? id)
        {
            var course = from c in dbContext.Course
                         where c.CourseID == id
                         select c;
            return course;
        }
        //通过课程来获取类别id
        public IEnumerable<Course> GetTypeById(int? id)
        {
            var typeid = from c in dbContext.Course
                         where c.CourseTypeID == id
                         select c;
            return (typeid);
        }
        //通过课程来获取教师id
        public IEnumerable<Course> GetTeacherById(int? id)
        {
            var teacherid = from c in dbContext.Course
                            where c.TeacherId == id
                            select c;
            return (teacherid);
        }

        public IQueryable<Comment_Course> GetComment_CourseByCourseId()
        {
            throw new NotImplementedException();
        }

        public void RemoveCourse(Course course)
        {
            dbContext.Course.Remove(course);
            dbContext.SaveChanges();
        }

        public void AddCourse(Course course)
        {
            dbContext.Course.Add(course);
            dbContext.SaveChanges();
        }

        public void EditVideo(Course course)
        {
            dbContext.Entry(course).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void RemoveRangeCommentCourse(IQueryable<Comment_Course> comcourse)
        {
            throw new NotImplementedException();
        }

        //获取最新课程
        public IEnumerable<Course> GetNewCourse()
        {
            var newcourse = from c in  dbContext.Course
                           orderby c.Course_Time descending
                           select c;
            return newcourse;
        }

        public IEnumerable<Course> GetHotCourse()
        {
            var hotcourse = from c in dbContext.Course
                            orderby c.Number descending
                            select c;
            return hotcourse;
        }

        public IEnumerable<Course> GetMostCourse()
        {
            
            var comall = dbContext.Comment_Course.Count();
            var mostcourse = from c in dbContext.Course
                             orderby comall descending
                             select c;
            return mostcourse;
        }
    }
}
