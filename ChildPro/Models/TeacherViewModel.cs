using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ChildPro.Models
{
    public class TeacherViewModel
    {
        public Teacher teachers { get; set; }
        public Course course { get; set; }
        public IEnumerable<Course> courses { get; set; }
    }
}