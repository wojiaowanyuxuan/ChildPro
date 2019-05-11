using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Abstract;
using Models.ViewModel;

namespace Models.Concrete
{
	public class EFUserInfoRepository:IUserInfoRepository
	{
		private LessProEntities LPE = new LessProEntities();

		public IEnumerable<User> Users
		{
			get
			{
				return LPE.User;
			}
		}

		//注册
		public void AddUser(User u)
		{
			if (u.UserID == 0)
			{
				LPE.User.Add(u);
			}
			try
			{
				LPE.SaveChanges();
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		//登录
		public LoginStatus LoginIn(string key,string password)
		{
			LoginStatus ls = null;
			User u = LPE.User.Where(e => (e.Phone == key || e.Email == key)).FirstOrDefault();
			if (u != null && (u.Password == password))
			{
				ls = new LoginStatus()
				{
					status = true,
					user = u
				};
			}
			else
			{
				ls = new LoginStatus()
				{
					status = false,
					user = null
				};
			}
			return ls;
		}

	}
}
