using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Validacao
/// </summary>

namespace Util
{
    public class Validacao
    {
        public Validacao()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Validações

        #region ValidaTextBox
        public static bool ValidaTextBox(Page pg, TextBox txt)
        {
            txt.BackColor = Color.White;
            if (txt.Text == "")
            {
                txt.BackColor = Color.FromArgb(255, 255, 153);
                txt.ToolTip = "Campo obrigatório.";
                return false;
            }
            else
            {
                txt.BackColor = Color.White;
                txt.ToolTip = "";
                return true;
            }
        }
        #endregion

        #region ValidaListBox
        public static bool ValidaListBox(Page pg, ListBox lst)
        {
            lst.BackColor = Color.White;
            if (lst.SelectedValue == "-1")
            {
                lst.BackColor = Color.FromArgb(255, 255, 153);
                return false;
            }
            else
            {
                lst.BackColor = Color.White;
                return true;
            }
        }
        #endregion

        #region ValidaDropDown
        public static bool ValidaListBox(Page pg, DropDownList ddl)
        {
            ddl.BackColor = Color.White;
            if (ddl.SelectedValue == "-1")
            {
                ddl.BackColor = Color.FromArgb(255, 255, 153);
                return false;
            }
            else
            {
                ddl.BackColor = Color.White;
                return true;
            }
        }
        #endregion

        #region ValidaRadioButtonList
        public static bool ValidaRadioButtonList(Page pg, RadioButtonList rbllst)
        {
            bool v = false;

            rbllst.BackColor = Color.White;

            for (int i = 0; i < rbllst.Items.Count; i++)
                if (rbllst.Items[i].Selected)
                    v = true;

            if (!v)
            {
                rbllst.BackColor = Color.FromArgb(255, 255, 153);
                return false;
            }
            else
            {
                rbllst.BackColor = Color.FromArgb(241, 241, 241);
                return true;
            }
        }
        #endregion

        #region ValidaData
        public static bool ValidaData(Page pg, TextBox txt, ref bool v)
        {
            txt.BackColor = Color.White;
            if (txt.Text != "")
            {
                try
                {
                    txt.BackColor = Color.White;
                    Convert.ToDateTime(txt.Text);
                    return true;
                }
                catch
                {
                    txt.BackColor = Color.FromArgb(255, 255, 153);
                    return false;
                }
            }
            else
                return v;
        }
        #endregion

        #region ValidaDecimal
        public static bool ValidaDecimal(Page pg, TextBox txt, ref bool v)
        {
            txt.BackColor = Color.White;
            if (txt.Text != "")
            {
                try
                {
                    txt.BackColor = Color.White;
                    Convert.ToDecimal(txt.Text);
                    txt.ToolTip = "";
                    return true;
                }
                catch
                {
                    txt.BackColor = Color.FromArgb(255, 255, 153);
                    txt.ToolTip = "Valor inválido.";
                    return false;
                }
            }
            else
                return v;
        }
        #endregion

        #region ValidaHorario
        public static bool ValidaHorario(Page pg, TextBox txt, ref bool v)
        {
            txt.BackColor = Color.White;
            if (txt.Text != "")
            {
                Regex regExHor = new Regex("[0-1][0-9]:[0-5][0-9]|2[0-3]:[0-5][0-9]");
                if (regExHor.IsMatch(txt.Text))
                {
                    txt.BackColor = Color.White;
                    return true;
                }
                else
                {
                    txt.BackColor = Color.FromArgb(255, 255, 153);
                    return false;
                }
            }
            else
            {
                if (!v)
                {
                    txt.BackColor = Color.FromArgb(255, 255, 153);
                }
                return v;
            }
        }

        
        #endregion

        #endregion
    }
}