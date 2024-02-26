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
    public partial class GerenciamentoEditores : System.Web.UI.Page
    {
        EditoresDAO ioEditoresDAO = new EditoresDAO();

        public List<Editores> ListaEditores
        {
            get
            {
                if ((List<Editores>)ViewState["ViewStateListaEditores"] == null)
                    this.CarregaDados();
                return (List<Editores>)ViewState["ViewStateListaEditores"];
            }
            set
            {
                ViewState["ViewStateListaEditores"] = value;
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
                this.ListaEditores = this.ioEditoresDAO.BuscaEditores().OrderBy(loEditor => loEditor.EDI_NM_EDITOR).ToList();
                this.gvGerenciamentoEditores.DataSource = this.ListaEditores;
                this.gvGerenciamentoEditores.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Editores.');</script>");
            }
        }

        protected void BtnNovoEditor_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdEditor = this.ListaEditores.OrderByDescending(a => a.EDI_ID_EDITOR).First().EDI_ID_EDITOR + 1;
                string lsNomeEditor = this.tbxCadastroNomeEditor.Text;
                string lsEmailEditor = this.tbxCadastroEmailEditor.Text;
                string lsUrlEditor = this.txbCadastroUrlEditor.Text;

                Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);

                this.ioEditoresDAO.InsertEditor(loEditor);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Editor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Editor.');</script>");
            }

            this.tbxCadastroNomeEditor.Text = String.Empty;
            this.tbxCadastroEmailEditor.Text = String.Empty;
            this.txbCadastroUrlEditor.Text = String.Empty;
        }

        protected void gvGerenciamentoEditores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoEditores.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }

        protected void gvGerenciamentoEditores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoEditores.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoEditores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("lblEditIdEditor") as Label).Text);
            string lsNomeEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditNomeEditor") as TextBox).Text;
            string lsEmailEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditEmailEditor") as TextBox).Text;
            string lsUrlEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditUrlEditor") as TextBox).Text;

            if (String.IsNullOrWhiteSpace(lsNomeEditor))
                HttpContext.Current.Response.Write("<script>alert('Digite o nome do editor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsEmailEditor))
                HttpContext.Current.Response.Write("<script>alert('Digite o email do editor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsUrlEditor))
                HttpContext.Current.Response.Write("<script>alert('Digite a URL do editor.');</script>");
            else
            {
                try
                {
                    Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);

                    this.ioEditoresDAO.AtualizaEditor(loEditor);

                    this.gvGerenciamentoEditores.EditIndex = -1;

                    this.CarregaDados();

                    HttpContext.Current.Response.Write("<script>alert('Editor atualizado com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do cadastro do editor.');</script>");
                }
            }
        }

        protected void gvGerenciamentoEditores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoEditores.Rows[e.RowIndex];
                decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("lblIdEditor") as Label).Text);
                Editores loEditor = this.ioEditoresDAO.BuscaEditores(ldcIdEditor).FirstOrDefault();

                if (loEditor != null)
                {
                    LivrosDAO loLivrosDAO = new LivrosDAO();

                    if (EditorPossuiLivroAssociado(loEditor.EDI_ID_EDITOR))
                    {
                        HttpContext.Current.Response.Write(@"<script>alert('Não é possível remover o editor selecionado pois existem livros associados a ele.');</script>");
                    }
                    else
                    {
                        this.ioEditoresDAO.RemoveEditor(loEditor);
                        HttpContext.Current.Response.Write(@"<script>alert('Editor removido com sucesso.');</script>");
                        this.CarregaDados();
                    }
                }
            }
            catch(Exception ex)
            {
                HttpContext.Current.Response.Write($"<script>alert('Erro na remoção do editor selecionado. Detalhes: {ex.Message}');</script>");
            }
        }

        private bool EditorPossuiLivroAssociado(decimal idEditor)
        {
            return this.ioEditoresDAO.LivrosAssociadosAoEditor(idEditor);
        }

        protected void gvGerenciamentoEditores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "CarregaLivrosEditor":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblIdEditor") as Label).Text);
                    string lsNomeEditor = (this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblNomeEditor") as Label).Text;

                    Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, "", "");
                    this.EditorSessao = loEditor;

                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;
                default:
                    break;
            }
        }

        public Editores EditorSessao
        {
            get
            {
                if (Session["EditorSessao"] == null)
                    return null;
                return (Editores)Session["EditorSessao"];
            }
            set
            {
                Session["EditorSessao"] = value;
            }
        }

        protected void gvGerenciamentoEditores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvGerenciamentoEditores.PageIndex = e.NewPageIndex;
            this.CarregaDados();
        }
    }
}
