using SoftProgDomain.GestClientes;
using SoftProgPersistance.GestClientes.DAO;
using SoftProgPersistance.GestClientes.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.GestClientes
{
    public class ClienteBO
    {
        private ClienteDAO daoCliente;
        public ClienteBO()
        {
            daoCliente = new ClienteImpl();
        }

        public BindingList<Cliente> listarPorDNIoNombre(string DNIoNombre)
        {
            return daoCliente.listarPorDNIoNombre(DNIoNombre);
        }
    }
}
