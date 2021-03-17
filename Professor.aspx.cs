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

public partial class Professor : System.Web.UI.Page
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
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir este professor ?')");
                DesabilitarCampos();
            }

            txtMatricula.Attributes.Add("MaxLength", "6");
            txtMatricula.Attributes.Add("mask", "______");
            txtMatricula.Attributes.Add("onkeydown", "EE_KeyDown(this)");
            txtMatricula.Attributes.Add("onkeypress", "EE_KeyPress(this)");
            txtMatricula.Attributes.Add("onclick", "EE_OnClick(this)");
            txtMatricula.Attributes.Add("onfocus", "EE_GotFocus(this)");
            txtMatricula.Attributes.Add("onblur", "EE_LostFocus(this)");

            txtTelefone1.Attributes.Add("MaxLength", "13");
            txtTelefone1.Attributes.Add("mask", "(__)____-____");
            txtTelefone1.Attributes.Add("onkeydown", "EE_KeyDown(this)");
            txtTelefone1.Attributes.Add("onkeypress", "EE_KeyPress(this)");
            txtTelefone1.Attributes.Add("onclick", "EE_OnClick(this)");
            txtTelefone1.Attributes.Add("onfocus", "EE_GotFocus(this)");
            txtTelefone1.Attributes.Add("onblur", "EE_LostFocus(this)");

            txtTelefone2.Attributes.Add("MaxLength", "13");
            txtTelefone2.Attributes.Add("mask", "(__)____-____");
            txtTelefone2.Attributes.Add("onkeydown", "EE_KeyDown(this)");
            txtTelefone2.Attributes.Add("onkeypress", "EE_KeyPress(this)");
            txtTelefone2.Attributes.Add("onclick", "EE_OnClick(this)");
            txtTelefone2.Attributes.Add("onfocus", "EE_GotFocus(this)");
            txtTelefone2.Attributes.Add("onblur", "EE_LostFocus(this)");

            MostraDados(ViewState["codigo"].ToString(), ViewState["operacao"].ToString());
         }

         if (ViewState["operacao"].ToString() == "A") // alterar
             Master.titulo = "Alteração de Professor";
         else if (ViewState["operacao"].ToString() == "I") // inclusao
             Master.titulo = "Inclusão de Professor";
         else if (ViewState["operacao"].ToString() == "E") // excluir
             Master.titulo = "Exclusão de Professor";
    }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;

        ProfessorDOM professor  = new ProfessorDOM();

        professor.codProfessor  = Conversor.ConverterParaInteiro(ViewState["codigo"].ToString());
        professor.matricula = Conversor.ConverterParaInteiro(txtMatricula.Text.Replace("_",""));
        professor.nome = txtNome.Text;
        professor.telefone1 = txtTelefone1.Text;
        professor.telefone2 = txtTelefone2.Text;
        professor.email = txtEmail.Text;

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                ProfessorDAO.IncluirProfessor(professor, true);
        }
        else if (ViewState["operacao"].ToString() == "A")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                ProfessorDAO.AlterarProfessor(professor);
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            string mens = ProfessorDAO.ExcluirProfessor(professor.codProfessor);
            if (!mens.Equals(string.Empty))
            {
                tudoOk = false;
                ExibirMensagemErro.Exibir(mens, this.Page);
            }
        }
        if (tudoOk)
            Response.Redirect("~/ProfessorLista.aspx");    
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/ProfessorLista.aspx"); 
    }

    public void MostraDados(string codigo, string acao)
    {
        ProfessorDOM professorDados = new ProfessorDOM();
        if (acao == "A" || acao == "E")
        {
            professorDados =  new ProfessorDAO().ObterProfessorPeloId(Convert.ToInt32(codigo)); 
        }
        ViewState["codigo"] = professorDados.codProfessor;

        if (professorDados.matricula == 0)
            txtMatricula.Text = "";
        else
        {
            txtMatricula.Text = StringSuporte.Completar(professorDados.matricula.ToString(),6, "_");
        }
        txtNome.Text = professorDados.nome;
        txtTelefone1.Text = professorDados.telefone1;
        txtTelefone2.Text = professorDados.telefone2;
        txtEmail.Text = professorDados.email;

        if (acao == "A" || acao == "I")
        {
            txtMatricula.Focus();
        }
    }

    public void DesabilitarCampos()
    {
        txtMatricula.Enabled = false;
        txtNome.Enabled = false;
        txtTelefone1.Enabled = false;
        txtTelefone2.Enabled = false;
        txtEmail.Enabled = false;
    }

    public bool ValidaForm()
    {
        bool v = true;

        if (ProfessorDAO.RegistroExiste(Conversor.ConverterParaInteiro(txtMatricula.Text.Replace("_", "")).ToString(), txtNome.Text, ViewState["operacao"].ToString(), ViewState["codigo"].ToString()))
        {
            ExibirMensagemErro.Exibir("Professor já cadastrado.", this.Page);
            txtMatricula.Focus();
            v = false;
        }

        if (v)
        {
            v = Validacao.ValidaTextBox(this.Page, txtMatricula);
            if (!v)
            {
                ExibirMensagemErro.Exibir("Favor informar a matrícula.", this.Page);
                txtMatricula.Focus();
            }

            if (v)
            {
                v = Validacao.ValidaTextBox(this.Page, txtNome);
                if (!v)
                {
                    ExibirMensagemErro.Exibir("Favor informar o nome.", this.Page);
                    txtNome.Focus();
                }
            }
        }
        return v;

    }
}
