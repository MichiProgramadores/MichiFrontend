using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftProgDomain.RRHH;
using SoftProgPersistance.RRHH.DAO;
using SoftProgPersistance.RRHH.Impl;
using System;
using System.ComponentModel;

namespace SoftProgTests
{
    [TestClass]
    public class AreaDAOTest
    {
        private static AreaDAO daoArea;
        private static BindingList<Area> areas;
        private static Area areaT;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            daoArea = new AreaImpl();
        }

        //Probando el insertar
        [TestMethod]
        public void TestMethod1()
        {
            Area area = new Area("FINANZAS");
            int resultado = daoArea.insertar(area);
            Assert.IsTrue(resultado != 0);
        }

        //Probando el listarTodos
        [TestMethod]
        public void TestMethod2()
        {
            areas = daoArea.listarTodos();
            Assert.IsNotNull(areas);
        }

        //Probando el modificar
        [TestMethod]
        public void TestMethod3()
        {
            areas = daoArea.listarTodos();
            areaT = areas[areas.Count-1];
            areaT.Nombre = "CAMBIANDO";
            int resultado = daoArea.modificar(areaT);
            Assert.IsTrue (resultado != 0); 
        }

        //Probando el eliminar
        [TestMethod]
        public void TestMethod4()
        {
            areas = daoArea.listarTodos();
            areaT = areas[areas.Count - 1];
            int resultado = daoArea.eliminar(areaT.IdArea);
            Assert.IsTrue(resultado != 0);
        }

        //Probando el obtener por ID
        [TestMethod]
        public void TestMethod5()
        {
            areas = daoArea.listarTodos();
            areaT = areas[areas.Count - 1];
            Area areaT2 = daoArea.obtenerPorId(areaT.IdArea);
            Assert.IsNotNull(areaT2);
        }
    }
}
