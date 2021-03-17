<%@ Page Language="C#" MasterPageFile="Principal.master" AutoEventWireup="true" CodeFile="NucleoLista.aspx.cs" Inherits="NucleoLista" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridNucleos" runat="server" AutoGenerateColumns="False" BorderStyle="None"
        CellPadding="4" EmptyDataText="Não existe nenhum Núcleo cadastrado. Clique em incluir."
        ForeColor="#333333" Height="78px" Width="100%" AllowPaging="True" OnPageIndexChanging="GridNucleos_PageIndexChanging" PageSize="7">
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <EmptyDataRowStyle BackColor="White" BorderColor="White" Font-Bold="True" ForeColor="Maroon"
            HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:BoundField DataField="cod_nucleo" HeaderText="C&#243;digo" Visible="False">
                <ItemStyle Width="20px" />
            </asp:BoundField>
            <asp:BoundField DataField="descricao" HeaderText="Nome do N&#250;cleo" />
            <asp:TemplateField>
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagens/alterar.gif" NavigateUrl='<%# Eval("cod_nucleo", "Nucleo.aspx?codigo={0}&operacao=A") %>'
                        Text='<%# Eval("cod_nucleo", "Alterar") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/imagens/excluir.gif" NavigateUrl='<%# Eval("cod_nucleo", "Nucleo.aspx?codigo={0}&operacao=E") %>'
                        Text='<%# Eval("cod_nucleo", "Excluir") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</asp:Content>

