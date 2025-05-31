using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.DAO
{
    public interface ICrud<T>
    {
        int insertar(T modelo);
        int modificar(T modelo);
        int eliminar(int idModelo);
        BindingList<T> listarTodos();
        T obtenerPorId(int idModelo);
    }
}
