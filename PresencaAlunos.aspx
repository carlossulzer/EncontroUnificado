<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="PresencaAlunos.aspx.cs" Inherits="PresencaAlunos" Title="Untitled Page" %>
<%@ MasterType VirtualPath="Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 100%">
        <tr>
            <td style="width: 100%; height: 12px;" valign="middle">
                <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="Data"></asp:Label>
                &nbsp;<asp:DropDownList ID="dropDataEvento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropDataEvento_SelectedIndexChanged"
                    Width="97px">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" ForeColor="#000000" Text="Evento"></asp:Label>
                <asp:DropDownList ID="dropEventos" runat="server" Width="571px" OnSelectedIndexChanged="dropEventos_SelectedIndexChanged" AutoPostBack="True" Enabled="False">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 100%; height: 12px" valign="middle">
                <asp:Label ID="Label3" runat="server" ForeColor="#000000" Text="Horário"></asp:Label>
                <asp:DropDownList ID="dropHorario" runat="server" AutoPostBack="True" Enabled="False"
                    OnSelectedIndexChanged="dropHorario_SelectedIndexChanged" Width="269px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 100%; height: 100%" valign="top">
                <asp:GridView ID="gridAlunosEvento" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="matricula,cod_ensino,cod_evento,data,cod_horario" 
                    ForeColor="#333333" Width="100%" OnRowDataBound="gridAlunosEvento_RowDataBound" OnRowDeleting="gridAlunosEvento_RowDeleting" EmptyDataText="Selecione uma data, um evento, e o seu respectivo horário para registrar as presenças dos alunos" EnableTheming="True" >
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="matricula" HeaderText="Matr&#237;cula" />
                        <asp:BoundField DataField="nm_aluno" HeaderText="Nome do Aluno" />
                        <asp:TemplateField HeaderText="Presente">
                            <ItemStyle HorizontalAlign="Center" />
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("presente") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbx_presenca" runat="server" Checked='<%# Bind("presente") %>' AutoPostBack="True" OnCheckedChanged="ckbx_presenca_CheckedChanged" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
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
                    <EmptyDataRowStyle BackColor="Maroon" BorderColor="White" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <EditRowStyle BorderColor="Transparent" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <asp:ImageButton ID="btnNovoIntegrante" runat="server" ImageUrl="~/imagens/btn_novo.gif" OnClick="btnNovoIntegrante_Click" Visible="False" /></td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <asp:Panel ID="pnlIntegrante" runat="server" BackColor="#CC6600" Height="32px" Width="100%" Visible="False">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 32px">
                        <tr>
                            <td style="width: 100%">
                                <asp:RadioButton ID="rbtGraduacao" runat="server" AutoPostBack="True" ForeColor="#000000"
                                    GroupName="ensino" OnCheckedChanged="rbtGraduacao_CheckedChanged" Text="Graduação" />
                                <asp:RadioButton ID="rbtTecnologo" runat="server" AutoPostBack="True" ForeColor="#000000"
                                    GroupName="ensino" OnCheckedChanged="rbtTecnologo_CheckedChanged" Text="Tecnólogo" />
                                &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblAluno" runat="server" ForeColor="Black"
                                    Height="4px" Text="Aluno" Visible="False" Width="47px"></asp:Label><asp:DropDownList
                                        ID="dropIntegrante" runat="server" Visible="False" Width="273px">
                                    </asp:DropDownList>
                                <asp:ImageButton ID="btnConfirmarIntegrante" runat="server" Height="14px" ImageUrl="~/imagens/btn_confirmar.gif"
                                    OnClick="btnConfirmarIntegrante_Click" Visible="False" Width="82px" />
                                <asp:ImageButton ID="btnCancelarIntegrante" runat="server" Height="14px" ImageUrl="~/imagens/btn_cancelar.gif"
                                    OnClick="btnCancelarIntegrante_Click" Visible="False" Width="85px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

