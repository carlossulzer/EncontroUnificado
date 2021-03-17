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
using DIC;
using DAO;
using Util;

public partial class Inscricao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Page.MaintainScrollPositionOnPostBack = true;


        Menu menuPrincipal = (Menu)Master.FindControl("Menu1");
        menuPrincipal.Visible = false;
        Master.titulo = "Ficha de Inscrição";

        MasterPage_Principal pagina = (MasterPage_Principal)this.Master;
        pagina.ConfirmarEvento += new EventoBotoes(ConfirmarClick);
        pagina.CancelarEvento += new EventoBotoes(CancelarClick);

        ImageButton salvar   = (ImageButton)Master.FindControl("btnConfirmar");

        ImageButton cancelar = (ImageButton)Master.FindControl("btnCancelar");

        if (!Page.IsPostBack)
        {
            ViewState["matricula"] = Request["matricula"].ToString();
            ViewState["ensino"]    = Request["ensino"].ToString();
            ViewState["ctrl"]      = Request["ctrl"].ToString();

            string mens = "matricula: " + ViewState["matricula"].ToString();
            mens += "ensino = " + ViewState["ensino"].ToString();
            mens += "senha = " + ViewState["ctrl"].ToString();

            if (!ViewState["matricula"].ToString().Equals(string.Empty) && !ViewState["ensino"].ToString().Equals(string.Empty))
            {
                DataSet aluno = AlunosDAO.ObterDadosAluno(ViewState["matricula"].ToString(), ViewState["ensino"].ToString()).InternalDataSet;
                if (aluno.Tables[0].Rows.Count > 0)
                {
                    //if (AlunosDAO.VerificaSenha(ViewState["matricula"].ToString(), ViewState["ctrl"].ToString(), ViewState["ensino"].ToString()))
                    //{
                        lblAluno.Text = ViewState["matricula"].ToString() + " - " + aluno.Tables[0].Rows[0][AlunoDIC.COL_NOME].ToString();

                        string _curso = aluno.Tables[0].Rows[0][CursoDIC.COL_CURSO].ToString();
                        string _serie = aluno.Tables[0].Rows[0][AlunoDIC.COL_SERIE].ToString();
                        string _turma = aluno.Tables[0].Rows[0][AlunoDIC.COL_LETRA].ToString();

                        lblCurso.Text = _curso + " - " + _serie + " º Período - Turma \"" + _turma+"\"";

                        if (!InscricaoDAO.InscricaoExiste(ViewState["matricula"].ToString()))
                        {
                            salvar.Visible = true;
                            cancelar.Visible = true;

                            pnlNaoInscrito.Visible = true;
                            pnlInscrito.Visible = false;
                            ObterDadosInscricao();
                            gridInscricao.DataSource = InscricaoDAO.ListarInscricao().InternalDataSet;
                            gridInscricao.DataBind();
                        }
                        else
                        {
                            pnlNaoInscrito.Visible = false;
                            pnlInscrito.Visible = true;
                            gridInscricao.Visible = false;
                            pnlInscricao.Visible = true;
                            btnFechar.Visible = true;
                        }

                   // }
                   // else
                   //     Response.Redirect("~/PaginaErro.aspx");
                }
                else
                    Response.Redirect("~/PaginaErro.aspx");

            }
            else
            {
                Response.Redirect("~/PaginaErro.aspx");
            }
        }
    }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        string msnErro = InscricaoDAO.SalvarInscricao(ViewState["matricula"].ToString(), ViewState["ensino"].ToString(), ((DataSet)ViewState["inscricao"])); //, ref codEvento, ref data, ref codHorario);
        if (!msnErro.Equals(string.Empty))
        {
            ExibirMensagemErro.Exibir(msnErro, this.Page);
        }
        else
        {
            Response.Redirect(string.Format("~/InscricaoImprime.aspx?matricula={0}&ensino={1}&ctrl={2}", ViewState["matricula"].ToString(), ViewState["ensino"].ToString(), ViewState["ctrl"].ToString()));
        }
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }


    protected void ckbxParticipar_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;
        string codEvento =  gridInscricao.DataKeys[row.RowIndex]["cod_evento"].ToString();
        string data = gridInscricao.DataKeys[row.RowIndex]["data"].ToString();
        string codHorario = gridInscricao.DataKeys[row.RowIndex]["cod_horario"].ToString();

        if (checkbox.Checked)
        {
            if (!IncluirInscricao(codEvento, data, codHorario))
                checkbox.Checked = false;
        }
        else
        {
            ExcluirInscricao(codEvento, data, codHorario);
        }
    }

    private bool IncluirInscricao(string codEvento, string data, string codHorario)
    {
        StringBuilder consulta = new StringBuilder();
        consulta.Append((CalendarioDIC.COL_DATA + " = '" + data + "' and "));
        consulta.Append(CalendarioDIC.COL_COD_HORARIO + " = " + codHorario);
        DataRow[] dr = ((DataSet)ViewState["inscricao"]).Tables[0].Select(consulta.ToString());

        if (dr.Length == 0)
        {
            DataRow rowInscricao = ((DataSet)ViewState["inscricao"]).Tables[0].NewRow();
            rowInscricao[EventoDIC.COL_COD_EVENTO] = codEvento;
            rowInscricao[CalendarioDIC.COL_DATA] = Conversor.ConverterParaDateTime(data);
            rowInscricao[CalendarioDIC.COL_COD_HORARIO] = Conversor.ConverterParaInteiro(codHorario);
            ((DataSet)ViewState["inscricao"]).Tables[0].Rows.Add(rowInscricao);
            return true;
        }
        else
        {
            ExibirMensagemErro.Exibir("Já existe um evento selecionado com mesmo dia e horário. Favor Verificar.", this.Page);
            return false;
        }
    }


    private void ExcluirInscricao(string codEvento, string data, string codHorario)
    {
        StringBuilder consulta = new StringBuilder();

        consulta.Append(EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString() + " and ");
        consulta.Append((CalendarioDIC.COL_DATA + " = '" + data + "' and "));
        consulta.Append(CalendarioDIC.COL_COD_HORARIO + " = " + codHorario);

        ViewState["inscricao"] = ((DataSet)ViewState["inscricao"]).GetChanges(DataRowState.Added);

        DataRow[] dr = ((DataSet)ViewState["inscricao"]).Tables[0].Select(consulta.ToString()); //, "",  DataViewRowState.Added);

        if (dr.Length == 1)
        {
            ((DataSet)ViewState["inscricao"]).Tables[0].Rows[0].AcceptChanges();
            ((DataSet)ViewState["inscricao"]).Tables[0].Rows[0].Delete();
        }

    }


    public void ObterDadosInscricao()
    {
        ViewState["inscricao"] = InscricaoDAO.InscricaoViewState().InternalDataSet;
    }


    protected void btnFechar_Click(object sender, EventArgs e)
    {
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "", @"<script>window.opener = ''; window.close()</script>");
    }
    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(string.Format("~/InscricaoImprime.aspx?matricula={0}&ensino={1}&ctrl={2}",ViewState["matricula"].ToString() , ViewState["ensino"].ToString(), ViewState["ctrl"].ToString() ));
    }
}
