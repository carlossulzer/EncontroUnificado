<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="RelAlunosPorEventos.aspx.cs" Inherits="RelAlunosPorEventos" Title="Encontro Unificado" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CR:CrystalReportViewer ID="crvAlunosPorEvento" runat="server" AutoDataBind="true" DisplayGroupTree="False" GroupTreeStyle-BorderWidth="2px" GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" Height="50px" Width="350px" />
</asp:Content>

