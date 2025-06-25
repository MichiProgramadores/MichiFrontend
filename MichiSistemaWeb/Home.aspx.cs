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
        protected ComprobanteWSClient comprobanteWS;

        protected void Page_Init(object sender, EventArgs e)
        {
            trabajadorWS = new TrabajadorWSClient();
            comprobanteWS = new ComprobanteWSClient();

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
            IDREPORTE.Text = string.Empty;  
            NOMBREREPORTE.Text = string.Empty;
        }

        protected void btnSeleccionarTrabajador_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idTrabajador = int.Parse(btn.CommandArgument);

            trabajador trabajador = trabajadorWS.obtenerTrabajador(idTrabajador);

            IDREPORTE.Text = idTrabajador.ToString();
            NOMBREREPORTE.Text = $"{trabajador.nombres} {trabajador.apellidos}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
            "$('#modalTrabajadores').modal('hide');" +  
            "$('#tipoTrabajadorModal').modal('hide');" +  
            "$('.modal-backdrop').remove();" +  
            "$('#tipoTrabajadorModal').modal('show');",  
            true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            int idTrabajador = string.IsNullOrEmpty(IDREPORTE.Text) ? 0 : int.Parse(IDREPORTE.Text);  

            string tipoTrabajador = ddlTipoTrabajador.SelectedValue;

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
            IDREPORTE.Text = string.Empty; 
            NOMBREREPORTE.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
            "$('#tipoTrabajadorModal').modal('hide');" + 
            "$('#modalTrabajadores').modal('hide');" + 
            "$('.modal-backdrop').remove();" + 
            "$('#tipoTrabajadorModal').modal('show');",
            true);
        }

        protected void ddlTipoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo_seleccionado = Convert.ToString(ddlTipoTrabajador.SelectedValue);

            if (tipo_seleccionado == "0")
            {
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listarTrabajadores();
                dgvTrabajadoresEleccion.DataBind();
            }
            else
            {
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listarTrabajadoresPorTipo(tipo_seleccionado);
                dgvTrabajadoresEleccion.DataBind();

            }
            IDREPORTE.Text = string.Empty;  
            NOMBREREPORTE.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
            "$('#tipoTrabajadorModal').modal('hide');" + 
            "$('#modalTrabajadores').modal('hide');" + 
            "$('.modal-backdrop').remove();" + 
            "$('#tipoTrabajadorModal').modal('show');", 
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

        /*protected void BotonLimpiarCampos_Click(object sender, EventArgs e)
        {
            txtFechaInicio.Text = string.Empty;
            txtFechaFin.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
            //"$('#reporteFacturacionModal').modal('hide');" +
            "$('.modal-backdrop').remove();" +
            "$('#reporteFacturacionModal').modal('show');",
            true);
        }*/

        protected void BotonReporteFacturacion_Click(object sender, EventArgs e)
        {
            
        }

        protected void BotonCerrar_Click(object sender, EventArgs e)
        {
            txtFechaInicio.Text = string.Empty;
            txtFechaFin.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                "$('.modal-backdrop').remove();" +
            "$('#reporteFacturacionModal').modal('hide');", true);
           
        }
    }
}