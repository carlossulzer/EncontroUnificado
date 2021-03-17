namespace DIC
{
	public class UsuarioDIC
	{
		public const string TABLE_USUARIO = "USUARIO";
		public const string COL_IDUSUARIO = "IDUSUARIO";
		public const string COL_LOGIN = "LOGIN";
		public const string COL_SENHA = "SENHA";
		public const string COL_NOMECOMPLETO = "NOMECOMPLETO";
		public const string COL_EMAIL = "EMAIL";
		public const string COL_BLOQUEADO = "BLOQUEADO";
		public const string COL_OBRIGARTROCADESENHA = "OBRIGARTROCADESENHA";

		public static string GetAllColumnsForSelect()
		{
			string allColumns = "";
			allColumns += UsuarioDIC.COL_IDUSUARIO + ",";
			allColumns += UsuarioDIC.COL_LOGIN + ",";
			allColumns += UsuarioDIC.COL_SENHA + ",";
			allColumns += UsuarioDIC.COL_NOMECOMPLETO + ",";
			allColumns += UsuarioDIC.COL_EMAIL + ",";
			allColumns += UsuarioDIC.COL_OBRIGARTROCADESENHA + ",";
			allColumns += UsuarioDIC.COL_BLOQUEADO ;

			return allColumns;
		}
	}
}
