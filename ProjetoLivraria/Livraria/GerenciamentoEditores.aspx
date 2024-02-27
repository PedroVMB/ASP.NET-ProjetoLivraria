<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoEditores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoEditores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left">
        <h2>Cadastro de novo editor</h2>
        <table>
            <tr style="display: grid">
                <td>
                     <asp:Label ID="lblCadastroNomeEditor" runat="server" Font-Size="16pt" Text="Nome"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeEditor" runat="server" CssClass="form-control" Height="35px" Width="400px"
                         oninput="capitalizeFirstLetter(this)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroNomeEditor"
                        Style="color: red;" ErrorMessage="* Digite o nome do editor."></asp:RequiredFieldValidator>
                </td>
                <td>
                     <asp:Label ID="lblCadastroEmailEditor" runat="server" Font-Size="16pt" Text="E-mail"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroEmailEditor" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCadastroEmailEditor"
                        Style="color: red;" ErrorMessage="* Digite o e-mail do editor."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="tbxCadastroEmailEditor"
                            ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                            ErrorMessage="* O E-mail não é válido. Por favor, digite um e-mail válido."
                            Style="color: red;"></asp:RegularExpressionValidator>
                </td>
                <td>
                     <asp:Label ID="lblCadastroUrlEditor" runat="server" Font-Size="16pt" Text="Url"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txbCadastroUrlEditor" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txbCadastroUrlEditor"
                        Style="color: red;" ErrorMessage="* Digite a url do editor."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorUrl" runat="server" ControlToValidate="txbCadastroUrlEditor"
                        ValidationExpression="^(?i)(https?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$"
                        ErrorMessage="* A URL do editor não é válida. Por favor, digite uma URL válida."
                        Style="color: red;"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Button ID="btnNovoEditor" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar" OnClick="BtnNovoEditor_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div class="row">
        <h2 style="text-align: center;">Lista de editores cadastrados</h2>
        <asp:GridView ID="gvGerenciamentoEditores" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="14px" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoEditores_RowCancelingEdit"
            OnRowEditing="gvGerenciamentoEditores_RowEditing" OnRowUpdating="gvGerenciamentoEditores_RowUpdating"
            OnRowDeleting="gvGerenciamentoEditores_RowDeleting" OnRowCommand="gvGerenciamentoEditores_RowCommand" AllowPaging="True" OnPageIndexChanging="gvGerenciamentoEditores_PageIndexChanging">
            <Columns>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdEditor" runat="server" Text='<%# Eval("EDI_ID_EDITOR") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoIdEditor" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdEditor" runat="server" Style="text-align: center;" Text='<%# Eval("EDI_ID_EDITOR") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeEditor" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("EDI_NM_EDITOR")
%>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeEditor" runat="server" Style="text-align: center; display:block;" Text="Nome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNomeEditor" runat="server" Style="text-align: left;" Text='<%# Eval("EDI_NM_EDITOR") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="150px"></HeaderStyle>
                    
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditEmailEditor" runat="server" CssClass="form-control" Height="35px" MaxLength="45" Text='<%#
Eval("EDI_DS_EMAIL") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="tbxEditEmailEditor"
                            ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                            ErrorMessage="* O e-mail não é válido. Por favor, digite um e-mail válido."
                            Style="color: red;"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEmailEditor" runat="server" Style="text-align: center; display: block" Text="E-mail"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEmailEditor" runat="server" Style="text-align: center;" Text='<%# Eval("EDI_DS_EMAIL") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="400px"></HeaderStyle>
                   
                </asp:TemplateField>

                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditUrlEditor" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%#
Eval("EDI_DS_URL") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorUrl" runat="server" ControlToValidate="tbxEditUrlEditor"
                        ValidationExpression="^(?i)(https?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$"
                        ErrorMessage="* A URL do editor não é válida. Por favor, digite uma URL válida."
                        Style="color: red;"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoUrlEditor" runat="server" Style="text-align: center; display: block;" Text="Url"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUrlEditor" runat="server" Style="text-align: center;" Text='<%# Eval("EDI_DS_URL") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="450px"></HeaderStyle>
                    
                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false" />&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" 
                            />
                    </EditItemTemplate>
                     <HeaderTemplate>
                        <asp:Label ID="lblTextoAcoesBotao" runat="server" Style="text-align: center; display: block" Text="Ações"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarAutor" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />&nbsp;
                        <asp:Button ID="btnDeletarAutor" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete"
                             CausesValidation="false"  OnClientClick="return confirm('Tem certeza que deseja deletar?');"/>
                         <asp:Button ID="btnCarregaLivrosEditor" runat="server" CssClass="btn btn-primary" Text="Livros" CommandName="CarregaLivrosEditor"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" />&nbsp;
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
