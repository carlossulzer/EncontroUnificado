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
using DAO;
using System.Text;

public partial class TipoEventoLista : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_Principal pagina = (MasterPage_Principal)this.Master;
        pagina.NovoEvento += new EventoBotoes(NovoClick);

        ImageButton novo = (ImageButton)Master.FindControl("btnNovo");
        novo.Visible = true;

        Master.titulo = "Tipos de Eventos Cadastrados";
        if (!Page.IsPostBack)
        {
            GridTipoEvento.DataSource = TipoEventoDAO.ListarTipoEvento(false).InternalDataSet;
            GridTipoEvento.DataBind();
        }
    }

    private void NovoClick(object sender, EventArgs e)
    {
        Response.Redirect("~/TipoEvento.aspx?codigo=0&operacao=I");
    }

    protected void GridTipoEvento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridTipoEvento.PageIndex = e.NewPageIndex;
        GridTipoEvento.DataSource = TipoEventoDAO.ListarTipoEvento(false).InternalDataSet;
        GridTipoEvento.DataBind();
    }
    protected void bntXML_Click(object sender, EventArgs e)
    {
		string _arq_xml = @"c:\TipoEvento.xml";
		DataSet dsXML = new DataSet();
        dsXML = TipoEventoDAO.ListarTipoEvento(false).InternalDataSet;
		// salva em xml
		dsXML.WriteXml(_arq_xml);
    }
}
