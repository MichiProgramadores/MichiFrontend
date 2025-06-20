﻿using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            CargarDatos();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            trabajadorWS = new TrabajadorWSClient();
            CargarDatos();
            */
        }
        protected void CargarDatos()
        {
            trabajadores = trabajadorWS.listaTrabajadoresActivos().ToList();
            dgvEmpleados.DataSource = trabajadores;
            dgvEmpleados.DataBind();
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
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
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

        protected void lbBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombreID.Text.Trim();

                if (int.TryParse(textoId, out int idTrabajador))
                {
                    // Buscar cliente por ID usando tu capa de negocio o servicio

                    var trabajador = trabajadorWS.obtenerTrabajador(idTrabajador);

                    if (trabajador != null)
                    {
                        // Si encontró el cliente, lo pone en una lista para enlazar
                        var lista = new List<trabajador> { trabajador };
                        dgvEmpleados.DataSource = lista;
                        dgvEmpleados.DataBind();
                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvEmpleados.DataSource = null;
                        dgvEmpleados.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvEmpleados.DataSource = trabajadores;
                    dgvEmpleados.DataBind();
                    //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }

        }

        protected void lbBuscarN_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string texto = txtNombre.Text.Trim();

                if (texto != null)
                {

                    List<trabajador> trabajador = trabajadorWS.listaTrabajadoresPorNombre(texto).ToList();

                    if (trabajador != null)
                    {

                        trabajadores = trabajador;
                        dgvEmpleados.DataSource =trabajador;
                        dgvEmpleados.DataBind();

                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvEmpleados.DataSource = null;
                        dgvEmpleados.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvEmpleados.DataSource = trabajadores;
                    dgvEmpleados.DataBind();
                    //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }
        }

        protected void ListarTodos_Click(object sender, EventArgs e)
        {
            trabajadores = trabajadorWS.listarTrabajadores().ToList();
            dgvEmpleados.DataSource = trabajadores;
            dgvEmpleados.DataBind();

        }
    }
}