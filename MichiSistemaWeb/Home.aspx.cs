using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MichiSistemaWeb.MichiBackend;

namespace MichiSistemaWeb
{
    public partial class Home : System.Web.UI.Page
    {

        protected TrabajadorWSClient trabajadorWS;

        protected void Page_Init(object sender, EventArgs e)
        {
            trabajadorWS = new TrabajadorWSClient();
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                CargarTrabajadores();

            }

        }

        private void CargarTrabajadores()
        {

            string tipo_seleccionado = Convert.ToString(ddlTipoTrabajador.SelectedValue);
            dgvTrabajadoresEleccion.DataSource = trabajadorWS.listarTrabajadoresPorTipo(tipo_seleccionado);
            dgvTrabajadoresEleccion.DataBind();
        }

        protected void LimpiarCampos()
        {
            IDREPORTE.Text = string.Empty;  // Vaciar el campo ID
            NOMBREREPORTE.Text = string.Empty;  // Vaciar el campo Nombre
        }

        protected void btnSeleccionarTrabajador_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idTrabajador = int.Parse(btn.CommandArgument);

            // Obtener los detalles del trabajador desde el servicio web
            trabajador trabajador = trabajadorWS.obtenerTrabajador(idTrabajador);

            // Asignar los valores al modal de tipo de trabajador
            IDREPORTE.Text = idTrabajador.ToString();
            NOMBREREPORTE.Text = $"{trabajador.nombres} {trabajador.apellidos}";

            // Utilizar JavaScript para cerrar solo el modal 2 y abrir el modal 1
            /*ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
            "$('#modalTrabajadores').modal('hide'); $('#tipoTrabajadorModal').modal('show');", true);*/
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
    "$('#modalTrabajadores').modal('hide');" +  // Cierra el primer modal
    "$('#tipoTrabajadorModal').modal('hide');" +  // Cierra el segundo modal si está abierto
    "$('.modal-backdrop').remove();" +  // Elimina cualquier fondo de modal residual
    "$('#tipoTrabajadorModal').modal('show');",   // Abre el modal de tipo trabajador
    true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // Obtener el valor del tipo de trabajador desde el DropDownList
            int idTrabajador = string.IsNullOrEmpty(IDREPORTE.Text) ? 0 : int.Parse(IDREPORTE.Text);  // Si el ID está vacío, se pasa como 0

            // Obtener el valor del tipo de trabajador desde el DropDownList
            string tipoTrabajador = ddlTipoTrabajador.SelectedValue;

            // Llamar al WebMethod y pasar los parámetros
            Byte[] FileBuffer = trabajadorWS.reporteTrabajadores(tipoTrabajador, idTrabajador);

            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }

        protected void LinkButtonLimpiar_Click(object sender, EventArgs e)
        {
            IDREPORTE.Text = string.Empty;  // Vaciar el campo ID
            NOMBREREPORTE.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
        "$('#tipoTrabajadorModal').modal('hide');" + // Cierra el primer modal
        "$('#modalTrabajadores').modal('hide');" + // Cierra el segundo modal si está abierto
        "$('.modal-backdrop').remove();" + // Elimina cualquier fondo residual de modal
        "$('#tipoTrabajadorModal').modal('show');", // Abre el modal de tipo trabajador
        true);
        }

        protected void ddlTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo_seleccionado = Convert.ToString(ddlTipoTrabajador.SelectedValue);

            // Verificamos si el valor seleccionado es el valor predeterminado "0" (Select type)
            if (tipo_seleccionado == "0")
            {
                // Si no se ha seleccionado un valor válido, mostrar todos los productos
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listarTrabajadores();
                dgvTrabajadoresEleccion.DataBind();
            }
            else
            {
                // Si se ha seleccionado un tipo válido, mostrar productos por tipo
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listarTrabajadoresPorTipo(tipo_seleccionado);
                dgvTrabajadoresEleccion.DataBind();

            }
            IDREPORTE.Text = string.Empty;  // Vaciar el campo ID
            NOMBREREPORTE.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
        "$('#tipoTrabajadorModal').modal('hide');" + // Cierra el primer modal
        "$('#modalTrabajadores').modal('hide');" + // Cierra el segundo modal si está abierto
        "$('.modal-backdrop').remove();" + // Elimina cualquier fondo residual de modal
        "$('#tipoTrabajadorModal').modal('show');", // Abre el modal de tipo trabajador
        true);
        }

        protected void LinkButtonCerrarModal2_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
        "$('#tipoTrabajadorModal').modal('hide');" + // Cierra el primer modal
        "$('#modalTrabajadores').modal('hide');" + // Cierra el segundo modal si está abierto
        "$('.modal-backdrop').remove();" + // Elimina cualquier fondo residual de modal
        "$('#tipoTrabajadorModal').modal('show');", // Abre el modal de tipo trabajador
        true);
        }
    }
}