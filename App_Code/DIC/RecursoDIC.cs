namespace DIC
{
	public class RecursoDIC
	{
		public const string TABLE_RECURSOS = "RECURSOS";

		public const string COL_COD_RECURSO = "COD_RECURSO";
		public const string COL_DESCRICAO = "DESCRICAO";
		

		public static string ObterColunasdaTabela(string condicao)
		{
			string allColumns = "";
            if(condicao == "S")
                allColumns += RecursoDIC.COL_COD_RECURSO + ",";

            allColumns += RecursoDIC.COL_DESCRICAO;

			return allColumns;
		}
	}
}














