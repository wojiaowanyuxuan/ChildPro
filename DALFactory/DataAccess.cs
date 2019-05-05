using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DALFactory
{
	public class DataAccess
	{
		//程序集的名称
		private static string AssemblyName = ConfigurationManager.AppSettings["Path"].ToString();
		//数据库名称
		private static string db = ConfigurationManager.AppSettings["DB"].ToString();

	}
}
