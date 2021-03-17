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

public partial class NucleoLista : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_Principal pagina = (MasterPage_Principal)this.Master;
        pagina.NovoEvento += new EventoBotoes(NovoClick);

        ImageButton novo = (ImageButton)Master.FindControl("btnNovo");
        novo.Visible = true;

        Master.titulo = "Núcleos Cadastrados";
        if (!Page.IsPostBack)
        {
            GridNucleos.DataSource = NucleoDAO.ListarNucleo(false).InternalDataSet;
            GridNucleos.DataBind();
        }
    }

    private void NovoClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Nucleo.aspx?codigo=0&operacao=I");
    }

    protected void GridNucleos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        GridNucleos.PageIndex = e.NewPageIndex;
        GridNucleos.DataSource = NucleoDAO.ListarNucleo(false).InternalDataSet;
        GridNucleos.DataBind();
    }
}
