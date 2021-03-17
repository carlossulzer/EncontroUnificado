namespace DIC
{
	public class CalendarioDIC
	{
		public const string TABLE_CALENDARIO = "CALENDARIO";

        public const string COL_COD_SALA = "COD_SALA";
        public const string COL_COD_HORARIO = "COD_HORARIO";
        public const string COL_DATA = "DATA";


        public static string ObterColunasdaTabela()
        {
            string allColumns = "";
            allColumns += COL_COD_SALA + ",";
            allColumns += COL_COD_HORARIO + ",";
            allColumns += EventoDIC.COL_COD_EVENTO + ",";
            allColumns += COL_DATA;

            return allColumns;
        }
	}
}














