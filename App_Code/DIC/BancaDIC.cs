namespace DIC
{
	public class BancaDIC
	{
		public const string TABLE_BANCA = "BANCA";

        public static string ObterColunasdaTabela()
        {
            string allColumns = "";
            allColumns += EventoDIC.COL_COD_EVENTO + ",";
            allColumns += ProfessorDIC.COL_COD_PROFESSOR;

            return allColumns;
        }
	}
}














