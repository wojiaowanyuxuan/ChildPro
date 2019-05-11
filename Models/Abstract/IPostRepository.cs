using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Abstract
{
	public interface IPostRepository
	{
		IEnumerable<Post> Posts { get; }

		void WritePost(Post p);
	}
}
