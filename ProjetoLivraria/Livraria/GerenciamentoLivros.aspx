<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoLivros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro de novo livro</h2>
        <table>
            <tr style="display: grid;">
                <td>
                    <asp:Label ID="lblCadastroNomeLivro" runat="server" Font-Size="16pt" Text="Nome: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeLivro" runat="server" CssClass="form-control" Height="35px" Width="500px"
                        oninput="capitalizeFirstLetter(this)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroNomeLivro"
                        Style="color: red;" ErrorMessage="*Digite o nome do livro"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCategoria" runat="server" Font-Size="16pt" Text="Categoria: "></asp:Label>
                </td>
                <td style="margin-top: 3px">
                    <asp:DropDownList ID="ddlCategoria" Style="width: 300px; height: 35px; margin-bottom: 20px;" runat="server" CssClass="form-control"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblEditor" runat="server" Font-Size="16pt" Text="Editor: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEditor" Style="width: 300px; height: 35px; margin-bottom: 20px;" runat="server" CssClass="form-control"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblAutor" runat="server" Font-Size="16pt" Text="Autor: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAutor" Style="width: 300px; height: 35px; margin-bottom: 20px;" runat="server" CssClass="form-control"></asp:DropDownList>
                </td>

                <td>
                    <asp:Label ID="lblCadastroPrecoLivro" runat="server" Font-Size="16pt" Text="Preço: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCadastroPrecoLivro" runat="server" CssClass="form-control" Height="35px" Width="500px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCadastroPrecoLivro"
                        Style="color: red;" ErrorMessage="* Digite o preço do livro"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPreco" runat="server" ControlToValidate="txtCadastroPrecoLivro"
                        ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="* Somente números são permitidos"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="RangeValidatorPreco" runat="server" ControlToValidate="txtCadastroPrecoLivro"
                        MinimumValue="0" MaximumValue="99999999" ErrorMessage="* O preço deve ser um valor não negativo"></asp:RangeValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroRoyaltyLivro" runat="server" Font-Size="16pt" Text="Royalty: "></asp:Label>
                </td>
                <td>
                   <asp:TextBox ID="txtCadastroRoyaltyLivro" runat="server" CssClass="form-control" Height="35px" Width="500px"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCadastroRoyaltyLivro"
                        Style="color: red;" ErrorMessage="* Digite o royalty do livro"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCadastroRoyaltyLivro"
                        ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="* Somente números são permitidos"></asp:RegularExpressionValidator>
                    
                </td>

                <td>
                    <asp:Label ID="lblCadastroEdicaoLivro" runat="server" Font-Size="16pt" Text="Edição: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCadastroEdicaoLivro" runat="server" CssClass="form-control" Height="35px" Width="500px"></asp:TextBox>


                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCadastroEdicaoLivro"
                        Style="color: red;" ErrorMessage="* Digite a edição do livro"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEdicao" runat="server" ControlToValidate="txtCadastroEdicaoLivro"
                        ValidationExpression="^\d+$" ErrorMessage="* Somente números inteiros são permitidos"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="RangeValidatorEdicao" runat="server" ControlToValidate="txtCadastroEdicaoLivro"
                        MinimumValue="1" MaximumValue="999999" ErrorMessage="* A edição deve ser um número não negativo e maior que zero"></asp:RangeValidator>
                </td>


                <td>
                    <asp:Label ID="lblCadastroResumoLivro" runat="server" Font-Size="16pt" Text="Resumo: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCadastroResumoLivro" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" Columns="50" Style="width: 279px; height: 122px;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCadastroResumoLivro" Style="color: red;" ErrorMessage="* Digite o resumo do livro"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnNovoLivro" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar" OnClick="BtnNovoLivro_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div class="row">
        <h2 style="text-align: center;">Lista de Livros cadastrados </h2>
        <asp:GridView ID="gvGerenciamentoLivros" runat="server" Width="100%" AutoGenerateColumns="false" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoLivros_RowCancelingEdit"
            OnRowEditing="gvGerenciamentoLivros_RowEditing" OnRowUpdating="gvGerenciamentoLivros_RowUpdating"
            OnRowDeleting="gvGerenciamentoLivros_RowDeleting" OnRowCommand="gvGerenciamentoLivros_RowCommand" OnRowDataBound="gvGerenciamentoLivros_RowDataBound">
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdLivro" runat="server" Text='<%# Eval("Liv_Id_Livro") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdLivro" runat="server" Style="width100%" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdLivro" runat="server" Style="text-align: center;" Text='<%# Eval("Liv_Id_Livro") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("Liv_Nm_Titulo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeLivro" runat="server" Style="text-align: center; display: block;" Text="Título"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNomeLivro" runat="server" Style="text-align: Left;" Text='<%# Eval("Liv_Nm_Titulo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="350px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditPrecoLivro" runat="server" CssClass="form-control" Height="25px" MaxLength="30" Text='<%# Eval("Liv_Vl_Preco", "{0:F2}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoPrecoLivro" runat="server" Style="text-align: center; display: block;" Text="Preço R$"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPrecoLivro" runat="server" Style="text-align: center;" Text='<%# Eval("Liv_Vl_Preco", "{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="150px" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditRoyaltyLivro" runat="server" CssClass="form-control" Height="30px" MaxLength="50" Text='<%# Eval("Liv_Pc_Royalty", "{0:F2}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoRoyaltyLivro" runat="server" Style="text-align: center; display: block;" Text="Royalty %"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRoyaltyLivro" runat="server" Style="text-align: center;" Text='<%# Eval("Liv_Pc_Royalty", "{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="350px" />
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>



                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditResumoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval("Liv_Ds_Resumo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoResumoLivro" runat="server" Style="text-align: center; display: block;" Text="Resumo"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblResumoLivro" runat="server" Style="text-align: center; display: block;" Text='<%# Eval("Liv_Ds_Resumo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="450px" />
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditEdicaoLivro" runat="server" CssClass="form-control" Height="30px" MaxLength="50" Text='<%# Eval("Liv_Nu_Edicao") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEdicaoLivro" runat="server" Style="text-align: center; display: block;" Text="Edição"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEdicaoLivro" runat="server" Style="text-align: center;" Text='<%# Eval("Liv_Nu_Edicao") %>'></asp:Label>

                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="350px" />
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>




                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEditCategoria" CssClass="form-control" Height="30px" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoCategoria" runat="server" Style="text-align: center; display: block;" Text="Categoria"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategoria" runat="server" Style="text-align: center;" Text='<%# this.ioCategoriaDAO.BuscarDescricaoCategoria(Convert.ToDecimal(Eval("Liv_Id_Tipo_Livro"))) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="350px" />


                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEditEditor" CssClass="form-control" Height="30px" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEditor" runat="server" Style="text-align: center; display: block;" Text="Editor"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEditor" runat="server" Style="text-align: center;" Text='<%# this.ioEditoresDAO.BuscaEditores(Convert.ToDecimal(Eval("Liv_Id_Editor"))).FirstOrDefault()?.EDI_NM_EDITOR ?? "" %>'> </asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="350px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEditAutor" CssClass="form-control" Height="35px" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoAutor" runat="server" Style="text-align: center; display: block;" Text="Autor"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAutor" runat="server" Style="text-align: center;" Text='<%# this.ioLivroAutorDAO.BuscaNomeAutorPeloIdDoLivroAssociado(Convert.ToDecimal(Eval("Liv_Id_Livro"))).FirstOrDefault()?.aut_nm_nome ?? "" %>'> </asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="400px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <div style="display: flex;">
                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false" />
                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" />
                        </div>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoAcoesBotao" runat="server" Style="text-align: center; display: block;" Text="Ações"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="display: flex; flex-direction: row;">
                            <asp:Button ID="btnEditarLivro" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />
                            <asp:Button ID="btnDeletarLivro" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete" CausesValidation="false"  OnClientClick="return confirm('Tem certeza que deseja deletar?');"/>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>

                </asp:TemplateField>
            </Columns>

            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" Font-Size="14px" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle HorizontalAlign="Center" Wrap="True" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size="14px" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>


    <script>
        function capitalizeFirstLetter(element) {
            var currentValue = element.value;
            if (currentValue.length > 0) {
                element.value = currentValue.charAt(0).toUpperCase() + currentValue.slice(1);
            }
        }
    </script>
</asp:Content>
