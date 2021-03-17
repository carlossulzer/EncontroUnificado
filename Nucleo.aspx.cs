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

public partial class Nucleo : System.Web.UI.Page
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
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir este núcleo ?')");
                DesabilitarCampos();
            }    
            MostraDados(ViewState["codigo"].ToString(), ViewState["operacao"].ToString());

        }
        if (ViewState["operacao"].ToString() == "A") // alterar
            Master.titulo = "Alteração de Núcleo";
        else if (ViewState["operacao"].ToString() == "I") // cadastrar
            Master.titulo = "Inclusão de Núcleo";
        else if (ViewState["operacao"].ToString() == "E") // excluir
            Master.titulo = "Exclusão de Núcleo";

    }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;

        NucleoDOM nucleo = new NucleoDOM();

        nucleo.codNucleo  = Conversor.ConverterParaInteiro(ViewState["codigo"].ToString());
        nucleo.descricao  = txtDescricao.Text;

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                NucleoDAO.IncluirNucleo(nucleo, true);
        }
        else if (ViewState["operacao"].ToString() == "A")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                NucleoDAO.AlterarNucleo(nucleo);
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            string mens = NucleoDAO.ExcluirNucleo(nucleo.codNucleo);

            if (!mens.Equals(string.Empty))
            {
                tudoOk = false;
                ExibirMensagemErro.Exibir(mens, this.Page);
            }
        }

        if (tudoOk)
            Response.Redirect("~/NucleoLista.aspx");    
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/NucleoLista.aspx"); 
    }

    public void MostraDados(string codigo, string acao)
    {
        NucleoDOM nucleoDados = new NucleoDOM();
        if (acao == "A" || acao == "E")
        {
            nucleoDados = new NucleoDAO().ObterNucleoPeloId(Convert.ToInt32(codigo)); 
        }
        ViewState["codigo"] = nucleoDados.codNucleo;
        txtDescricao.Text   = nucleoDados.descricao;

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

        if (NucleoDAO.RegistroExiste(txtDescricao.Text, ViewState["operacao"].ToString(), ViewState["codigo"].ToString()))
        {
            ExibirMensagemErro.Exibir("Núcleo já cadastrado.", this.Page);
            txtDescricao.Focus();
            v = false;
        }

        if (v)
        {
            v = Validacao.ValidaTextBox(this.Page, txtDescricao);
            if (!v)
            {
                ExibirMensagemErro.Exibir("Favor informar um núcleo.", this.Page);
                txtDescricao.Focus();
            }
        }
        return v;

    }


}
