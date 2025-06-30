using MichiSistemaWeb.MichiBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MichiSistemaWeb
{
    public partial class ListarProductos : System.Web.UI.Page
    {
        protected ProductoWSClient productoWS;
        protected List<producto> productos;
        protected void Page_Init(object sender, EventArgs e)
        {
            productoWS = new ProductoWSClient();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        protected void CargarDatos()
        {
            // Si ya está cargado en ViewState (evita llamar al WS de nuevo)
            if (ViewState["ProductosFiltrados"] != null)
            {
                productos = (List<producto>)ViewState["ProductosFiltrados"];
            }
            else
            {
                // Si no, filtra y guarda en ViewState
                try
                {
                    productos = productoWS.listaProductosActivos().ToList();
                    ViewState["ProductosFiltrados"] = productos;
                }
                catch
                {
                    // No se coloca nada, porque evita desde la BD
                }

            }
            dgvProductos.DataSource = productos;
            dgvProductos.DataBind();
        }

        protected void dgvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = DataBinder.Eval(e.Row.DataItem, "producto_id").ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "nombre").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "categoriaProducto").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "precio").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "stockMinimo").ToString();
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "stockActual").ToString();
                e.Row.Cells[6].Text = DataBinder.Eval(e.Row.DataItem, "unidadMedida").ToString();
                bool estado = (bool)DataBinder.Eval(e.Row.DataItem, "estado");
                if (estado)
                {
                    e.Row.Cells[7].Text = "Activo";
                }
                else
                {
                    e.Row.Cells[7].Text = "Inactivo";
                }
            }
        }

        protected void dgvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProductos.PageIndex = e.NewPageIndex;
            dgvProductos.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarProducto.aspx");
        }

        protected void lbModificar_Click(object sender, EventArgs e)
        {
            int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument); //queda actualizar esto por producto
            producto producto= productos.SingleOrDefault(x => x.producto_id == idProducto);          
            Session["productoSeleccionado"] = producto;
            Response.Redirect("RegistrarProducto.aspx?accion=modificar");
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idProducto = int.Parse(hfIdEliminar.Value);
            productoWS.eliminarProducto(idProducto);
            Response.Redirect("ListarProductos.aspx");
        }

        protected void lbVisualizar_Click(object sender, EventArgs e)
        {
            int idProducto = Int32.Parse(((LinkButton)sender).CommandArgument);
            producto producto = productos.SingleOrDefault(x => x.producto_id == idProducto);
            Session["productoSeleccionado"] = producto;
            Response.Redirect("RegistrarProducto.aspx?accion=ver");
        }

        protected void ListarTodos_Click(object sender, EventArgs e)
        {
            productos = productoWS.listarProductos().ToList();
            ViewState["ProductosFiltrados"] = productos; // Guarda en ViewState
            dgvProductos.DataSource = productos;
            dgvProductos.DataBind();
            txtID.Text = "";
            txtNombre.Text = "";

            // Limpiar el dropdown (si es necesario)
            DdlTipoProducto.SelectedIndex = 0;
        }

        protected void lblBuscarN_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Obtener parámetros de búsqueda
                string textoNombre = txtNombre.Text.Trim();
                string textoId = txtID.Text.Trim();
                string tipoSeleccionado = DdlTipoProducto.SelectedValue;

                // 2. Obtener TODOS los productos (sin filtros iniciales)
                List<producto> listaCompleta = productoWS.listarProductos().ToList();
                List<producto> resultados = listaCompleta;

                // 3. Aplicar filtros SOLO si el campo tiene valor
                if (!string.IsNullOrEmpty(textoNombre))
                {
                    resultados = resultados
                        .Where(c => c.nombre.ToLower().Contains(textoNombre.ToLower()))
                        .ToList();
                }

                if (!string.IsNullOrEmpty(textoId))
                {
                    resultados = resultados
                        .Where(c => c.producto_id.ToString().Contains(textoId))
                        .ToList();
                }

                if (tipoSeleccionado != "0" && !string.IsNullOrEmpty(tipoSeleccionado))
                {
                    resultados = resultados
                        .Where(c => c.categoriaProducto.ToString() == tipoSeleccionado)
                        .ToList();
                }

                if (textoNombre == "" && textoId == "" && tipoSeleccionado == "0")
                {
                    resultados = productoWS.listaProductosActivos().ToList();
                }

                // 4. Mostrar resultados
                dgvProductos.DataSource = resultados;
                dgvProductos.DataBind();

                // 5. Opcional: Guardar en ViewState
                ViewState["ProductosFiltrados"] = resultados;
            }
            catch (Exception ex)
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataBind();
               
            }
        }
    }
}