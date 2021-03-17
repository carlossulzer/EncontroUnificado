using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for SetarFocus
/// </summary>
public class SetarFocus
{
	public SetarFocus()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void SetFocus(System.Web.UI.Page page, Control controle)
    {
        ScriptManager focus = ScriptManager.GetCurrent(page);
        focus.SetFocus(controle);
    }

}
