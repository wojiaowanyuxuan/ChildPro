using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Abstract;

namespace Models.Concrete
{
	public class EFPraisedRepository:IPraisedRepository
	{
		private LessProEntities lpe = new LessProEntities();

		//对评论点赞
		public void ForCom(int creNum,int comid,int userid)
		{
			Praised p;
			Comment c = lpe.Comment.Where(e => e.CommentID == comid).FirstOrDefault();
			c.Com_Praise_Num += creNum;
			if(creNum == 1)
			{
				//creNum为1表示点赞
				p = new Praised()
				{
					User_Id = userid,
					Com_Id = comid
				};
				lpe.Praised.Add(p);
			}
			else
			{
				//否则表示取消点赞
				p = lpe.Praised.Where(e => (e.Com_Id == comid && e.User_Id == userid)).FirstOrDefault();
				lpe.Praised.Remove(p);
			}
			try
			{
				lpe.SaveChanges();
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		//对回复的点赞
		public void ForRep(int creNum,int repid,int userid)
		{
			Praised p;
			Replys R = lpe.Replys.Where(e => e.ReplyID == repid).FirstOrDefault();
			R.Rep_Praise_Num += creNum;

			if (creNum == 1)
			{
				p = new Praised()
				{
					Rep_Id = repid,
					User_Id = userid
				};
				lpe.Praised.Add(p);
			}
			else
			{
				//否则取消点赞回复
				p = lpe.Praised.Where(e => (e.Rep_Id == repid && e.User_Id == userid)).FirstOrDefault();
				lpe.Praised.Remove(p);
			}
			try
			{
				lpe.SaveChanges();
			}
			catch(Exception e)
			{
				throw e;
			}
		}
	}
}
