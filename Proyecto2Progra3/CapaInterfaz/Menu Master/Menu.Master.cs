using Proyecto2Progra3.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto2Progra3
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["nombreEmpleado"] != null)
                {
                    lblUsuario.Text = Session["nombreEmpleado"].ToString();
                }


                if (Session["imagenEmpleado"] != null)
                {

                    imgEmpleado.Src = Session["imagenEmpleado"].ToString();
                }

                DateTime fechaHoraActual = DateTime.Now;
                lblFechaHora.Text = fechaHoraActual.ToString();
            }
        }

    }
}