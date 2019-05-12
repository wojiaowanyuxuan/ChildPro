using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Abstract;

namespace ChildPro.Controllers
{
    public class UserController : Controller
    {
		private IUserInfoRepository user_repository;
		//解析依赖项 
		public UserController(IUserInfoRepository rep)
		{
			user_repository = rep;
		}

        // GET: User
        public ActionResult Index()
        {
            return View();
        }


		//获取自定义头像
		public FileContentResult GetImage(int userid)
		{
			User u = user_repository.Users.Where(e => e.UserID == userid).FirstOrDefault();
			if (u != null)
			{
				//将二进制数据转换为图像
				return File(u.ImageData, u.ImageMineType);
			}
			else
			{
				return null;
			}
		}
    }
}