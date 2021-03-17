<%@ Page Language="C#" MasterPageFile="Principal.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="Principal.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<asp:HyperLink ID="lnkInscricao" runat="server" Font-Bold="True" Font-Size="14pt"
        ForeColor="#FF3300" NavigateUrl="Inscricao.aspx?matricula=4609&ensino=2&ctrl=8dc5983b8c4ef1d8fcd5f325f9a65511" Visible="False">Inscrição - 01</asp:HyperLink>
    <br />
    <br />
    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Size="14pt"
        ForeColor="#FF3300" NavigateUrl="Inscricao.aspx?matricula=4635&ensino=2&ctrl=9b7de329e29300a138ed67f0916e3c74" Visible="False">Inscrição - 02</asp:HyperLink>        
</asp:Content>

<%--
"Inscricao.aspx?matricula=4609&ensino=2&ctrl=8dc5983b8c4ef1d8fcd5f325f9a65511"
"Inscricao.aspx?matricula=4635&ensino=2&ctrl=9b7de329e29300a138ed67f0916e3c74"
--%>