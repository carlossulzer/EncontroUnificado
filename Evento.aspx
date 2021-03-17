<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Evento.aspx.cs" Inherits="Evento" Title="Encontro Unificado" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 240px">
        <tr>
            <td style="width: 779px; height: 18px; background-color: #cc6600; text-align: center">
                &nbsp;<asp:Label ID="lblTituloView" runat="server" Text="Evento" Width="770px"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 779px; height: 220px; text-align: center;" valign="top">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server" EnableTheming="True">
                        <table border="0" cellpadding="1" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
                            padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 10%" width="100%">
                            <tr>
                                <td align="right" style="width: 24%; height: 16px">
                                    <strong><span style="color: #000099">Título&nbsp;</span></strong></td>
                                <td colspan="3" style="height: 16px; text-align: left">
                                    <asp:TextBox ID="txtTitulo" runat="server" MaxLength="250" TabIndex="1" Width="618px" CssClass="TextBox" Height="48px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 24%; height: 16px">
                                    <span style="color: #000099">Tipo de Evento</span></td>
                                <td colspan="3" style="height: 16px; text-align: left">
                                    <asp:DropDownList ID="dropTipoEvento" runat="server" TabIndex="2" Width="325px">
                                    </asp:DropDownList>
                                    <span style="color: #000099"></span>
                                </td>
                            </tr>
                            <tr style="color: #000099">
                                <td align="right" style="width: 24%; height: 16px">
                                    <span style="color: #000099">Ementa</span></td>
                                <td colspan="3" style="height: 16px; text-align: left">
                                    <asp:TextBox ID="txtEmenta" runat="server" Height="48px" MaxLength="200" TabIndex="3"
                                        TextMode="MultiLine" Width="618px" CssClass="TextBox"></asp:TextBox>
                                    <span style="color: #000099"></span>
                                </td>
                            </tr>
                            <tr style="color: #000099">
                                <td align="right" style="width: 24%; height: 16px">
                                    <span style="color: #000099">Objetivos&nbsp;</span></td>
                                <td colspan="3" style="height: 16px; text-align: left">
                                    <asp:TextBox ID="txtObjetivos" runat="server" MaxLength="100" TabIndex="4" TextMode="MultiLine"
                                        Width="618px" CssClass="TextBox"></asp:TextBox>
                                    <span style="color: #000099"></span>
                                </td>
                            </tr>
                            <tr style="color: #000099">
                                <td align="right" style="width: 24%; height: 16px">
                                    <span style="color: #000099">Público Alvo</span></td>
                                <td colspan="3" style="height: 16px; text-align: left">
                                    <asp:TextBox ID="txtPublicoAlvo" runat="server" MaxLength="60" TabIndex="5" Width="618px" CssClass="TextBox"></asp:TextBox>
                                    <span style="color: #000099"></span>
                                </td>
                            </tr>
                            <tr style="color: #000099">
                                <td align="right" style="width: 24%; height: 16px">
                                    <span style="color: #000099">Núcleo</span></td>
                                <td style="width: 41%; height: 16px; text-align: left;">
                                    <asp:DropDownList ID="dropNucleo" runat="server" TabIndex="6" Width="223px">
                                    </asp:DropDownList>
                                    <span style="color: #000099"></span>
                                </td>
                                <td style="width: 19%; height: 16px; text-align: right">
                                    Caracterização</td>
                                <td style="width: 70%; height: 16px; text-align: left">
                                    <asp:DropDownList ID="dropCaracterizacao" runat="server" TabIndex="7" Width="289px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr style="color: #000099">
                                <td align="right" style="width: 24%; height: 16px">
                                    <span style="color: #000099">Recusro</span></td>
                                <td style="width: 41%; height: 16px; text-align: left;">
                                    <asp:DropDownList ID="dropRecursos" runat="server" TabIndex="8" Width="223px">
                                    </asp:DropDownList></td>
                                <td style="width: 19%; height: 16px; text-align: right">
                                    Nº de Vagas</td>
                                <td style="width: 70%; height: 16px; text-align: left">
                                    <asp:TextBox ID="txtNumVagas" runat="server" MaxLength="3" TabIndex="9" Width="63px"></asp:TextBox></td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server" EnableTheming="True" >
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;">
                            <tr>
                                <td style="width: 100%; height: 121px;" align="left" valign="top">
                                    <asp:GridView ID="gridBanca" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        EmptyDataText="Não existe nenhum professor cadastrado na banca avaliadora. Clique no botão Novo para cadastrar um professor."
                                        ForeColor="#333333" Width="100%" OnRowDataBound="gridBanca_RowDataBound" OnRowDeleting="gridBanca_RowDeleting" >
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="cod_professor" HeaderText="C&#243;digo" Visible="False" />
                                            <asp:BoundField DataField="nome" HeaderText="Nome do Professor" />
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
                                        <EmptyDataRowStyle BackColor="Transparent" BorderColor="Transparent" BorderWidth="0px" />
                                    </asp:GridView>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; text-align: center" align="center">
                                    &nbsp;<asp:ImageButton ID="btnNovoBanca" runat="server" ImageUrl="~/imagens/btn_novo.gif"
                                        OnClick="btnNovoBanca_Click" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 32px">
                                    <asp:Panel ID="pnlBanca" runat="server" BackColor="#CC6600" ForeColor="White"
                                        Height="32px" HorizontalAlign="Center" Visible="False" Width="100%">
                                        
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 32px">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:Label ID="lblBancaProf" runat="server" Height="4px" Text="Professor" Width="69px" Visible="False" ForeColor="Black"></asp:Label><asp:DropDownList ID="dropBancaProf" runat="server" Width="311px" Visible="False">
                                                    </asp:DropDownList>
                                                    <asp:ImageButton ID="btnConfirmarBanca" runat="server" ImageUrl="~/imagens/btn_confirmar.gif" OnClick="btnConfirmarBanca_Click" Visible="False" Width="82px" Height="14px" />
                                                    <asp:ImageButton ID="btnCancelarBanca" runat="server" ImageUrl="~/imagens/btn_cancelar.gif" OnClick="btnCancelarBanca_Click" Visible="False" Width="85px" Height="14px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;">
                            <tr>
                                <td style="width: 100%; height: 121px;" align="left" valign="top">
                                    <asp:GridView ID="gridOrientador" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        EmptyDataText="Não existe nenhum professor cadastrado como orientador. Clique no botão Novo para cadastrar um orientador."
                                        ForeColor="#333333" Width="100%" OnRowDataBound="gridOrientador_RowDataBound" OnRowDeleting="gridOrientador_RowDeleting" >
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <EmptyDataRowStyle BackColor="Transparent" BorderColor="Transparent" BorderWidth="0px" />
                                        <Columns>
                                            <asp:BoundField DataField="cod_professor" HeaderText="C&#243;digo" Visible="False" />
                                            <asp:BoundField DataField="nome" HeaderText="Nome do Orientador" />
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
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; text-align: center" align="center">
                                    &nbsp;<asp:ImageButton ID="btnNovoOrientador" runat="server" ImageUrl="~/imagens/btn_novo.gif"
                                        OnClick="btnNovoOrientador_Click" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 32px">
                                    <asp:Panel ID="pnlOrientador" runat="server" BackColor="#CC6600" ForeColor="White"
                                        Height="32px" HorizontalAlign="Center" Visible="False" Width="100%">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 32px">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:Label ID="lblOrientadorProf" runat="server" ForeColor="Black" Height="4px" Text="Professor"
                                                        Visible="False" Width="69px"></asp:Label><asp:DropDownList ID="dropOrientadorProf" runat="server" Width="311px" Visible="False">
                                                        </asp:DropDownList>
                                                    <asp:ImageButton ID="btnConfirmarOrientador" runat="server" ImageUrl="~/imagens/btn_confirmar.gif" OnClick="btnConfirmarOrientador_Click" Visible="False" Width="82px" Height="14px" />
                                                    <asp:ImageButton ID="btnCancelarOrientador" runat="server" ImageUrl="~/imagens/btn_cancelar.gif" OnClick="btnCancelarOrientador_Click" Visible="False" Width="85px" Height="14px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    &nbsp; &nbsp;
                    <asp:View ID="View4" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;">
                            <tr>
                                <td style="width: 100%; height: 121px;" align="left" valign="top">
                                    <asp:GridView ID="gridPalestrante" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        EmptyDataText="Não existe nenhum professor cadastrado como palestrante. Clique no botão Novo para cadastrar um palestrante."
                                        ForeColor="#333333" Width="100%" OnRowDataBound="gridPalestrante_RowDataBound" OnRowDeleting="gridPalestrante_RowDeleting" >
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <EmptyDataRowStyle BackColor="Transparent" BorderColor="Transparent" BorderWidth="0px" />
                                        <Columns>
                                            <asp:BoundField DataField="cod_professor" HeaderText="C&#243;digo" Visible="False" />
                                            <asp:BoundField DataField="nome" HeaderText="Nome do Palestrante" />
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
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; text-align: center" align="center">
                                    &nbsp;<asp:ImageButton ID="btnNovoPalestrante" runat="server" ImageUrl="~/imagens/btn_novo.gif"
                                        OnClick="btnNovoPalestrante_Click" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 32px">
                                    <asp:Panel ID="pnlPalestrante" runat="server" BackColor="#CC6600" ForeColor="White"
                                        Height="32px" HorizontalAlign="Center" Visible="False" Width="100%">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 32px">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:Label ID="lblPalestranteProf" runat="server" ForeColor="Black" Height="4px"
                                                        Text="Professor" Visible="False" Width="69px"></asp:Label><asp:DropDownList ID="dropPalestranteProf" runat="server" Width="311px" Visible="False">
                                                        </asp:DropDownList>
                                                    <asp:ImageButton ID="btnConfirmarPalestrante" runat="server" ImageUrl="~/imagens/btn_confirmar.gif" OnClick="btnConfirmarPalestrante_Click" Visible="False" Width="82px" Height="14px" />
                                                    <asp:ImageButton ID="btnCancelarPalestrante" runat="server" ImageUrl="~/imagens/btn_cancelar.gif" OnClick="btnCancelarPalestrante_Click" Visible="False" Width="85px" Height="14px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View5" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;">
                            <tr>
                                <td style="width: 100%; height: 121px;" align="left" valign="top">
                                    <asp:GridView ID="gridIntegrante" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        EmptyDataText="Não existe nenhum aluno cadastrado como integrante. Clique no botão Novo para cadastrar um integrante."
                                        ForeColor="#333333" Width="100%" OnRowDataBound="gridIntegrante_RowDataBound" OnRowDeleting="gridIntegrante_RowDeleting" >
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <EmptyDataRowStyle BackColor="Transparent" BorderColor="Transparent" BorderWidth="0px" />
                                        <Columns>
                                            <asp:BoundField DataField="ra" HeaderText="C&#243;digo" Visible="False" />
                                            <asp:BoundField DataField="nm_aluno" HeaderText="Nome do Integrante" />
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
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; text-align: center; height: 32px;" align="center">
                                    &nbsp;<asp:ImageButton ID="btnNovoIntegrante" runat="server" ImageUrl="~/imagens/btn_novo.gif" OnClick="btnNovoIntegrante_Click" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 32px">
                                    <asp:Panel ID="pnlIntegrante" runat="server" BackColor="#CC6600" ForeColor="White"
                                        Height="32px" HorizontalAlign="Center" Visible="False" Width="100%">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 32px">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:RadioButton ID="rbtGraduacao" runat="server" ForeColor="#000000"
                                                        GroupName="ensino" Text="Graduação" AutoPostBack="True" OnCheckedChanged="rbtGraduacao_CheckedChanged" />
                                                    <asp:RadioButton ID="rbtTecnologo" runat="server" ForeColor="#000000" GroupName="ensino"
                                                        Text="Tecnólogo" AutoPostBack="True" OnCheckedChanged="rbtTecnologo_CheckedChanged" />
                                                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblAluno" runat="server" ForeColor="Black"
                                                        Height="4px" Text="Integrante" Visible="False" Width="69px"></asp:Label><asp:DropDownList ID="dropIntegrante" runat="server" Width="273px" Visible="False">
                                                        </asp:DropDownList>
                                                    <asp:ImageButton ID="btnConfirmarIntegrante" runat="server" ImageUrl="~/imagens/btn_confirmar.gif" OnClick="btnConfirmarIntegrante_Click" Visible="False" Width="82px" Height="14px" />
                                                    <asp:ImageButton ID="btnCancelarIntegrante" runat="server" ImageUrl="~/imagens/btn_cancelar.gif" OnClick="btnCancelarIntegrante_Click" Visible="False" Width="85px" Height="14px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View6" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;">
                            <tr>
                                <td style="width: 100%; height: 121px;" align="left" valign="top">
                                    <asp:GridView ID="gridCalendario" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        EmptyDataText="Não existe nenhum calendário cadastrado no evento. Clique no botão Novo para cadastrar um calendário."
                                        ForeColor="#333333" Width="100%" OnRowDataBound="gridCalendario_RowDataBound" OnRowDeleting="gridCalendario_RowDeleting" >
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
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; text-align: center" align="center">
                                    &nbsp;<asp:ImageButton ID="btnNovoCalendario" runat="server" ImageUrl="~/imagens/btn_novo.gif" OnClick="btnNovoCalendario_Click" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 32px">
                                    <asp:Panel ID="pnlCalendario" runat="server" BackColor="#CC6600" ForeColor="White"
                                        Height="32px" HorizontalAlign="Center" Visible="False" Width="100%">
                                        <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; height: 32px">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:Label ID="lblData" runat="server" ForeColor="Black" Height="4px" Text="Data" Width="35px"></asp:Label>
                                                    <asp:TextBox ID="txtData" runat="server" Width="79px"></asp:TextBox>
                                                    &nbsp; &nbsp;&nbsp;
                                                    <asp:Label ID="lblSala" runat="server" ForeColor="Black" Height="4px" Text="Sala" Width="35px"></asp:Label>
                                                    <asp:DropDownList ID="dropSala" runat="server" Width="273px">
                                                    </asp:DropDownList>
                                                    &nbsp; &nbsp; &nbsp;<asp:Label ID="lblHorario" runat="server" ForeColor="Black" Height="4px"
                                                        Text="Horário" Width="35px"></asp:Label>&nbsp;
                                                    <asp:DropDownList ID="dropHorario" runat="server" Width="179px">
                                                    </asp:DropDownList>&nbsp;<br />
                                                    <br />
                                                    <asp:ImageButton ID="btnConfirmarCalendario" runat="server" ImageUrl="~/imagens/btn_confirmar.gif" OnClick="btnConfirmarCalendario_Click" Width="82px" Height="14px" />
                                                    <asp:ImageButton ID="btnCancelarCalendario" runat="server" ImageUrl="~/imagens/btn_cancelar.gif" OnClick="btnCancelarCalendario_Click" Width="85px" Height="14px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                 
                </asp:MultiView>
            </td>
        </tr>
        <tr>
         </tr>
    </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 32px" >
                <tr align="center">
                    <td align="center" style="width: 97px; background-color: #cc6600; text-align: center;" valign="middle">
                        &nbsp;<asp:LinkButton ID="lnkEvento" runat="server" OnClick="lnkEvento_Click">Evento</asp:LinkButton></td>
                    <td align="center" style="width: 90px; background-color: #cc6600; text-align: center;" valign="middle">
                        &nbsp;<asp:LinkButton ID="lnkBanca" runat="server" OnClick="lnkBanca_Click">Banca</asp:LinkButton></td>
                    <td align="center" style="width: 13px; background-color: #cc6600; text-align: center;" valign="middle">
                        &nbsp;<asp:LinkButton ID="lnkOrientador" runat="server" OnClick="lnkOrientador_Click">Orientador</asp:LinkButton></td>
                    <td align="center" style="width: 100px; background-color: #cc6600; text-align: center;" valign="middle">
                        &nbsp;<asp:LinkButton ID="lnkPalestrante" runat="server" OnClick="lnkPalestrante_Click">Palestrante</asp:LinkButton></td>
                    <td align="center" style="width: 100px; background-color: #cc6600; text-align: center"
                        valign="middle">
                        <asp:LinkButton ID="lnkIntegrantes" runat="server" OnClick="lnkIntegrantes_Click">Integrantes</asp:LinkButton></td>
                    <td align="center" style="width: 100px; background-color: #cc6600; text-align: center;" valign="middle">
                        &nbsp;<asp:LinkButton ID="lnkCalendario" runat="server" OnClick="lnkCalendario_Click">Calendário</asp:LinkButton></td>
                </tr>
            </table>
</asp:Content>

