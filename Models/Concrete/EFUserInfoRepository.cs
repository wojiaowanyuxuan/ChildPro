using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Abstract;

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

	}
}
