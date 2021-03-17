namespace DIC
{
    public class InscricaoDIC
    {
        public const string TABLE_INSCRICAO = "AGENDA";
        public const string COL_MATRICULA = "MATRICULA";
        public const string COL_COD_ENSINO = "COD_ENSINO";
        public const string COL_DATA = "DATA";
        public const string COL_VAGAS = "VAGAS_DISP";
        public const string COL_PRESENTE = "PRESENTE";

        public static string ObterColunasdaTabela()
        {
            string allColumns = "";
            allColumns += COL_MATRICULA + ", ";
            allColumns += COL_COD_ENSINO + ", ";
            allColumns += EventoDIC.COL_COD_EVENTO+", ";
            allColumns += COL_DATA + ",";
            allColumns += HorarioDIC.COL_COD_HORARIO+", ";
            allColumns += COL_PRESENTE;

            return allColumns;
        }
    }
}











