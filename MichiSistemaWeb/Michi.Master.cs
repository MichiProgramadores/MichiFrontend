using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class Michi : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["trabajador"] != null)
            {
                trabajador  trabajador= (trabajador)Session["trabajador"];
                lblNombreCompleto.Text = trabajador.nombres;  // Asignamos el nombre del usuario
            }
        }
    }
}