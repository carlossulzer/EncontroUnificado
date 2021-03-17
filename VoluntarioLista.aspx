<%@ Page Language="C#" MasterPageFile="Principal.master" AutoEventWireup="true" CodeFile="VoluntarioLista.aspx.cs" Inherits="VoluntarioLista" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gridVoluntario" runat="server" AutoGenerateColumns="False" BorderStyle="None"  CellPadding="4" EmptyDataText="Não existe nenhum voluntário cadastrado. Clique no botão incluir para cadastrar um novo voluntário." Width="100%" AllowPaging="True" PageSize="4" OnPageIndexChanging="gridVoluntario_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="cod_evento" HeaderText="C&#243;digo do Evento" Visible="False" />
            <asp:BoundField DataField="cod_voluntario" HeaderText="C&#243;digo do Volunt&#225;rio"
                Visible="False" />
            <asp:BoundField DataField="cod_ensino" HeaderText="C&#243;digo do Ensino" Visible="False" />
            <asp:BoundField DataField="nm_aluno" HeaderText="Nome do Volunt&#225;rio" />
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/imagens/alterar.gif" NavigateUrl='<%# String.Format("Voluntario.aspx?matricula={0}&codEnsino={1}&codEvento={2}&operacao=A", Eval("matricula"), Eval("cod_ensino"),Eval("cod_evento")) %>' 
                    Text='<%# Eval("matricula", "Alterar") %>'>
                    </asp:HyperLink>            
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagens/excluir.gif" NavigateUrl='<%# String.Format("Voluntario.aspx?matricula={0}&codEnsino={1}&codEvento={2}&operacao=E", Eval("matricula"), Eval("cod_ensino"), Eval("cod_evento")) %>' 
                    Text='<%# Eval("matricula", "Excluir") %>'>
                    </asp:HyperLink>            
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

