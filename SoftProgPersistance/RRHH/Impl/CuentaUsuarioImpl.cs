using MySql.Data.MySqlClient;
using SoftProgDBManager;
using SoftProgDomain.RRHH;
using SoftProgPersistance.RRHH.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.RRHH.Impl
{
    public class CuentaUsuarioImpl : CuentaUsuarioDAO
    {
        private MySqlDataReader lector;
        public int eliminar(int idModelo)
        {
            throw new NotImplementedException();
        }

        public int insertar(CuentaUsuario modelo)
        {
            throw new NotImplementedException();
        }

        public BindingList<CuentaUsuario> listarTodos()
        {
            throw new NotImplementedException();
        }

        public int modificar(CuentaUsuario modelo)
        {
            throw new NotImplementedException();
        }

        public CuentaUsuario obtenerPorId(int idModelo)
        {
            throw new NotImplementedException();
        }

        public int verificar(CuentaUsuario cuentaUsuario)
        {
            int resultado = 0;
            MySqlParameter[] parametros = new MySqlParameter[2];
            parametros[0] = 
            new MySqlParameter("_username", cuentaUsuario.Username);
            parametros[1] =
            new MySqlParameter("_password", cuentaUsuario.Password);
            lector = DBManager.getInstance().
            EjecutarProcedimientoLectura("VERIFICAR_CUENTA_USUARIO", 
            parametros);
            if (lector.Read())
            {
                resultado = lector.GetInt32("fid_empleado");
            }
            DBManager.getInstance().CerrarConexion();
            return resultado;
        }
    }
}
