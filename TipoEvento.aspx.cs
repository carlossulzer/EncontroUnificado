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
using Dominio;
using DAO;
using Util;

public partial class TipoEvento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_Principal pagina = (MasterPage_Principal)this.Master;
        pagina.ConfirmarEvento += new EventoBotoes(ConfirmarClick);
        pagina.CancelarEvento  += new EventoBotoes(CancelarClick);

        ImageButton salvar   = (ImageButton)Master.FindControl("btnConfirmar");
        salvar.Visible = true;

        ImageButton cancelar = (ImageButton)Master.FindControl("btnCancelar");
        cancelar.Visible = true;

        if (!Page.IsPostBack)
        {
            ViewState["codigo"]   = Request["codigo"].Trim();
            ViewState["operacao"] = Request["operacao"].Trim();

            if (ViewState["operacao"].ToString() == "E") // excluir
            {
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir este tipo de evento ?')");
                DesabilitarCampos();
            }    
            MostraDados(ViewState["codigo"].ToString(), ViewState["operacao"].ToString());

         }
         if (ViewState["operacao"].ToString() == "A") // alterar
             Master.titulo = "Alteração de Tipo de Evento";
         else if (ViewState["operacao"].ToString() == "I") // inclusao
             Master.titulo = "Inclusão de Tipo de Evento";
         else if (ViewState["operacao"].ToString() == "E") // excluir
         {
             salvar.Attributes.Add("onclick", "return confirm('Deseja excluir este tipo de evento ?')");
             DesabilitarCampos();
         }
     }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;

        TipoEventoDOM tipoEvento = new TipoEventoDOM();

        tipoEvento.codTipoEvento = Conversor.ConverterParaInteiro(ViewState["codigo"].ToString());
        tipoEvento.descricao = txtDescricao.Text;

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                TipoEventoDAO.IncluirTipoEvento(tipoEvento, true);
           
        }
        else if (ViewState["operacao"].ToString() == "A")
        {   
            tudoOk = ValidaForm();
            if (tudoOk)
                TipoEventoDAO.AlterarTipoEvento(tipoEvento);
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            string mens = TipoEventoDAO.ExcluirTipoEvento(tipoEvento.codTipoEvento);
            if (!mens.Equals(string.Empty))
            {
                tudoOk = false;
                ExibirMensagemErro.Exibir(mens, this.Page);
            }
        }

        if (tudoOk)
            Response.Redirect("~/TipoEventoLista.aspx");
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/TipoEventoLista.aspx"); 
    }

    public void MostraDados(string codigo, string acao)
    {
        TipoEventoDOM tipoEventoDados = new TipoEventoDOM();
        if (acao == "A" || acao == "E")
        {
            tipoEventoDados =  new TipoEventoDAO().ObterTipoEventoPeloId(Convert.ToInt32(codigo)); 
        }
        ViewState["codigo"] = tipoEventoDados.codTipoEvento;
        txtDescricao.Text   = tipoEventoDados.descricao;

        if (acao == "A" || acao == "I") 
            txtDescricao.Focus();
   
    }

    public void DesabilitarCampos()
    {
        txtDescricao.Enabled = false;
    }

    public bool ValidaForm()
    {
        bool v = true;

        if (TipoEventoDAO.RegistroExiste(txtDescricao.Text, ViewState["operacao"].ToString(), ViewState["codigo"].ToString()))
        {
            ExibirMensagemErro.Exibir("Tipo de evento já cadastrado.", this.Page);
            txtDescricao.Focus();
            v = false;
        }

        if (v)
        {
            v = Validacao.ValidaTextBox(this.Page, txtDescricao);
            if (!v)
            {
                ExibirMensagemErro.Exibir("Favor informar um tipo de evento.", this.Page);
                txtDescricao.Focus();
            }
        }
        return v;

    }


}
