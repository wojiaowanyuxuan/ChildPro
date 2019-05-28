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
    public class AdminController : Controller
    {
		private LessProEntities lpe = new LessProEntities();
		private IAdminRepository admin_repository;
		//解析依赖项
		public AdminController(IAdminRepository rep)
		{
			admin_repository = rep;
		}
        // GET: Admin
		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Login(string account,string password)
		{
			tip t = null;
			AdminLoginStatus als = admin_repository.LoginIn(account, password);
			if (als.status)
			{
				t = new tip()
				{
					message = "登陆成功",
					code = 200
				};
				//将管理员加入当前会话
				Session["admin"] = als.admin;
			}
			else
			{
				t = new tip()
				{
					message = "登陆失败",
					code = 500
				};
			}
			return base.Json(t);
		}

		//退出登录
		public RedirectToRouteResult LoginOut()
		{
			Session["admin"] = null;
			return RedirectToRoute(new { controller = "Home", action = "Index" });
		}

		//修改帖子访问权限
		[HttpPost]
		public JsonResult ModifyAccess(int accessibility,int postid,int userid)
		{
			tip t = new tip()
			{
				message = "帖子权限已被修改",
				code = 200
			};
			Post p = lpe.Post.Where(e => e.PostID == postid).FirstOrDefault();
			if (p != null)
			{
				p.Accessibility = accessibility;
				lpe.SaveChanges();
			}
			return base.Json(t);
		}
    }
}