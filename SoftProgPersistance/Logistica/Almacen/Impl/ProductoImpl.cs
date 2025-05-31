using MySql.Data.MySqlClient;
using SoftProgDBManager;
using SoftProgDomain.GestClientes;
using SoftProgDomain.Logistica.Almacen;
using SoftProgPersistance.Logistica.Almacen.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.Logistica.Almacen.Impl
{
    public class ProductoImpl : ProductoDAO
    {
        private MySqlDataReader lector;
        public int eliminar(int idModelo)
        {
            throw new NotImplementedException();
        }

        public int insertar(ProductoDAO modelo)
        {
            throw new NotImplementedException();
        }

        public BindingList<Producto> listarPorNombre(string nombre)
        {
            BindingList<Producto> productos = null;
            MySqlParameter[] parametros = new MySqlParameter[1];
            parametros[0] = new MySqlParameter("_nombre", nombre);
            lector = DBManager.getInstance().EjecutarProcedimientoLectura("LISTAR_PRODUCTOS_X_NOMBRE", parametros);
            while (lector.Read())
            {
                if (productos == null) productos = new BindingList<Producto>();
                Producto producto = new Producto();
                if (!lector.IsDBNull(lector.GetOrdinal("id_producto"))) producto.IdProducto = lector.GetInt32("id_producto");
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) producto.Nombre = lector.GetString("nombre");
                if (!lector.IsDBNull(lector.GetOrdinal("unidad_medida"))) producto.UnidadMedida = lector.GetString("unidad_medida");
                if (!lector.IsDBNull(lector.GetOrdinal("precio"))) producto.Precio = lector.GetDouble("precio");
                productos.Add(producto);
            }
            DBManager.getInstance().CerrarConexion();
            return productos;
        }

        public BindingList<ProductoDAO> listarTodos()
        {
            throw new NotImplementedException();
        }

        public int modificar(ProductoDAO modelo)
        {
            throw new NotImplementedException();
        }

        public ProductoDAO obtenerPorId(int idModelo)
        {
            throw new NotImplementedException();
        }
    }
}
