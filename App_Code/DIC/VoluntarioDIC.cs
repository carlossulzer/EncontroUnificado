namespace DIC
{
    public class VoluntarioDIC
    {
        public const string TABLE_VOLUNTARIO = "VOLUNTARIO";
        public const string COL_MATRICULA = "MATRICULA";
        public const string COL_COD_ENSINO = "COD_ENSINO";
        public const string COL_DATA = "DATA";

        public static string ObterColunasdaTabela()
        {
            string allColumns = "";
            allColumns += COL_MATRICULA + ", ";
            allColumns += COL_COD_ENSINO + ", ";
            allColumns += EventoDIC.COL_COD_EVENTO+", ";
            allColumns += COL_DATA + ", ";
            allColumns += HorarioDIC.COL_COD_HORARIO + ",";
            allColumns += SalaDIC.COL_COD_SALA;

            return allColumns;
        }
    }
}











