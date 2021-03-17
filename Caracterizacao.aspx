<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Caracterizacao.aspx.cs" Inherits="Caracterizacao" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="5" cellspacing="3" style="height: 10%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;" width="100%">
        <tr>
            <td align="right" style="width: 30%; height: 16px;">
                <strong><span style="color: #000099">Caracterização</span></strong></td>
            <td style="width: 70%; height: 16px;">
                <asp:TextBox ID="txtDescricao" runat="server" Width="428px" MaxLength="60" CssClass="TextBox"></asp:TextBox></td>
        </tr>
    </table>
</asp:Content>

