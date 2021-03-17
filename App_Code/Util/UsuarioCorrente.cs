using System.Web;

namespace Util
{
	public class UsuarioCorrente
	{
		public static string Login
		{
			get
			{
				return HttpContext.Current.Session["usuarioCorrente"].ToString();
			}
			set
			{
				HttpContext.Current.Session["usuarioCorrente"] = value;
			}
		}

	}

}
