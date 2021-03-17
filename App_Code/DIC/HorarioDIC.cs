namespace DIC
{
	public class HorarioDIC
	{
		public const string TABLE_HORARIO = "HORARIO";

		public const string COL_COD_HORARIO = "COD_HORARIO";
		public const string COL_HORAINICIAL = "HORA_INICIAL";
        public const string COL_HORAFINAL = "HORA_FINAL";
        public const string COL_DESC_CONSULTA = "DESC_HORARIO";

        public static string ObterColunasdaTabela(string condicao)
		{
			string allColumns = "";
            if(condicao == "S")
                allColumns += HorarioDIC.COL_COD_HORARIO + ", ";

            allColumns += HorarioDIC.COL_HORAINICIAL + ", ";
            allColumns += HorarioDIC.COL_HORAFINAL;

			return allColumns;
		}
	}
}














