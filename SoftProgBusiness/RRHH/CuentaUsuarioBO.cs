using SoftProgDomain.RRHH;
using SoftProgPersistance.RRHH.DAO;
using SoftProgPersistance.RRHH.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgBusiness.RRHH
{
    
    public class CuentaUsuarioBO
    {
        private CuentaUsuarioDAO daoCuentaUsuario;
        public CuentaUsuarioBO()
        {
            daoCuentaUsuario = new CuentaUsuarioImpl();
        }

        public int verificarCuentaUsuario(CuentaUsuario cuentaUsuario)
        {
            return daoCuentaUsuario.verificar(cuentaUsuario);
        }
    }
}
