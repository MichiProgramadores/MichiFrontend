using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftProgDomain.RRHH;
using SoftProgPersistance.RRHH.DAO;
using SoftProgPersistance.RRHH.Impl;
using System;
using System.ComponentModel;

namespace SoftProgTests
{
    [TestClass]
    public class EmpleadoDAOTest
    {
        private static EmpleadoDAO daoEmpleado;
        private static BindingList<Empleado> empleados;
        private static Empleado empleado;
        private static Area area;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            daoEmpleado = new EmpleadoImpl();
            AreaDAO daoArea = new AreaImpl();
            BindingList<Area> areas = daoArea.listarTodos();
            area = areas[areas.Count-1];
        }

        //Probando el insertar
        [TestMethod]
        public void TestMethod1()
        {
            empleado = new Empleado();
            empleado.Nombre = "JUAN";
            empleado.ApellidoPaterno = "PEREZ";
            empleado.DNI = "17872029";
            empleado.FechaNacimiento = new DateTime(1993,01,18);
            empleado.Sueldo = 1700.50;
            empleado.Cargo = "JEFE DE VENTAS";
            empleado.Area = area;
            empleado.Genero = 'M';
            int resultado = daoEmpleado.insertar(empleado);
            Assert.IsTrue(resultado != 0);
        }

        //Probando el listarTodos
        [TestMethod]
        public void TestMethod2()
        {
            empleados = daoEmpleado.listarTodos();
            Assert.IsNotNull(empleados);
        }

        //Probando el modificar
        [TestMethod]
        public void TestMethod3()
        {
            empleados = daoEmpleado.listarTodos();
            empleado = empleados[empleados.Count - 1];
            empleado.Nombre = "MARIA";
            empleado.Genero = 'F';
            int resultado = daoEmpleado.modificar(empleado);
            Assert.IsTrue(resultado != 0);
        }

        //Probando el eliminar
        [TestMethod]
        public void TestMethod4()
        {
            empleados = daoEmpleado.listarTodos();
            empleado = empleados[empleados.Count - 1];
            int resultado = daoEmpleado.eliminar(empleado.IdPersona);
            Assert.IsTrue(resultado != 0);
        }
    }
}
