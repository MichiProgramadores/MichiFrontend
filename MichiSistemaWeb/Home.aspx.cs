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
        protected OrdenWSClient ordenWS;

        protected void Page_Init(object sender, EventArgs e)
        {
            trabajadorWS = new TrabajadorWSClient();
            comprobanteWS = new ComprobanteWSClient();
            ordenWS=new OrdenWSClient();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                // Fecha máxima = hoy
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                // Fecha mínima = 1 de enero de 2020
                string minDate = new DateTime(2020, 1, 1).ToString("yyyy-MM-dd");
                // Asignar las fechas mínima y máxima a los controles de fecha de renta
                txtFechaRentaIni.Attributes["min"] = minDate;
                txtFechaRentaIni.Attributes["max"] = today;

                txtFechaRentaFin.Attributes["min"] = minDate;
                txtFechaRentaFin.Attributes["max"] = today;

                //Asignamos las fechas mínima y máxima a los controles de fecha de facturación

                txtFechaFactInicio.Attributes["min"] = minDate;
                txtFechaFactInicio.Attributes["max"] = today;

                txtFechaFactFin.Attributes["min"] = minDate;
                txtFechaFactFin.Attributes["max"] = today;

                // También actualizamos el LiteralMaxDate para mostrar la fecha actual en la interfaz
                LiteralMaxDate.Text = today;

                // Llamar a la función para cargar los trabajadores
                CargarTrabajadores();

            }

        }

        private void CargarTrabajadores()
        {

            string tipo_seleccionado = Convert.ToString(ddlTipoTrabajador.SelectedValue);
            string estado_seleccionado = Convert.ToString(ddlEstadoTrabajador.SelectedValue);

            if (tipo_seleccionado == "GENERAL")
            {
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listaTrabajadoresPorEstado(estado_seleccionado);
                dgvTrabajadoresEleccion.DataBind();
            }
            else
            {
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listaTrabajadoresPorTipoEstado(tipo_seleccionado, estado_seleccionado);
                dgvTrabajadoresEleccion.DataBind();

            }
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

            string estadoTrabajador = ddlEstadoTrabajador.SelectedValue;

            Byte[] FileBuffer = trabajadorWS.reporteTrabajadores(tipoTrabajador, idTrabajador, estadoTrabajador);

            string idTrabajadorTexto = (idTrabajador == 0) ? "TODOS" : idTrabajador.ToString();  // Si el ID es 0, poner "TODOS"
            string nombreArchivo = $"Reporte_Trabajadores_{idTrabajadorTexto}_{tipoTrabajador}_{estadoTrabajador}.pdf";

            if (FileBuffer == null || FileBuffer.Length == 0)
            {
                
                Response.Write("<script>alert('El reporte está vacío. No hay datos para mostrar.');</script>");

            }
            else
            {
                // Si el archivo tiene contenido, proceder con la descarga
                Response.Clear();  // Limpia la respuesta actual
                Response.ContentType = "application/pdf";  // Establece el tipo de contenido
                Response.AddHeader("content-length", FileBuffer.Length.ToString());  // Agrega el encabezado content-length
                Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}");  // Define el nombre del archivo PDF
                Response.BinaryWrite(FileBuffer);  // Escribe los bytes del PDF en la respuesta
                Response.End();
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
            string estado_seleccionado = Convert.ToString(ddlEstadoTrabajador.SelectedValue);


            if (tipo_seleccionado == "GENERAL")
            {
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listaTrabajadoresPorEstado(estado_seleccionado);
                dgvTrabajadoresEleccion.DataBind();
            }
            else
            {
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listaTrabajadoresPorTipoEstado(tipo_seleccionado, estado_seleccionado);
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
        protected void BotonReporteFacturacion_Click(object sender, EventArgs e)
        {
            try
            {
                string fechaInicioStr = txtFechaFactInicio.Text;
                string fechaFinStr = txtFechaFactFin.Text;

                DateTime fechaInicio;
                if (string.IsNullOrEmpty(fechaInicioStr))
                {
                    int currentYear = DateTime.Now.Year;
                    fechaInicio = new DateTime(currentYear, DateTime.Now.Month, 1);
                }
                else
                {
                    fechaInicio = DateTime.Parse(fechaInicioStr);
                }

                DateTime fechaFin;
                if (string.IsNullOrEmpty(fechaFinStr))
                {
                    fechaFin = DateTime.Now;
                }
                else
                {
                    fechaFin = DateTime.Parse(fechaFinStr);
                }

                Byte[] fileBuffer = comprobanteWS.reporteFacturacion(fechaInicio, fechaFin);

                if (fileBuffer == null || fileBuffer.Length == 0)
                {
                    // Si el archivo está vacío, muestra un mensaje al usuario
                    Response.Write("<script>alert('El reporte está vacío. No hay datos para mostrar.');</script>");
                }
                else
                {

                    string nombreArchivo = $"Reporte_Facturacion_{fechaInicio.ToString("yyyy-MM-dd")}_a_{fechaFin.ToString("yyyy-MM-dd")}.pdf";
                    // Si el archivo tiene contenido, proceder con la descarga
                    Response.Clear();  // Limpia la respuesta actual
                    Response.ContentType = "application/pdf";  // Establece el tipo de contenido
                    Response.AddHeader("content-length", fileBuffer.Length.ToString());  // Agrega el encabezado content-length
                    Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}");  // Define el nombre del archivo PDF
                    Response.BinaryWrite(fileBuffer);  // Escribe los bytes del PDF en la respuesta
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void BotonCerrar_Click(object sender, EventArgs e)
        {
            txtFechaFactInicio.Text = string.Empty;
            txtFechaFactFin.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                "$('.modal-backdrop').remove();" +
            "$('#reporteFacturacionModal').modal('hide');", true);
           
        }

        protected void BotonCerrarRenta_Click(object sender, EventArgs e)
        {
            txtFechaRentaIni.Text = string.Empty;
            txtFechaRentaFin.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                "$('.modal-backdrop').remove();" +
            "$('#reporteRentasModal').modal('hide');", true);
        }

        protected void BotonReporteRenta_Click(object sender, EventArgs e)
        {
            try
            {
                string fechaInicioStr = txtFechaRentaIni.Text;
                string fechaFinStr = txtFechaRentaFin.Text;

                DateTime fechaInicio;
                if (string.IsNullOrEmpty(fechaInicioStr))
                {
                    int currentYear = DateTime.Now.Year;
                    fechaInicio = new DateTime(currentYear, DateTime.Now.Month, 1);
                }
                else
                {
                    fechaInicio = DateTime.Parse(fechaInicioStr);
                }

                DateTime fechaFin;
                if (string.IsNullOrEmpty(fechaFinStr))
                {
                    fechaFin = DateTime.Now;
                }
                else
                {
                    fechaFin = DateTime.Parse(fechaFinStr);
                }

                Byte[] fileBuffer = ordenWS.reporteRenta(fechaInicio, fechaFin);

                if (fileBuffer == null || fileBuffer.Length == 0)
                {
                    // Si el archivo está vacío, muestra un mensaje al usuario
                    Response.Write("<script>alert('El reporte está vacío. No hay datos para mostrar.');</script>");
                }
                else
                {

                    string nombreArchivo = $"Reporte_Renta_{fechaInicio.ToString("yyyy-MM-dd")}_a_{fechaFin.ToString("yyyy-MM-dd")}.pdf";
                    Response.Clear();  // Limpia la respuesta actual
                    Response.ContentType = "application/pdf";  // Establece el tipo de contenido
                    Response.AddHeader("content-length", fileBuffer.Length.ToString());  // Agrega el encabezado content-length
                    Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}");  // Define el nombre del archivo PDF
                    Response.BinaryWrite(fileBuffer);  // Escribe los bytes del PDF en la respuesta
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void ddlEstadoTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo_seleccionado = Convert.ToString(ddlTipoTrabajador.SelectedValue);
            string estado_seleccionado = Convert.ToString(ddlEstadoTrabajador.SelectedValue);

            if (tipo_seleccionado == "GENERAL")
            {
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listaTrabajadoresPorEstado(estado_seleccionado);
                dgvTrabajadoresEleccion.DataBind();
            }
            else
            {
                dgvTrabajadoresEleccion.DataSource = trabajadorWS.listaTrabajadoresPorTipoEstado(tipo_seleccionado, estado_seleccionado);
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

        protected void BotonErrorModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalAndShowNext",
        "$('#tipoTrabajadorModal').modal('hide');" + // Cierra el primer modal
        "$('#errorModal').modal('hide');" + // Cierra el segundo modal si está abierto
        "$('.modal-backdrop').remove();" + // Elimina cualquier fondo residual de modal
        "$('#tipoTrabajadorModal').modal('show');", // Abre el modal de tipo trabajador
        true);
        }
    }
}