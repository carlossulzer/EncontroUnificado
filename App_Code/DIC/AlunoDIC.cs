namespace DIC
{
	public class AlunoDIC
	{
        //---------------------------------------------------------------------------------------------
        //Tirar comentario destas duas linhas abaixo
        //---------------------------------------------------------------------------------------------
        public const string TABLE_ALUNOG = "DB_GRADUACAO.DBO.ALUNO";  // ALUNO DE GRADUACAO
        public const string TABLE_ALUNOT = "DB_CURSO_TECNO.DBO.ALUNO";  // ALUNO DE TECNOLOGIA

        //---------------------------------------------------------------------------------------------
        // Clocar comentario nestas duas linhas abaixo
        //---------------------------------------------------------------------------------------------
        //public const string TABLE_ALUNOG = "ALUNOG";  // ALUNO DE GRADUACAO
        //public const string TABLE_ALUNOT = "ALUNOT";  // ALUNO DE TECNOLOGIA

		public const string COL_MATRICULA = "RA";
		public const string COL_NOME = "NM_ALUNO";
		public const string COL_SERIE = "SER_PER";
		public const string COL_LETRA = "LETRA";
		public const string COL_TEL = "TEL";

		public static string ObterColunasdaTabela()
		{
			string allColumns = "";
                allColumns += COL_MATRICULA+ ",";       
                allColumns += COL_NOME+ ",";
                allColumns += COL_SERIE+ ",";  
                allColumns += COL_LETRA+ ",";    
                allColumns += COL_TEL+ ",";
                allColumns += CursoDIC.COL_COD_CURSO;

			return allColumns;
		}
	}
}














