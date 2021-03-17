using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Util;

/// <summary>
/// Summary description for clMensagem
/// </summary>
namespace Util
{
    public class ExibirMensagemErro
    {
        public ExibirMensagemErro()
        {
            // TODO: Add constructor logic here
        }
        public static void Exibir(string mensagem, System.Web.UI.Page objPagina)
        {
            ScriptManager.RegisterClientScriptBlock(objPagina, objPagina.GetType(), "@MSG", "<script>alert('" + mensagem + "');</script>", false);   
        }
    }
}

