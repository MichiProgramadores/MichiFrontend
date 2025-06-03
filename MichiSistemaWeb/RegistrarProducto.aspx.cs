using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MichiSistemaWeb.MichiBackend;

namespace MichiSistemaWeb
{
    public partial class RegistrarProducto : System.Web.UI.Page
    {

        protected ProductoWSClient productoService;
        protected producto producto;

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
        }

        protected void AsignarValores()
        {
            producto = new producto();
            producto.nombre = txtNombre.Text.Trim();
            producto.precio = double.Parse(txtPrecio.Text.Trim());
            producto.edad_minima = int.Parse(txtEdadMin.Text.Trim());
            producto.stockActual = int.Parse(txtStockAct.Text.Trim());
            producto.stockMinimo = int.Parse(txtStockMin.Text.Trim());
            producto.volumen = double.Parse(txtVolumen.Text.Trim());
            producto.descripcion = txtDescrip.Text.Trim();
             producto.estado = true;
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
                    string.IsNullOrWhiteSpace(ddlEstado.SelectedValue) ||
                    string.IsNullOrWhiteSpace(ddlUnidadMedida.SelectedValue))
                {
                    lanzarMensajedeError("Por favor, complete todos los campos.");
                    return;
                }

                // Asignar los valores del formulario al objeto producto
                AsignarValores();

                // Insertar producto
                string valorSeleccionadoTipo = ddlTipo.SelectedValue;
                string valorSeleccionadoMedida = ddlUnidadMedida.SelectedValue;
                productoService.registrarProducto(producto, valorSeleccionadoTipo, valorSeleccionadoMedida);

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