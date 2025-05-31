using MySql.Data.MySqlClient;
using SoftProgDBManager;
using SoftProgDomain.RRHH;
using SoftProgPersistance.RRHH.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.RRHH.Impl
{
    public class EmpleadoImpl : EmpleadoDAO
    {
        private MySqlConnection con;
        private MySqlCommand com;
        private MySqlDataReader lector;

        public int eliminar(int idEmpleado)
        {
            MySqlParameter[] parametros = new MySqlParameter[1];
            parametros[0] = new MySqlParameter("_id_empleado", idEmpleado);
            return DBManager.getInstance().EjecutarProcedimiento("ELIMINAR_EMPLEADO", parametros); ;
        }

        public int insertar(Empleado empleado)
        {
            MySqlParameter[] parametros = new MySqlParameter[9];
            parametros[0] = new MySqlParameter("_id_empleado",MySqlDbType.Int32);
            parametros[0].Direction = ParameterDirection.Output;
            parametros[1] = new MySqlParameter("_fid_area",empleado.Area.IdArea);
            parametros[2] = new MySqlParameter("_DNI", empleado.DNI);
            parametros[3] = new MySqlParameter("_nombre", empleado.Nombre);
            parametros[4] = new MySqlParameter("_apellido_paterno",empleado.ApellidoPaterno);
            parametros[5] = new MySqlParameter("_genero",empleado.Genero);
            parametros[6] = new MySqlParameter("_fecha_nacimiento",empleado.FechaNacimiento);
            parametros[7] = new MySqlParameter("_cargo",empleado.Cargo);
            parametros[8] = new MySqlParameter("_sueldo",empleado.Sueldo);
            DBManager.getInstance().EjecutarProcedimiento("INSERTAR_EMPLEADO",parametros);
            empleado.IdPersona = Int32.Parse(parametros[0].Value.ToString());
            return empleado.IdPersona;
        }

        public BindingList<Empleado> listarPorNombreDNI(string nombreDNI)
        {
            throw new NotImplementedException();
        }

        public BindingList<Empleado> listarTodos()
        {
            BindingList<Empleado> empleados = null;
            lector = DBManager.getInstance().EjecutarProcedimientoLectura("LISTAR_EMPLEADOS_TODOS", null);
            while (lector.Read())
            {
                if (empleados == null) empleados = new BindingList<Empleado>();
                Empleado empleado = new Empleado();
                if (!lector.IsDBNull(lector.GetOrdinal("id_persona"))) empleado.IdPersona = lector.GetInt32("id_persona");
                if (!lector.IsDBNull(lector.GetOrdinal("DNI"))) empleado.DNI = lector.GetString("DNI");
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) empleado.Nombre = lector.GetString("nombre");
                if (!lector.IsDBNull(lector.GetOrdinal("apellido_paterno"))) empleado.ApellidoPaterno = lector.GetString("apellido_paterno");
                if (!lector.IsDBNull(lector.GetOrdinal("genero"))) empleado.Genero = lector.GetChar("genero");
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_nacimiento"))) empleado.FechaNacimiento = lector.GetDateTime("fecha_nacimiento");
                if (!lector.IsDBNull(lector.GetOrdinal("cargo"))) empleado.Cargo = lector.GetString("cargo");
                if (!lector.IsDBNull(lector.GetOrdinal("sueldo"))) empleado.Sueldo = lector.GetDouble("sueldo");
                Area area = new Area();
                if (!lector.IsDBNull(lector.GetOrdinal("nombre_area"))) area.Nombre = lector.GetString("nombre_area");
                if (!lector.IsDBNull(lector.GetOrdinal("id_area"))) area.IdArea = lector.GetInt32("id_area");
                area.Activo = true;
                empleado.Area = area;
                empleados.Add(empleado);
            }
            DBManager.getInstance().CerrarConexion();
            return empleados;
        }

        public int modificar(Empleado empleado)
        {
            MySqlParameter[] parametros = new MySqlParameter[9];
            parametros[0] = new MySqlParameter("_id_empleado", empleado.IdPersona);
            parametros[1] = new MySqlParameter("_fid_area", empleado.Area.IdArea);
            parametros[2] = new MySqlParameter("_DNI", empleado.DNI);
            parametros[3] = new MySqlParameter("_nombre", empleado.Nombre);
            parametros[4] = new MySqlParameter("_apellido_paterno", empleado.ApellidoPaterno);
            parametros[5] = new MySqlParameter("_genero", empleado.Genero);
            parametros[6] = new MySqlParameter("_fecha_nacimiento", empleado.FechaNacimiento);
            parametros[7] = new MySqlParameter("_cargo", empleado.Cargo);
            parametros[8] = new MySqlParameter("_sueldo", empleado.Sueldo);
            return DBManager.getInstance().EjecutarProcedimiento("MODIFICAR_EMPLEADO", parametros);
        }

        public Empleado obtenerPorId(int idEmpleado)
        {
            Empleado empleado = null;
            MySqlParameter[] parametros = new MySqlParameter[1];
            parametros[0] = new MySqlParameter("_id_empleado", idEmpleado);
            lector = DBManager.getInstance().EjecutarProcedimientoLectura("OBTENER_EMPLEADO_X_ID", parametros);
            while (lector.Read())
            {
                if (empleado == null) empleado = new Empleado();
                if (!lector.IsDBNull(lector.GetOrdinal("id_persona"))) empleado.IdPersona = lector.GetInt32("id_persona");
                if (!lector.IsDBNull(lector.GetOrdinal("DNI"))) empleado.DNI = lector.GetString("DNI");
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) empleado.Nombre = lector.GetString("nombre");
                if (!lector.IsDBNull(lector.GetOrdinal("apellido_paterno"))) empleado.ApellidoPaterno = lector.GetString("apellido_paterno");
                if (!lector.IsDBNull(lector.GetOrdinal("genero"))) empleado.Genero = lector.GetChar("genero");
                if (!lector.IsDBNull(lector.GetOrdinal("fecha_nacimiento"))) empleado.FechaNacimiento = lector.GetDateTime("fecha_nacimiento");
                if (!lector.IsDBNull(lector.GetOrdinal("cargo"))) empleado.Cargo = lector.GetString("cargo");
                if (!lector.IsDBNull(lector.GetOrdinal("sueldo"))) empleado.Sueldo = lector.GetDouble("sueldo");
                Area area = new Area();
                if (!lector.IsDBNull(lector.GetOrdinal("nombre_area"))) area.Nombre = lector.GetString("nombre_area");
                if (!lector.IsDBNull(lector.GetOrdinal("id_area"))) area.IdArea = lector.GetInt32("id_area");
                area.Activo = true;
                empleado.Area = area;
            }
            DBManager.getInstance().CerrarConexion();
            return empleado;
        }
    }
}
