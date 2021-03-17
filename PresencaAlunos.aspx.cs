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
using System.Text;
using DAO;
using DIC;
using Util;

public partial class PresencaAlunos : System.Web.UI.Page
{

    #region Page_Load(object sender, EventArgs e)
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager foco = ScriptManager.GetCurrent(Page);
        foco.SetFocus(dropDataEvento);

        MasterPage_Principal pagina = (MasterPage_Principal)this.Master;
        pagina.ConfirmarEvento += new EventoBotoes(ConfirmarClick);
        pagina.CancelarEvento += new EventoBotoes(CancelarClick);

        ImageButton salvar = (ImageButton)Master.FindControl("btnConfirmar");
        salvar.Visible = true;
        ImageButton cancelar = (ImageButton)Master.FindControl("btnCancelar");
        cancelar.Visible = true;

        Master.titulo = "Presença de Alunos no Evento";

        if (!Page.IsPostBack)
        {
            LimparDropDown();
        }
    }
    #endregion

    #region ConfirmarClick(object sender, EventArgs e)
    private void ConfirmarClick(object sender, EventArgs e)
    {
        if (dropDataEvento.SelectedValue.Equals("-1") || dropEventos.SelectedValue.Equals("-1") || dropHorario.SelectedValue.Equals("-1"))
        {
            ExibirMensagemErro.Exibir("Selecione uma data, um evento, e o seu respectivo horário para registrar as presenças dos alunos.", this.Page);
            dropDataEvento.Focus();
        }
        else
        {
            if (pnlIntegrante.Visible)
            {
                ExibirMensagemErro.Exibir("Favor confirmar o aluno antes de confirmar a gravação das presenças.", this.Page);
            }
            else
            {
                string mensErro = PresencaAlunosDAO.SalvarPresencadoAluno(((DataSet)ViewState["integrante"]));
                if (!mensErro.Equals(string.Empty))
                    ExibirMensagemErro.Exibir(mensErro, this.Page);
                else
                {
                    LimparDropDown();
                    ExibirMensagemErro.Exibir("Presenças de alunos gravadas com sucesso.", this.Page);
                }
            }
        }
    }
    #endregion

    #region CancelarClick(object sender, EventArgs e)
    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    #endregion

    #region dropEventos_SelectedIndexChanged(object sender, EventArgs e)
    protected void dropEventos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!dropEventos.SelectedValue.Equals("-1"))
        {
            SelecionarHorariosdosEventos(Conversor.ConverterParaInteiro(dropEventos.SelectedValue), Conversor.ConverterParaDateTime(dropDataEvento.SelectedValue));
        }
        else
        {
            dropHorario.SelectedValue = "-1";
            dropHorario.Enabled = false;
            SelecionarAlunosdoEvento(-1, Conversor.ConverterParaDateTime("01/01/1900"), -1);
        }
    }
    #endregion

    #region dropDataEvento_SelectedIndexChanged(object sender, EventArgs e)
    protected void dropDataEvento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!dropDataEvento.SelectedValue.Equals("-1"))
        {
            SelecionarEventos(Conversor.ConverterParaDateTime(dropDataEvento.SelectedValue) , true);

            dropHorario.SelectedValue = "-1";
            dropHorario.Enabled = false;
            SelecionarAlunosdoEvento(-1, Conversor.ConverterParaDateTime("01/01/1900"), -1);
        }
        else
            dropEventos.Enabled = false;
    }
    #endregion

    #region IncluirDadosIntegrante(string matricula, string codEnsino, string nome, string codEvento, string codHorario, string dataEvento)
    private void IncluirDadosIntegrante(string matricula, string codEnsino, string nome, string codEvento, string codHorario, string dataEvento)
    {

        DataRow[] dr = ((DataSet)ViewState["integrante"]).Tables[0].Select(InscricaoDIC.COL_MATRICULA+ " = " + matricula.ToString() + " and "+ InscricaoDIC.COL_COD_ENSINO+"= " + codEnsino.ToString());
        if (dr.Length == 0)
        {
            DataRow rowIntegrante = ((DataSet)ViewState["integrante"]).Tables[0].NewRow();
            rowIntegrante[InscricaoDIC.COL_MATRICULA] = Conversor.ConverterParaInteiro(matricula);
            rowIntegrante[InscricaoDIC.COL_COD_ENSINO] = Conversor.ConverterParaInteiro(codEnsino);
            rowIntegrante[AlunoDIC.COL_NOME] = nome;
            rowIntegrante[InscricaoDIC.COL_PRESENTE] = 1;
            rowIntegrante[EventoDIC.COL_COD_EVENTO] = Conversor.ConverterParaInteiro(codEvento);
            rowIntegrante[HorarioDIC.COL_COD_HORARIO] = Conversor.ConverterParaInteiro(codHorario);
            rowIntegrante[InscricaoDIC.COL_DATA] = Conversor.ConverterParaDateTime(dataEvento);

            ((DataSet)ViewState["integrante"]).Tables[0].Rows.Add(rowIntegrante);

            gridAlunosEvento.DataSource = ViewState["integrante"];
            gridAlunosEvento.DataBind();
        }
        else
            ExibirMensagemErro.Exibir("Aluno(a) (" + nome.Trim() + ") já adicionado(a) ao evento.", this.Page);
    }
    #endregion

    #region btnConfirmarIntegrante_Click(object sender, ImageClickEventArgs e)
    protected void btnConfirmarIntegrante_Click(object sender, ImageClickEventArgs e)
    {
        BotoesIntegrante(false);
        string tipoEnsino = string.Empty;
        if (rbtGraduacao.Checked)
            tipoEnsino = "1";
        else
            tipoEnsino = "2";

        IncluirDadosIntegrante(dropIntegrante.SelectedValue, tipoEnsino, dropIntegrante.SelectedItem.Text, dropEventos.SelectedValue, dropHorario.SelectedValue, dropDataEvento.SelectedValue);

    }
    #endregion

    #region btnCancelarIntegrante_Click(object sender, ImageClickEventArgs e)
    protected void btnCancelarIntegrante_Click(object sender, ImageClickEventArgs e)
    {
        BotoesIntegrante(false);
    }
    #endregion

    #region btnNovoIntegrante_Click(object sender, ImageClickEventArgs e)
    protected void btnNovoIntegrante_Click(object sender, ImageClickEventArgs e)
    {
        BotoesIntegrante(true);
        rbtGraduacao.Checked = true;
        rbtTecnologo.Checked = false;
        CarregaAlunosGraduacao();
        dropIntegrante.Focus();
    }
    #endregion

    #region BotoesIntegrante(bool status)
    protected void BotoesIntegrante(bool status)
    {
        lblAluno.Visible = status;
        dropIntegrante.Visible = status;
        btnConfirmarIntegrante.Visible = status;
        btnCancelarIntegrante.Visible = status;
        btnNovoIntegrante.Enabled = !status;
        pnlIntegrante.Visible = status;
    }
    #endregion

    #region ObterAlunosdoEvento()
    public DataSet ObterAlunosdoEvento(int codEvento, DateTime dataEvento, int codHorario)
    {
        ViewState["integrante"] = PresencaAlunosDAO.AlunosdoEvento(codEvento, dataEvento, codHorario).InternalDataSet;
        return (DataSet)ViewState["integrante"];
    }
    #endregion

    #region CarregaAlunosGraduacao()
    protected void CarregaAlunosGraduacao()
    {
        dropIntegrante.DataSource = AlunosDAO.DropDownAlunoGraduacao().InternalDataSet;
        dropIntegrante.DataTextField = AlunoDIC.COL_NOME;
        dropIntegrante.DataValueField = AlunoDIC.COL_MATRICULA;
        dropIntegrante.DataBind();
    }
    #endregion

    #region rbtTecnologo_CheckedChanged(object sender, EventArgs e)
    protected void rbtTecnologo_CheckedChanged(object sender, EventArgs e)
    {
        dropIntegrante.DataSource = AlunosDAO.DropDownAlunoTecnologo().InternalDataSet;
        dropIntegrante.DataTextField = AlunoDIC.COL_NOME;
        dropIntegrante.DataValueField = AlunoDIC.COL_MATRICULA;
        dropIntegrante.DataBind();
    }
    #endregion

    #region rbtGraduacao_CheckedChanged(object sender, EventArgs e)
    protected void rbtGraduacao_CheckedChanged(object sender, EventArgs e)
    {
        CarregaAlunosGraduacao();
    }
    #endregion

    #region dropHorario_SelectedIndexChanged(object sender, EventArgs e)
    protected void dropHorario_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!dropHorario.SelectedValue.Equals("-1"))
        {
            SelecionarAlunosdoEvento(Conversor.ConverterParaInteiro(dropEventos.SelectedValue), Conversor.ConverterParaDateTime(dropDataEvento.SelectedValue), Conversor.ConverterParaInteiro(dropHorario.SelectedValue));
            btnNovoIntegrante.Visible = true;
            
        }
        else
        {
            SelecionarAlunosdoEvento(-1, Conversor.ConverterParaDateTime("01/01/1900"), -1);
            btnNovoIntegrante.Visible = false;
        }
    }
    #endregion

    #region gridAlunosEvento_RowDataBound(object sender, GridViewRowEventArgs e)
    protected void gridAlunosEvento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton MyButton = (ImageButton)e.Row.FindControl("imgbtnExcluir");
            string nome = (string)DataBinder.Eval(e.Row.DataItem, AlunoDIC.COL_NOME);
            MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirma a exclusão do(a) aluno(a) (" + nome.Trim() + ") ?')");
        }
    }
    #endregion

    #region gridAlunosEvento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    protected void gridAlunosEvento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex].RowState != DataRowState.Added)
        {
            // Rotina que apaga o registro do dataset e adiciona no final com o RowState "Deleted"
            DataRow row = ((DataSet)ViewState["integrante"]).Tables[0].NewRow();
            row[EventoDIC.COL_COD_EVENTO] = ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex][EventoDIC.COL_COD_EVENTO];
            row[InscricaoDIC.COL_MATRICULA] = ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex][InscricaoDIC.COL_MATRICULA];
            row[InscricaoDIC.COL_COD_ENSINO] = ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex][InscricaoDIC.COL_COD_ENSINO];
            row[InscricaoDIC.COL_DATA] = ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex][InscricaoDIC.COL_DATA];
            row[HorarioDIC.COL_COD_HORARIO] = ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex][HorarioDIC.COL_COD_HORARIO];
            row[InscricaoDIC.COL_PRESENTE] = ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex][InscricaoDIC.COL_PRESENTE];

            ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex].Delete();
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex].AcceptChanges();

            ((DataSet)ViewState["integrante"]).Tables[0].Rows.Add(row);
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[((DataSet)ViewState["integrante"]).Tables[0].Rows.Count - 1].AcceptChanges();
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[((DataSet)ViewState["integrante"]).Tables[0].Rows.Count - 1].Delete();
        }
        else
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridAlunosEvento.PageSize * gridAlunosEvento.PageIndex) + e.RowIndex].Delete();

        if (e.RowIndex == 0 && gridAlunosEvento.PageIndex > 0)
            gridAlunosEvento.PageIndex -= 1;
        gridAlunosEvento.DataSource = ViewState["integrante"];
        gridAlunosEvento.DataBind();
    }

    #endregion

    #region ckbx_presenca_CheckedChanged(object sender, EventArgs e)
    protected void ckbx_presenca_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;

        string matricula = gridAlunosEvento.DataKeys[row.RowIndex][InscricaoDIC.COL_MATRICULA.ToLower()].ToString();
        string codEnsino = gridAlunosEvento.DataKeys[row.RowIndex][InscricaoDIC.COL_COD_ENSINO.ToLower()].ToString();
        string codEvento = gridAlunosEvento.DataKeys[row.RowIndex][EventoDIC.COL_COD_EVENTO.ToLower()].ToString();
        string codHorario = gridAlunosEvento.DataKeys[row.RowIndex][HorarioDIC.COL_COD_HORARIO.ToLower()].ToString();
        string data = gridAlunosEvento.DataKeys[row.RowIndex][InscricaoDIC.COL_DATA.ToLower()].ToString();

        DataTable linhas = ((DataSet)ViewState["integrante"]).Tables[0];
        for (int i = 0; i <= linhas.Rows.Count - 1; i++)
	    {
		    if((linhas.Rows[i][InscricaoDIC.COL_MATRICULA].ToString() == matricula) && 
                (linhas.Rows[i][InscricaoDIC.COL_COD_ENSINO].ToString() == codEnsino) && 
                (linhas.Rows[i][EventoDIC.COL_COD_EVENTO].ToString() == codEvento) && 
                (linhas.Rows[i][HorarioDIC.COL_COD_HORARIO].ToString() == codHorario) && 
                (linhas.Rows[i][InscricaoDIC.COL_DATA].ToString() == data))
            {

                linhas.Rows[i][InscricaoDIC.COL_PRESENTE] = checkbox.Checked;
            }
	    }
    }
    #endregion

    #region SelecionarHorariosdosEventos(int codEvento, DateTime dataEvento)
    public void SelecionarHorariosdosEventos(int codEvento, DateTime dataEvento)
    {
        dropHorario.Enabled = true;
        dropHorario.DataTextField = HorarioDIC.COL_DESC_CONSULTA;
        dropHorario.DataValueField = HorarioDIC.COL_COD_HORARIO;
        dropHorario.DataSource = EventoDAO.HorarioDoEvento(codEvento, dataEvento).InternalDataSet;
        dropHorario.DataBind();
        dropHorario.Items.Add(new ListItem("SELECIONE O HORÁRIO", "-1"));
        dropHorario.SelectedValue = "-1";
        btnNovoIntegrante.Visible = false;
    }
    #endregion

    #region SelecionarAlunosdoEvento(int codEvento, DateTime dataEvento, int codHorario)
    public void SelecionarAlunosdoEvento(int codEvento, DateTime dataEvento, int codHorario)
    {
        gridAlunosEvento.DataSource = ObterAlunosdoEvento(codEvento, dataEvento, codHorario);
        gridAlunosEvento.DataBind();
    }
    #endregion

    #region SelecionarEventos(DateTime dataEvento, bool status)
    public void SelecionarEventos(DateTime dataEvento, bool status)
    {
        dropEventos.Enabled = status;
        dropEventos.DataTextField = EventoDIC.COL_TITULO;
        dropEventos.DataValueField = EventoDIC.COL_COD_EVENTO;
        dropEventos.DataSource = EventoDAO.DropDownEventosInscricao(dataEvento.ToString()).InternalDataSet;
        dropEventos.DataBind();
        dropEventos.Items.Add(new ListItem("SELECIONE UM EVENTO","-1"));
        dropEventos.SelectedValue = "-1";
    }
    #endregion

    #region LimparDropDown()
    public void LimparDropDown()
    {
        SelecionarDatadoEvento();
        SelecionarEventos(Conversor.ConverterParaDateTime("01/01/1900"), false);
        SelecionarHorariosdosEventos(-1, Conversor.ConverterParaDateTime("01/01/1900"));
        SelecionarAlunosdoEvento(-1, Conversor.ConverterParaDateTime("01/01/1900"), -1);
        dropEventos.Enabled = false;
        dropHorario.Enabled = false;
        ViewState["integrante"] = null;
        dropDataEvento.Focus();
    }
    #endregion

    #region SelecionarDatadoEvento()
    public void SelecionarDatadoEvento()
    {
        dropDataEvento.DataTextField = InscricaoDIC.COL_DATA;
        dropDataEvento.DataValueField = InscricaoDIC.COL_DATA;
        dropDataEvento.DataSource = InscricaoDAO.DropDownDatadaInscricao().InternalDataSet;
        dropDataEvento.DataBind();
        dropDataEvento.Items.Add(new ListItem("", "-1"));
        dropDataEvento.SelectedValue = "-1";
    }
    #endregion


}
