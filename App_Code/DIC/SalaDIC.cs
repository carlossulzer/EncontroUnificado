namespace DIC
{
	public class SalaDIC
	{
		public const string TABLE_SALA = "SALA";

		public const string COL_COD_SALA = "COD_SALA";
		public const string COL_DESCRICAO = "DESCRICAO";
        public const string COL_ANDAR = "ANDAR";
        public const string COL_BLOCO = "BLOCO";
        public const string COL_DESC_CONSULTA = "DESC_SALA";

		public static string ObterColunasdaTabela(string condicao)
		{
			string allColumns = "";
            if(condicao == "S")
                allColumns += SalaDIC.COL_COD_SALA + ", ";

            allColumns += SalaDIC.COL_DESCRICAO + ", ";
            allColumns += SalaDIC.COL_ANDAR + ", ";
            allColumns += SalaDIC.COL_BLOCO;

			return allColumns;
		}
	}
}














