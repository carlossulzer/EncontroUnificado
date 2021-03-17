namespace DIC
{
	public class TipoEventoDIC
	{
		public const string TABLE_TIPO_EVENTO = "TIPO_EVENTO";

		public const string COL_COD_TIPO_EVENTO = "COD_TIPO_EVENTO";
		public const string COL_DESCRICAO = "DESCRICAO";
        public const string COL_TIPO_EVENTO_DESC = "TIPO_EVENTO_DESC";

		public static string ObterColunasdaTabela(string condicao)
		{
			string allColumns = "";
            if(condicao == "S")
                allColumns += TipoEventoDIC.COL_COD_TIPO_EVENTO + ",";

            allColumns += TipoEventoDIC.COL_DESCRICAO;

			return allColumns;
		}
	}
}














