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
using Util;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session.Count > 0)
            {
                lblLogin.Visible = true;
                lblLogin.Text = "Usuário conectado : " + UsuarioCorrente.Login;
            }
            else
                lblLogin.Visible = false;

            txtLogin.Focus();
        }

    }
    protected void btnConectar_Click(object sender, EventArgs e)
    {
        VerificaAcesso acesso = new VerificaAcesso();


        bool _retorno = acesso.Verifica_Senha(txtLogin.Text, txtSenha.Text, txtSenhaNova.Text, txtSenhaConf.Text, txtSenhaNova.Visible);

        if (_retorno == false)
        {
            if (acesso.TrocaSenha == true)
            {
                if (!txtSenhaNova.Visible)
                {
                    txtSenhaNova.Visible = true;
                    txtSenhaConf.Visible = true;
                    lblSenhaAtu.Visible = true;
                    lblNovaSenha.Visible = true;
                    lblConfSenha.Visible = true;
                    txtSenha.Attributes.Add("value", txtSenha.Text);

                    txtSenhaNova.Focus();
                }
                else
                {
                    Util.ExibirMensagemErro.Exibir(acesso.MensagemdeRetorno, this.Page);
                    txtSenhaNova.Focus();
                }
            }
            else
            {
                Util.ExibirMensagemErro.Exibir(acesso.MensagemdeRetorno, this.Page);
                if (acesso.SetarFoco == 1)
                    txtLogin.Focus();
                else if (acesso.SetarFoco == 2)
                    txtSenha.Focus();
                else if (acesso.SetarFoco == 3)
                    txtSenhaNova.Focus();
            }
        }
        else
        {
            UsuarioCorrente.Login = acesso.UsuarioLogado;
            AdministradorLogado.LoginAdministrador = acesso.TipodeUsuario;

            if (Session.Count > 0)
            {
                FormsAuthentication.SignOut();
            }

            if (FormsAuthentication.GetRedirectUrl("ENCONTROUNIF", false) != "~/Default.aspx")
            {
                FormsAuthentication.RedirectFromLoginPage("ENCONTROUNIF", false);
            }
            else
            {
                Response.Redirect("~/Defaut.aspx");
            }
        }

    }
    protected void btnDesconectar_Click(object sender, EventArgs e)
    {
        AdministradorLogado.LoginAdministrador = "N";
        UsuarioCorrente.Login = string.Empty;
        lblLogin.Text = UsuarioCorrente.Login;
        FormsAuthentication.SignOut();
        lblLogin.Visible = false;
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
