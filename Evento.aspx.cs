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
using Dominio;
using DAO;
using DIC;
using Util;

public partial class Evento : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPage_Principal pagina = (MasterPage_Principal)this.Master;
        pagina.ConfirmarEvento += new EventoBotoes(ConfirmarClick);
        pagina.CancelarEvento += new EventoBotoes(CancelarClick);

        ImageButton salvar = (ImageButton)Master.FindControl("btnConfirmar");
        salvar.Visible = true;

        ImageButton cancelar = (ImageButton)Master.FindControl("btnCancelar");
        cancelar.Visible = true;

        if (!Page.IsPostBack)
        {
            ViewState["cod_evento"] = Request["codigo"].Trim();
            ViewState["operacao"] = Request["operacao"].Trim();

            if (ViewState["operacao"].ToString() == "E") // excluir
            {
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir este evento ?')");
                DesabilitarCampos();
            }

            dropTipoEvento.DataSource = TipoEventoDAO.ListarTipoEvento(true).InternalDataSet;
            dropTipoEvento.DataTextField = TipoEventoDIC.COL_DESCRICAO;
            dropTipoEvento.DataValueField = TipoEventoDIC.COL_COD_TIPO_EVENTO;
            dropTipoEvento.DataBind();
            dropTipoEvento.SelectedValue = "1";

            dropNucleo.DataSource = NucleoDAO.ListarNucleo(true).InternalDataSet;
            dropNucleo.DataTextField = NucleoDIC.COL_DESCRICAO;
            dropNucleo.DataValueField = NucleoDIC.COL_CODNUCLEO;
            dropNucleo.DataBind();
            dropNucleo.SelectedValue = "1";

            dropCaracterizacao.DataSource = CaracterizacaoDAO.ListarCaracterizacao(true).InternalDataSet;
            dropCaracterizacao.DataTextField = CaracterizacaoDIC.COL_DESCRICAO;
            dropCaracterizacao.DataValueField = CaracterizacaoDIC.COL_COD_CARACTERIZACAO;
            dropCaracterizacao.DataBind();
            dropCaracterizacao.SelectedValue = "1";

            dropRecursos.DataSource = RecursoDAO.ListarRecurso(true).InternalDataSet;
            dropRecursos.DataTextField = RecursoDIC.COL_DESCRICAO;
            dropRecursos.DataValueField = RecursoDIC.COL_COD_RECURSO;
            dropRecursos.DataBind();
            dropRecursos.SelectedValue = "1";

            gridBanca.DataSource = ObterDadosBanca();
            gridBanca.DataBind();

            gridOrientador.DataSource = ObterDadosOrientador();
            gridOrientador.DataBind();

            gridPalestrante.DataSource = ObterDadosPalestrante();
            gridPalestrante.DataBind();

            gridIntegrante.DataSource = ObterDadosIntegrante();
            gridIntegrante.DataBind();

            gridCalendario.DataSource = ObterDadosCalendario();
            gridCalendario.DataBind();

            txtNumVagas.Attributes.Add("MaxLength", "3");
            txtNumVagas.Attributes.Add("mask", "___");
            txtNumVagas.Attributes.Add("onkeydown", "EE_KeyDown(this)");
            txtNumVagas.Attributes.Add("onkeypress", "EE_KeyPress(this)");
            txtNumVagas.Attributes.Add("onclick", "EE_OnClick(this)");
            txtNumVagas.Attributes.Add("onfocus", "EE_GotFocus(this)");
            txtNumVagas.Attributes.Add("onblur", "EE_LostFocus(this)");

            txtData.Attributes.Add("MaxLength", "10");
            txtData.Attributes.Add("mask", "__/__/____");
            txtData.Attributes.Add("onkeydown", "EE_KeyDown(this)");
            txtData.Attributes.Add("onkeypress", "EE_KeyPress(this)");
            txtData.Attributes.Add("onclick", "EE_OnClick(this)");
            txtData.Attributes.Add("onfocus", "EE_GotFocus(this)");
            txtData.Attributes.Add("onblur", "EE_LostFocus(this)");

            MostraDados(ViewState["cod_evento"].ToString(), ViewState["operacao"].ToString());
        }
        if (ViewState["operacao"].ToString() == "A") // alterar
            Master.titulo = "Alteração do Evento";
        else if (ViewState["operacao"].ToString() == "I") // inclusao
            Master.titulo = "Inclusão do Evento";
        else if (ViewState["operacao"].ToString() == "E") // excluir
            Master.titulo = "Exclusão do Evento";


    }

    #region Eventos

        #region ConfirmarClick(object sender, EventArgs e)
    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;
        string mens = string.Empty;

        EventoDOM evento = new EventoDOM();

        evento.codEvento = Conversor.ConverterParaInteiro(ViewState["cod_evento"].ToString());
        evento.titulo = txtTitulo.Text;
        evento.codTipoEvento = Conversor.ConverterParaInteiro(dropTipoEvento.SelectedValue);
        evento.ementa = txtEmenta.Text;
        evento.objetivos = txtObjetivos.Text;
        evento.publicoAlvo = txtPublicoAlvo.Text;
        evento.codNucleo = Conversor.ConverterParaInteiro(dropNucleo.SelectedValue);
        evento.codCaracterizacao = Conversor.ConverterParaInteiro(dropCaracterizacao.SelectedValue);
        evento.codRecurso = Conversor.ConverterParaInteiro(dropRecursos.SelectedValue);
        evento.numVagas = Conversor.ConverterParaInteiro(txtNumVagas.Text.Replace("_",""));

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm(txtTitulo.Text);
            if (tudoOk)
            {
                DataSet dsBancaInc = ((DataSet)ViewState["banca"]);
                DataSet dsOrientadorInc = ((DataSet)ViewState["orientador"]);
                DataSet dsPalestranteInc = ((DataSet)ViewState["palestrante"]);
                DataSet dsIntegranteInc = ((DataSet)ViewState["integrante"]);
                DataSet dsCalendarioInc = ((DataSet)ViewState["calendario"]);

                EventoDAO.IncluirEvento(evento, dsBancaInc, dsOrientadorInc, dsPalestranteInc, dsIntegranteInc, dsCalendarioInc);
            }
        }
        else if (ViewState["operacao"].ToString() == "A")
        {
            tudoOk = ValidaForm(txtTitulo.Text);
            if (tudoOk)
            {
                DataSet dsBancaAlt = ((DataSet)ViewState["banca"]);
                DataSet dsOrientadorAlt = ((DataSet)ViewState["orientador"]);
                DataSet dsPalestranteAlt = ((DataSet)ViewState["palestrante"]);
                DataSet dsIntegranteAlt = ((DataSet)ViewState["integrante"]);
                DataSet dsCalendarioAlt = ((DataSet)ViewState["calendario"]);

                EventoDAO.AlterarEvento(evento, dsBancaAlt, dsOrientadorAlt, dsPalestranteAlt, dsIntegranteAlt, dsCalendarioAlt);
            }
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            mens = EventoDAO.ExcluirEvento(evento.codEvento);
            if (!mens.Equals(string.Empty))
               tudoOk = false;
        }

        if(tudoOk)
            Response.Redirect("~/EventoLista.aspx");
        else
        {
            MultiView1.ActiveViewIndex = 0;
            lblTituloView.Text = "Evento";
            if ((ViewState["operacao"].ToString() == "E") && (!mens.Equals(string.Empty)))
                ExibirMensagemErro.Exibir(mens, this.Page);
            else
                ExibirMensagemErro.Exibir("Favor verificar os campos destacados.", this.Page);
        }
    }

    #endregion

        #region CancelarClick(object sender, EventArgs e)
    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/EventoLista.aspx");
    }
    #endregion

        #region MostraDados(string codigo, string acao)
    public void MostraDados(string codigo, string acao)
    {
        EventoDOM eventoDados = new EventoDOM();
        if (acao == "A" || acao == "E")
        {
            eventoDados = new EventoDAO().ObterEventoPeloId(Convert.ToInt32(codigo));
        }
        ViewState["cod_evento"] = eventoDados.codEvento;


        txtTitulo.Text = eventoDados.titulo;

        if (acao == "I")
        {
            dropTipoEvento.SelectedValue = "-1";
            dropNucleo.SelectedValue = "-1";
            dropCaracterizacao.SelectedValue = "-1";
            dropRecursos.SelectedValue = "-1";
        }
        else
        {
            dropTipoEvento.SelectedValue = eventoDados.codTipoEvento.ToString();
            dropNucleo.SelectedValue = eventoDados.codNucleo.ToString();
            dropCaracterizacao.SelectedValue = eventoDados.codCaracterizacao.ToString();
            dropRecursos.SelectedValue = eventoDados.codRecurso.ToString();
        }

        txtEmenta.Text = eventoDados.ementa;
        txtObjetivos.Text = eventoDados.objetivos;
        txtPublicoAlvo.Text = eventoDados.publicoAlvo;

        if (eventoDados.numVagas == 0)
            txtNumVagas.Text = "";
        else
            txtNumVagas.Text = StringSuporte.Completar(eventoDados.numVagas.ToString(),3, "_");

        if (acao == "A" || acao == "I")
            txtData.Focus();
    }
    #endregion

        #region DesabilitarCampos()
    public void DesabilitarCampos()
    {
        txtTitulo.Enabled = false;
        dropTipoEvento.Enabled = false;
        txtEmenta.Enabled = false;
        txtObjetivos.Enabled = false;
        txtPublicoAlvo.Enabled = false;
        dropNucleo.Enabled = false;
        dropCaracterizacao.Enabled = false;
        dropRecursos.Enabled = false;
        txtNumVagas.Enabled = false;
        btnNovoBanca.Enabled = false;
        btnNovoOrientador.Enabled = false;
        btnNovoPalestrante.Enabled = false;
        btnNovoIntegrante.Enabled = false;
        btnNovoCalendario.Enabled = false;
    }
    #endregion

        #region ValidaForm()
    public bool ValidaForm(string titulo)
    {
        bool v = true;

        if (EventoDAO.RegistroExiste(ViewState["cod_evento"].ToString(), titulo, ViewState["operacao"].ToString()))
        {
            ExibirMensagemErro.Exibir("Evento já cadastrado.", this.Page);
            MultiView1.ActiveViewIndex = 0;
            txtTitulo.Focus();
            v = false;
        }

        if (v)
            v = Validacao.ValidaTextBox(this.Page, txtTitulo);
        //if (v)
        //    v = Validacao.ValidaTextBox(this.Page, txtEmenta);
        //if (v)
        //    v = Validacao.ValidaTextBox(this.Page, txtObjetivos);
        //if (v)
        //    v = Validacao.ValidaTextBox(this.Page, txtPublicoAlvo);
        if (v)
            v = Validacao.ValidaTextBox(this.Page, txtNumVagas);
        if (v)
            v = Validacao.ValidaListBox(this.Page, dropTipoEvento);
        //if (v)
        //    v = Validacao.ValidaListBox(this.Page, dropNucleo);
        if (v)
            v = Validacao.ValidaListBox(this.Page, dropCaracterizacao);
        //if (v)
        //    v = Validacao.ValidaListBox(this.Page, dropRecursos);

        return v;
    }
    #endregion

        #region lnkEvento_Click(object sender, EventArgs e)
    protected void lnkEvento_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        lblTituloView.Text = "Evento";
    }
    #endregion

        #region lnkBanca_Click(object sender, EventArgs e)
    protected void lnkBanca_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        lblTituloView.Text = "Banca";
    }
    #endregion

        #region lnkOrientador_Click(object sender, EventArgs e)
    protected void lnkOrientador_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;
        lblTituloView.Text = "Orientador";
    }
    #endregion

        #region lnkPalestrante_Click(object sender, EventArgs e)
    protected void lnkPalestrante_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;
        lblTituloView.Text = "Palestrante";
    }
    #endregion

        #region lnkIntegrantes_Click(object sender, EventArgs e)
    protected void lnkIntegrantes_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
        lblTituloView.Text = "Integrantes";
    }
    #endregion

        #region lnkCalendario_Click(object sender, EventArgs e)
    protected void lnkCalendario_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 5;
        lblTituloView.Text = "Calendário";
    }
    #endregion

        #region rbtGraduacao_CheckedChanged(object sender, EventArgs e)



    protected void CarregaAlunosGraduacao()
    {
        dropIntegrante.DataSource = AlunosDAO.DropDownAlunoGraduacao().InternalDataSet;
        dropIntegrante.DataTextField = AlunoDIC.COL_NOME;
        dropIntegrante.DataValueField = AlunoDIC.COL_MATRICULA;
        dropIntegrante.DataBind();
    }


    protected void rbtGraduacao_CheckedChanged(object sender, EventArgs e)
    {
        CarregaAlunosGraduacao();
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

    #endregion

    #region Banca Avaliadora

    #region ObterDadosBanca()
    public DataSet ObterDadosBanca()
    {
        ViewState["banca"] = EventoDAO.BancaDoEvento(Conversor.ConverterParaInteiro(ViewState["cod_evento"])).InternalDataSet;
        return (DataSet)ViewState["banca"];
    }
    #endregion

        #region BotoesBanca(bool status)
    protected void BotoesBanca(bool status)
    {
        lblBancaProf.Visible = status;
        dropBancaProf.Visible = status;
        btnConfirmarBanca.Visible = status;
        btnCancelarBanca.Visible = status;
        btnNovoBanca.Enabled = !status;
        pnlBanca.Visible = status;
    }
    #endregion

        #region btnNovoBanca_Click(object sender, ImageClickEventArgs e)
    protected void btnNovoBanca_Click(object sender, ImageClickEventArgs e)
    {
        BotoesBanca(true);
        dropBancaProf.DataSource = ProfessorDAO.DropDownProfessor().InternalDataSet;
        dropBancaProf.DataTextField = "nome";
        dropBancaProf.DataValueField = "cod_professor";
        dropBancaProf.DataBind();
        dropBancaProf.Focus();
    }
    #endregion

        #region btnConfirmarBanca_Click(object sender, ImageClickEventArgs e)
    protected void btnConfirmarBanca_Click(object sender, ImageClickEventArgs e)
    {
        BotoesBanca(false);
        IncluirDadosBanca(dropBancaProf.SelectedValue, dropBancaProf.SelectedItem.Text);
    }
    #endregion

        #region IncluirDadosBanca(string codigo, string nome)
    private void IncluirDadosBanca(string codigo, string nome)
    {
        DataRow[] dr = ((DataSet)ViewState["banca"]).Tables[0].Select("cod_evento = " + ViewState["cod_evento"].ToString() + " and cod_professor = " + codigo.ToString());
        if (dr.Length == 0)
        {
            DataRow rowBanca = ((DataSet)ViewState["banca"]).Tables[0].NewRow();
            rowBanca["cod_evento"] = Conversor.ConverterParaInteiro(ViewState["cod_evento"].ToString());
            rowBanca["cod_professor"] = Conversor.ConverterParaInteiro(codigo);
            rowBanca["nome"] = nome;
            ((DataSet)ViewState["banca"]).Tables[0].Rows.Add(rowBanca);

            gridBanca.DataSource = ViewState["banca"];
            gridBanca.DataBind();
        }
        else
            ExibirMensagemErro.Exibir("O professor (" + nome + ") já está adicionado à banca deste evento.", this.Page);
    }
    #endregion

        #region btnCancelarBanca_Click(object sender, ImageClickEventArgs e)
    protected void btnCancelarBanca_Click(object sender, ImageClickEventArgs e)
    {
        BotoesBanca(false);
    }
    #endregion

        #region gridBanca_RowDataBound(object sender, GridViewRowEventArgs e)
    protected void gridBanca_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton MyButton = (ImageButton)e.Row.FindControl("imgbtnExcluir");

            MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirma a exclusão do Professor (" + DataBinder.Eval(e.Row.DataItem, "nome") + ") da banca ?')");
        }
    }
    #endregion

        #region gridBanca_RowDeleting(object sender, GridViewDeleteEventArgs e)
    protected void gridBanca_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (((DataSet)ViewState["banca"]).Tables[0].Rows[(gridBanca.PageSize * gridBanca.PageIndex) + e.RowIndex].RowState != DataRowState.Added)
        {
            // Rotina que apaga o registro do dataset e adiciona no final com o RowState "Deleted"
            DataRow row = ((DataSet)ViewState["banca"]).Tables[0].NewRow();
            row["cod_evento"] = ((DataSet)ViewState["banca"]).Tables[0].Rows[(gridBanca.PageSize * gridBanca.PageIndex) + e.RowIndex]["cod_evento"];
            row["cod_professor"] = ((DataSet)ViewState["banca"]).Tables[0].Rows[(gridBanca.PageSize * gridBanca.PageIndex) + e.RowIndex]["cod_professor"];
            ((DataSet)ViewState["banca"]).Tables[0].Rows[(gridBanca.PageSize * gridBanca.PageIndex) + e.RowIndex].Delete();
            ((DataSet)ViewState["banca"]).Tables[0].Rows[(gridBanca.PageSize * gridBanca.PageIndex) + e.RowIndex].AcceptChanges();

            ((DataSet)ViewState["banca"]).Tables[0].Rows.Add(row);
            ((DataSet)ViewState["banca"]).Tables[0].Rows[((DataSet)ViewState["banca"]).Tables[0].Rows.Count - 1].AcceptChanges();
            ((DataSet)ViewState["banca"]).Tables[0].Rows[((DataSet)ViewState["banca"]).Tables[0].Rows.Count - 1].Delete();
        }
        else
            ((DataSet)ViewState["banca"]).Tables[0].Rows[(gridBanca.PageSize * gridBanca.PageIndex) + e.RowIndex].Delete();

        if (e.RowIndex == 0 && gridBanca.PageIndex > 0)
            gridBanca.PageIndex -= 1;
        gridBanca.DataSource = ViewState["banca"];
        gridBanca.DataBind();

    }
    #endregion

    #endregion

    #region Orientadores do Evento

        #region ObterDadosOrientador()
    public DataSet ObterDadosOrientador()
    {
        ViewState["orientador"] = EventoDAO.OrientadorDoEvento(Conversor.ConverterParaInteiro(ViewState["cod_evento"])).InternalDataSet;
        return (DataSet)ViewState["orientador"];
    }
    #endregion

        #region BotoesOrientador(bool status)
    protected void BotoesOrientador(bool status)
    {
        lblOrientadorProf.Visible = status;
        dropOrientadorProf.Visible = status;
        btnConfirmarOrientador.Visible = status;
        btnCancelarOrientador.Visible = status;
        btnNovoOrientador.Enabled = !status;
        pnlOrientador.Visible = status;

    }
    #endregion

        #region btnNovoOrientador_Click(object sender, ImageClickEventArgs e)
    protected void btnNovoOrientador_Click(object sender, ImageClickEventArgs e)
    {
        BotoesOrientador(true);
        dropOrientadorProf.DataSource = ProfessorDAO.DropDownProfessor().InternalDataSet;
        dropOrientadorProf.DataTextField = "nome";
        dropOrientadorProf.DataValueField = "cod_professor";
        dropOrientadorProf.DataBind();
        dropOrientadorProf.Focus();
    }
    #endregion

        #region IncluirDadosOrientador(string codigo, string nome)
    private void IncluirDadosOrientador(string codigo, string nome)
    {

        DataRow[] dr = ((DataSet)ViewState["orientador"]).Tables[0].Select("cod_evento = " + ViewState["cod_evento"].ToString() + " and cod_professor = " + codigo.ToString());
        if (dr.Length == 0)
        {
            DataRow rowOrientador = ((DataSet)ViewState["orientador"]).Tables[0].NewRow();
            rowOrientador["cod_evento"] = Conversor.ConverterParaInteiro(ViewState["cod_evento"].ToString());
            rowOrientador["cod_professor"] = Conversor.ConverterParaInteiro(codigo);
            rowOrientador["nome"] = nome;
            ((DataSet)ViewState["orientador"]).Tables[0].Rows.Add(rowOrientador);

            gridOrientador.DataSource = ViewState["orientador"];
            gridOrientador.DataBind();
        }
        else
            ExibirMensagemErro.Exibir("O professor (" + nome + ") já está adicionado aos orientadores deste evento.", this.Page);
    }
    #endregion

        #region btnConfirmarOrientador_Click(object sender, ImageClickEventArgs e)
    protected void btnConfirmarOrientador_Click(object sender, ImageClickEventArgs e)
    {
        BotoesOrientador(false);
        IncluirDadosOrientador(dropOrientadorProf.SelectedValue, dropOrientadorProf.SelectedItem.Text);

    }
    #endregion

        #region btnCancelarOrientador_Click(object sender, ImageClickEventArgs e)
    protected void btnCancelarOrientador_Click(object sender, ImageClickEventArgs e)
    {
        BotoesOrientador(false);
    }
    #endregion

        #region gridOrientador_RowDataBound(object sender, GridViewRowEventArgs e)
    protected void gridOrientador_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton MyButton = (ImageButton)e.Row.FindControl("imgbtnExcluir");

            MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirma a exclusão do Professor (" + DataBinder.Eval(e.Row.DataItem, "nome") + ") dos orientadores do evento ?')");
        }
    }
    #endregion

        #region gridOrientador_RowDeleting(object sender, GridViewDeleteEventArgs e)
    protected void gridOrientador_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (((DataSet)ViewState["orientador"]).Tables[0].Rows[(gridOrientador.PageSize * gridOrientador.PageIndex) + e.RowIndex].RowState != DataRowState.Added)
        {
            // Rotina que apaga o registro do dataset e adiciona no final com o RowState "Deleted"
            DataRow row = ((DataSet)ViewState["orientador"]).Tables[0].NewRow();
            row["cod_evento"] = ((DataSet)ViewState["orientador"]).Tables[0].Rows[(gridOrientador.PageSize * gridOrientador.PageIndex) + e.RowIndex]["cod_evento"];
            row["cod_professor"] = ((DataSet)ViewState["orientador"]).Tables[0].Rows[(gridOrientador.PageSize * gridOrientador.PageIndex) + e.RowIndex]["cod_professor"];
            ((DataSet)ViewState["orientador"]).Tables[0].Rows[(gridOrientador.PageSize * gridOrientador.PageIndex) + e.RowIndex].Delete();
            ((DataSet)ViewState["orientador"]).Tables[0].Rows[(gridOrientador.PageSize * gridOrientador.PageIndex) + e.RowIndex].AcceptChanges();

            ((DataSet)ViewState["orientador"]).Tables[0].Rows.Add(row);
            ((DataSet)ViewState["orientador"]).Tables[0].Rows[((DataSet)ViewState["orientador"]).Tables[0].Rows.Count - 1].AcceptChanges();
            ((DataSet)ViewState["orientador"]).Tables[0].Rows[((DataSet)ViewState["orientador"]).Tables[0].Rows.Count - 1].Delete();
        }
        else
            ((DataSet)ViewState["orientador"]).Tables[0].Rows[(gridOrientador.PageSize * gridOrientador.PageIndex) + e.RowIndex].Delete();

        if (e.RowIndex == 0 && gridOrientador.PageIndex > 0)
            gridOrientador.PageIndex -= 1;
        gridOrientador.DataSource = ViewState["orientador"];
        gridOrientador.DataBind();
    }
    #endregion

    #endregion

    #region Palestrantes do Evento

        #region ObterDadosPalestrante()
    public DataSet ObterDadosPalestrante()
    {
        ViewState["palestrante"] = EventoDAO.PalestranteDoEvento(Conversor.ConverterParaInteiro(ViewState["cod_evento"])).InternalDataSet;
        return (DataSet)ViewState["palestrante"];
    }
    #endregion

        #region BotoesPalestrante(bool status)
    protected void BotoesPalestrante(bool status)
    {
        lblPalestranteProf.Visible = status;
        dropPalestranteProf.Visible = status;
        btnConfirmarPalestrante.Visible = status;
        btnCancelarPalestrante.Visible = status;
        btnNovoPalestrante.Enabled = !status;
        pnlPalestrante.Visible = status;
    }
    #endregion

        #region btnNovoPalestrante_Click(object sender, ImageClickEventArgs e)
    protected void btnNovoPalestrante_Click(object sender, ImageClickEventArgs e)
    {
        BotoesPalestrante(true);
        dropPalestranteProf.DataSource = ProfessorDAO.DropDownProfessor().InternalDataSet;
        dropPalestranteProf.DataTextField = "nome";
        dropPalestranteProf.DataValueField = "cod_professor";
        dropPalestranteProf.DataBind();
        dropPalestranteProf.Focus();
    }
    #endregion

        #region IncluirDadosPalestrante(string codigo, string nome)
    private void IncluirDadosPalestrante(string codigo, string nome)
    {

        DataRow[] dr = ((DataSet)ViewState["palestrante"]).Tables[0].Select("cod_evento = " + ViewState["cod_evento"].ToString() + " and cod_professor = " + codigo.ToString());
        if (dr.Length == 0)
        {
            DataRow rowPalestrante = ((DataSet)ViewState["palestrante"]).Tables[0].NewRow();
            rowPalestrante["cod_evento"] = Conversor.ConverterParaInteiro(ViewState["cod_evento"].ToString());
            rowPalestrante["cod_professor"] = Conversor.ConverterParaInteiro(codigo);
            rowPalestrante["nome"] = nome;
            ((DataSet)ViewState["palestrante"]).Tables[0].Rows.Add(rowPalestrante);

            gridPalestrante.DataSource = ViewState["palestrante"];
            gridPalestrante.DataBind();
        }
        else
            ExibirMensagemErro.Exibir("O professor (" + nome + ") já está adicionado aos palestrantes deste evento.", this.Page);
    }
    #endregion

        #region btnConfirmarPalestrante_Click(object sender, ImageClickEventArgs e)
    protected void btnConfirmarPalestrante_Click(object sender, ImageClickEventArgs e)
    {
        BotoesPalestrante(false);
        IncluirDadosPalestrante(dropPalestranteProf.SelectedValue, dropPalestranteProf.SelectedItem.Text);
    }
    #endregion

        #region btnCancelarPalestrante_Click(object sender, ImageClickEventArgs e)
    protected void btnCancelarPalestrante_Click(object sender, ImageClickEventArgs e)
    {
        BotoesPalestrante(false);
    }
    #endregion

        #region gridPalestrante_RowDataBound(object sender, GridViewRowEventArgs e)
    protected void gridPalestrante_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton MyButton = (ImageButton)e.Row.FindControl("imgbtnExcluir");

            MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirma a exclusão do Palestrante (" + DataBinder.Eval(e.Row.DataItem, "nome") + ") ?')");
        }
    }
    #endregion

        #region gridPalestrante_RowDeleting(object sender, GridViewDeleteEventArgs e)
    protected void gridPalestrante_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (((DataSet)ViewState["palestrante"]).Tables[0].Rows[(gridPalestrante.PageSize * gridPalestrante.PageIndex) + e.RowIndex].RowState != DataRowState.Added)
        {
            // Rotina que apaga o registro do dataset e adiciona no final com o RowState "Deleted"
            DataRow row = ((DataSet)ViewState["palestrante"]).Tables[0].NewRow();
            row["cod_evento"] = ((DataSet)ViewState["palestrante"]).Tables[0].Rows[(gridPalestrante.PageSize * gridPalestrante.PageIndex) + e.RowIndex]["cod_evento"];
            row["cod_professor"] = ((DataSet)ViewState["palestrante"]).Tables[0].Rows[(gridPalestrante.PageSize * gridPalestrante.PageIndex) + e.RowIndex]["cod_professor"];
            ((DataSet)ViewState["palestrante"]).Tables[0].Rows[(gridPalestrante.PageSize * gridPalestrante.PageIndex) + e.RowIndex].Delete();
            ((DataSet)ViewState["palestrante"]).Tables[0].Rows[(gridPalestrante.PageSize * gridPalestrante.PageIndex) + e.RowIndex].AcceptChanges();

            ((DataSet)ViewState["palestrante"]).Tables[0].Rows.Add(row);
            ((DataSet)ViewState["palestrante"]).Tables[0].Rows[((DataSet)ViewState["palestrante"]).Tables[0].Rows.Count - 1].AcceptChanges();
            ((DataSet)ViewState["palestrante"]).Tables[0].Rows[((DataSet)ViewState["palestrante"]).Tables[0].Rows.Count - 1].Delete();
        }
        else
            ((DataSet)ViewState["palestrante"]).Tables[0].Rows[(gridPalestrante.PageSize * gridPalestrante.PageIndex) + e.RowIndex].Delete();

        if (e.RowIndex == 0 && gridPalestrante.PageIndex > 0)
            gridPalestrante.PageIndex -= 1;
        gridPalestrante.DataSource = ViewState["palestrante"];
        gridPalestrante.DataBind();
    }
    #endregion

    #endregion

    #region Alunos Integrantes do Evento

        #region ObterDadosIntegrante()
    public DataSet ObterDadosIntegrante()
    {
        ViewState["integrante"] = EventoDAO.IntegranteDoEvento(Conversor.ConverterParaInteiro(ViewState["cod_evento"])).InternalDataSet;
        return (DataSet)ViewState["integrante"];
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

        #region btnNovoIntegrante_Click(object sender, ImageClickEventArgs e)
    protected void btnNovoIntegrante_Click(object sender, ImageClickEventArgs e)
    {
        BotoesIntegrante(true);
        
        rbtGraduacao.Checked = true;
        CarregaAlunosGraduacao();
        dropIntegrante.Focus();
    }
    #endregion

        #region IncluirDadosIntegrante(string matricula, string codEnsino, string nome)
    private void IncluirDadosIntegrante(string matricula, string codEnsino, string nome)
    {

        DataRow[] dr = ((DataSet)ViewState["integrante"]).Tables[0].Select("cod_evento = " + ViewState["cod_evento"].ToString() + " and matricula = " + matricula.ToString() + " and cod_ensino =" + codEnsino.ToString());
        if (dr.Length == 0)
        {
            DataRow rowIntegrante = ((DataSet)ViewState["integrante"]).Tables[0].NewRow();
            rowIntegrante[EventoDIC.COL_COD_EVENTO] = Conversor.ConverterParaInteiro(ViewState["cod_evento"].ToString());
            rowIntegrante[IntegranteDIC.COL_MATRICULA] = Conversor.ConverterParaInteiro(matricula);
            rowIntegrante[IntegranteDIC.COL_COD_ENSINO] = Conversor.ConverterParaInteiro(codEnsino);
            rowIntegrante[AlunoDIC.COL_NOME] = nome;

            ((DataSet)ViewState["integrante"]).Tables[0].Rows.Add(rowIntegrante);

            gridIntegrante.DataSource = ViewState["integrante"];
            gridIntegrante.DataBind();
        }
        else
            ExibirMensagemErro.Exibir("O integrante (" + nome + ") já está adicionado aos integrantes deste evento.", this.Page);
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

        IncluirDadosIntegrante(dropIntegrante.SelectedValue, tipoEnsino, dropIntegrante.SelectedItem.Text);

    }
    #endregion
  
        #region btnCancelarIntegrante_Click(object sender, ImageClickEventArgs e)
    protected void btnCancelarIntegrante_Click(object sender, ImageClickEventArgs e)
    {
        BotoesIntegrante(false);
    }
    #endregion

        #region gridIntegrante_RowDataBound(object sender, GridViewRowEventArgs e)
    protected void gridIntegrante_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton MyButton = (ImageButton)e.Row.FindControl("imgbtnExcluir");

            MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirma a exclusão do Integrante (" + DataBinder.Eval(e.Row.DataItem, AlunoDIC.COL_NOME) + ") ?')");
        }
    }
    #endregion

        #region gridIntegrante_RowDeleting(object sender, GridViewDeleteEventArgs e)
    protected void gridIntegrante_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridIntegrante.PageSize * gridIntegrante.PageIndex) + e.RowIndex].RowState != DataRowState.Added)
        {
            // Rotina que apaga o registro do dataset e adiciona no final com o RowState "Deleted"
            DataRow row = ((DataSet)ViewState["integrante"]).Tables[0].NewRow();
            row[EventoDIC.COL_COD_EVENTO] = ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridIntegrante.PageSize * gridIntegrante.PageIndex) + e.RowIndex][EventoDIC.COL_COD_EVENTO];
            row[ProfessorDIC.COL_COD_PROFESSOR] = ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridIntegrante.PageSize * gridIntegrante.PageIndex) + e.RowIndex][ProfessorDIC.COL_COD_PROFESSOR];
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridIntegrante.PageSize * gridIntegrante.PageIndex) + e.RowIndex].Delete();
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridIntegrante.PageSize * gridIntegrante.PageIndex) + e.RowIndex].AcceptChanges();

            ((DataSet)ViewState["integrante"]).Tables[0].Rows.Add(row);
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[((DataSet)ViewState["integrante"]).Tables[0].Rows.Count - 1].AcceptChanges();
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[((DataSet)ViewState["integrante"]).Tables[0].Rows.Count - 1].Delete();
        }
        else
            ((DataSet)ViewState["integrante"]).Tables[0].Rows[(gridIntegrante.PageSize * gridIntegrante.PageIndex) + e.RowIndex].Delete();

        if (e.RowIndex == 0 && gridIntegrante.PageIndex > 0)
            gridIntegrante.PageIndex -= 1;
        gridIntegrante.DataSource = ViewState["integrante"];
        gridIntegrante.DataBind();
    }

    #endregion

    #endregion

    #region Calendário do Evento

        #region ObterDadosCalendario()
    public DataSet ObterDadosCalendario()
    {
         ViewState["calendario"] = EventoDAO.CalendarioDoEvento(Conversor.ConverterParaInteiro(ViewState["cod_evento"])).InternalDataSet;
        return (DataSet)ViewState["calendario"];
    }
    #endregion

        #region Botoescalendario(bool status)
    protected void BotoesCalendario(bool status)
    {
        txtData.Enabled = status;
        dropSala.Enabled = status;
        dropHorario.Enabled = status;
        btnConfirmarCalendario.Enabled = status;
        btnCancelarCalendario.Enabled = status;
        btnNovoCalendario.Enabled = !status;
        pnlCalendario.Visible = status;

     }
    #endregion

        #region btnNovoCalendario_Click(object sender, ImageClickEventArgs e)
    protected void btnNovoCalendario_Click(object sender, ImageClickEventArgs e)
    {
        BotoesCalendario(true);

        txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
        dropSala.DataSource = SalaDAO.DropDownSala().InternalDataSet;
        dropSala.DataTextField = SalaDIC.COL_DESC_CONSULTA;
        dropSala.DataValueField = SalaDIC.COL_COD_SALA;
        dropSala.DataBind();

        dropHorario.DataSource = HorarioDAO.DropDownHorario().InternalDataSet;
        dropHorario.DataTextField = HorarioDIC.COL_DESC_CONSULTA;
        dropHorario.DataValueField = HorarioDIC.COL_COD_HORARIO;
        dropHorario.DataBind();

        txtData.Focus();
    }
    #endregion

        #region IncluirDadosCalendario(string codSala, string codHorario, string data)
    private bool IncluirDadosCalendario(string codSala, string codHorario, string data)
    {
        StringBuilder consulta = new StringBuilder();
        consulta.Append(EventoDIC.COL_COD_EVENTO+" = " + ViewState["cod_evento"].ToString() + " and ");
        //consulta.Append(CalendarioDIC.COL_COD_SALA + " = " + codSala + " and ");
        consulta.Append(CalendarioDIC.COL_COD_HORARIO+" = " + codHorario +" and ");
        consulta.Append((CalendarioDIC.COL_DATA+" = '"+ data+" 00:00:00"+"'" ));

        //consulta.Append((CalendarioDIC.COL_DATA + " = '" + Conversor.ConverterParaDateTime(data).ToString("MM/dd/yyyy hh:mm:ss") + "'"));
        DataRow[] dr = ((DataSet)ViewState["calendario"]).Tables[0].Select(consulta.ToString());

        if (dr.Length == 0)
        {
            DataRow rowCalendario = ((DataSet)ViewState["calendario"]).Tables[0].NewRow();
            rowCalendario[CalendarioDIC.COL_COD_SALA] = Conversor.ConverterParaInteiro(codSala);
            rowCalendario[CalendarioDIC.COL_COD_HORARIO] = Conversor.ConverterParaInteiro(codHorario);
            rowCalendario[EventoDIC.COL_COD_EVENTO] = Conversor.ConverterParaInteiro(ViewState["cod_evento"].ToString());
            rowCalendario[CalendarioDIC.COL_DATA] = Conversor.ConverterParaDateTime(data);
            rowCalendario[SalaDIC.COL_DESC_CONSULTA] = dropSala.SelectedItem.Text;
            rowCalendario[HorarioDIC.COL_DESC_CONSULTA] = dropHorario.SelectedItem.Text;

            ((DataSet)ViewState["calendario"]).Tables[0].Rows.Add(rowCalendario);

            gridCalendario.DataSource = ViewState["calendario"];
            gridCalendario.DataBind();
            return true;
        }
        else
        {
            ExibirMensagemErro.Exibir("Este calendário já está adicionado a este evento.", this.Page);
            return false;
        }
    }
    #endregion

        #region btnConfirmarCalendario_Click(object sender, ImageClickEventArgs e)
    protected void btnConfirmarCalendario_Click(object sender, ImageClickEventArgs e)
    {
       bool v = true;
       bool dataOk = false;
       if (v)
           v = Validacao.ValidaData(this.Page, txtData, ref dataOk);
       
       if (v)
       {
           if (IncluirDadosCalendario(dropSala.SelectedValue, dropHorario.SelectedValue, txtData.Text))
               BotoesCalendario(false);
           else
               txtData.Focus();
       }
       else
       {
           ExibirMensagemErro.Exibir("Favor digitar uma data válida.", this.Page);
           txtData.Focus();
       }
    }
    #endregion

        #region btnCancelarCalendario_Click(object sender, ImageClickEventArgs e)
    protected void btnCancelarCalendario_Click(object sender, ImageClickEventArgs e)
    {
        BotoesCalendario(false);
    }
    #endregion

        #region gridCalendario_RowDataBound(object sender, GridViewRowEventArgs e)
    protected void gridCalendario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton MyButton = (ImageButton)e.Row.FindControl("imgbtnExcluir");

            MyButton.Attributes.Add("onclick", "javascript:return " + "confirm('Confirma a exclusão deste Calendário ?')");
        }
    }
    #endregion

        #region gridCalendario_RowDeleting(object sender, GridViewDeleteEventArgs e)
    protected void gridCalendario_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (((DataSet)ViewState["calendario"]).Tables[0].Rows[(gridCalendario.PageSize * gridCalendario.PageIndex) + e.RowIndex].RowState != DataRowState.Added)
        {
            // Rotina que apaga o registro do dataset e adiciona no final com o RowState "Deleted"
            DataRow row = ((DataSet)ViewState["calendario"]).Tables[0].NewRow();

            row[CalendarioDIC.COL_COD_SALA] = ((DataSet)ViewState["calendario"]).Tables[0].Rows[(gridCalendario.PageSize * gridIntegrante.PageIndex) + e.RowIndex][CalendarioDIC.COL_COD_SALA];
            row[CalendarioDIC.COL_COD_HORARIO] = ((DataSet)ViewState["calendario"]).Tables[0].Rows[(gridCalendario.PageSize * gridIntegrante.PageIndex) + e.RowIndex][CalendarioDIC.COL_COD_HORARIO];
            row[EventoDIC.COL_COD_EVENTO] = ((DataSet)ViewState["calendario"]).Tables[0].Rows[(gridCalendario.PageSize * gridIntegrante.PageIndex) + e.RowIndex][EventoDIC.COL_COD_EVENTO];
            row[CalendarioDIC.COL_DATA] = ((DataSet)ViewState["calendario"]).Tables[0].Rows[(gridCalendario.PageSize * gridIntegrante.PageIndex) + e.RowIndex][CalendarioDIC.COL_DATA];

            ((DataSet)ViewState["calendario"]).Tables[0].Rows[(gridCalendario.PageSize * gridCalendario.PageIndex) + e.RowIndex].Delete();
            ((DataSet)ViewState["calendario"]).Tables[0].Rows[(gridCalendario.PageSize * gridCalendario.PageIndex) + e.RowIndex].AcceptChanges();

            ((DataSet)ViewState["calendario"]).Tables[0].Rows.Add(row);
            ((DataSet)ViewState["calendario"]).Tables[0].Rows[((DataSet)ViewState["calendario"]).Tables[0].Rows.Count - 1].AcceptChanges();
            ((DataSet)ViewState["calendario"]).Tables[0].Rows[((DataSet)ViewState["calendario"]).Tables[0].Rows.Count - 1].Delete();
        }
        else
            ((DataSet)ViewState["calendario"]).Tables[0].Rows[(gridCalendario.PageSize * gridCalendario.PageIndex) + e.RowIndex].Delete();

        if (e.RowIndex == 0 && gridIntegrante.PageIndex > 0)
            gridCalendario.PageIndex -= 1;
        gridCalendario.DataSource = ViewState["calendario"];
        gridCalendario.DataBind();

    }
    #endregion

    #endregion
}
