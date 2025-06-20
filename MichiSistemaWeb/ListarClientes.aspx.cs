﻿using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarClientes : System.Web.UI.Page
    {
        protected ClienteWSClient clienteWS;
        protected List<cliente> clientes;
        protected void Page_Init(object sender, EventArgs e)
        {
            clienteWS = new ClienteWSClient();
            CargarDatos();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //clienteWS = new ClienteWSClient();
            //CargarDatos();
        }
        protected void CargarDatos()
        {
            clientes = clienteWS.listarClientesActivos().ToList();
            dgvClientes.DataSource = clientes;
            dgvClientes.DataBind();
        }
        protected void dgvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "persona_id").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Nombres").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "Apellidos").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "Celular").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "Email").ToString();
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "Puntuacion").ToString();
                object estadoObj = DataBinder.Eval(e.Row.DataItem, "Estado");

                bool estado = false;
                if (estadoObj != null && bool.TryParse(estadoObj.ToString(), out bool result))
                {
                    estado = result;
                }

                // Mostrar texto según el estado
                e.Row.Cells[6].Text = estado ? "Activo" : "Inactivo";

                //e.Row.Cells[6].Text = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
            }
        }

        protected void dgvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClientes.PageIndex = e.NewPageIndex;
            dgvClientes.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarCliente.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idCliente = Int32.Parse(((LinkButton)sender).CommandArgument);
           
            cliente cliente= clientes.SingleOrDefault(x => x.persona_id == idCliente);
      
            if (cliente != null)
            {
                // Almacenar el cliente seleccionado en la sesión
                Session["clienteSeleccionado"] = cliente;

                // Redirigir a la página de edición
                Response.Redirect("RegistrarCliente.aspx?accion=modificar");
            }
          
        }

        /*
        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            int idCliente = Int32.Parse(((LinkButton)sender).CommandArgument);
            clienteWS.eliminarCliente(idCliente);
            Response.Redirect("ListarClientes.aspx");
        }
        */

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idCliente = int.Parse(hfIdEliminar.Value);
            clienteWS.eliminarCliente(idCliente);
            Response.Redirect("ListarClientes.aspx");
        }



        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idCliente = Int32.Parse(((LinkButton)sender).CommandArgument);
            cliente cliente = clientes.SingleOrDefault(x => x.persona_id == idCliente);
            //ClienteWS empleado = empleados.SingleOrDefault(x => x.IdPersona == idEmpleado);
            Session["clienteSeleccionado"] = cliente;
            Response.Redirect("RegistrarCliente.aspx?accion=ver");
        }
        protected void lbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string textoId = txtNombreID.Text.Trim();

                if (int.TryParse(textoId, out int idCliente))
                {
                    // Buscar cliente por ID usando tu capa de negocio o servicio

                    //var cliente = clienteWS.obtenerCliente(idCliente);
                    cliente cliente = clientes.SingleOrDefault(x => x.persona_id == idCliente);

                    if (cliente != null)
                    {
                        // Si encontró el cliente, lo pone en una lista para enlazar

                        /*
                        var lista = new List<cliente> { cliente };
                        dgvClientes.DataSource = lista;
                        dgvClientes.DataBind();
                        */

                        clientes= new List<cliente> { cliente };
                        dgvClientes.DataSource = clientes;
                        dgvClientes.DataBind();

                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvClientes.DataSource = null;
                        dgvClientes.DataBind();
                       // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvClientes.DataSource = clientes;
                    dgvClientes.DataBind();
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

                if (texto!=null)
                {

                    List<cliente> cliente = clienteWS.listarClientesPorNombre(texto).ToList();

                    if (cliente != null)
                    {

                        clientes = cliente;
                        dgvClientes.DataSource = clientes;
                        dgvClientes.DataBind();

                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvClientes.DataSource = null;
                        dgvClientes.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvClientes.DataSource = clientes;
                    dgvClientes.DataBind();
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
            clientes = clienteWS.listarClientes().ToList();
            dgvClientes.DataSource = clientes;
            dgvClientes.DataBind();

        }

        protected void DdlTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo_seleccionado = Convert.ToString(DdlTipoCliente.SelectedValue);

            // Verificamos si el valor seleccionado es el valor predeterminado "0" (Select type)
            if (tipo_seleccionado == "0")
            {
                // Si no se ha seleccionado un valor válido, mostrar todos los productos
                dgvClientes.DataSource = clienteWS.listarClientes();
                dgvClientes.DataBind();
            }
            else
            {
                // Si se ha seleccionado un tipo válido, mostrar productos por tipo
                dgvClientes.DataSource = clienteWS.listarClientesPorTipo(tipo_seleccionado);
                dgvClientes.DataBind();
            }
        }
    }
}