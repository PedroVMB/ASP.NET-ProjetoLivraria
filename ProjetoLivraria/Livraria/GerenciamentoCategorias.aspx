<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoCategorias.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left">
        <h2>Cadastro de Categorias</h2>
        <table>
            <tr style="display: grid">
                <td>
                    <asp:Label ID="lblCadastroCategoria" runat="server" Font-Size="16pt" Text="Descrição"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroCategoria" runat="server" CssClass="form-control" Height="35px" Width="400px"
                        oninput="capitalizeFirstLetter(this)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroCategoria"
                        Style="color: red;" ErrorMessage="* Digite  a categoria do Livro."></asp:RequiredFieldValidator>

                </td>
                <td>
                    <asp:Button ID="btnNovoCategoria" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar" OnClick="BtnNovoCategoria_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div class="row" style="">
        <h2 style="text-align: center;">Lista de categorias cadastradas</h2>
        <asp:GridView ID="gvGerenciamentoCategoria" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoCategoria_RowCancelingEdit"
            OnRowEditing="gvGerenciamentoCategoria_RowEditing" OnRowUpdating="gvGerenciamentoCategoria_RowUpdating"
            OnRowDeleting="gvGerenciamentoCategoria_RowDeleting" OnRowCommand="gvGerenciamentoCategoria_RowCommand" AllowPaging="True" OnPageIndexChanging="gvGerenciamentoCategoria_PageIndexChanging" >
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdCategoria" runat="server" Text='<%# Eval("TIL_ID_TIPO_LIVRO") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdCategoria" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdCategoria" runat="server" Style="text-align: center;" Text='<%# Eval("TIL_ID_TIPO_LIVRO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditDescricaoCategoria" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("TIL_DS_DESCRICAO")
%>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoDescricaoCategoria" runat="server" Style="text-align: center;" Text="Descrição da Categoria"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescricaoCategoria" runat="server" Style="text-align: left; margin-left: 50px" Text='<%# Eval("TIL_DS_DESCRICAO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>



                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false" />&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" />
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <div style="text-align: right; display: block; margin-right: 50px">
                            <asp:Label ID="lblTextoDescricaoCategoria" runat="server" Text="Ações"></asp:Label>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarCategoria" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />&nbsp;
                        <asp:Button ID="btnDeletarCategoria" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('Tem certeza que deseja deletar?');"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
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
