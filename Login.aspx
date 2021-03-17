<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Encontro Unificado</title>
</head>
<body style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; width: 100%; padding-top: 0px; height: 100%">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="width: 20%; height: 20%" valign="bottom">
                </td>
                <td align="center" style="width: 20%; height: 20%" valign="bottom">
                </td>
                <td align="center" style="width: 20%; height: 20%" valign="bottom">
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 20%; height: 60%;" valign="middle">
                </td>
                <td align="center" style="width: 60%; height: 60%;" valign="middle">
                    <table id="TableForm"  border="0" cellpadding="0"
                        cellspacing="0" style="width: 330px; height: 148px">
                        <tr>
                            <td align="center" class="Normal" style="height: 51px; background-color: #cc6600; width:2px" colspan="5" valign="middle">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black" Width="400px">Entre com seu login e senha para ter acesso ao sistema</asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" class="Normal" style="width: 43px; height: 30px; background-color: #cc9900">
                            </td>
                            <td align="right" class="Normal" style="width: 65px; height: 30px; background-color: #cc9900">
                            </td>
                            <td class="Normal" style="width: 2px; height: 30px; background-color: #cc9900">
                            </td>
                            <td class="Normal" style="width: 60px; height: 30px; background-color: #cc9900">
                            </td>
                            <td class="Normal" style="width: 29px; height: 30px; background-color: #cc9900">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="Normal" style="width: 43px; height: 30px; background-color: #cc9900">
                            </td>
                            <td align="right" class="Normal" style="width: 65px; height: 30px; background-color: #cc9900">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Black">Usuário</asp:Label>&nbsp;</td>
                            <td class="Normal" style="width: 2px; height: 30px; background-color: #cc9900">
                                <asp:TextBox ID="txtLogin" runat="server" CssClass="TextBox" MaxLength="100" TabIndex="1"
                                    Width="152px"></asp:TextBox></td>
                            <td class="Normal" style="width:60px; height: 30px; background-color: #cc9900">
                                <asp:Button ID="btnConectar" runat="server" CssClass="Button" OnClick="btnConectar_Click"
                                    TabIndex="5" Text="Conectar" Width="80px" /></td>
                            <td class="Normal" style="width: 29px; height: 30px; background-color: #cc9900" >
                                &nbsp; &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="Normal" style="width: 43px; height: 30px; background-color: #cc9900">
                            </td>
                            <td align="right" class="Normal" style="width: 65px; height: 30px; background-color: #cc9900">
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Black" Width="53px">Senha</asp:Label>&nbsp;</td>
                            <td class="Normal" style="width: 2px; height: 30px; background-color: #cc9900">
                                <asp:TextBox ID="txtSenha" runat="server" CssClass="TextBox" MaxLength="25" TabIndex="2"
                                    TextMode="Password" Width="110px"></asp:TextBox>&nbsp;
                                <asp:Label ID="lblSenhaAtu" runat="server" ForeColor="Red" Visible="False" Width="121px">Senha atual</asp:Label></td>
                            <td class="Normal" style="width: 60px; height: 30px; background-color: #cc9900">
                                <asp:Button ID="btnDesconectar" runat="server" OnClick="btnDesconectar_Click" TabIndex="6"
                                    Text="Desconectar" Width="80px" /></td>
                            <td class="Normal" style="width: 29px; height: 30px; background-color: #cc9900" >
                            </td>
                        </tr>
                        <tr>
                            <td class="Normal" style="width: 43px; height: 30px; background-color: #cc9900" >
                            </td>
                            <td class="Normal" style="width: 65px; height: 30px; background-color: #cc9900">
                            </td>
                            <td class="Normal" style="width: 2px; height: 30px; background-color: #cc9900">
                                <asp:TextBox ID="txtSenhaNova" runat="server" CssClass="TextBox" MaxLength="25" TabIndex="3"
                                    TextMode="Password" Visible="False" Width="110px"></asp:TextBox>&nbsp;
                                <asp:Label ID="lblNovaSenha" runat="server" ForeColor="Red" Visible="False" Width="75px">Nova senha</asp:Label></td>
                            <td class="Normal" style="width: 60px; height: 30px; background-color: #cc9900" >
                            </td>
                            <td class="Normal" style="width: 29px; height: 30px; background-color: #cc9900">
                            </td>
                        </tr>
                        <tr>
                            <td class="Normal" style="width: 43px; height: 30px; background-color: #cc9900">
                            </td>
                            <td class="Normal" style="width: 65px; height: 30px; background-color: #cc9900" >
                            </td>
                            <td class="Normal" style="width: 2px; height: 30px; background-color: #cc9900">
                                <asp:TextBox ID="txtSenhaConf" runat="server" CssClass="TextBox" MaxLength="25" TabIndex="4"
                                    TextMode="Password" Visible="False" Width="110px"></asp:TextBox>&nbsp;
                                <asp:Label ID="lblConfSenha" runat="server" ForeColor="Red" Visible="False" Width="103px">Confirma senha</asp:Label></td>
                            <td class="Normal" style="width: 60px; height: 30px; background-color: #cc9900" >
                            </td>
                            <td class="Normal" style="width: 29px; height: 30px; background-color: #cc9900">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5" style="width: 271px; background-color: #cc6600; height:35px ">
                                &nbsp;
                                <asp:Label ID="lblLogin" runat="server" Font-Bold="True" ForeColor="Black" Width="384px"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td align="center" style="width: 60%; height: 60%;" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 20%; height: 20%" valign="middle">
                </td>
                <td align="center" style="width: 20%; height: 20%" valign="middle">
                </td>
                <td align="center" style="width: 20%; height: 20%" valign="middle">
                </td>
            </tr>
        </table>
 
 
    </form>
</body>
</html>
