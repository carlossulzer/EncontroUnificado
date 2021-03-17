<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="SalaLista.aspx.cs" Inherits="SalaLista" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridSala" runat="server" AutoGenerateColumns="False" BorderStyle="None" CellPadding="4"
        Height="78px" Width="100%" EmptyDataText="N�o existe nenhuma Sala cadastrada. Clique em incluir." ForeColor="#333333" AllowPaging="True" OnPageIndexChanging="GridSala_PageIndexChanging" PageSize="7">
        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
        <Columns>
            <asp:BoundField DataField="cod_sala" HeaderText="C&#243;digo" Visible="False" >
                <ItemStyle Width="20px" />
            </asp:BoundField>
            <asp:BoundField DataField="descricao" HeaderText="Nome da Sala" />
            <asp:BoundField DataField="andar" HeaderText="Andar" />
            <asp:BoundField DataField="bloco" HeaderText="Bloco" />
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagens/alterar.gif" NavigateUrl='<%# Eval("cod_sala", "Sala.aspx?codigo={0}&operacao=A") %>'
                        Text='<%# Eval("cod_sala", "Alterar") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/imagens/excluir.gif" NavigateUrl='<%# Eval("cod_sala", "Sala.aspx?codigo={0}&operacao=E") %>'
                        Text='<%# Eval("cod_sala", "Excluir") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <EmptyDataRowStyle BackColor="White" BorderColor="White" Font-Bold="True" ForeColor="Maroon"
            HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:GridView>
</asp:Content>

