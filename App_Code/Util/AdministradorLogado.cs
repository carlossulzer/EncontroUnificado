using System.Web;

namespace Util
{
	/// <summary>
	/// Summary description for AdministradorLogado.
	/// </summary>
	public class AdministradorLogado
	{
		public static string LoginAdministrador
		{
			get
			{
				return HttpContext.Current.Session["administradorLogado"].ToString();
			}
			set
			{
				HttpContext.Current.Session["administradorLogado"] = value;
			}
			}
	}
}

