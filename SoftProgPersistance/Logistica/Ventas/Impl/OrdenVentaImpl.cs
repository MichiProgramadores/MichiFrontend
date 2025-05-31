using MySql.Data.MySqlClient;
using SoftProgDBManager;
using SoftProgDomain.Logistica.Ventas;
using SoftProgDomain.RRHH;
using SoftProgPersistance.Logistica.Ventas.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.Logistica.Ventas.Impl
{
    public class OrdenVentaImpl : OrdenVentaDAO
    {
        private MySqlConnection con;
        private MySqlTransaction transaction;
        public int eliminar(int idModelo)
        {
            throw new NotImplementedException();
        }

        public int insertar(OrdenVenta ordenVenta)
        {
            try
            {
                con = DBManager.getInstance().Connection;
                transaction = con.BeginTransaction();
                MySqlParameter[] parametros = new MySqlParameter[4];
                parametros[0] = new MySqlParameter("_id_orden_venta", MySqlDbType.Int32);
                parametros[0].Direction = ParameterDirection.Output;
                parametros[1] = new MySqlParameter("_fid_cliente", ordenVenta.Cliente.IdPersona);
                parametros[2] = new MySqlParameter("_fid_empleado", ordenVenta.Empleado.IdPersona);
                parametros[3] = new MySqlParameter("_total", ordenVenta.Total);
                DBManager.getInstance().EjecutarProcedimientoTransaccion("INSERTAR_ORDEN_VENTA", parametros, transaction);
                ordenVenta.IdOrdenVenta = Int32.Parse(parametros[0].Value.ToString());
                foreach (LineaOrdenVenta lineaOrdenVenta in ordenVenta.LineasOrdenVenta)
                {
                    parametros = new MySqlParameter[5];
                    parametros[0] = new MySqlParameter("_id_linea_orden_venta", MySqlDbType.Int32);
                    parametros[0].Direction = ParameterDirection.Output;
                    parametros[1] = new MySqlParameter("_fid_orden_venta", ordenVenta.IdOrdenVenta);
                    parametros[2] = new MySqlParameter("_fid_producto", lineaOrdenVenta.Producto.IdProducto);
                    parametros[3] = new MySqlParameter("_cantidad", lineaOrdenVenta.Cantidad);
                    parametros[4] = new MySqlParameter("_subtotal", lineaOrdenVenta.Subtotal);
                    DBManager.getInstance().EjecutarProcedimientoTransaccion("INSERTAR_LINEA_ORDEN_VENTA", parametros, transaction);
                    lineaOrdenVenta.IdLineaOrdenVenta = Int32.Parse(parametros[0].Value.ToString());
                }
                transaction.Commit();
            }
            catch(Exception ex)
            {
                try { transaction.Rollback(); } catch (Exception ex2) { throw new Exception(ex2.Message); }
            }
            finally
            {
                DBManager.getInstance().CerrarConexion();
            }
            return ordenVenta.IdOrdenVenta;
        }

        public BindingList<OrdenVenta> listarTodos()
        {
            throw new NotImplementedException();
        }

        public int modificar(OrdenVenta modelo)
        {
            throw new NotImplementedException();
        }

        public OrdenVenta obtenerPorId(int idModelo)
        {
            throw new NotImplementedException();
        }
    }
}
