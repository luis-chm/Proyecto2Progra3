using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto2Progra3
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        // Dentro de Menu.Master.cs
        protected void Page_Load(object sender, EventArgs e)
        {
            lblFechaHora.Text = DateTime.Now.ToString("f"); // Asegúrate de tener un Label con este ID.
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                lblUsuario.Text = HttpContext.Current.User.Identity.Name; // Y uno para el usuario.
            }
        }

    }
}