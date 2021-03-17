<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="ProfessorLista.aspx.cs" Inherits="ProfessorLista" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridProfessor" runat="server" AutoGenerateColumns="False" AllowPaging="True" BorderStyle="None" CellPadding="4"
        Height="100px" Width="100%" EmptyDataText="Não existe nenhum Professor cadastrado. Clique em incluir." ForeColor="#333333" PageSize="7" OnPageIndexChanging="GridProfessor_PageIndexChanging" >
        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
        
        <Columns>
            <asp:BoundField DataField="cod_professor" HeaderText="C&#243;digo" Visible="False" >
                <ItemStyle Width="20px" />
            </asp:BoundField>
            <asp:BoundField DataField="nome" HeaderText="Nome" />
            <asp:BoundField DataField="matricula" HeaderText="Matr&#237;cula" />
            <asp:BoundField DataField="email" HeaderText="E-mail" />
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagens/alterar.gif" NavigateUrl='<%# Eval("cod_professor", "Professor.aspx?codigo={0}&operacao=A") %>'
                        Text='<%# Eval("cod_professor", "Alterar") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/imagens/excluir.gif" NavigateUrl='<%# Eval("cod_professor", "Professor.aspx?codigo={0}&operacao=E") %>'
                        Text='<%# Eval("cod_Professor", "Excluir") %>'></asp:HyperLink>
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

