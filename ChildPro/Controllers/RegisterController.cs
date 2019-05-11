using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Abstract;
using ChildPro.Models;

namespace ChildPro.Controllers
{
	public class RegisterController : Controller
	{
		private IUserInfoRepository UIRepository;
		public RegisterController(IUserInfoRepository rep)
		{
			UIRepository = rep;
		}
		// GET: Register
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Index(string UserName, string Phone, string Email, string Password)
		{
			User u = new User()
			{
				UserName = UserName,
				Email = Email,
				Phone = Phone,
				Password = Password
			};
			//调用注册方法
			UIRepository.AddUser(u);
			tip t = new tip
			{
				message = "注册成功",
				code = 200
			};
			return base.Json(t);

		}
	}
}
