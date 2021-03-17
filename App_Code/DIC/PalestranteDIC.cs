namespace DIC
{
    public class PalestranteDIC
    {
        public const string TABLE_PALESTRANTE = "PALESTRANTE";

        public static string ObterColunasdaTabela()
        {
            string allColumns = "";
            allColumns += EventoDIC.COL_COD_EVENTO + ",";
            allColumns += ProfessorDIC.COL_COD_PROFESSOR;

            return allColumns;
        }
    }
}













