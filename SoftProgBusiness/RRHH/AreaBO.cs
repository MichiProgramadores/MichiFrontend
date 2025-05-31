using SoftProgDomain.RRHH;
using SoftProgPersistance.RRHH.DAO;
using SoftProgPersistance.RRHH.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.RRHH
{
    public class AreaBO
    {
        private AreaDAO daoArea;

        public AreaBO()
        {
            daoArea = new AreaImpl();
        }

        public BindingList<Area> listarTodos()
        {
            return daoArea.listarTodos();
        }

        public int insertar(Area area)
        {
            int resultado = 0;
            if(area.Nombre == "")
                throw new Exception("Debe ingresar el nombre.");
            resultado = daoArea.insertar(area);
            return resultado;
        }

        public int modificar(Area area)
        {
            return daoArea.modificar(area);
        }

        public int eliminar(int idArea)
        {
            return daoArea.eliminar(idArea);
        }
    }
}
