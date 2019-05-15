using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.Abstract
{
	public interface IPraisedRepository
	{ 
		void ForCom(int creNum, int comid, int userid);

		void ForRep(int creNum, int repid, int userid);
	}
}
