using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface ICourse
    {
        //获取所有课程
        IEnumerable<Course> GetCourse();
        //获取课程id的集合
        IEnumerable<Course> GetCourseById(int? id);

        //获取课程id
        Course GetCourseById1(int? id);
        //通过课程获得类别id
        IEnumerable<Course> GetTypeById(int? id);

        IEnumerable<Course> GetTeacherById(int? id);
        //获取课程评论对应的课程ID
        IQueryable<Comment_Course> GetComment_CourseByCourseId();

        //获取所有课程
        IEnumerable<Course> GetMostCourse();

        //获取最新课程
        IEnumerable<Course> GetNewCourse();

        //获取最热课程
        IEnumerable<Course> GetHotCourse();

        void RemoveCourse(Course course);
        void AddCourse(Course course);
        void EditVideo(Course course);
        void RemoveRangeCommentCourse(IQueryable<Comment_Course> comcourse);

    }
}
