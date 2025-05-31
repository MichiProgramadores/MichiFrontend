using SoftProgDomain.Logistica.Almacen;
using SoftProgPersistance.Logistica.Almacen.DAO;
using SoftProgPersistance.Logistica.Almacen.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.Logistica.Almacen
{
    public class ProductoBO
    {
        private ProductoDAO daoProducto;

        public ProductoBO()
        {
            daoProducto = new ProductoImpl();
        }

        public BindingList<Producto> listarPorNombre(String nombre)
        {
            return daoProducto.listarPorNombre(nombre);
        }
    }
}
