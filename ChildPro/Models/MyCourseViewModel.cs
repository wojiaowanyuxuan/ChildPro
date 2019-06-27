using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ChildPro.Models
{
    public class MyCourseViewModel
    {
        public IEnumerable<User> Uses1 { get; set; }
        public User User { get; set; } //用来修改资料
        public int UserA { get; set; }  //关注数
        public IEnumerable<View_Guanzhu> UsesAa { get; set; }
        public int UserB { get; set; }   //粉丝数
        public IEnumerable<View_Guanzhu> UsesBb { get; set; }
        public IEnumerable <Add_Course> courses { get; set; }
        public IEnumerable <Collect> collect_courses { get; set; }

    }
}