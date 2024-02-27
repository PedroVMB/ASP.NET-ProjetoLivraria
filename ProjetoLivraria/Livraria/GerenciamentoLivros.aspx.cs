

using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoLivros : System.Web.UI.Page
    {
        public LivrosDAO ioLivroDAO = new LivrosDAO();
        public EditoresDAO ioEditoresDAO = new EditoresDAO();
        public CategoriaDAO ioCategoriaDAO = new CategoriaDAO();
        public LivroAutorDAO ioLivroAutorDAO = new LivroAutorDAO();
        public AutoresDAO ioAutoresDAO = new AutoresDAO();

        public List<Livros> ListaLivros
        {
            get
            {
              
                if ((List<Livros>)ViewState["ViewStateListaLivros"] == null)
                    this.CarregaDados();
                
                return (List<Livros>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                CarregaDados();
            }
        }
        private void PopularDropDownListCategoria()
        {




            BindingList<Categorias> tiposLivro = new BindingList<Categorias>(
        ioCategoriaDAO.BuscaCategorias().OrderBy(c => c.TIL_DS_DESCRICAO).ToList());


            ddlCategoria.DataSource = tiposLivro;
            ddlCategoria.DataTextField = "til_ds_descricao";
            ddlCategoria.DataValueField = "til_id_tipo_livro";
            ddlCategoria.DataBind();

           
            ddlCategoria.Items.Insert(0, new ListItem("Selecione...", ""));
        }
        private void PopularDropDownListEditor()
        {
            
            


            BindingList<Editores> editores = new BindingList<Editores>(
                ioEditoresDAO.BuscaEditores().OrderBy(e => e.EDI_NM_EDITOR).ToList());


            ddlEditor.DataSource = editores;
            ddlEditor.DataTextField = "edi_nm_editor"; 
            ddlEditor.DataValueField = "edi_id_editor"; 
            ddlEditor.DataBind();

           
            ddlEditor.Items.Insert(0, new ListItem("Selecione...", ""));
        }
        private void PopularDropDownListAutor()
        {




            BindingList<Autores> autores = new BindingList<Autores>(
                ioAutoresDAO.BuscaAutores().OrderBy(a => a.aut_nm_nome).ToList());



            ddlAutor.DataSource = autores;
            ddlAutor.DataTextField = "aut_nm_nome"; 
            ddlAutor.DataValueField = "aut_id_autor"; 
            ddlAutor.DataBind();

           
            ddlAutor.Items.Insert(0, new ListItem("Selecione...", ""));
        }
        private void CarregarDropDownList() { 
}
        private void CarregaDados()
        {
            try
            {


                if (AutorSessao != null) //AutorSelecionado != null)
                {
                    this.ListaLivros = this.ioLivroDAO.FindLivrosByAutor(AutorSessao).OrderBy(loLivroAutor => loLivroAutor.Liv_Nm_Titulo).ToList();
                    //Session.Remove("SessionAutorSelecionado");

                }
                else if (EditorSessao != null)
                {
                    this.ListaLivros = this.ioLivroDAO.FindLivrosByEditor(EditorSessao.EDI_ID_EDITOR).OrderBy(loLivroEditor => loLivroEditor.Liv_Nm_Titulo).ToList();
                    //Session.Remove("SessionEditorSelecionado");
                }
                else if (CategoriaSessao != null)
                {
                    this.ListaLivros = this.ioLivroDAO.FindLivrosByCategoria(CategoriaSessao.TIL_ID_TIPO_LIVRO).OrderBy(loLivroTipoLivro => loLivroTipoLivro.Liv_Nm_Titulo).ToList();
                    //Session.Remove("SessionTipoLivroSelecionado");
                }
                else
                {
                    this.ListaLivros = this.ioLivroDAO.BuscaLivros().OrderBy(loLivro => loLivro.Liv_Nm_Titulo).ToList();
                }

                //AutorSelecionado = AutorSessao;
                //EditorSelecionado = EditorSessao;
                //TipoLivroSelecionado = CategoriaSessao;


                this.gvGerenciamentoLivros.DataSource = this.ListaLivros/*.OrderBy(loLivro => loLivro.liv_nm_titulo)*/;
                this.gvGerenciamentoLivros.DataBind();

                PopularDropDownListAutor();
                PopularDropDownListEditor();
                PopularDropDownListCategoria();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Livros!');</script>");
            }
        }



        protected void BtnNovoLivro_Click(object sender, EventArgs e)
        {
            try
            {
                
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(l => l.Liv_Id_Livro).First().Liv_Id_Livro + 4;
                decimal liv_id_tipo_livro = Convert.ToDecimal(ddlCategoria.SelectedValue);
                decimal liv_id_editor = Convert.ToDecimal(ddlEditor.SelectedValue);
                decimal idAutorSelecionado = Convert.ToDecimal(ddlAutor.SelectedValue);
                decimal lsRoyaltyLivro = Convert.ToDecimal(this.txtCadastroRoyaltyLivro.Text);

                LivroAutorDAO livroAutorDAO = new LivroAutorDAO();
                LivroAutor novoLivroAutor = new LivroAutor(idAutorSelecionado, ldcIdLivro, lsRoyaltyLivro);
                livroAutorDAO.InsereLivroAutor(novoLivroAutor);

                string lsNomeLivro = this.tbxCadastroNomeLivro.Text;
                decimal lsPrecoLivro = Convert.ToDecimal(this.txtCadastroPrecoLivro.Text);
                string lsResumoLivro = this.txtCadastroResumoLivro.Text;
                int lsEdicaoLivro = Convert.ToInt32(this.txtCadastroEdicaoLivro.Text);
               
                Livros loLivro = new Livros(ldcIdLivro, liv_id_tipo_livro, liv_id_editor, lsNomeLivro, lsPrecoLivro, lsRoyaltyLivro, lsResumoLivro, lsEdicaoLivro);
               
                this.ioLivroDAO.InsertLivro(loLivro);
               
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Livro cadastrado com sucesso!');</script>");
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Message;
                HttpContext.Current.Response.Write($"<script>console.log('Erro no cadastro do Livro: {errorMessage}');</script>");
            }

            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"<script>alert('Erro no cadastro do Livro: {ex.Message}');</script>");
            }
          
            this.tbxCadastroNomeLivro.Text = String.Empty;
            this.txtCadastroPrecoLivro.Text = String.Empty;
            this.txtCadastroRoyaltyLivro.Text = String.Empty;
            this.txtCadastroResumoLivro.Text = String.Empty;
            this.txtCadastroEdicaoLivro.Text = String.Empty;
        }
        protected void gvGerenciamentoLivros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }
        protected void gvGerenciamentoLivros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = -1;
            this.CarregaDados();
        }
        protected void gvGerenciamentoLivros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            decimal ldcIdLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblEditIdLivro") as
           Label).Text);
            string lsNomeLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditNomeLivro") as TextBox).Text;
            decimal lsPrecoLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditPrecoLivro") as
           TextBox).Text);
            decimal lsRoyaltyLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditRoyaltyLivro") as TextBox).Text);
            string lsResumoLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditResumoLivro") as TextBox).Text;
            int lsEdicaoLivro = Convert.ToInt32((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditEdicaoLivro") as TextBox).Text);
            decimal idCategoria = this.ioCategoriaDAO.BuscaIdCategoriaByNome((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditCategoria") as DropDownList).SelectedValue);
            decimal idEditor = this.ioEditoresDAO.BuscaIdEditorByNome((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditEditor") as DropDownList).SelectedValue);
            decimal idAutor = this.ioAutoresDAO.BuscaIdAutorByNome((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditAutor") as DropDownList).SelectedValue);


            try
            {
               
                Livros loLivro = new Livros(ldcIdLivro, idCategoria, idEditor, lsNomeLivro, lsPrecoLivro, lsRoyaltyLivro, lsResumoLivro, lsEdicaoLivro);

                LivroAutor loLivroAutor = new LivroAutor(idAutor, ldcIdLivro, lsRoyaltyLivro);

                this.ioLivroDAO.AtualizaLivro(loLivro);

                this.ioLivroAutorDAO.AtualizaIdAutorPeloIdDoLivro(loLivroAutor);
               
                this.gvGerenciamentoLivros.EditIndex = -1;
               
                this.CarregaDados();

                HttpContext.Current.Response.Write("<script>alert('livro atualizado com sucesso!');</script>");
            }
            catch (SqlException sqlEx)
            {
                HttpContext.Current.Response.Write($"<script>alert('Erro no cadastro/atualização do Livro: {sqlEx.Message}');</script>");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write($"<script>alert('Erro no cadastro/atualização do Livro: {ex.Message}');</script>");
            }
        }
        protected void gvGerenciamentoLivros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoLivros.Rows[e.RowIndex];
                decimal ldcIdLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblIdLivro") as
               Label).Text);
                Livros loLivro = this.ioLivroDAO.BuscaLivros(ldcIdLivro).FirstOrDefault();
                if (loLivro != null)
                {
                    this.ioLivroDAO.RemoveLivro(loLivro);
                    this.CarregaDados();
                    HttpContext.Current.Response.Write(@"<script>alert('Livro removido com sucesso!.');</script>");
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do livro selecionado.');</script>");
            }
        }
        protected void gvGerenciamentoLivros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CarregaLivrosAutor")
            {     
                gvGerenciamentoLivros.DataSource = this.ListaLivros;
                gvGerenciamentoLivros.DataBind();
            }
        }
        protected string BuscarCategoriaDescricao(object idCategoria)
        {
            if (idCategoria != null)
            {
                decimal idCategoriaDecimal;
                if (decimal.TryParse(idCategoria.ToString(), out idCategoriaDecimal))
                {
                    
                    string descricaoCategoria = ioCategoriaDAO.BuscarDescricaoCategoria(idCategoriaDecimal);

                    return descricaoCategoria;
                }
            }

            return string.Empty;
        }
        protected string BuscarNomeEditor(object idEditor)
        {
            if (idEditor != null)
            {
                decimal idEditorDecimal;
                if (decimal.TryParse(idEditor.ToString(), out idEditorDecimal))
                {
                    EditoresDAO editoresDAO = new EditoresDAO();
                    string nomeEditor = editoresDAO.BuscarNomeEditor(idEditorDecimal);

                    return nomeEditor;
                }
            }
            return string.Empty;
        }
        protected void gvGerenciamentoLivros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddlEditCategoria = (DropDownList)e.Row.FindControl("ddlEditCategoria");
                DropDownList ddlEditEditor = (DropDownList)e.Row.FindControl("ddlEditEditor");
                DropDownList ddlEditAutor = (DropDownList)e.Row.FindControl("ddlEditAutor");

                Livros var = (Livros)e.Row.DataItem;

                ddlEditCategoria.DataSource = ddlCategoria.Items;
                ddlEditCategoria.SelectedValue = this.ioCategoriaDAO.BuscaCategorias(var.Liv_Id_Tipo_Livro).FirstOrDefault()?.TIL_DS_DESCRICAO ?? "";
                ddlEditCategoria.DataBind();

                ddlEditEditor.DataSource = ddlEditor.Items;
                ddlEditEditor.SelectedValue = this.ioEditoresDAO.BuscaEditores(var.Liv_Id_Editor).FirstOrDefault()?.EDI_NM_EDITOR ?? "";
                ddlEditEditor.DataBind();

                ddlEditAutor.DataSource = ddlAutor.Items;
                ddlEditAutor.SelectedValue = this.ioLivroAutorDAO.BuscaNomeAutorPeloIdDoLivroAssociado(var.Liv_Id_Livro).FirstOrDefault()?.aut_nm_nome ?? "";
                ddlEditAutor.DataBind();
            }
        }
        protected void gvGerenciamentoLivros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGerenciamentoLivros.PageIndex = e.NewPageIndex;
            CarregaDados(); 
        }
        public Autores AutorSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }
        public Editores EditorSessao
        {
            get { return (Editores)Session["SessionEditorSelecionado"]; }
            set { Session["SessionEditorSelecionado"] = value; }
        }
        public Categorias CategoriaSessao
        {
            get { return (Categorias)Session["sessionCategoriaSelecionado"]; }
            set { Session["sessionCategoriaSelecionado"] = value; }
        }


    }
}