namespace DIC
{
	public class EventoDIC
	{
		public const string TABLE_EVENTO = "EVENTOS";

		public const string COL_COD_EVENTO = "COD_EVENTO";
		public const string COL_TITULO = "TITULO";
        public const string COL_EMENTA = "EMENTA";
        public const string COL_OBJETIVOS = "OBJETIVOS";
        public const string COL_PUBLICO_ALVO = "PUBLICO_ALVO";
        public const string COL_NUM_VAGAS = "NUM_VAGAS";
		

		public static string ObterColunasdaTabela(string condicao)
		{
			string allColumns = "";
            if(condicao == "S")
                allColumns += EventoDIC.COL_COD_EVENTO + ",";

            allColumns += CaracterizacaoDIC.COL_COD_CARACTERIZACAO + ", ";
            allColumns += RecursoDIC.COL_COD_RECURSO + ", ";
            allColumns += EventoDIC.COL_TITULO + ", ";
            allColumns += TipoEventoDIC.COL_COD_TIPO_EVENTO + ", ";
            allColumns += EventoDIC.COL_EMENTA + ", ";
            allColumns += NucleoDIC.COL_CODNUCLEO + ", ";
            allColumns += EventoDIC.COL_OBJETIVOS + ", ";
            allColumns += EventoDIC.COL_PUBLICO_ALVO + ", ";
            allColumns += EventoDIC.COL_NUM_VAGAS;

			return allColumns;
		}
	}
}














