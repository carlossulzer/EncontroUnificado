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

public delegate void EventoBotoes(object sender, EventArgs e);

public partial class MasterPage_Principal : System.Web.UI.MasterPage
{
    public event EventoBotoes NovoEvento;
    public event EventoBotoes ConfirmarEvento;
    public event EventoBotoes CancelarEvento;
    EventoBotoes NovoFun;
    EventoBotoes ConfirmarFun;
    EventoBotoes CancelarFun;


    public string titulo = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        NovoFun = NovoEvento;
        ConfirmarFun = ConfirmarEvento;
        CancelarFun = CancelarEvento;
        lblTitulo.Text = titulo;
    }

    public string Titulo
    {
        set 
        { 
            titulo = value; 
        }
    }
   
    protected void btnConfirmar_Click(object sender, ImageClickEventArgs e)
    {
        if (ConfirmarFun != null)
            ConfirmarFun(sender, e);
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (CancelarFun != null)
            CancelarFun(sender, e);

    }



    protected void btnNovo_Click(object sender, ImageClickEventArgs e)
    {
        if (NovoFun != null)
            NovoFun(sender, e);

    }
}