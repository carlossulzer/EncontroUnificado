namespace DIC
{
	public class ProfessorDIC
	{
		public const string TABLE_PROFESSOR = "PROFESSOR";

		public const string COL_COD_PROFESSOR = "COD_PROFESSOR";
		public const string COL_MATRICULA = "MATRICULA";
        public const string COL_NOME = "NOME";
        public const string COL_TELEFONE1 = "TELEFONE1";
        public const string COL_TELEFONE2 = "TELEFONE2";
        public const string COL_EMAIL = "EMAIL";

		public static string ObterColunasdaTabela(string condicao)
		{
			string allColumns = "";
            if(condicao == "S")
                allColumns += ProfessorDIC.COL_COD_PROFESSOR + ",";

            allColumns += ProfessorDIC.COL_MATRICULA + ", ";
            allColumns += ProfessorDIC.COL_NOME + ", ";
            allColumns += ProfessorDIC.COL_TELEFONE1 + ", ";
            allColumns += ProfessorDIC.COL_TELEFONE2 + ", ";
            allColumns += ProfessorDIC.COL_EMAIL;

			return allColumns;
		}
	}
}














