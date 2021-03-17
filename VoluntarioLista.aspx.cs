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

public partial class VoluntarioLista : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_Principal pagina = (MasterPage_Principal)this.Master;
        pagina.NovoEvento += new EventoBotoes(NovoClick);

        ImageButton novo = (ImageButton)Master.FindControl("btnNovo");
        novo.Visible = true;

        Master.titulo = "Voluntários Cadastrados";
        if (!Page.IsPostBack)
        {
            gridVoluntario.DataSource = VoluntarioDAO.ListarVoluntario().InternalDataSet;
            gridVoluntario.DataBind();
        }
    }

    private void NovoClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Voluntario.aspx?matricula=0&codEnsino=0&codEvento=0&operacao=I");
    }

    protected void gridVoluntario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridVoluntario.PageIndex = e.NewPageIndex;
        gridVoluntario.DataSource = VoluntarioDAO.ListarVoluntario().InternalDataSet;
        gridVoluntario.DataBind();

    }
}
