<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="PaginaErro.aspx.cs" Inherits="PaginaErro" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 100%">
        <tr>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center">
                <span style="font-size: 11pt; color: red; width: 100%;">
                    <asp:Label ID="Label1" runat="server" Font-Size="16pt" Text="UM ERRO INESPERADO ACONTECEU"></asp:Label><br />
                    <br />
                    <br />
                    </span></td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
        </tr>
    </table>
</asp:Content>

