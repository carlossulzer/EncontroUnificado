namespace DIC
{
    public class IntegranteDIC
    {
        public const string TABLE_INTEGRANTE = "INTEGRANTES";
        public const string COL_MATRICULA = "MATRICULA";
        public const string COL_COD_ENSINO = "COD_ENSINO";

        public static string ObterColunasdaTabela()
        {
            string allColumns = "";
            allColumns += COL_MATRICULA + ", ";
            allColumns += COL_COD_ENSINO + ", ";
            allColumns += EventoDIC.COL_COD_EVENTO;

            return allColumns;
        }
    }
}











