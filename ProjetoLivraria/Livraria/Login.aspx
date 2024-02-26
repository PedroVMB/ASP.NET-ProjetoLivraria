<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjetoLivraria.Livraria.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-4 text-center">
                <h2 class="mb-4">Página de Login</h2>
                <asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="text-danger"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control mb-2" placeholder="Nome de Usuário"></asp:TextBox>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mb-2" placeholder="Senha"></asp:TextBox>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" style="margin-top: 10px" OnClick="btnLogin_Click" />
                <asp:Label ID="lblSuccessMessage" runat="server" Text="" CssClass="text-success" style="margin-top: 10px"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
