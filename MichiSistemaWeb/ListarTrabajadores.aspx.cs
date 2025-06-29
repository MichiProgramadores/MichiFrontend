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
            //CargarDatos();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //trabajadorWS = new TrabajadorWSClient();
            CargarDatos();

        }
        protected void CargarDatos()
        {
            /*
            trabajadores = trabajadorWS.listaTrabajadoresActivos().ToList();
            dgvEmpleados.DataSource = trabajadores;
            dgvEmpleados.DataBind();
            */

            // Si ya está cargado en ViewState (evita llamar al WS de nuevo)
            if (ViewState["TrabajadoresFiltrados"] != null)
            {
                trabajadores = (List<trabajador>)ViewState["TrabajadoresFiltrados"];
            }
            else
            {
                // Si no, filtra y guarda en ViewState
                trabajadores = trabajadorWS.listaTrabajadoresActivos().ToList();
                ViewState["TrabajadoresFiltrados"] = trabajadores;
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
            trabajadorWS.eliminarTrabajador(idEmpleado);
            Response.Redirect("ListarTrabajadores.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idEmpleado = Int32.Parse(((LinkButton)sender).CommandArgument);
            trabajador trabajador = trabajadores.SingleOrDefault(x => x.persona_id == idEmpleado);
            Session["trabajadorSeleccionado"] = trabajador;
            Response.Redirect("RegistrarTrabajador.aspx?accion=ver");
        }

        //protected void lbBuscar_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        // Obtener el texto del textbox
        //        string textoId = txtNombreID.Text.Trim();

        //        if (int.TryParse(textoId, out int idTrabajador))
        //        {
        //            // Buscar cliente por ID usando tu capa de negocio o servicio

        //            var trabajador = trabajadorWS.obtenerTrabajador(idTrabajador);

        //            if (trabajador != null)
        //            {
        //                // Si encontró el cliente, lo pone en una lista para enlazar
        //                var lista = new List<trabajador> { trabajador };
        //                dgvEmpleados.DataSource = lista;
        //                dgvEmpleados.DataBind();
        //                //lblMensaje.Text = "";
        //            }
        //            else
        //            {
        //                // Si no encontró resultados
        //                dgvEmpleados.DataSource = null;
        //                dgvEmpleados.DataBind();
        //                // lblMensaje.Text = "No se encontró cliente con ese ID.";
        //            }
        //        }
        //        else
        //        {
        //            // Si no ingresó un número válido
        //            dgvEmpleados.DataSource = trabajadores;
        //            dgvEmpleados.DataBind();
        //            //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        dgvEmpleados.DataSource = null;
        //        dgvEmpleados.DataBind();
        //        // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
        //    }


        //}

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
                // lblMensaje.Text = "Error: " + ex.Message;
            }
            //    if (texto != null)
            //    {

            //        List<trabajador> trabajador = trabajadorWS.listaTrabajadoresPorNombre(texto).ToList();

            //        if (trabajador != null)
            //        {

            //            trabajadores = trabajador;
            //            dgvEmpleados.DataSource =trabajador;
            //            dgvEmpleados.DataBind();

            //            //lblMensaje.Text = "";
            //        }
            //        else
            //        {
            //            // Si no encontró resultados
            //            dgvEmpleados.DataSource = null;
            //            dgvEmpleados.DataBind();
            //            // lblMensaje.Text = "No se encontró cliente con ese ID.";
            //        }
            //    }
            //    else
            //    {
            //        // Si no ingresó un número válido
            //        dgvEmpleados.DataSource = trabajadores;
            //        dgvEmpleados.DataBind();
            //        //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
            //}
        }

    }
}