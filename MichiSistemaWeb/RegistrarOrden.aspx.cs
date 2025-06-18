using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class RegistrarOrden : System.Web.UI.Page
    {
        protected OrdenWSClient ordenService;
        protected TrabajadorWSClient trabajadorService;
        protected ClienteWSClient clienteService;
        protected ProductoWSClient productoService;
        protected orden orden;
        private List<detalleOrden> detallesOrden;

        protected Estado estado;
        protected void Page_Init(object sender, EventArgs e)
        {
            clienteService = new ClienteWSClient();
            trabajadorService = new TrabajadorWSClient();
            productoService = new ProductoWSClient();

            ordenService = new OrdenWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                detallesOrden = new List<detalleOrden>();
                Session["DetallesOrden"] = detallesOrden;
                CargarClientes();
                CargarTrabajadores();
                CargarProductos();
                // Obtener los valores del enum TipoRecepcion
                ddlTipoRecepcion.DataSource = Enum.GetValues(typeof(tipoRecepcion));
                ddlTipoRecepcion.DataBind();

                // Opcional: agregar un valor por defecto
                ddlTipoRecepcion.Items.Insert(0, new ListItem("-- Seleccione --", ""));

                // Obtener los valores del enum tipoCliente
                ddlTipoEstDevol.DataSource = Enum.GetValues(typeof(tipoEstadoDevolucion));
                ddlTipoEstDevol.DataBind();

                // Opcional: agregar un valor por defecto
                ddlTipoEstDevol.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }

            string accion = Request.QueryString["accion"];
            if (accion == null)
            {
                estado = Estado.Nuevo;
                orden = new orden();
                lblTitulo.Text = "Registrar Orden";

                lblIdOrden.Visible = false;
                txtIdOrden.Visible = false;

                //lblIdTrabaj.Visible = false;
                //txtIdTrabaj.Visible = false;

                lblIdCliente.Visible = true;
                txtIdCliente.Visible = true;

                //lblTipoEstDevol.Visible = false;
                //ddlTipoEstDevol.Visible = false;

                //lblTipoRecepcion.Visible = false;
                //ddlTipoRecepcion.Enabled = false;

                //lblFechaEntr.Visible = false;
                //txtFechaEntr.Enabled = false;

                txtMonto.Visible = false;
                txtSaldo.Visible = false;
                txtCantDias.Visible = false;
                //txtFechaDevol.Enabled = false;
                //txtFechaEmis.Enabled = false;
                lblMontoTotal.Visible = false;
                lblSaldo.Visible = false;
                lblCantDias.Visible = false;

                //lblFechaDevol.Visible = false;
                //lblFechaEmis.Visible = false;
                //lblSetUpPersonalizado.Visible = false;
                //txtSetUpPersonalizado.Enabled = false;
                lblFechaReg.Visible = false;
                txtFechaReg.Visible = false;

            }
            else if (accion == "modificar")
            {
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar Orden";
                orden = ordenService.obtenerOrden(((orden)Session["ordenSeleccionada"]).idOrden);
                if (!IsPostBack)
                {
                    // Cargar detalles de la orden en la sesión
                    detallesOrden = orden.listaOrdenes != null ? orden.listaOrdenes.ToList() : new List<detalleOrden>();
                    Session["DetallesOrden"] = detallesOrden;

                    AsignarValoresTexto();
                    ActualizarGrillaDetalles();
                }

                lblIdOrden.Visible = false;
                txtIdOrden.Visible = false;

                //lblIdTrabaj.Visible = false;
                //txtIdTrabaj.Visible = false;

                lblIdCliente.Visible = true;
                txtIdCliente.Visible = true;

                //lblTipoEstDevol.Visible = false;
                //ddlTipoEstDevol.Visible = false;

                //lblTipoRecepcion.Visible = false;
                //ddlTipoRecepcion.Enabled = false;

                //lblFechaEntr.Visible = false;
                //txtFechaEntr.Enabled = false;

                txtMonto.Enabled = false;
                // txtSaldo.Enabled = false;
                txtCantDias.Visible = false;
                //txtFechaDevol.Enabled = false;
                //txtFechaEmis.Enabled = false;
                lblMontoTotal.Enabled = false;
                lblSaldo.Enabled = false;
                lblCantDias.Visible = false;
                //lblFechaDevol.Visible = false;
                //lblFechaEmis.Visible = false;
                //lblSetUpPersonalizado.Visible = false;
                //txtSetUpPersonalizado.Enabled = false;
                lblFechaReg.Visible = false;
                txtFechaReg.Visible = false;
            }
            else if (accion == "ver")
            {

                lblTitulo.Text = "Visualizar Orden";
                orden = (orden)Session["ordenSeleccionada"];
                if (!IsPostBack)
                {
                    // Cargar detalles de la orden en la sesión
                    detallesOrden = orden.listaOrdenes != null ? orden.listaOrdenes.ToList() : new List<detalleOrden>();
                    Session["DetallesOrden"] = detallesOrden;

                    AsignarValoresTexto();
                    ActualizarGrillaDetalles();
                }

                lblIdOrden.Visible = false;
                txtIdOrden.Visible = false;
                txtSetUpPersonalizado.Enabled = false;
                txtIdCliente.Enabled = false;
                txtIdTrabaj.Enabled = false;
                txtIdOrden.Enabled = false;
                txtFechaDevol.Enabled = false;
                txtFechaEmis.Enabled = false;
                txtFechaEntr.Enabled = false;
                txtMonto.Enabled = false;
                lblTipoRecepcion.Enabled = false;
                txtSaldo.Enabled = false;
                txtCantDias.Visible = false;
                lblFechaReg.Visible = false;
                txtFechaReg.Visible = false;
                ddlTipoEstDevol.Enabled = false;
                btnGuardar.Visible = false;
                btnGuardar.Enabled = false;
                lblCantDias.Visible = false;
                gvDetalles.Columns[4].Visible = false;
                phAgregarProducto.Visible = false;
                ddlTipoRecepcion.Enabled = false;
            }
        }

        protected void AsignarValoresTexto()
        {

            int idTrabajador = orden.trabajadorID;
            trabajador trabajador = trabajadorService.obtenerTrabajador(idTrabajador);
            hdnTrabajadorId.Value = idTrabajador.ToString();
            txtIdTrabaj.Text = $"{trabajador.nombres} {trabajador.apellidos}";
            txtIdOrden.Text = orden.idOrden.ToString();

            int clienteId = orden.clienteID;
            cliente cliente = clienteService.obtenerCliente(clienteId);

            hdnClienteId.Value = clienteId.ToString();
            txtIdCliente.Text = $"{cliente.nombres} {cliente.apellidos}";

            txtSetUpPersonalizado.Text = orden.setUpPersonalizado;

            ddlTipoRecepcion.SelectedValue = orden.tipoRecepcion.ToString();
            txtFechaReg.Text = orden.fecha_emisión.ToString("yyyy-MM-dd");
            txtFechaDevol.Text = orden.fecha_devolucion.ToString("yyyy-MM-dd");
            txtFechaEntr.Text = orden.fecha_entrega.ToString("yyyy-MM-dd");
            txtFechaEmis.Text = orden.fecha_emisión.ToString("yyyy-MM-dd");
            if (orden.tipoEstadoDevolucion.ToString() != null)
                ddlTipoEstDevol.SelectedValue = orden.tipoEstadoDevolucion.ToString();
            txtMonto.Text = orden.totalPagar.ToString();
            txtSaldo.Text = orden.saldo.ToString();

            gvDetalles.DataSource = orden.listaOrdenes;
            gvDetalles.DataBind();

        }

        private void CargarProductos()
        {
            ddlProductos.DataSource = productoService.listarProductos();
            ddlProductos.DataTextField = "nombre";
            ddlProductos.DataValueField = "producto_id";
            ddlProductos.DataBind();
            ddlProductos.Items.Insert(0, new ListItem("Seleccione un producto", ""));
        }

        private void CargarClientes()
        {
            dgvClientes.DataSource = clienteService.listarClientes();
            dgvClientes.DataBind();
        }

        private void CargarTrabajadores()
        {
            dgvTrabajadores.DataSource = trabajadorService.listarTrabajadores();
            dgvTrabajadores.DataBind();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int index = Convert.ToInt32(btn.CommandArgument);
            detallesOrden = (List<detalleOrden>)Session["DetallesOrden"];
            detallesOrden.RemoveAt(index);
            Session["DetallesOrden"] = detallesOrden;
            ActualizarGrillaDetalles();
        }

        private void ActualizarGrillaDetalles()
        {
            detallesOrden = (List<detalleOrden>)Session["DetallesOrden"];
            gvDetalles.DataSource = detallesOrden;
            gvDetalles.DataBind();

            double total = 0;
            foreach (detalleOrden detalle in detallesOrden)
            {
                //CALCULO DE SUBTOTAL FALTA

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
        protected void btnSeleccionarTrabajador_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idTrabajador = int.Parse(btn.CommandArgument);
            trabajador trabajador = trabajadorService.obtenerTrabajador(idTrabajador);

            hdnTrabajadorId.Value = idTrabajador.ToString();
            txtIdTrabaj.Text = $"{trabajador.nombres} {trabajador.apellidos}";
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlProductos.SelectedValue))
            {
                int productoId = Convert.ToInt32(ddlProductos.SelectedValue);
                producto producto = productoService.obtenerProducto(productoId);
                txtPrecioUnitario.Text = producto.precio.ToString("F2");
                txtStockProducto.Text = producto.stockActual.ToString();

            }
            else
            {
                txtPrecioUnitario.Text = "";
                txtStockProducto.Text = "";
            }
            upModalDetalle.Update();
        }
        private void LimpiarError()
        {
            lblError.Text = "";
            divError.Style["display"] = "none";
        }
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            divError.Style["display"] = "block";
        }
        protected void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            LimpiarError();

            if (string.IsNullOrEmpty(ddlProductos.SelectedValue))
            {
                MostrarError("Debe seleccionar un producto");
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MostrarError("La cantidad debe ser un número mayor a 0");
                return;
            }


            if (!double.TryParse(txtPrecioUnitario.Text, out double precio) || precio <= 0)
            {
                MostrarError("El precio debe ser un número mayor a 0");
                return;
            }

            int productoId = Convert.ToInt32(ddlProductos.SelectedValue);
            producto producto = productoService.obtenerProducto(productoId);

            if (cantidad > producto.stockActual)
            {
                MostrarError($"No hay suficiente stock. Stock disponible: {producto.stockActual}");
                return;
            }

            detallesOrden = (List<detalleOrden>)Session["DetallesOrden"];
            detalleOrden detalle = new detalleOrden
            {

                producto = producto.producto_id,
                cantidadSolicitada = cantidad,
                precioAsignado = precio,
                subtotal = precio * cantidad
            };

            detallesOrden.Add(detalle);
            Session["DetallesVenta"] = detallesOrden;

            ActualizarGrillaDetalles();
            LimpiarFormularioDetalle();
        }

        private void LimpiarFormularioDetalle()
        {
            ddlProductos.SelectedIndex = 0;
            txtPrecioUnitario.Text = "";
            txtCantidad.Text = "";
            txtStockProducto.Text = "";
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarError();

                if (string.IsNullOrEmpty(hdnClienteId.Value))
                {
                    MostrarError("Debe seleccionar un cliente");
                    return;
                }

                if (string.IsNullOrEmpty(hdnTrabajadorId.Value))
                {
                    MostrarError("Debe seleccionar un trabajador");
                    return;
                }

                detallesOrden = (List<detalleOrden>)Session["DetallesOrden"];
                if (detallesOrden.Count == 0)
                {
                    MostrarError("Debe agregar al menos un producto");
                    return;
                }
                CultureInfo cultura = CultureInfo.InvariantCulture;
                DateTime fechaInicio = DateTime.ParseExact(txtFechaEmis.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime fechaFin = DateTime.ParseExact(txtFechaDevol.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                int diasDiferencia = (fechaFin - fechaInicio).Days;



                orden orden = new orden()
                {
                    tipoRecepcion = (tipoRecepcion)Enum.Parse(typeof(tipoRecepcion), ddlTipoRecepcion.SelectedValue),

                    setUpPersonalizado = txtSetUpPersonalizado.Text.ToString(),

                    totalPagar = double.Parse(txtMonto.Text.Trim()),

                    saldo = double.Parse(txtMonto.Text.Trim()),
                    cantDias = diasDiferencia,
                    fecha_registro = DateTime.Today,
                    fecha_devolucion = fechaFin,
                    fecha_emisión = fechaInicio,
                    clienteID = Convert.ToInt32(hdnClienteId.Value),
                    trabajadorID = Convert.ToInt32(hdnTrabajadorId.Value),
                    listaOrdenes = detallesOrden.ToArray(),

                };
                Console.WriteLine("Tipo de recepción asignado en la orden: " + orden.tipoRecepcion.ToString());
                ordenService.registrarOrden(orden);
                Response.Redirect("ListaOrdenes.aspx");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
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
            Response.Redirect("ListarOrdenes.aspx");
        }

    }
}