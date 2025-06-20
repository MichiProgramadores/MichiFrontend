﻿using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class RegistrarComprobante : System.Web.UI.Page
    {
        protected ComprobanteWSClient comprobanteService;
        protected comprobante comprobante;
        protected Estado estado;

        protected OrdenWSClient ordenService;
        protected ClienteWSClient clienteService;

        //protected ProductoWSClient productoService;
        private List<detalleComprobante> detallesComprobante;

        protected void Page_Init(object sender, EventArgs e)
        {
            comprobanteService = new ComprobanteWSClient();

            ordenService = new OrdenWSClient();
            clienteService = new ClienteWSClient();
            //productoService = new ProductoWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                detallesComprobante = new List<detalleComprobante>();
                Session["DetallesComprobante"] = detallesComprobante;
                CargarClientes();
                CargarOrdenes();
                //CargarProductos();

                //Obtener los valores del enum tipoComprobante
                ddlTipoComprobante.DataSource = Enum.GetValues(typeof(tipoComprobante));
                ddlTipoComprobante.DataBind();

                // Opcional: agregar un valor por defecto
                ddlTipoComprobante.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }

            string accion = Request.QueryString["accion"];
            if (accion == null)
            {
                estado = Estado.Nuevo;
                comprobante = new comprobante();
                lblTitulo.Text = "Registrar Comprobante";

                lblIdComprobante.Visible = false;
                txtIdComprobante.Visible = false;

                lblMontoTotal.Visible = false;
                txtMonto.Visible = false;

                lblFechaEmis.Visible = false;
                txtFechaEmis.Visible = false;

            }
            else if (accion == "modificar")
            {
                
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar Comprobante";
                comprobante = (comprobante)Session["comprobanteSeleccionado"];
                if (!IsPostBack)
                {
                    AsignarValoresTexto();
                }
                lblIdComprobante.Visible = true;
                txtIdComprobante.Visible = true;
                txtIdComprobante.Enabled = false;
                
            }
            else if (accion == "ver")
            {
                
                lblTitulo.Text = "Ver Comprobante";
                comprobante = (comprobante)Session["comprobanteSeleccionado"];
                AsignarValoresTexto();

                lblIdComprobante.Visible = true;
                txtIdComprobante.Visible = true;
                txtIdComprobante.Enabled = false;

                txtMonto.Enabled = false;
                txtEstado.Enabled = false;
                txtFechaEmis.Enabled = false;
                txtTax.Enabled = false;
                txtIdCliente.Enabled = false;
                txtIdOrden.Enabled = false;

                ddlTipoComprobante.Enabled = false;

                btnGuardar.Visible = false;
            }
        }

        private void CargarClientes()
        {
            dgvClientes.DataSource = clienteService.listarClientes();
            dgvClientes.DataBind();
        }

        private void CargarOrdenes()
        {
            dgvOrdenes.DataSource = ordenService.listarOrdenes();
            dgvOrdenes.DataBind();
        }

        /*
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int index = Convert.ToInt32(btn.CommandArgument);
            detallesComprobante = (List<detalleComprobante>)Session["DetallesComprobante"];
            detallesComprobante.RemoveAt(index);
            Session["DetallesComprobante"] = detallesComprobante;
            ActualizarGrillaDetalles();
        }
        */

        private void ActualizarGrillaDetalles()
        {
            detallesComprobante = (List<detalleComprobante>)Session["DetallesComprobante"];
            gvDetalles.DataSource = detallesComprobante;
            gvDetalles.DataBind();

            double total = 0;
            foreach (detalleComprobante detalle in detallesComprobante)
            {
                total += detalle.subtotal;
            }

            txtMonto.Text = total.ToString("F2");
            lblTotal.Text = total.ToString("F2");
        }

        protected void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            //prueba para despues escribir//
            LinkButton btn = (LinkButton)sender;
            int clienteId = Convert.ToInt32(btn.CommandArgument);
            cliente cliente = clienteService.obtenerCliente(clienteId);

            hdnClienteId.Value = clienteId.ToString();
            txtIdCliente.Text = $"{cliente.nombres} {cliente.apellidos}"; // mostrar nombre y apellido

        }
        protected void btnSeleccionarOrden_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idOrden = int.Parse(btn.CommandArgument);
            orden orden= ordenService.obtenerOrden(idOrden);

            hdnOrdenId.Value = idOrden.ToString();
            txtIdOrden.Text = idOrden.ToString();

            List<detalleOrden> detallesOrden;
            detallesOrden = orden.listaOrdenes != null ? orden.listaOrdenes.ToList() : new List<detalleOrden>();

            detallesComprobante = new List<detalleComprobante>();
            foreach (var detalleOrden in detallesOrden)
            {
                //producto producto = productoService.obtenerProducto(detalleOrden.producto);

                detalleComprobante detalle = new detalleComprobante
                {
                    producto_id = detalleOrden.producto,
                    cantidad = detalleOrden.cantidadEntregada,
                    subtotal = detalleOrden.subtotal,
                    unidad_medida = (unidadMedida1)detalleOrden.unidadMedida
                };

                detallesComprobante.Add(detalle);
            }

            Session["DetallesComprobante"] = detallesComprobante;
            ActualizarGrillaDetalles();

        }

        protected void AsignarValoresTexto()
        {

            txtIdComprobante.Text = comprobante.id_comprobante.ToString();
            txtMonto.Text = comprobante.monto_total.ToString();
            txtEstado.Text = comprobante.estado;
            txtFechaEmis.Text = comprobante.fecha_emision.ToString("yyyy-MM-dd");
            txtTax.Text = comprobante.tax.ToString();
            //txtIdCliente.Text = comprobante.cliente_id.ToString();
            txtIdOrden.Text = comprobante.orden_id.ToString();

            ddlTipoComprobante.SelectedValue = comprobante.tipoComprobante.ToString();

            int clienteId = comprobante.cliente_id;
            cliente cliente = clienteService.obtenerCliente(clienteId);

            hdnClienteId.Value = clienteId.ToString();
            txtIdCliente.Text = $"{cliente.nombres} {cliente.apellidos}";

        }

        protected void AsignarValoresComprobante()
        {
            comprobante.orden_id = int.Parse(txtIdOrden.Text.Trim());
            comprobante.cliente_id = int.Parse(hdnClienteId.Value);
            comprobante.estado = txtEstado.Text.Trim();

            //Test, no hay ordenes pa testear xd:
            comprobante.monto_total = 500;
            //
            //Real:
            //comprobante.monto_total = double.Parse(lblTotal.Text.Trim());

            comprobante.tax = double.Parse(txtTax.Text.Trim());
            
            string valorSeleccionado = ddlTipoComprobante.SelectedValue;
            comprobante.tipoComprobante = (tipoComprobante)Enum.Parse(typeof(tipoComprobante), valorSeleccionado);

            /* DEBUG 1:
            string valorSeleccionado = ddlTipoCliente.SelectedValue;
            lblMensajeError.Text = "Tipo seleccionado: " + valorSeleccionado; // DEBUG
            System.Diagnostics.Debug.WriteLine("Tipo seleccionado: " + ddlTipoCliente.SelectedValue);

            if (string.IsNullOrEmpty(valorSeleccionado))
            {
                throw new Exception("Debe seleccionar un tipo de cliente.");
            }
            */

            /* DEBUG 2:
            string valorSeleccionado = ddlTipoCliente.SelectedValue;
            //cliente.tipoCliente = (tipoCliente)Enum.Parse(typeof(tipoCliente), valorSeleccionado);
            //cliente.tipoCliente = tipoCliente.EIN;

            System.Diagnostics.Debug.WriteLine("Tipo seleccionado: " + cliente.tipoCliente.ToString());
            */

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación básica
                if (string.IsNullOrWhiteSpace(txtIdOrden.Text) ||
                    string.IsNullOrWhiteSpace(txtIdCliente.Text) ||
                    string.IsNullOrWhiteSpace(txtEstado.Text) ||
                    string.IsNullOrWhiteSpace(txtTax.Text) ||
                    string.IsNullOrWhiteSpace(ddlTipoComprobante.SelectedValue))
                {
                    lanzarMensajedeError("Por favor, complete todos los campos.");
                    return;
                }

                // Asignar los valores del formulario al objeto cliente
                AsignarValoresComprobante();

                // Insertar o modificar el cliente:
                string valorSeleccionado = ddlTipoComprobante.SelectedValue;

                //clienteService.registrarCliente(cliente, valorSeleccionado);
                //clienteService.registrarCliente(cliente);

                if (estado == Estado.Nuevo)
                {
                    comprobanteService.registrarComprobante(comprobante);
                    //comprobanteService.registrarComprobante(comprobante, valorSeleccionado);
                }
                else if (estado == Estado.Modificar)
                {
                    comprobanteService.actualizarComprobante(comprobante);
                    //comprobanteService.actualizarComprobante(comprobante, valorSeleccionado);
                }

                // Redirigir
                Response.Redirect("ListarComprobantes.aspx");
            }
            catch (Exception ex)
            {
                lanzarMensajedeError("No se pudo registrar el comprobante: " + ex.Message);
            }
        }

        public void lanzarMensajedeError(String mensaje)
        {
            lblMensajeError.Text = mensaje;
            string script = "mostrarModalError();";
            ScriptManager.RegisterStartupScript(this, GetType(), "modalError", script, true);
        }

        public void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarComprobantes.aspx");
        }

    }
}