<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Professor.aspx.cs" Inherits="Professor" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="5" cellspacing="3" style="height: 10%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;" width="100%">
        <tr>
            <td align="right" style="width: 30%; height: 16px;">
                <strong><span style="color: #000099">Matrícula</span></strong></td>
            <td style="width: 70%; height: 16px;">
                <asp:TextBox ID="txtMatricula" runat="server" Width="104px" MaxLength="6" CssClass="TextBox"></asp:TextBox></td>
        </tr>
        <tr>        
            <td align="right" style="width: 30%; height: 16px;">
                <strong><span style="color: #000099">Nome</span></strong></td>
            <td style="width: 70%; height: 16px;">
                <asp:TextBox ID="txtNome" runat="server" Width="428px" MaxLength="80" CssClass="TextBox"></asp:TextBox></td>
        </tr>
        <tr>        
            <td align="right" style="width: 30%; height: 16px;">
                <strong><span style="color: #000099">Telefone (1)</span></strong></td>
            <td style="width: 70%; height: 16px;">
                <asp:TextBox ID="txtTelefone1" runat="server" Width="196px" MaxLength="15" CssClass="TextBox"></asp:TextBox></td>
         </tr>
         <tr>        
            <td align="right" style="width: 30%; height: 16px;">
                <strong><span style="color: #000099">Telefone (2)</span></strong></td>
            <td style="width: 70%; height: 16px;">
                <asp:TextBox ID="txtTelefone2" runat="server" Width="196px" MaxLength="15" CssClass="TextBox"></asp:TextBox></td>
          </tr>
          <tr>       
            <td align="right" style="width: 30%; height: 16px;">
                <strong><span style="color: #000099">E-mail</span></strong></td>
            <td style="width: 70%; height: 16px;">
                <asp:TextBox ID="txtEmail" runat="server" Width="428px" MaxLength="40" CssClass="TextBoxLower"></asp:TextBox></td>
          </tr>
    </table>
</asp:Content>

