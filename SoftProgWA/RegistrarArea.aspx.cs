using SoftProgBusiness.RRHH;
using SoftProgDomain.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftProgWA
{
    public partial class RegistrarArea : System.Web.UI.Page
    {
        private AreaBO boArea;
        private Estado estado;
        private Area area;

        protected void Page_Load(object sender, EventArgs e)
        {
            string accion = Request.QueryString["accion"];
            if (accion == null)
            {
                estado = Estado.Nuevo;
                area = new Area();
                lblTitulo.Text = "Registrar Area";
            }
            else if (accion == "modificar")
            {
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar Area";
                area = (Area)Session["areaSeleccionada"];
                if (!IsPostBack)
                {
                    txtIDArea.Text = area.IdArea.ToString();
                    txtNombre.Text = area.Nombre;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            boArea = new AreaBO();
            area.Nombre = txtNombre.Text;
            try
            {
                if (estado == Estado.Nuevo)
                {
                    boArea.insertar(area);
                }
                else if (estado == Estado.Modificar)
                {
                    boArea.modificar(area);
                }
            }catch(Exception ex)
            {
                lblMensajeError.Text = ex.Message;
                string script =
                    "mostrarModalError();";
                ScriptManager.RegisterStartupScript
                    (this, GetType(), "modalError", script, true);
                return;
            }
            Response.Redirect("ListarAreas.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarAreas.aspx");
        }
    }
}