using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using ProjetoLivraria.Models;

namespace ProjetoLivraria
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
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

        protected void AnularSessoes()
        {
            AutorSessao = null;
            EditorSessao = null;
            CategoriaSessao = null;
        }

        protected void autorRedirect_ServerClick(object sender, EventArgs e)
        {
            AnularSessoes();
            Response.Redirect("~/Livraria/GerenciamentoAutores");
        }
        protected void editorRedirect_ServerClick(object sender, EventArgs e)
        {
            AnularSessoes();
            Response.Redirect("~/Livraria/GerenciamentoEditores");
        }
        protected void categoriaRedirect_ServerClick(object sender, EventArgs e)
        {
            AnularSessoes();
            Response.Redirect("~/Livraria/GerenciamentoCategorias");
        }
        protected void livroRedirect_ServerClick(object sender, EventArgs e)
        {
            AnularSessoes();
            Response.Redirect("~/Livraria/GerenciamentoLivros");
        }
    }

}