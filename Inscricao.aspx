<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Inscricao.aspx.cs" Inherits="Inscricao" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="height: 20px; background-color: #ff9900;">
                &nbsp;<asp:Label ID="Label2" runat="server" ForeColor="#000000" Text="Aluno : "></asp:Label>
                <asp:Label ID="lblAluno" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="height: 20px; background-color: #ff9900">
                &nbsp;<asp:Label ID="Label5" runat="server" ForeColor="#000000" Text="Curso : "></asp:Label>
                <asp:Label ID="lblCurso" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="font-weight: bold; color: white; background-color: #ff0000;
                text-align: left" valign="top">
                    <asp:Panel ID="pnlNaoInscrito" runat="server" Width="100%">
                        <table border="1" cellpadding="2" cellspacing="2" style="width: 100%; height: 40px">
                            <tr>
                                <td style="width: 100%; height: 24px;">
                                    - Verifique os dados antes de confirmar
                    sua inscrição, pois a mesma não poderá ser alterada.</td>
                            </tr>
                            <tr>
                                <td style="width: 100%; height: 60px;">
                                    - Caso dois alunos escolham o mesmo evento e só haja uma vaga disponível, quem gravar
                                    primeiro irá ficar com a vaga, então, aparecerá uma mensagem informando a ocorrência
                                    do evento esgotado; desmarque o evento correspondente e
                    confirme novamente.</td>
                            </tr>
                            <tr>
                                <td style="width: 100%; height: 24px;">
                                    - Os eventos que já estiverem com suas vagas esgotadas, não mais estarão disponíveis.</td>
                            </tr>
                            <tr>
                                <td style="width: 100%; height: 42px;">
                                    - Após clicar no botão "Confirmar" no final desta página, será aberta uma tela para
                                    impressão da sua inscrição. Caso deseje imprimir futuramente, acesse o portal do
                                    aluno novamente.</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                     <asp:Panel ID="pnlInscrito" runat="server" Height="20px" Width="100%">
                        <table border="1" cellpadding="2" cellspacing="2" style="width: 100%; height: 20px">
                            <tr>
                                <td align="center" style="width: 100%; height: 20px">
                                    Sua inscrição foi efetivada e não poderá ser alterada. Clique no botão abaixo para
                                    imprimi-la.</td>
                            </tr>
                        </table>
                    </asp:Panel>               
                </td>
        </tr>
        <tr>
            <td style="height: 100%">
                <asp:GridView ID="gridInscricao" runat="server" AutoGenerateColumns="False" DataKeyNames="cod_evento, data, cod_horario"
                    BorderStyle="None" CellPadding="4" PageSize="4"  Width="100%" BackColor="White" BorderColor="#CC9966" BorderWidth="1px">
                    <Columns>
                        <asp:BoundField DataField="cod_evento" HeaderText="C&#243;digo do Evento" Visible="False" />
                        <asp:TemplateField HeaderText="Data" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("data") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("data", "{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cod_horario" HeaderText="C&#243;digo do Hor&#225;rio"
                            Visible="False" />
                        <asp:TemplateField HeaderText="Selecione os eventos aos quais deseje participar">
                            <ItemTemplate>
                                <table style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px; border-left-color: maroon; border-bottom-color: maroon; border-top-style: none; border-top-color: maroon; border-right-style: none; border-left-style: none; border-right-color: maroon; border-bottom-style: none; width: 100%; height: 100%;" border="0" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td align="right" style="width: 128px; height: 22px; background-color: #ffffcc;">
                                            <asp:Label ID="Label1" runat="server" Font-Underline="False" ForeColor="#CC0000" Text="Tipo de Evento"
                                                Width="117px"></asp:Label></td>
                                        <td style="width: 331px; height: 22px;">
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("tipo_evento_desc") %>' Width="200px" Font-Bold="True" ForeColor="#FF6633"></asp:Label>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 128px; height: 22px; background-color: #ffffcc;">
                                            <asp:Label ID="Label6" runat="server" Font-Underline="False" ForeColor="#CC0000" Text="Título do Evento"
                                                Width="117px"></asp:Label></td>
                                        <td colspan="1" style="height: 22px; width: 331px;">
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("titulo") %>' Width="514px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 128px; height: 22px; background-color: #ffffcc;">
                                            <asp:Label ID="Label7" runat="server" Font-Underline="False" ForeColor="#CC0000" Text="Data / Hora"
                                                Width="117px"></asp:Label></td>
                                        <td colspan="1" style="height: 22px; width: 331px;">
                                            <asp:Label ID="Label8" runat="server" Text='<%# String.Format("{0:d} - {1}", Eval("data") , Eval("desc_horario")) %>' Width="514px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbxParticipar" runat="server" Text="Participar" AutoPostBack="True" OnCheckedChanged="ckbxParticipar_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="White" ForeColor="#330099" BorderColor="Black" BorderStyle="Dotted" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" BorderStyle="None" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" BorderColor="Black" BorderStyle="Dotted" BorderWidth="2px" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <AlternatingRowStyle BorderColor="Black" BorderStyle="None" />
                    <EmptyDataRowStyle BackColor="White" BorderColor="White" Font-Bold="True" ForeColor="Maroon"
                        HorizontalAlign="Center" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <EditRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
                </asp:GridView>
                <asp:Panel ID="pnlInscricao" runat="server" Height="100%" Visible="False" Width="100%">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td style="width: 20%; height: 40px;">
                            </td>
                            <td align="center" valign="middle" style="height: 40px">
                                <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/imagens/btn_print.gif" OnClick="btnImprimir_Click" />&nbsp;
                                <asp:ImageButton ID="btnFechar" runat="server" ImageUrl="~/imagens/btn_canc.gif"
                                    OnClick="ImageButton2_Click" /></td>
                            <td style="width: 20%; height: 40px;">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    &nbsp;&nbsp;
</asp:Content>

