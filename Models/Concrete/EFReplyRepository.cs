using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Abstract;
using Models;

namespace Models.Concrete
{
	public class EFReplyRepository:IReplyRepository
	{
		private LessProEntities lpe = new LessProEntities();

		//添加回复
		public void AddReply(int comid, int rep_userid, int rep_to_userid, string date, string rep_content, int rep_typ)
		{
			Replys r = new Replys()
			{
				Com_Id = comid,
				Rep_Content = rep_content,
				Rep_UserId = rep_userid,
				Rep_To_UserId = rep_to_userid,
				Rep_Date = date,
				Rep_Type = rep_typ,
				Rep_Praise_Num = 0
			};
			try
			{
				lpe.Replys.Add(r);
				lpe.SaveChanges();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
