<%@ Page Language="C#" MasterPageFile="Principal.master" AutoEventWireup="true" CodeFile="Voluntario.aspx.cs" Inherits="Voluntario" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="1" cellspacing="1" style="height: 10%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;" width="100%">
        <tr>
            <td align="right" style="width: 85px;">
                <strong><span style="color: #000099"> Ensino</span></strong></td>
            <td colspan="3">
                <asp:RadioButton ID="rbtGraduacao" runat="server" AutoPostBack="True" ForeColor="#000000"
                    GroupName="ensino" Text="Graduação" OnCheckedChanged="rbtGraduacao_CheckedChanged" Checked="True" />
                &nbsp; &nbsp;
                <asp:RadioButton ID="rbtTecnologo" runat="server" AutoPostBack="True" ForeColor="#000000"
                    GroupName="ensino" Text="Tecnólogo" OnCheckedChanged="rbtTecnologo_CheckedChanged" />&nbsp;</td>
            <td colspan="1">
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 85px; height: 24px">
                <span style="color: #000099"> Voluntário</span></td>
            <td colspan="3" style="height: 24px">
                <asp:DropDownList ID="dropVoluntario" runat="server" Width="451px" OnSelectedIndexChanged="dropVoluntario_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td align="center" colspan="1" style="height: 24px">
                <asp:ImageButton ID="btnNovo" runat="server" ImageUrl="~/imagens/btn_novo.gif" OnClick="btnNovo_Click" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 85px; height: 13px">
                <span style="color: #000099">Data</span></td>
            <td style="width: 40px; height: 13px">
                <asp:TextBox ID="txtData" runat="server" Width="73px"></asp:TextBox></td>
            <td align="right" style="width: 62px; height: 13px;">
                <span style="color: #000099">Horário</span></td>
            <td style="width: 49px; height: 13px">
                <asp:DropDownList ID="dropHorario" runat="server" Width="243px">
                </asp:DropDownList></td>
            <td style="width: 58px; height: 13px" align="left">
                <asp:ImageButton ID="btnConfirmar" runat="server" ImageUrl="~/imagens/btn_confirmar.gif" Visible="False" /></td>
        </tr>
        <tr>
            <td align="right" style="width: 85px; height: 4px">
                <span style="color: #000099">Sala</span></td>
            <td colspan="3" style="height: 4px">
                <asp:DropDownList ID="dropSala" runat="server" Width="451px">
                </asp:DropDownList>
            </td>
            <td colspan="1" style="height: 4px" align="left">
                <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/imagens/btn_cancelar.gif" Visible="False" OnClick="btnCancelar_Click" /></td>
        </tr>
        <tr>
            <td colspan="5" style="height: 4px; text-align: center; border-top: black 1px solid;">
                <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="Selecione o voluntário desejado e clique no botão novo para incluir a data, horário e sala para voluntariado."></asp:Label></td>
        </tr>
        <tr>
            <td colspan="5" style="border-top: black 1px solid; height: 16px">
                <asp:GridView ID="gridVoluntario" runat="server" AutoGenerateColumns="False" CellPadding="2"
                    EmptyDataText="Não existe nenhum voluntário cadastrado. Selecione um voluntario e clique no botão novo para acrescentar um horário."
                    ForeColor="#333333" Width="100%">
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <EmptyDataRowStyle BackColor="Transparent" BorderColor="Transparent" BorderWidth="0px" />
                    <Columns>
                        <asp:BoundField DataField="cod_sala" HeaderText="C&#243;digo da sala" Visible="False" />
                        <asp:BoundField DataField="cod_horario" HeaderText="Codigo do horario" Visible="False" />
                        <asp:BoundField DataField="ra" HeaderText="C&#243;digo do Evento" Visible="False" />
                        <asp:TemplateField HeaderText="Data">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("data") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("data", "{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="desc_sala" HeaderText="Sala" />
                        <asp:BoundField DataField="desc_horario" HeaderText="Hor&#225;rio" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnExcluir" runat="server" CausesValidation="false" CommandName="Delete"
                                    ImageUrl="~/imagens/excluir.gif" Text="Button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                &nbsp;</td>
        </tr>
    </table>    
</asp:Content>

