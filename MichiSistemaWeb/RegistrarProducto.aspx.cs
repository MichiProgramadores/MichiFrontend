using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class RegistrarProducto : System.Web.UI.Page
    {

        protected ProductoWSClient productoService;
        protected producto producto;
        protected Estado estado;

        protected void Page_Init(object sender, EventArgs e)
        {
            productoService = new ProductoWSClient();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener los valores del enum tipoProducto
                ddlTipo.DataSource = Enum.GetValues(typeof(tipoProducto));
                ddlTipo.DataBind();
                // Opcional: agregar un valor por defecto
                ddlTipo.Items.Insert(0, new ListItem("-- Seleccione --", ""));

                // Obtener los valores del enum UnidadMedida
                ddlUnidadMedida.DataSource = Enum.GetValues(typeof(unidadMedida));
                ddlUnidadMedida.DataBind();
                // Opcional: agregar un valor por defecto
                ddlUnidadMedida.Items.Insert(0, new ListItem("-- Seleccione --", ""));

            }
            string accion = Request.QueryString["accion"];
            if (accion == null)
            {
                estado = Estado.Nuevo;
                producto = new producto();
                lblTitulo.Text = "Registrar producto";

                lblID.Visible = false;
                txtIDProducto.Visible = false;

                lblEstado.Visible = false;
                txtEstado.Visible = false;

            }
            else if (accion == "modificar")
            {
                estado = Estado.Modificar;
                lblTitulo.Text = "Modificar producto";
                producto = (producto)Session["productoSeleccionado"];
                if (!IsPostBack)
                {
                    AsignarValoresTexto();
                }
                lblID.Visible = true;
                txtIDProducto.Visible = true;
                txtIDProducto.Enabled = false;
                txtEstado.Visible = true;
                txtEstado.Enabled = false;

            }
            else if (accion == "ver")
            {
                lblTitulo.Text = "Ver producto";
                producto = (producto)Session["productoSeleccionado"];
                AsignarValoresTexto();

                lblID.Visible = true;
                txtIDProducto.Visible = true;
                txtIDProducto.Enabled = false;

                txtNombre.Enabled = false;
                ddlTipo.Enabled = false;       
                txtPrecio.Enabled = false;
                txtStockMin.Enabled = false;
                txtStockAct.Enabled = false;
                ddlUnidadMedida.Enabled = false;
                txtVolumen.Enabled = false;
                txtDescrip.Enabled = false;
                txtEstado.Enabled = false;
                txtEdadMin.Enabled = false;
                btnGuardar.Visible = false;
            }
        }

        protected void AsignarValoresTexto()
        {

            txtNombre.Text = producto.nombre;
            txtIDProducto.Text = producto.producto_id.ToString();
            ddlTipo.SelectedValue = producto.categoriaProducto.ToString(); 
            txtPrecio.Text = producto.precio.ToString("F2"); // Formatear a dos decimales
            txtEdadMin.Text = producto.edad_minima.ToString();
            txtStockAct.Text = producto.stockActual.ToString();
            txtStockMin.Text = producto.stockMinimo.ToString();
            ddlUnidadMedida.SelectedValue = producto.unidadMedida.ToString();
            txtVolumen.Text = producto.volumen.ToString("F2"); // Formatear a dos decimales
            txtDescrip.Text = producto.descripcion;
            txtEstado.Text = producto.estado ? "Activo" : "Inactivo"; // Mostrar estado como texto

        }
        protected void AsignarValoresProducto()
        {
            producto.nombre = txtNombre.Text.Trim();
            producto.categoriaProducto = (tipoProducto)Enum.Parse(typeof(tipoProducto), ddlTipo.SelectedValue);
            producto.precio = double.Parse(txtPrecio.Text.Trim());
            producto.edad_minima = int.Parse(txtEdadMin.Text.Trim());
            producto.stockActual = int.Parse(txtStockAct.Text.Trim());
            producto.stockMinimo = int.Parse(txtStockMin.Text.Trim());
            producto.unidadMedida = (unidadMedida1)Enum.Parse(typeof(unidadMedida), ddlUnidadMedida.SelectedValue);
            producto.volumen = double.Parse(txtVolumen.Text.Trim());
            producto.descripcion = txtDescrip.Text.Trim();
            producto.estado = true; // Convertir a booleano
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación básica
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtEdadMin.Text) ||
                    string.IsNullOrWhiteSpace(txtStockAct.Text) ||
                    string.IsNullOrWhiteSpace(txtStockMin.Text) ||
                    string.IsNullOrWhiteSpace(txtVolumen.Text) ||
                    string.IsNullOrWhiteSpace(txtDescrip.Text) ||
                    string.IsNullOrWhiteSpace(ddlTipo.SelectedValue) ||
                    string.IsNullOrWhiteSpace(ddlUnidadMedida.SelectedValue))
                {
                    lanzarMensajedeError("Por favor, complete todos los campos.");
                    return;
                }

                // Asignar los valores del formulario al objeto producto
                AsignarValoresProducto();

                // Insertar producto
                string valorSeleccionadoTipo = ddlTipo.SelectedValue;
                string valorSeleccionadoMedida = ddlUnidadMedida.SelectedValue;
                if (estado== Estado.Nuevo)
                {
                    productoService.registrarProducto(producto, valorSeleccionadoTipo, valorSeleccionadoMedida);
                }
                else if (estado == Estado.Modificar)
                {
                    productoService.actualizarProducto(producto);
                }

                // Redirigir
                Response.Redirect("ListarProductos.aspx");
            }
            catch (Exception ex)
            {
                lanzarMensajedeError("No se pudo registrar el producto: " + ex.Message);
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
            Response.Redirect("ListarProductos.aspx");
        }
    }
}