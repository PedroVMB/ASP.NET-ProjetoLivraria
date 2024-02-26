<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="ProjetoLivraria.Livraria.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 text-center">
        <div class="row justify-content-center">
            <div class="justify-content-center">
                <h1 class="mb-4">Página de gerenciamento principal</h1>
                <p>
                    Nesta página, você pode escolher qual gerenciamento deseja realizar, seja de Autores, Editores, Categorias e/ou Livros.
                </p>
                <p>
                    Este projeto foi desenvolvido durante o treinamento de ASP.NET na TecnoTrends, proporcionando uma experiência prática e enriquecedora no desenvolvimento web.
                </p>
                <p>
                    Fique à vontade para explorar as diversas funcionalidades disponíveis e aproveitar ao máximo o conhecimento adquirido durante o treinamento.
                </p>
            </div>
        </div>
        <hr />
        <div class="row mt-4  justify-content-center">
            <div class="col-md-3 mb-4">
                <a href="GerenciamentoAutores.aspx" class="btn btn-primary btn-block">Gerenciamento de Autores</a>
            </div>
            <div class="col-md-3 mb-4">
                <a href="GerenciamentoEditores.aspx" class="btn btn-success btn-block">Gerenciamento de Editores</a>
            </div>
            <div class="col-md-3 mb-4">
                <a href="GerenciamentoCategorias.aspx" class="btn btn-info btn-block">Gerenciamento de Categorias</a>
            </div>
            <div class="col-md-3 mb-4">
                <a href="GerenciamentoLivros.aspx" class="btn btn-warning btn-block">Gerenciamento de Livros</a>
            </div>
        </div>
    </div>
</asp:Content>
