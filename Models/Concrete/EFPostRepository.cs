using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Abstract;

namespace Models.Concrete
{
	public class EFPostRepository:IPostRepository
	{
		private LessProEntities LPE = new LessProEntities();

		public IEnumerable<Post> Posts
		{
			get
			{
				return LPE.Post;
			}
		}

		//添加帖子
		public void WritePost(Post p)
		{
			if(p.PostID == 0)
			{
				LPE.Post.Add(p);
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
	}
}
