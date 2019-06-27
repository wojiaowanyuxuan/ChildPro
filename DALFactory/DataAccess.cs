using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using IDAL;

namespace DALFactory
{
	public class DataAccess
	{
		//程序集的名称
		private static string AssemblyName = ConfigurationManager.AppSettings["Path"].ToString();
		//数据库名称
		private static string db = ConfigurationManager.AppSettings["DB"].ToString();

        //具体实现抽象工厂
        public static IUser CreateUser()
        {
            string className = AssemblyName + "." + db + "User";
            return (IUser)Assembly.Load(AssemblyName).CreateInstance(className);
        }

        public static ITeacher CreateTeacher()
        {
            string className = AssemblyName + "." + db + "Teacher";
            return (ITeacher)Assembly.Load(AssemblyName).CreateInstance(className);
        }

        public static ICourse CreateCourse()
        {
            string className = AssemblyName + "." + db + "Course";
            return (ICourse)Assembly.Load(AssemblyName).CreateInstance(className);
        }

        public static IVideo CreateVideo()
        {
            string className = AssemblyName + "." + db + "Video";
            return (IVideo)Assembly.Load(AssemblyName).CreateInstance(className);
        }

        public static ICourse_Class CreateClass()
        {
            string className = AssemblyName + "." + db + "Course_Class";
            return (ICourse_Class)Assembly.Load(AssemblyName).CreateInstance(className);
        }

        public static IComment_Course CreateComment()
        {
            string className = AssemblyName + "." + db + "Comment_Course";
            return (IComment_Course)Assembly.Load(AssemblyName).CreateInstance(className);
        }
        public static IReplys_Course CreateReply()
        {
            string className = AssemblyName + "." + db + "Replys_Course";
            return (IReplys_Course)Assembly.Load(AssemblyName).CreateInstance(className);
        }

        public static IView_CourseAllTable CreateCourseTableAll()
        {
            string className = AssemblyName + "." + db + "View_CourseAllTable";
            return (IView_CourseAllTable)Assembly.Load(AssemblyName).CreateInstance(className);
        }
		public static IProduct SearchProduct()
		{
			string className = AssemblyName + "." + db + "Product";
			return (IProduct)Assembly.Load(AssemblyName).CreateInstance(className);
		}
		public static ICart GetCart_Pro()
		{
			string className = AssemblyName + "." + db + "Cart";
			return (ICart)Assembly.Load(AssemblyName).CreateInstance(className);
		}

		public static IStore GetComment()
		{
			string className = AssemblyName + "." + db + "Comment_Product";
			return (IStore)Assembly.Load(AssemblyName).CreateInstance(className);
		}

	}
}
