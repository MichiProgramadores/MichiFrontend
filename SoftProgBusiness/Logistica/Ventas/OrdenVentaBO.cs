using SoftProgDomain.Logistica.Ventas;
using SoftProgPersistance.Logistica.Ventas.DAO;
using SoftProgPersistance.Logistica.Ventas.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.Logistica.Ventas
{
    public class OrdenVentaBO
    {
        private OrdenVentaDAO daoOrdenVenta;

        public OrdenVentaBO()
        {
            daoOrdenVenta = new OrdenVentaImpl();
        }

        public int insertar(OrdenVenta ordenVenta)
        {
            return daoOrdenVenta.insertar(ordenVenta);
        }
    }
}
