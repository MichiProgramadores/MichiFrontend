using MySql.Data.MySqlClient;
using SoftProgDBManager;
using SoftProgDomain.RRHH;
using SoftProgPersistance.RRHH.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.RRHH.Impl
{
    public class AreaImpl : AreaDAO
    {
        private MySqlDataReader lector;
        public int eliminar(int idArea)
        {
            MySqlParameter[] parametros = new MySqlParameter[1];
            parametros[0] = new MySqlParameter("_id_area", idArea);
            return DBManager.getInstance().EjecutarProcedimiento("ELIMINAR_AREA", parametros);        }

        public int insertar(Area area)
        {
            MySqlParameter[] parametros = new MySqlParameter[2];
            parametros[0] = new MySqlParameter("_id_area",MySqlDbType.Int32);
            parametros[0].Direction = ParameterDirection.Output;
            parametros[1] = new MySqlParameter("_nombre", area.Nombre);
            DBManager.getInstance().EjecutarProcedimiento("INSERTAR_AREA",parametros);
            area.IdArea = Int32.Parse(parametros[0].Value.ToString());
            return area.IdArea;
        }

        public BindingList<Area> listarTodos()
        {
            BindingList<Area> areas = null;
            lector = DBManager.getInstance().EjecutarProcedimientoLectura("LISTAR_AREAS_TODAS",null);
            while (lector.Read())
            {
                if (areas == null) areas = new BindingList<Area>();
                Area area = new Area();
                if (!lector.IsDBNull(lector.GetOrdinal("id_area"))) area.IdArea = lector.GetInt32("id_area");
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) area.Nombre = lector.GetString("nombre");
                area.Activo = true;
                areas.Add(area);
            }
            DBManager.getInstance().CerrarConexion();
            return areas;
        }

        public int modificar(Area area)
        {
            MySqlParameter[] parametros = new MySqlParameter[2];
            parametros[0] = new MySqlParameter("_id_area", area.IdArea);
            parametros[1] = new MySqlParameter("_nombre", area.Nombre);
            DBManager.getInstance().EjecutarProcedimiento("MODIFICAR_AREA", parametros);
            area.IdArea = Int32.Parse(parametros[0].Value.ToString());
            return area.IdArea;
        }

        public Area obtenerPorId(int idArea)
        {
            Area area = null;
            MySqlParameter[] parametros = new MySqlParameter[1];
            parametros[0] = new MySqlParameter("_id_area", idArea);
            lector = DBManager.getInstance().EjecutarProcedimientoLectura("OBTENER_AREA_X_ID", parametros);
            if (lector.Read())
            {
                if (area == null) area = new Area();
                if (!lector.IsDBNull(lector.GetOrdinal("id_area"))) area.IdArea = lector.GetInt32("id_area");
                if (!lector.IsDBNull(lector.GetOrdinal("nombre"))) area.Nombre = lector.GetString("nombre");
                area.Activo = true;
            }
            DBManager.getInstance().CerrarConexion();
            return area;
        }
    }
}
