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

public partial class LoginNovo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Menu menuPrincipal = (Menu)Master.FindControl("Menu1");
        menuPrincipal.Visible = false;
        Master.titulo = "Acesso ao Sistema";
        SetarFocus.SetFocus(this.Page, txtLogin);
    }

    protected void btnConfirmar_Click(object sender, ImageClickEventArgs e)
    {
        VerificaAcesso acesso = new VerificaAcesso();


        bool _retorno = acesso.Verifica_Senha(txtLogin.Text, txtSenha.Text, string.Empty, string.Empty, false);

        if (_retorno)
        {

            UsuarioCorrente.Login = acesso.UsuarioLogado;
            AdministradorLogado.LoginAdministrador = acesso.TipodeUsuario;

           
            if (Session.Count > 0)
            {
                FormsAuthentication.SignOut();
            }

            //if (FormsAuthentication.GetRedirectUrl("ENCONTROUNIF", false) != (HttpContext.Current.Request.ApplicationPath + "/Default.aspx"))
            if (FormsAuthentication.GetRedirectUrl("ENCONTROUNIF", false) != "Default.aspx")
            {
                FormsAuthentication.RedirectFromLoginPage("ENCONTROUNIF", false);
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "REDIRECT", "window.location.href='Default.aspx';", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(\"\"window.location='Default.aspx';\"\",3000);",true);

 

               Response.Redirect("Default.aspx", false);
            }
        }

    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "", @"<script>window.opener = ''; window.close()</script>");
    }
}
