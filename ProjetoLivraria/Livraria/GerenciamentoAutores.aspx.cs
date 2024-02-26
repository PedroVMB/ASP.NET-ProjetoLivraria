using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoAutores : System.Web.UI.Page
    {
        AutoresDAO ioAutoresDAO = new AutoresDAO();

        public BindingList<Autores> ListaAutores
        {
            get
            {
               
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null)
                    this.CarregaDados();
                
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!this.IsPostBack)
                this.CarregaDados();
        }


        private void CarregaDados()
        {
            try
            {
               
                this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
               
                this.gvGerenciamentoAutores.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nm_nome);
               
                this.gvGerenciamentoAutores.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Autores.');</script>");
            }
        }

        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                
                decimal ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;
               
                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.tbxCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.tbxCadastroEmailAutor.Text;
               
                Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
               
                this.ioAutoresDAO.InsertAutor(loAutor);
               
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Autor.');</script>");
            }
          
            this.tbxCadastroNomeAutor.Text = String.Empty;
            this.tbxCadastroSobrenomeAutor.Text = String.Empty;
            this.tbxCadastroEmailAutor.Text = String.Empty;
        }

        protected void gvGerenciamentoAutores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoAutores.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }
        protected void gvGerenciamentoAutores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
           
            this.gvGerenciamentoAutores.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoAutores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
           
            decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblEditIdAutor") as
           Label).Text);
            string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditNomeAutor") as TextBox).Text;
            string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditSobrenomeAutor") as
           TextBox).Text;
            string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditEmailAutor") as TextBox).Text;
            
            if (String.IsNullOrWhiteSpace(lsNomeAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o nome do autor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsSobrenomeAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o sobrenome do autor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsEmailAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o E-mail do autor.');</script>");
            else
            {
                try
                {
                    
                    Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                   
                    this.ioAutoresDAO.AtualizaAutor(loAutor);
                  
                    this.gvGerenciamentoAutores.EditIndex = -1;
                   
                    this.CarregaDados();

                    HttpContext.Current.Response.Write("<script>alert('Autor atualizado com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do cadastro do autor.');</script>");
                }
            }
        }

        protected void gvGerenciamentoAutores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoAutores.Rows[e.RowIndex];
                decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblIdAutor") as
               Label).Text);
                Autores loAutor = this.ioAutoresDAO.BuscaAutores(ldcIdAutor).FirstOrDefault();
                if (loAutor != null)
                {
                   
                    LivrosDAO loLivrosDAO = new LivrosDAO();
                    if (loLivrosDAO.FindLivrosByAutor(loAutor.aut_id_autor).Count != 0)
                    {
                        HttpContext.Current.Response.Write(@"<script>alert('Não é possível remover o autor selecionado pois existem livros
                       associados a ele.');</script>");
                    }else
                    {
                        this.ioAutoresDAO.RemoveAutor(loAutor);
                        this.CarregaDados();
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do autor selecionado.');</script>");
            }
        }

        protected void gvGerenciamentoAutores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            switch (e.CommandName)
            {
                case "CarregaLivrosAutor":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblIdAutor") as
                   Label).Text);
                    string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblNomeAutor") as Label).Text;
                    string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblSobrenomeAutor") as
                   Label).Text;
                    string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblEmailAutor") as Label).Text;
                    Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                   
                    this.AutorSessao = loAutor;
                  
                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;
                default:
                    break;
            }
        }


        public Autores AutorSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }




    }
}