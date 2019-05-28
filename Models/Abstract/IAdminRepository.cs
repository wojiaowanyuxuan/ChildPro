using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModel;

namespace Models.Abstract
{
	public interface IAdminRepository
	{
		//验证管理员登录
		AdminLoginStatus LoginIn(string account, string password);

	}
}
