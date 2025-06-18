using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
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
        protected void CerrarSesion_Click(object sender, EventArgs e)
        {
            // Cerrar la sesión del usuario
            Session.Abandon(); // Elimina la sesión activa

            // También puedes limpiar los datos de autenticación si es necesario
            FormsAuthentication.SignOut();

            // Redirigir al usuario a la página de inicio de sesión
            Response.Redirect("~/Login.aspx");
        }


    }
}