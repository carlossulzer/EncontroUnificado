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

public partial class Recurso : System.Web.UI.Page
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
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir este recurso ?')");
                DesabilitarCampos();
            }    
            MostraDados(ViewState["codigo"].ToString(), ViewState["operacao"].ToString());

         }
         if (ViewState["operacao"].ToString() == "A") // alterar
             Master.titulo = "Alteração de Recurso";
         else if (ViewState["operacao"].ToString() == "I") // inclusao
             Master.titulo = "Inclusão de Recurso";
         else if (ViewState["operacao"].ToString() == "E") // excluir
             Master.titulo = "Exclusão de Recurso";
    }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;

        RecursoDOM recurso  = new RecursoDOM();

        recurso.codRecurso  = Conversor.ConverterParaInteiro(ViewState["codigo"].ToString());
        recurso.descricao = txtDescricao.Text;

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                RecursoDAO.IncluirRecurso(recurso, true);
        }
        else if (ViewState["operacao"].ToString() == "A")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                RecursoDAO.AlterarRecurso(recurso);
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            string mens = RecursoDAO.ExcluirRecurso(recurso.codRecurso);
            if (!mens.Equals(string.Empty))
            {
                tudoOk = false;
                ExibirMensagemErro.Exibir(mens, this.Page);
            }
        }



        if (tudoOk)
            Response.Redirect("~/RecursoLista.aspx");    
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/RecursoLista.aspx"); 
    }

    public void MostraDados(string codigo, string acao)
    {
        RecursoDOM recursoDados = new RecursoDOM();
        if (acao == "A" || acao == "E")
        {
            recursoDados =  new RecursoDAO().ObterRecursoPeloId(Convert.ToInt32(codigo)); 
        }
        ViewState["codigo"] = recursoDados.codRecurso;
        txtDescricao.Text   = recursoDados.descricao;

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

        if (RecursoDAO.RegistroExiste(txtDescricao.Text, ViewState["operacao"].ToString(), ViewState["codigo"].ToString()))
        {
            ExibirMensagemErro.Exibir("Recurso já cadastrado.", this.Page);
            txtDescricao.Focus();
            v = false;
        }

        if (v)
        {
            v = Validacao.ValidaTextBox(this.Page, txtDescricao);
            if (!v)
            {
                ExibirMensagemErro.Exibir("Favor informar um recurso.", this.Page);
                txtDescricao.Focus();
            }
        }
        return v;

    }


}
