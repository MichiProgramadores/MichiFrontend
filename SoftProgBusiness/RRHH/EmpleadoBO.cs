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
    public class EmpleadoBO
    {
        private EmpleadoDAO daoEmpleado;
        private AreaDAO daoArea;

        public EmpleadoBO()
        {
            daoEmpleado = new EmpleadoImpl();
            daoArea = new AreaImpl();
        }

        public Empleado obtenerEmpleadoPorId(int idEmpleado)
        {
            Empleado empleado = daoEmpleado.obtenerPorId(idEmpleado);
            empleado.Area = daoArea.obtenerPorId(empleado.Area.IdArea);
            return empleado;
        }

        public int insertar(Empleado empleado)
        {
            validaciones(empleado);
            return daoEmpleado.insertar(empleado);
        }

        public int modificar(Empleado empleado)
        {
            validaciones(empleado);
            return daoEmpleado.modificar(empleado);
        }

        public int eliminar(int idEmpleado)
        {
            return daoEmpleado.eliminar(idEmpleado);
        }

        public BindingList<Empleado> listarTodos()
        {
            return daoEmpleado.listarTodos();
        }

        public void validaciones(Empleado empleado)
        {
            if (empleado.DNI.Trim() == "")
                throw new Exception("Debe colocar un DNI.");
            if (empleado.DNI.Length != 8)
                throw new Exception("Debe colocar un DNI de 8 dígitos.");
            if (empleado.Nombre.Trim() == "")
                throw new Exception("Debe colocar un nombre.");
            if (empleado.ApellidoPaterno.Trim() == "")
                throw new Exception("Debe colocar un apellido paterno.");
        }

    }
}
