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
using DIC;
using Util;

public partial class Voluntario : System.Web.UI.Page
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
            ViewState["matricula"] = Request["matricula"].Trim();
            ViewState["codEnsino"] = Request["codEnsino"].Trim();
            ViewState["codEvento"] = Request["codEvento"].Trim();
            ViewState["operacao"] = Request["operacao"].Trim();

            if (ViewState["operacao"].ToString() == "E") // excluir
            {
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir este voluntário ?')");
                DesabilitarCampos();
            }
            int matricula = Conversor.ConverterParaInteiro(ViewState["matricula"].ToString());
            int codEnsino = Conversor.ConverterParaInteiro(ViewState["codEnsino"].ToString());
            int codEvento = Conversor.ConverterParaInteiro(ViewState["codEvento"].ToString());

            dropSala.DataSource = SalaDAO.DropDownSala().InternalDataSet;
            dropSala.DataTextField = SalaDIC.COL_DESC_CONSULTA;
            dropSala.DataValueField = SalaDIC.COL_COD_SALA;
            dropSala.DataBind();

            dropHorario.DataSource = HorarioDAO.DropDownHorario().InternalDataSet;
            dropHorario.DataTextField = HorarioDIC.COL_DESC_CONSULTA;
            dropHorario.DataValueField = HorarioDIC.COL_COD_HORARIO;
            dropHorario.DataBind();

            txtData.Attributes.Add("MaxLength", "10");
            txtData.Attributes.Add("mask", "__/__/____");
            txtData.Attributes.Add("onkeydown", "EE_KeyDown(this)");
            txtData.Attributes.Add("onkeypress", "EE_KeyPress(this)");
            txtData.Attributes.Add("onclick", "EE_OnClick(this)");
            txtData.Attributes.Add("onfocus", "EE_GotFocus(this)");
            txtData.Attributes.Add("onblur", "EE_LostFocus(this)");

            MostraDados(matricula, codEnsino, codEvento, ViewState["operacao"].ToString());
            HabilitarCamposeBotoes(false);
 
        }
        if (ViewState["operacao"].ToString() == "A") // alterar
        {
            Master.titulo = "Alteração de Voluntário";
            dropVoluntario.Enabled = false;
        }
        else if (ViewState["operacao"].ToString() == "I") // cadastrar
            Master.titulo = "Inclusão de Voluntário";
        else if (ViewState["operacao"].ToString() == "E") // excluir
            Master.titulo = "Exclusão de Voluntário";

    }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;

        VoluntarioDOM voluntario = new VoluntarioDOM();

        voluntario.matricula = Conversor.ConverterParaInteiro(dropVoluntario.SelectedValue);
        voluntario.codEnsino = (rbtGraduacao.Checked ? 1 : 2);
        voluntario.codEvento = Conversor.ConverterParaInteiro(dropHorario.SelectedValue);
        voluntario.codHorario = Conversor.ConverterParaInteiro(dropHorario.SelectedValue);
        voluntario.codSala = Conversor.ConverterParaInteiro(dropSala.SelectedValue);
        voluntario.data = Conversor.ConverterParaDateTime(txtData.Text);

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm(voluntario);
            if (tudoOk)
                VoluntarioDAO.IncluirVoluntario(voluntario);
        }
        else if (ViewState["operacao"].ToString() == "A")
        {
            tudoOk = ValidaForm(voluntario);
            if (tudoOk)
                VoluntarioDAO.AlterarVoluntario(voluntario, ViewState["codEvento"].ToString());
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            string mens = VoluntarioDAO.ExcluirVoluntario(voluntario);
            if (!mens.Equals(string.Empty))
            {
                tudoOk = false;
                ExibirMensagemErro.Exibir(mens, this.Page);
            }
        }

        if (tudoOk)
            Response.Redirect("~/VoluntarioLista.aspx");    
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/VoluntarioLista.aspx"); 
    }

    public void MostraDados(int matricula, int codEnsino, int codEvento, string acao)
    {
        VoluntarioDOM voluntarioDados = new VoluntarioDOM();
        if (acao == "A" || acao == "E")
        {
            voluntarioDados = new VoluntarioDAO().ObterVoluntarioPeloRegistro(matricula, codEnsino, codEvento); 
        }
        ViewState["matricula"] = voluntarioDados.matricula;
        ViewState["codEnsino"] = voluntarioDados.codEnsino;
        ViewState["codEvento"] = voluntarioDados.codEvento;

        if (voluntarioDados.codEnsino.Equals(2))
        {
            rbtTecnologo.Checked = true;
            CarregaAlunosTecnologo();
        }
        else
        {
            rbtGraduacao.Checked = true;
            CarregaAlunosGraduacao();
        }


        if (voluntarioDados.matricula.Equals(0))
            dropVoluntario.SelectedValue = "-1";
        else
            dropVoluntario.SelectedValue = voluntarioDados.matricula.ToString();

        if (voluntarioDados.codEvento.Equals(0))
            dropHorario.SelectedValue = "1";
        else
            dropHorario.SelectedValue = voluntarioDados.codHorario.ToString();

        if (voluntarioDados.codEvento.Equals(0))
            dropSala.SelectedValue = "1";
        else
            dropSala.SelectedValue = voluntarioDados.codSala.ToString();

        if (voluntarioDados.codEvento.Equals(0))
            txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
        else
            txtData.Text = voluntarioDados.data.ToString("dd/MM/yyyy");

        if (acao == "A" || acao == "I")
            rbtGraduacao.Focus();
    }

    public void DesabilitarCampos()
    {
        rbtGraduacao.Enabled = false;
        rbtTecnologo.Enabled = false;
        dropVoluntario.Enabled = false;
        dropHorario.Enabled = false;
        dropSala.Enabled = false;
        txtData.Enabled = false;
    }

    public bool ValidaForm(VoluntarioDOM voluntario)
    {
        bool v = true;

        if (VoluntarioDAO.RegistroExiste(voluntario.matricula.ToString(), voluntario.codEnsino.ToString(), voluntario.codEvento.ToString() , ViewState["operacao"].ToString()))
        {
            ExibirMensagemErro.Exibir("Voluntário já cadastrado para este evento.", this.Page);
            rbtGraduacao.Focus();
            v = false;
        }
  
        return v;

    }


    protected void rbtGraduacao_CheckedChanged(object sender, EventArgs e)
    {
        CarregaAlunosGraduacao();
    }

    protected void CarregaAlunosGraduacao()
    {
        dropVoluntario.DataSource = AlunosDAO.DropDownAlunoGraduacao().InternalDataSet;
        dropVoluntario.DataTextField = AlunoDIC.COL_NOME;
        dropVoluntario.DataValueField = AlunoDIC.COL_MATRICULA;
        dropVoluntario.DataBind();
        dropVoluntario.Items.Add(new ListItem("SELECIONE UM VOLUNTÁRIO", "-1"));
    }

    protected void CarregaAlunosTecnologo()
    {
        dropVoluntario.DataSource = AlunosDAO.DropDownAlunoTecnologo().InternalDataSet;
        dropVoluntario.DataTextField = AlunoDIC.COL_NOME;
        dropVoluntario.DataValueField = AlunoDIC.COL_MATRICULA;
        dropVoluntario.DataBind();
        dropVoluntario.Items.Add(new ListItem("SELECIONE UM VOLUNTÁRIO", "-1"));
    }


    protected void rbtTecnologo_CheckedChanged(object sender, EventArgs e)
    {
        CarregaAlunosTecnologo();
    }
    protected void btnNovo_Click(object sender, ImageClickEventArgs e)
    {
        HabilitarCamposeBotoes(true);
    }

    protected void HabilitarCamposeBotoes(bool status)
    {
        rbtGraduacao.Enabled = !status;
        rbtTecnologo.Enabled = !status;
        dropVoluntario.Enabled = !status;
        btnNovo.Enabled = !status;
        txtData.Enabled = status;
        dropHorario.Enabled = status;
        dropSala.Enabled = status;
        btnConfirmar.Visible = status;
        btnCancelar.Visible = status;
        btnConfirmar.Enabled = status;
        btnCancelar.Enabled = status;
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        HabilitarCamposeBotoes(false);
    }
    protected void dropVoluntario_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropVoluntario.SelectedValue.Equals("-1"))
            btnNovo.Enabled = false;
        else
            btnNovo.Enabled = true;
    }
}
