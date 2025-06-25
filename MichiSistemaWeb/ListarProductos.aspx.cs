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
            //CargarDatos();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //productoWS = new ProductoWSClient();
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
                productos = productoWS.listaProductosActivos().ToList();
                ViewState["ProductosFiltrados"] = productos;
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
                //e.Row.Cells[7].Text = DataBinder.Eval(e.Row.DataItem, "descripcion").ToString();
                //e.Row.Cells[7].Text = DataBinder.Eval(e.Row.DataItem, "estado").ToString();
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
                // lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        /*
        protected void lblBuscarN_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string texto = txtNombre.Text.Trim();

                if (texto != null)
                {

                    List<producto> producto = productoWS.listarProductosPorNombre(texto).ToList();

                    if (producto != null)
                    {

                        productos = producto;
                        dgvProductos.DataSource = productos;
                        dgvProductos.DataBind();

                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvProductos.DataSource = null;
                        dgvProductos.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvProductos.DataSource = productos;
                    dgvProductos.DataBind();
                    //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }
        }
        */


        /*
        protected void lbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto del textbox
                string textoId = txtID.Text.Trim();
                if (int.TryParse(textoId, out int idProducto))
                {
                    // Buscar cliente por ID usando tu capa de negocio o servicio
                    var producto = productoWS.obtenerProducto(idProducto);
                    if (producto != null)
                    {
                        // Si encontró el cliente, lo pone en una lista para enlazar
                        var lista = new List<producto> { producto };
                        dgvProductos.DataSource = lista;
                        dgvProductos.DataBind();
                        //lblMensaje.Text = "";
                    }
                    else
                    {
                        // Si no encontró resultados
                        dgvProductos.DataSource = null;
                        dgvProductos.DataBind();
                        // lblMensaje.Text = "No se encontró cliente con ese ID.";
                    }
                }
                else
                {
                    // Si no ingresó un número válido
                    dgvProductos.DataSource = productos; // Volver a mostrar todos los productos
                    dgvProductos.DataBind();
                    //  lblMensaje.Text = "Ingrese un ID válido (número entero).";
                }
            }
            catch (Exception ex)
            {
                // lblMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }

        }
        */

        /*
        protected void DdlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo_seleccionado = Convert.ToString(DdlTipoProducto.SelectedValue);

            // Verificamos si el valor seleccionado es el valor predeterminado "0" (Select type)
            if (tipo_seleccionado == "0")
            {
                // Si no se ha seleccionado un valor válido, mostrar todos los productos
                dgvProductos.DataSource = productoWS.listarProductos();
                dgvProductos.DataBind();
            }
            else
            {
                // Si se ha seleccionado un tipo válido, mostrar productos por tipo
                dgvProductos.DataSource = productoWS.listarProductosPorTipo(tipo_seleccionado);
                dgvProductos.DataBind();
            }
        }
        */

    }
}