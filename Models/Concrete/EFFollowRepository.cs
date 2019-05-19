using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Abstract;


namespace Models.Concrete
{
	public class EFFollowRepository:IFollowRepository
	{
		private LessProEntities lpe = new LessProEntities();

		
		public void AddAttention(int Followed_id, int Fans_id)
		{
			lpe.Follow.Add(new Follow()
			{
				Followed_Person = Followed_id,
				Fans = Fans_id
			});
			lpe.SaveChanges();
		}
		//获取我的粉丝
		public IEnumerable<Follow> GetAllFans(int userid)
		{
			return lpe.Follow.Include("User").Where(e => e.Followed_Person == userid);
		}
		//获取我的关注  
		public IEnumerable<Follow> GetAllAttention(int userid)
		{
			return lpe.Follow.Include("User").Where(e => e.Fans == userid);
		}
	}
}
