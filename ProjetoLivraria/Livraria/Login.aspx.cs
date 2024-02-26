using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("Principal.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
           
            if (txtUsername.Text.Trim().ToLower() == "pbarreiro" && txtPassword.Text.Trim() == "teste123")
            {
                Response.Redirect("Principal.aspx");
            }
            else
            {
               
                lblErrorMessage.Text = "Credenciais inválidas. Tente novamente.";
            }
        }
    }
}