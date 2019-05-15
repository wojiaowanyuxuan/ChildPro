using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Abstract
{
	public interface IReplyRepository
	{
		void AddReply(int comid,int rep_userid,int rep_to_userid,string date,string rep_content,int rep_type);
	}
}
