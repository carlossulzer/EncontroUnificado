namespace DIC
{
	public class OrientadorDIC
	{
		public const string TABLE_ORIENTADOR = "ORIENTADOR";

        public static string ObterColunasdaTabela()
        {
            string allColumns = "";
            allColumns += EventoDIC.COL_COD_EVENTO + ",";
            allColumns += ProfessorDIC.COL_COD_PROFESSOR;

            return allColumns;
        }
	}
}














