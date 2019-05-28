using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Abstract;
using Models.ViewModel;

namespace Models.Concrete
{
	public class EFAdminRepository:IAdminRepository
	{
		private LessProEntities lpe = new LessProEntities();

		//实现管理员登录
		public AdminLoginStatus LoginIn(string account,string password)
		{
			AdminLoginStatus als = null;
			Admin a = lpe.Admin.Where(e => (e.AdminName == account && e.Password == password)).FirstOrDefault();

			if (a != null)
			{
				als = new AdminLoginStatus()
				{
					admin = a,
					status = true,
				};
			}
			else
			{
				als = new AdminLoginStatus()
				{
					admin = null,
					status = false
				};
			}
			return als;
		}

	}
}
