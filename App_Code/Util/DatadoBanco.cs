using System;
using DAO;
using System.Text;

namespace UTIL
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class DatadoBanco
	{
		public static DateTime ObterDataBanco()
		{
			string sql = "select getdate()";
			//BDAdapter bd = BDFactory.GetAdapter();
			//return bd.GetDate(sql);
            return DateTime.Now.Date;
		}
	}
}
