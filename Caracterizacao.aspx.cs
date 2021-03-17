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

public partial class Caracterizacao : System.Web.UI.Page
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
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir esta Caracterização ?')");
                DesabilitarCampos();
            }    
            MostraDados(ViewState["codigo"].ToString(), ViewState["operacao"].ToString());

         }
         if (ViewState["operacao"].ToString() == "A") // alterar
             Master.titulo = "Alteração de Caracterização";
         else if (ViewState["operacao"].ToString() == "I") // inclusao
             Master.titulo = "Inclusão de Caracterização";
         else if (ViewState["operacao"].ToString() == "E") // excluir
             Master.titulo = "Exclusão de Caracterização";
     }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;

        CaracterizacaoDOM caracterizacao  = new CaracterizacaoDOM();

        caracterizacao.codCaracterizacao = Conversor.ConverterParaInteiro(ViewState["codigo"].ToString());
        caracterizacao.descricao = txtDescricao.Text;

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                CaracterizacaoDAO.IncluirCaracterizacao(caracterizacao, true);
        }
        else if (ViewState["operacao"].ToString() == "A")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                CaracterizacaoDAO.AlterarCaracterizacao(caracterizacao);
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            string mens = CaracterizacaoDAO.ExcluirCaracterizacao(caracterizacao.codCaracterizacao);
            if (!mens.Equals(string.Empty))
            {
                tudoOk = false;
                ExibirMensagemErro.Exibir(mens, this.Page);
            }
        }

        if (tudoOk)
            Response.Redirect("~/CaracterizacaoLista.aspx");    
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/CaracterizacaoLista.aspx"); 
    }

    public void MostraDados(string codigo, string acao)
    {
        CaracterizacaoDOM caracterizacaoDados = new CaracterizacaoDOM();
        if (acao == "A" || acao == "E")
        {
            caracterizacaoDados =  new CaracterizacaoDAO().ObterCaracterizacaoPeloId(Convert.ToInt32(codigo)); 
        }
        ViewState["codigo"] = caracterizacaoDados.codCaracterizacao;
        txtDescricao.Text   = caracterizacaoDados.descricao;

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

        if (CaracterizacaoDAO.RegistroExiste(txtDescricao.Text, ViewState["operacao"].ToString(), ViewState["codigo"].ToString()))
        {
            ExibirMensagemErro.Exibir("Caracterização já cadastrada.", this.Page);
            txtDescricao.Focus();
            v = false;
        }

        if (v)
        {
            v = Validacao.ValidaTextBox(this.Page, txtDescricao);
            if (!v)
            {
                ExibirMensagemErro.Exibir("Favor informar uma caracterização.", this.Page);
                txtDescricao.Focus();
            }
        }
        return v;

    }



}
