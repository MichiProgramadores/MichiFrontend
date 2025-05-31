using SoftProgDomain.Logistica.Almacen;
using SoftProgPersistance.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.Logistica.Almacen.DAO
{
    public interface ProductoDAO : ICrud<ProductoDAO>
    {
        BindingList<Producto> listarPorNombre(String nombre); 
    }
}
