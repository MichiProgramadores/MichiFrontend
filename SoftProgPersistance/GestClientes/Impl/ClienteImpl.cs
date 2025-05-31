using MySql.Data.MySqlClient;
using SoftProgDBManager;
using SoftProgDomain.GestClientes;
using SoftProgDomain.RRHH;
using SoftProgPersistance.GestClientes.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestClientes.Impl
{
    public class ClienteImpl : ClienteDAO
    {
        private MySqlDataReader lector;
        public int eliminar(int idModelo)
        {
            throw new NotImplementedException();
        }

        public int insertar(Cliente modelo)
        {
            throw new NotImplementedException();
        }

        public BindingList<Cliente> listarPorDNIoNombre(string DNIoNombre)
        {
            BindingList<Cliente> clientes = null;
            MySqlParameter[] parametros = new MySqlParameter[1];
            parametros[0] = new MySqlParameter("_dni_nombre", DNIoNombre);
            lector = DBManager.getInstance().EjecutarProcedimientoLectura("LISTAR_CLIENTES_X_DNI_NOMBRE", parametros);
            while (lector.Read())
            {
                if (clientes == null) clientes = new BindingList<Cliente>();
                Cliente cliente = new Cliente();
                if (!lector.IsDBNull(lector.GetOrdinal("id_cliente"))) cliente.IdPersona = lector.GetInt32("id_cliente");
                if (!lector.IsDBNull(lector.GetOrdinal("DNI"))) cliente.DNI = lector.GetString("DNI");
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) cliente.Nombre = lector.GetString("nombre");
                if (!lector.IsDBNull(lector.GetOrdinal("apellido_paterno"))) cliente.ApellidoPaterno = lector.GetString("apellido_paterno");
                clientes.Add(cliente);
            }
            DBManager.getInstance().CerrarConexion();
            return clientes;
        }

        public BindingList<Cliente> listarTodos()
        {
            throw new NotImplementedException();
        }

        public int modificar(Cliente modelo)
        {
            throw new NotImplementedException();
        }

        public Cliente obtenerPorId(int idModelo)
        {
            throw new NotImplementedException();
        }
    }
}
