using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Abstract;
using Models;


namespace ChildPro.Controllers
{
    public class HomeController : Controller
    {
		private IUserInfoRepository UIRepository;
		
		//解析依赖项 生成实现了该参数接口的实例
		public HomeController(IUserInfoRepository rep)
		{
			UIRepository = rep;
		}

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}