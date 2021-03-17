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

public partial class Horario : System.Web.UI.Page
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
            //mascar para Hora Inicial
            txtHoraInicial.Attributes.Add("MaxLength", "5");
            txtHoraInicial.Attributes.Add("mask", "__:__");
            txtHoraInicial.Attributes.Add("onkeydown", "EE_KeyDown(this)");
            txtHoraInicial.Attributes.Add("onkeypress", "EE_KeyPress(this)");
            txtHoraInicial.Attributes.Add("onclick", "EE_OnClick(this)");
            txtHoraInicial.Attributes.Add("onfocus", "EE_GotFocus(this)");
            txtHoraInicial.Attributes.Add("onblur", "EE_LostFocus(this)");

            //mascar para Hora Final
            txtHoraFinal.Attributes.Add("MaxLength", "5");
            txtHoraFinal.Attributes.Add("mask", "__:__");
            txtHoraFinal.Attributes.Add("onkeydown", "EE_KeyDown(this)");
            txtHoraFinal.Attributes.Add("onkeypress", "EE_KeyPress(this)");
            txtHoraFinal.Attributes.Add("onclick", "EE_OnClick(this)");
            txtHoraFinal.Attributes.Add("onfocus", "EE_GotFocus(this)");
            txtHoraFinal.Attributes.Add("onblur", "EE_LostFocus(this)"); 


            ViewState["codigo"]   = Request["codigo"].Trim();
            ViewState["operacao"] = Request["operacao"].Trim();

            if (ViewState["operacao"].ToString() == "E") // excluir
            {
                salvar.Attributes.Add("onclick", "return confirm('Deseja excluir este horário ?')");
                DesabilitarCampos();
            }    
            MostraDados(ViewState["codigo"].ToString(), ViewState["operacao"].ToString());

         }

         if (ViewState["operacao"].ToString() == "A") // alterar
             Master.titulo = "Alteração de Horário";
         else if (ViewState["operacao"].ToString() == "I") // inclusao
             Master.titulo = "Inclusão de Horário";
         else if (ViewState["operacao"].ToString() == "E") // excluir
             Master.titulo = "Exclusão de Horário";
     }

    private void ConfirmarClick(object sender, EventArgs e)
    {
        bool tudoOk = true;

        HorarioDOM horario  = new HorarioDOM();

        horario.codHorario  = Conversor.ConverterParaInteiro(ViewState["codigo"].ToString());
        horario.horaInicial = txtHoraInicial.Text;
        horario.horaFinal = txtHoraFinal.Text;

        if (ViewState["operacao"].ToString() == "I")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
                HorarioDAO.IncluirHorario(horario, true);
        }
        else if (ViewState["operacao"].ToString() == "A")
        {
            tudoOk = ValidaForm();
            if (tudoOk)
               HorarioDAO.AlterarHorario(horario);
        }
        else if (ViewState["operacao"].ToString() == "E")
        {
            string mens = HorarioDAO.ExcluirHorario(horario.codHorario);
            if (!mens.Equals(string.Empty))
            {
                tudoOk = false;
                ExibirMensagemErro.Exibir(mens, this.Page);
            }
        }

        if (tudoOk)
            Response.Redirect("~/HorarioLista.aspx");    
    }

    private void CancelarClick(object sender, EventArgs e)
    {
        Response.Redirect("~/HorarioLista.aspx"); 
    }

    public void MostraDados(string codigo, string acao)
    {
        HorarioDOM horarioDados = new HorarioDOM();
        if (acao == "A" || acao == "E")
        {
            horarioDados =  new HorarioDAO().ObterHorarioPeloId(Convert.ToInt32(codigo)); 
        }
        ViewState["codigo"] = horarioDados.codHorario;
        txtHoraInicial.Text = horarioDados.horaInicial;
        txtHoraFinal.Text   = horarioDados.horaFinal;

        if (acao == "A" || acao == "I") 
            txtHoraInicial.Focus();
   
    }

    public void DesabilitarCampos()
    {
        txtHoraInicial.Enabled = false;
        txtHoraFinal.Enabled = false;
    }

    public bool ValidaForm()
    {
        bool v = true;
        bool horaErrada = false;

        v = Validacao.ValidaHorario(this.Page, txtHoraInicial, ref horaErrada);
        if (!v)
        {
            ExibirMensagemErro.Exibir("Hora inicial inválida.", this.Page);
            txtHoraInicial.Focus();
            return false;
        }

        v = Validacao.ValidaHorario(this.Page, txtHoraFinal, ref horaErrada);
        if (!v)
        {
            ExibirMensagemErro.Exibir("Hora final inválida.", this.Page);
            txtHoraFinal.Focus();
            return false;
        }


        if (Conversor.ConverterParaDateTime(txtHoraFinal.Text) <= Conversor.ConverterParaDateTime(txtHoraInicial.Text))
        {
            ExibirMensagemErro.Exibir("Hora final inválida.", this.Page);
            txtHoraFinal.Focus();
            return false;
        }

        if (HorarioDAO.RegistroExiste(txtHoraInicial.Text, txtHoraFinal.Text, ViewState["operacao"].ToString(), ViewState["codigo"].ToString()))
        {
            ExibirMensagemErro.Exibir("Horário já cadastrado.", this.Page);
            txtHoraInicial.Focus();
            return false;
        }


        v = Validacao.ValidaTextBox(this.Page, txtHoraInicial);
        if (! v)
        {
            ExibirMensagemErro.Exibir("Favor informar a hora inicial.", this.Page);
            txtHoraInicial.Focus();
            return true;
        }
            
        v = Validacao.ValidaTextBox(this.Page, txtHoraFinal);
        if (!v)
        {
            ExibirMensagemErro.Exibir("Favor informar a hora final.", this.Page);
            txtHoraFinal.Focus();
            return false;
        }
   
        return v;
    }


}
