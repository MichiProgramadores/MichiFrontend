using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarTrabajadores : System.Web.UI.Page
    {

        protected TrabajadorWSClient trabajadorWS;
        protected List<trabajador> trabajadores;
        protected void Page_Init(object sender, EventArgs e)
        {
            trabajadorWS = new TrabajadorWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDatos();

        }
        protected void CargarDatos()
        {

            // Si ya está cargado en ViewState (evita llamar al WS de nuevo)
            if (ViewState["TrabajadoresFiltrados"] != null)
            {
                trabajadores = (List<trabajador>)ViewState["TrabajadoresFiltrados"];
            }
            else
            {
                // Si no, filtra y guarda en ViewState
                try { 
                trabajadores = trabajadorWS.listaTrabajadoresActivos().ToList();
                    ViewState["TrabajadoresFiltrados"] = trabajadores;
                }
                  catch (Exception ex){
                 
                }
             }

            dgvEmpleados.DataSource = trabajadores;
            dgvEmpleados.DataBind();

        }

        protected void ListarTodos_Click(object sender, EventArgs e)
        {
            trabajadores = trabajadorWS.listarTrabajadores().ToList();
            ViewState["TrabajadoresFiltrados"] = trabajadores; // Guarda en ViewState
            dgvEmpleados.DataSource = trabajadores;
            dgvEmpleados.DataBind();
            txtNombreID.Text = "";
            txtNombre.Text = "";
            DdlTipoTrabajador.SelectedIndex = 0;
        }

        protected void dgvEmpleados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "persona_id").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Nombres").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "Apellidos").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "Celular").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "Email").ToString();
                bool estado = (bool)DataBinder.Eval(e.Row.DataItem, "Estado");
                if (estado)
                {
                    e.Row.Cells[5].Text = "Activo";
                }
                else
                {
                    e.Row.Cells[5].Text = "Inactivo";
                }
            }
        }

        protected void dgvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmpleados.PageIndex = e.NewPageIndex;
            dgvEmpleados.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarTrabajador.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            trabajador trabajador = trabajadores.SingleOrDefault(x => x.persona_id == idEmpleado);
            Session["trabajadorSeleccionado"] = trabajador;
            Response.Redirect("RegistrarTrabajador.aspx?accion=modificar");
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idEmpleado = int.Parse(hfIdEliminar.Value);
            trabajador trabajador = (trabajador)Session["trabajador"];
            int idSesion = trabajador.persona_id;

            if (idEmpleado == idSesion)
            {
                lanzarMensajedeError("ERROR: No se puede eliminar a usted mismo");
            }
            else
            {
                trabajadorWS.eliminarTrabajador(idEmpleado);
                Response.Redirect("ListarTrabajadores.aspx");
            }
        }

        public void lanzarMensajedeError(String mensaje)
        {
            lblMensajeError.Text = mensaje;
            string script = "mostrarModalError();";
            ScriptManager.RegisterStartupScript(this, GetType(), "modalError", script, true);
        }
        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            trabajador trabajador = trabajadores.SingleOrDefault(x => x.persona_id == idEmpleado);
            Session["trabajadorSeleccionado"] = trabajador;
            Response.Redirect("RegistrarTrabajador.aspx?accion=ver");
        }
        protected void lbBuscarN_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombreID.Text.Trim();
                string textoNombre = txtNombre.Text.Trim();
                string tipoSeleccionado = DdlTipoTrabajador.SelectedValue;
                // 2. Obtener TODOS los productos (sin filtros iniciales)
                List<trabajador> listaCompleta = trabajadorWS.listarTrabajadores().ToList();
                List<trabajador> resultados = listaCompleta;

                // 3. Aplicar filtros SOLO si el campo tiene valor

                if(!string.IsNullOrEmpty(textoNombre) && !string.IsNullOrEmpty(textoId) 
                    && tipoSeleccionado != "0" && !string.IsNullOrEmpty(tipoSeleccionado))
                {
                    resultados = resultados
                        .Where(c => c.nombres.ToLower().Contains(textoNombre.ToLower()) &&
                            c.persona_id.ToString().Contains(textoId) &&
                            c.tipoTrabajador.ToString() == tipoSeleccionado)
                        .ToList();
                        
                }
                else
                {
                    if (!string.IsNullOrEmpty(textoNombre) && string.IsNullOrEmpty(textoId)
                    && (tipoSeleccionado == "0" || string.IsNullOrEmpty(tipoSeleccionado)))
                    {
                        resultados = resultados
                            .Where(c => c.nombres.ToLower().Contains(textoNombre.ToLower()))
                            .ToList();
                    }

                    if (!string.IsNullOrEmpty(textoId) )
                    {
                        resultados = resultados
                            .Where(c => c.persona_id.ToString().Equals(textoId))
                            .ToList();
                    }
                    if (tipoSeleccionado != "0" && !string.IsNullOrEmpty(tipoSeleccionado)
                            && string.IsNullOrEmpty(textoId) && !string.IsNullOrEmpty(textoNombre))
                    {
                        resultados = resultados
                            .Where(c => c.tipoTrabajador.ToString() == tipoSeleccionado &&
                                c.nombres.ToLower().Contains(textoNombre.ToLower()))
                            .ToList();
                    }
                    if (tipoSeleccionado != "0" && !string.IsNullOrEmpty(tipoSeleccionado)
                         && !string.IsNullOrEmpty(textoId) && string.IsNullOrEmpty(textoNombre))
                    {
                        resultados = resultados
                            .Where(c => c.tipoTrabajador.ToString() == tipoSeleccionado &&
                                c.persona_id.ToString().Contains(textoId))
                            .ToList();
                    }
                    if (tipoSeleccionado != "0" && !string.IsNullOrEmpty(tipoSeleccionado) 
                        && string.IsNullOrEmpty(textoId) && string.IsNullOrEmpty(textoNombre))
                    {
                        resultados = resultados
                            .Where(c => c.tipoTrabajador.ToString() == tipoSeleccionado)
                            .ToList();
                    }
                }

                if (textoNombre == "" && textoId == "" && tipoSeleccionado == "0")
                {
                    resultados = trabajadorWS.listarTrabajadores().ToList();
                }

                // 4. Mostrar resultados
                dgvEmpleados.DataSource = resultados;
                dgvEmpleados.DataBind();

                // 5. Opcional: Guardar en ViewState
                ViewState["TrabajadoresFiltrados"] = resultados;
            }
            catch (Exception ex)
            {
                dgvEmpleados.DataSource = null;
                dgvEmpleados.DataBind();
            }
        }
    }
}