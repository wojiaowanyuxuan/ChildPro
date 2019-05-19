using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Abstract
{
	public interface IFollowRepository
	{
		//关注用户
		void AddAttention(int Followed_id,int Fans_id);

		//获取当前用户所有粉丝
		IEnumerable<Follow> GetAllFans(int userid);

		//获取用户所有关注
		IEnumerable<Follow> GetAllAttention(int userid);
	}
}
