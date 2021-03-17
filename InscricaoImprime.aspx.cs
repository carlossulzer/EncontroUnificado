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

public partial class InscricaoImprime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Menu menuPrincipal = (Menu)Master.FindControl("Menu1");
        menuPrincipal.Visible = false;
        Master.titulo = "Ficha de Inscrição";

        if (!Page.IsPostBack)
        {
            ViewState["matricula"] = Request["matricula"].ToString();
            ViewState["ensino"]    = Request["ensino"].ToString();
            ViewState["ctrl"]      = Request["ctrl"].ToString();

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

                        gridInscricao.DataSource = InscricaoDAO.ImprimirInscricao(ViewState["matricula"].ToString(), ViewState["ensino"].ToString() ).InternalDataSet;
                        gridInscricao.DataBind();

                    //}
                    //else
                    //    Response.Redirect("~/PaginaErro.aspx");
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "", @"<script>if(confirm(""Deseja imprimir a ficha de inscrição ?"")){window.opener = ''; window.print()}</script>");
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "", @"<script>window.opener = ''; window.close()</script>");
    }
}


