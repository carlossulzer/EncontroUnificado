namespace DIC
{
	public class CaracterizacaoDIC
	{
		public const string TABLE_CARACTERIZACAO = "CARACTERIZACAO";

		public const string COL_COD_CARACTERIZACAO = "COD_CARACTERIZACAO";
		public const string COL_DESCRICAO = "DESCRICAO";
		

		public static string ObterColunasdaTabela(string condicao)
		{
			string allColumns = "";
            if(condicao == "S")
                allColumns += CaracterizacaoDIC.COL_COD_CARACTERIZACAO + ",";

            allColumns += CaracterizacaoDIC.COL_DESCRICAO;

			return allColumns;
		}
	}
}














