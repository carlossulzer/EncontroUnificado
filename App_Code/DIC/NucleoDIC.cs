namespace DIC
{
	public class NucleoDIC
	{
        public const string TABLE_NUCLEO = "NUCLEO";
        public const string COL_CODNUCLEO = "COD_NUCLEO";
        public const string COL_DESCRICAO = "DESCRICAO";

		public static string ObterColunasdaTabela(string condicao)
		{
			string allColumns = "";
            if (condicao == "S")
                allColumns += NucleoDIC.COL_CODNUCLEO + ",";
                
            allColumns += NucleoDIC.COL_DESCRICAO ;

			return allColumns;
		}
	}
}














