using SoftProgDomain.RRHH;
using SoftProgPersistance.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.RRHH.DAO
{
    public interface EmpleadoDAO : ICrud<Empleado>
    {
        BindingList<Empleado> listarPorNombreDNI(String nombreDNI);

    }
}
