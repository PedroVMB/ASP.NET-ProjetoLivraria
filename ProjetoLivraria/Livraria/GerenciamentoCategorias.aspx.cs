using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoCategorias : System.Web.UI.Page
    {
        CategoriaDAO ioCategoriaDAO = new CategoriaDAO();
        LivrosDAO ioLivrosDAO = new LivrosDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!this.IsPostBack)
                this.CarregaDados();
        }

        public List<Categorias> ListaCategoria
        {
            get
            {
               
                if ((List<Categorias>)ViewState["ViewStateListaCategoria"] == null)
                    this.CarregaDados();
               
                return (List<Categorias>)ViewState["ViewStateListaCategoria"];
            }
            set
            {
                ViewState["ViewStateListaCategoria"] = value;
            }
        }

        private void CarregaDados()
        {
            try
            {
               
                this.ListaCategoria = this.ioCategoriaDAO.BuscaCategorias().OrderBy(loCategoria => loCategoria.TIL_DS_DESCRICAO).ToList();

                this.gvGerenciamentoCategoria.DataSource = this.ListaCategoria;
                
                this.gvGerenciamentoCategoria.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar as categorias.');</script>");
            }
        }


        protected void BtnNovoCategoria_Click(object sender, EventArgs e)
        {
            try
            {

                decimal ldcIdCategoria = this.ListaCategoria.OrderByDescending(a => a.TIL_ID_TIPO_LIVRO).First().TIL_ID_TIPO_LIVRO + 1;
                string lsDescricaoCategoria = this.tbxCadastroCategoria.Text;
                Categorias ioCategoria = new Categorias(ldcIdCategoria, lsDescricaoCategoria);

                this.ioCategoriaDAO.InsertCategoria(ioCategoria);
               
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Categoria cadastrada com sucesso!');</script>");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"<script>alert('Erro ao tentar cadastrar nova Categoria: {ex.ToString()}');</script>");
            }
           
            this.tbxCadastroCategoria.Text = String.Empty;
        }

        protected void gvGerenciamentoCategoria_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoCategoria.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }
        protected void gvGerenciamentoCategoria_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           
            this.gvGerenciamentoCategoria.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoCategoria_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal ldcIdCategoria = Convert.ToDecimal((this.gvGerenciamentoCategoria.Rows[e.RowIndex].FindControl("lblEditIdCategoria") as Label).Text);
            string lsCategoria = (this.gvGerenciamentoCategoria.Rows[e.RowIndex].FindControl("tbxEditDescricaoCategoria") as TextBox).Text;

            if (String.IsNullOrWhiteSpace(lsCategoria))
            {
                HttpContext.Current.Response.Write("<script>alert('Digite o nome da categoria.');</script>");
            }
            else
            {
                try
                {
                    Categorias loCategoria = new Categorias(ldcIdCategoria, lsCategoria);

                    this.ioCategoriaDAO.AtualizaCategoria(loCategoria);

                    this.gvGerenciamentoCategoria.EditIndex = -1;

                    this.CarregaDados();

                    HttpContext.Current.Response.Write("<script>alert('Categoria atualizada com sucesso!');</script>");
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write($"<script>alert('Erro na atualização da categoria: {ex}');</script>");
                }
            }
        }


        protected void gvGerenciamentoCategoria_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoCategoria.Rows[e.RowIndex];
                decimal ldcIdCategoria = Convert.ToDecimal((this.gvGerenciamentoCategoria.Rows[e.RowIndex].FindControl("lblIdCategoria") as Label).Text);
                Categorias ioCategoria = this.ioCategoriaDAO.BuscaCategorias(ldcIdCategoria).FirstOrDefault();

                if (ioCategoria != null)
                {
                   
                    if (CategoriaPossuiLivrosAssociados(ioCategoria.TIL_ID_TIPO_LIVRO))
                    {
                        HttpContext.Current.Response.Write(@"<script>alert('Não é possível remover a categoria selecionada pois existem livros associados a ele.');</script>");
                    }
                    else
                    {
                        this.ioCategoriaDAO.RemoveCategoria(ioCategoria);
                        HttpContext.Current.Response.Write(@"<script>alert('Categoria removida com sucesso.');</script>");
                        this.CarregaDados();
                    }
                }
            }
            catch(Exception ex)
            {
                HttpContext.Current.Response.Write($"<script>alert('Erro na remoção da categoria selecionada. Detalhes: {ex.Message}');</script>");
            }
        }

       
        private bool CategoriaPossuiLivrosAssociados(decimal idCategoria)
        {
            return this.ioLivrosDAO.FindLivrosByCategoria(idCategoria).Count > 0;
        }

        protected void gvGerenciamentoCategoria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            switch (e.CommandName)
            {
                case "CarregaLivrosCategoria":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdCategoria = Convert.ToDecimal((this.gvGerenciamentoCategoria.Rows[liRowIndex].FindControl("lblIdCategoria") as
                   Label).Text);
                    string lsDescricaoCategoria = (this.gvGerenciamentoCategoria.Rows[liRowIndex].FindControl("lblDescricaoCategoria") as Label).Text;

                    Categorias loCategoria = new Categorias(ldcIdCategoria, lsDescricaoCategoria);
                   
                    this.CategoriaSessao = loCategoria;

                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;
                default:
                    break;
            }
        }

        public Categorias CategoriaSessao
        {
            get { return (Categorias)Session["sessionCategoriaSelecionado"]; }
            set { Session["sessionCategoriaSelecionado"] = value; }
        }

        protected void gvGerenciamentoCategoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvGerenciamentoCategoria.PageIndex = e.NewPageIndex;
            this.CarregaDados();
        }
    }
}