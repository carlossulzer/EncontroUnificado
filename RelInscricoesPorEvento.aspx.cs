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

public partial class RelInscricoesPorEvento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CrystalReportSource relInscricoes = new CrystalReportSource();
        DataTable rel = ObterDadosInscricaoPorEvento().InternalDataSet.Tables[0];
        relInscricoes.Report.FileName = "InscricoesPorEvento.rpt";
        relInscricoes.ReportDocument.SetDataSource(rel);
        crvInscircoesPorEvento.ReportSource = relInscricoes;
        crvInscircoesPorEvento.DataBind();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
    }


    public ResultadoQuery ObterDadosInscricaoPorEvento()
    {
        clsObjetosBanco objbanco = new clsObjetosBanco();
        StringBuilder sql = new StringBuilder();
        sql.Append(" SELECT  "+EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_COD_EVENTO+", ");
        sql.Append(   EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_TITULO+", ");
        sql.Append(   EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_NUM_VAGAS+", ");
        sql.Append(   " (SELECT COUNT("+InscricaoDIC.COL_MATRICULA+") FROM "+InscricaoDIC.TABLE_INSCRICAO);
        sql.Append(   " WHERE "+InscricaoDIC.TABLE_INSCRICAO+"."+EventoDIC.COL_COD_EVENTO+" = "+EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_COD_EVENTO+" ) AS INSCRICOES");
        sql.Append(" FROM "+EventoDIC.TABLE_EVENTO);
        sql.Append(" ORDER BY " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_TITULO);
        
        
        //+" WHERE "+EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_NUM_VAGAS+" = ");
        //sql.Append(   " (SELECT COUNT("+InscricaoDIC.COL_MATRICULA+") FROM "+InscricaoDIC.TABLE_INSCRICAO);
        //sql.Append(   " WHERE "+InscricaoDIC.TABLE_INSCRICAO+"."+EventoDIC.COL_COD_EVENTO+" = "+EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_COD_EVENTO+" )");
        return objbanco.MontaDataSet(sql.ToString());
    }
}
