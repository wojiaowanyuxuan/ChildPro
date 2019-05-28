using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModel;

namespace Models.Abstract
{
	public interface IUserInfoRepository
	{
		IEnumerable<User> Users { get; }

		//注册接口
		void AddUser(User u);

		//登录接口
		LoginStatus LoginIn(string key, string password);

		//修改用户信息 不包含头像 
		void SaveEditor(int userid,string username = null, string phone = null, string signature = null, string birthday = null, string sex = null);

	}
}
