using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Abstract;


namespace ChildPro.Controllers
{
    public class LoginController : Controller
    {
		private IUserInfoRepository UIRepository;
		//解析依赖项
		public LoginController(IUserInfoRepository rep)
		{
			UIRepository = rep;
		}
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
    }
}