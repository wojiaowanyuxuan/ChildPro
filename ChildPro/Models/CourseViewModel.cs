using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ChildPro.Models
{
    public class CourseViewModel
    {
        public IEnumerable<Course> course { get; set; } 
        public IEnumerable<Course_Class> courseclass { get; set; }
        public IEnumerable<Video> video { get; set; } 
        public IEnumerable<Teacher> teachers { get; set; }
        public IEnumerable<CourseType> coursetype { get; set; }
        //获取一个评论下的所有回复
        public Comment_Course com { get; set; }
        public IEnumerable<Replys_Course> rep { get; set; }
        public IList<Replys_Course> ps;       
        
        public Course coursetable { get; set; }           
        public Teacher teacher { get; set; }
        public User user { get; set; }
        public bool isC { get; set; }
        public bool isJ { get; set; }

        //最新课程
        public IEnumerable<Course> GetNewCourse { get; set; }
        //推荐课程
        public IEnumerable<Course> GetRecommendCourse { get; set; }
        //最热课程
        public IEnumerable<Course> GetHotCourse { get; set; }
        //免费课程
        public IEnumerable<Course> GetFreeCourse { get; set; }

    }
}