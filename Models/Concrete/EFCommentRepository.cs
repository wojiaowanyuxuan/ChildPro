using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Abstract;

namespace Models.Concrete
{
	public class EFCommentRepository:ICommentRepository
	{
		private LessProEntities lpe = new LessProEntities();
		//增加评论
		public void AddComment(int userid, int postid, string date, string com_content)
		{
			Comment c = new Comment()
			{
				User_Id = userid,
				Com_Content = com_content,
				Com_Date = date,
				Post_Id = postid,
				Com_Praise_Num = 0
			};
			try
			{
				lpe.Comment.Add(c);
				lpe.SaveChanges();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
