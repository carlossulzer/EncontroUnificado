<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Sala.aspx.cs" Inherits="Sala" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="5" cellspacing="3" style="height: 10%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;" width="100%">
        <tr>
            <td align="right" style="width: 30%; height: 16px;">
                <strong><span style="color: #000099">Nome da Sala</span></strong></td>
            <td style="width: 70%; height: 16px;">
                <asp:TextBox ID="txtSala" runat="server" Width="428px" MaxLength="20" CssClass="TextBox"></asp:TextBox></td>
        </tr>
    <tr>
        <td align="right" style="width: 30%; height: 16px">
            <span style="color: #000099">Andar</span></td>
        <td style="width: 70%; height: 16px">
            <asp:TextBox ID="txtAndar" runat="server" MaxLength="20" Width="428px" CssClass="TextBox"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right" style="width: 30%; height: 16px">
            <span style="color: #000099">Bloco</span></td>
        <td style="width: 70%; height: 16px">
            <asp:TextBox ID="txtBloco" runat="server" MaxLength="20" Width="428px" CssClass="TextBox"></asp:TextBox></td>
    </tr>
    </table>
</asp:Content>

