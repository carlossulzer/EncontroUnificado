<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Horario.aspx.cs" Inherits="Horario" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table border="0" cellpadding="5" cellspacing="3" style="height: 10%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;" width="100%">
        <tr>
            <td align="right" style="width: 30%; height: 16px;">
                <strong><span style="color: #000099">Hora Inicial&nbsp;</span></strong></td>
            <td style="width: 70%; height: 16px;">
                <asp:TextBox ID="txtHoraInicial" runat="server" Width="62px" MaxLength="5"></asp:TextBox></td>
        </tr>
    <tr>
        <td align="right" style="width: 30%; height: 16px">
            <span style="color: #000099">Hora Final</span>&nbsp;</td>
        <td style="width: 70%; height: 16px">
            <asp:TextBox ID="txtHoraFinal" runat="server" MaxLength="60" Width="62px"></asp:TextBox></td>
    </tr>
    </table>
</asp:Content>

