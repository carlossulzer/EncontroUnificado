<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="LoginNovo.aspx.cs" Inherits="LoginNovo" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 100%">
        <tr>
            <td style="width: 20%; height: 20%;">
            </td>
            <td style="width: 60%; height: 20%;">
            </td>
            <td style="width: 20%; height: 20%;">
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 20%; height: 20%">
            </td>
            <td align="center" style="width: 60%; height: 20%">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 63%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;">
                        <tr>
                            <td rowspan="3" style="width: 30%; border-right: #000000 1px solid; border-top: #000000 1px solid; border-left: #000000 1px solid; border-bottom: #000000 1px solid;">
                                <asp:Image ID="Image1" runat="server" BorderColor="Transparent"
                                    ImageUrl="~/imagens/login.jpg" /></td>
                            <td style="width: 25%; height: 40px; border-top: #000000 1px solid; background-color: #cc6600;" align="right">
                                <asp:Label ID="Label1" runat="server" Text="Usuário" ForeColor="#000000"></asp:Label></td>
                            <td style="width: 45%;height: 40px; border-right: #000000 1px solid; border-top: #000000 1px solid; background-color: #cc6600;" align="left" valign="middle">
                                &nbsp;<asp:TextBox ID="txtLogin" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 25%; height: 39px; background-color: #cc6600;" align="right">
                                <asp:Label ID="Label2" runat="server" Text="Senha" ForeColor="#000000"></asp:Label></td>
                            <td style="width: 45%;border-right: #000000 1px solid; height: 39px; background-color: #cc6600;" align="left" valign="middle">
                                &nbsp;<asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Width="149px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="border-right: #000000 1px solid; border-bottom: #000000 1px solid; width: 70%; background-color: #cc6600; height: 37px;" valign="middle">
                                &nbsp;<asp:ImageButton ID="btnConfirmar" runat="server" ImageUrl="~/imagens/btn_conf.gif" OnClick="btnConfirmar_Click" />&nbsp;
                                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/imagens/btn_canc.gif" OnClick="btnCancelar_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
            </td>
            <td align="center" style="width: 20%; height: 20%">
            </td>
        </tr>
        <tr>
            <td style="width: 20%; height: 20%;">
            </td>
            <td style="width: 60%; height: 20%;">
            </td>
            <td style="width: 20%; height: 20%;">
            </td>
        </tr>
    </table>
</asp:Content>

