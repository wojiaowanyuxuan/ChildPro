using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Abstract;
using ChildPro.Models;
using Models.ViewModel;


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
		[HttpGet]
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public JsonResult Index(string key,string password)
		{
			tip t = null;
			LoginStatus ls = UIRepository.LoginIn(key, password);
			if (ls.status)
			{
				t = new tip
				{
					message = "登陆成功",
					code = 200
				};
				Session["user"] = ls.user;
				Session["userid"] = ls.user.UserID;
			}
			else
			{
				t = new tip
				{
					message = "密码错误",
					code = 500
				};
			}
			return base.Json(t);
		}
    }
}