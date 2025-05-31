using SoftProgDomain.GestClientes;
using SoftProgPersistance.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgPersistance.GestClientes.DAO
{
    public interface ClienteDAO : ICrud<Cliente>
    {
        BindingList<Cliente> listarPorDNIoNombre(String DNIoNombre);
    }
}
