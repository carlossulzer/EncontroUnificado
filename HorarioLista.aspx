<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="HorarioLista.aspx.cs" Inherits="HorarioLista" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridHorario" runat="server" AutoGenerateColumns="False" BorderStyle="None" CellPadding="4"
        Height="78px" Width="100%" EmptyDataText="Não existe nenhum Horário cadastrado. Clique em incluir." ForeColor="#333333" AllowPaging="True" OnPageIndexChanging="GridHorario_PageIndexChanging" PageSize="7">
        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
        <Columns>
            <asp:BoundField DataField="cod_horario" HeaderText="C&#243;digo" Visible="False" >
                <ItemStyle Width="20px" />
            </asp:BoundField>
            
            <asp:BoundField DataField="hora_inicial" HeaderText="Hora Inicial" />
            <asp:BoundField DataField="hora_final" HeaderText="Hora Final" />
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/imagens/alterar.gif" NavigateUrl='<%# Eval("cod_horario", "Horario.aspx?codigo={0}&operacao=A") %>'
                        Text='<%# Eval("cod_horario", "Alterar") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/imagens/excluir.gif" NavigateUrl='<%# Eval("cod_horario", "Horario.aspx?codigo={0}&operacao=E") %>'
                        Text='<%# Eval("cod_horario", "Excluir") %>'></asp:HyperLink>
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

