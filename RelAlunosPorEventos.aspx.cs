using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security; 
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using CrystalDecisions.Web;
using Banco;
using DIC;

public partial class RelAlunosPorEventos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CrystalReportSource relInscricoes = new CrystalReportSource();
        DataTable rel = ObterDadosEventosComAlunos().InternalDataSet.Tables[0];
        relInscricoes.Report.FileName = Server.MapPath("AlunosPorEventos.rpt");
        //relInscricoes.Report.FileName = Server.MapPath("relnovo.rpt");
        relInscricoes.ReportDocument.SetDataSource(rel);
        crvAlunosPorEvento.ReportSource = relInscricoes;
        crvAlunosPorEvento.DataBind();
    }

    public ResultadoQuery ObterDadosEventosComAlunos()
    {
        clsObjetosBanco objbanco = new clsObjetosBanco();
        StringBuilder sql = new StringBuilder();

        sql.Append(" SELECT  " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + ", ");
        sql.Append(    EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_TITULO + ",");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA + ",");
        sql.Append(    TipoEventoDIC.TABLE_TIPO_EVENTO + "." + TipoEventoDIC.COL_DESCRICAO + " AS " + TipoEventoDIC.COL_TIPO_EVENTO_DESC + ", ");
        sql.Append(    HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAINICIAL + "+' às '+" + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAFINAL + " AS " + HorarioDIC.COL_DESC_CONSULTA + ", ");
        sql.Append("'SALA :'+" + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_DESCRICAO + "+' - BLOCO:'+" + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_BLOCO + "+' - '+" + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_ANDAR + "+'º ANDAR' AS " + SalaDIC.COL_DESC_CONSULTA+", ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_MATRICULA + ", ");
        sql.Append(    AlunoDIC.TABLE_ALUNOG + "." + AlunoDIC.COL_NOME);
        sql.Append(" FROM " + InscricaoDIC.TABLE_INSCRICAO + ", " + EventoDIC.TABLE_EVENTO + ", " + CalendarioDIC.TABLE_CALENDARIO + ", ");
        sql.Append(    HorarioDIC.TABLE_HORARIO + ", " + SalaDIC.TABLE_SALA + ", " + TipoEventoDIC.TABLE_TIPO_EVENTO + ", " + AlunoDIC.TABLE_ALUNOG);
        sql.Append(" WHERE ");
        sql.Append(    EventoDIC.TABLE_EVENTO + "." + TipoEventoDIC.COL_COD_TIPO_EVENTO + " = " + TipoEventoDIC.TABLE_TIPO_EVENTO + "." + TipoEventoDIC.COL_COD_TIPO_EVENTO + " and ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + " = " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + " and ");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + EventoDIC.COL_COD_EVENTO + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + " and ");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + HorarioDIC.COL_COD_HORARIO + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO + " and ");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_DATA + " and ");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + SalaDIC.COL_COD_SALA + " = " + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_COD_SALA + " and ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO + " = " + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_COD_HORARIO + " and ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_COD_ENSINO + " = 1 and ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_MATRICULA + " = " + AlunoDIC.TABLE_ALUNOG + "." + AlunoDIC.COL_MATRICULA );
        sql.Append(" UNION ");
        sql.Append(" SELECT  " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + ", ");
        sql.Append(    EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_TITULO + ",");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA + ",");
        sql.Append(    TipoEventoDIC.TABLE_TIPO_EVENTO + "." + TipoEventoDIC.COL_DESCRICAO + " AS " + TipoEventoDIC.COL_TIPO_EVENTO_DESC + ", ");
        sql.Append(    HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAINICIAL + "+' às '+" + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAFINAL + " AS " + HorarioDIC.COL_DESC_CONSULTA + ", ");
        sql.Append("   'SALA :'+" + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_DESCRICAO + "+' - BLOCO:'+" + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_BLOCO + "+' - '+" + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_ANDAR + "+'º ANDAR' AS " + SalaDIC.COL_DESC_CONSULTA + ", ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_MATRICULA + ", ");
        sql.Append(    AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_NOME);
        sql.Append(" FROM " + InscricaoDIC.TABLE_INSCRICAO + ", " + EventoDIC.TABLE_EVENTO + ", " + CalendarioDIC.TABLE_CALENDARIO + ", ");
        sql.Append(    HorarioDIC.TABLE_HORARIO + ", " + SalaDIC.TABLE_SALA + ", " + TipoEventoDIC.TABLE_TIPO_EVENTO + ", " + AlunoDIC.TABLE_ALUNOT);
        sql.Append(" WHERE ");
        sql.Append(    EventoDIC.TABLE_EVENTO + "." + TipoEventoDIC.COL_COD_TIPO_EVENTO + " = " + TipoEventoDIC.TABLE_TIPO_EVENTO + "." + TipoEventoDIC.COL_COD_TIPO_EVENTO + " and ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + " = " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + " and ");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + EventoDIC.COL_COD_EVENTO + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + " and ");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + HorarioDIC.COL_COD_HORARIO + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO + " and ");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_DATA + " and ");
        sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + SalaDIC.COL_COD_SALA + " = " + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_COD_SALA + " and ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO + " = " + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_COD_HORARIO + " and ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_COD_ENSINO + " = 2 and ");
        sql.Append(    InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_MATRICULA + " = " + AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_MATRICULA);
        sql.Append(" ORDER BY 2, 8");


        return objbanco.MontaDataSet(sql.ToString());
    }
}
