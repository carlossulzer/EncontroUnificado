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

public partial class Sala : System.Web.UI.Page
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
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir esta sala ?')");
                DesabilitarCampos();
            }    
            MostraDados(ViewState["codigo"].ToString(), ViewState["operacao"].ToString());
         }
         if (ViewState["operacao"].ToString() == "A") // alterar
             Master.titulo = "Alteração de Sala";
         else if (ViewState["operacao"].ToString() == "I") // inclusao
             Master.titulo = "Inclusão de Sala";
         else if (ViewState["operacao"].ToString() == "E") // excluir
             Master.titulo = "Exclusão de Sala";
    }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;

        SalaDOM sala = new SalaDOM();

        sala.codSala   = Conversor.ConverterParaInteiro(ViewState["codigo"].ToString());
        sala.descricao = txtSala.Text;
        sala.andar = txtAndar.Text;
        sala.bloco = txtBloco.Text;

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                SalaDAO.IncluirSala(sala, true);
        }
        else if (ViewState["operacao"].ToString() == "A")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                SalaDAO.AlterarSala(sala);
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            string mens = SalaDAO.ExcluirSala(sala.codSala);
            if (!mens.Equals(string.Empty))
            {
                tudoOk = false;
                ExibirMensagemErro.Exibir(mens, this.Page);
            }
        }

        if (tudoOk)
            Response.Redirect("~/SalaLista.aspx");    
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/SalaLista.aspx"); 
    }

    public void MostraDados(string codigo, string acao)
    {
        SalaDOM salaDados = new SalaDOM();
        if (acao == "A" || acao == "E")
        {
            salaDados =  new SalaDAO().ObterSalaPeloId(Convert.ToInt32(codigo)); 
        }
        ViewState["codigo"] = salaDados.codSala;
        txtSala.Text   = salaDados.descricao;
        txtAndar.Text = salaDados.andar;
        txtBloco.Text = salaDados.bloco;

        if (acao == "A" || acao == "I") 
            txtSala.Focus();
   
    }

    public void DesabilitarCampos()
    {
        txtSala.Enabled = false;
        txtAndar.Enabled = false;
        txtBloco.Enabled = false;

    }

    public bool ValidaForm()
    {
        bool v = true;

        if (SalaDAO.RegistroExiste(txtSala.Text, txtAndar.Text, txtBloco.Text, ViewState["operacao"].ToString(), ViewState["codigo"].ToString()))
        {
            ExibirMensagemErro.Exibir("Sala já cadastrada.", this.Page);
            txtSala.Focus();
            v = false;
        }

        if (v)
        {
            v = Validacao.ValidaTextBox(this.Page, txtSala);
            if (!v)
            {
                ExibirMensagemErro.Exibir("Favor informar a sala.", this.Page);
                txtSala.Focus();
            }

            if (v)
            {
                v = Validacao.ValidaTextBox(this.Page, txtAndar);
                if (!v)
                {
                    ExibirMensagemErro.Exibir("Favor informar o andar.", this.Page);
                    txtAndar.Focus();
                }
            }

            if (v)
            {
                v = Validacao.ValidaTextBox(this.Page, txtBloco);
                if (!v)
                {
                    ExibirMensagemErro.Exibir("Favor informar o bloco.", this.Page);
                    txtBloco.Focus();
                }
            }
        }
        return v;
    }
}
