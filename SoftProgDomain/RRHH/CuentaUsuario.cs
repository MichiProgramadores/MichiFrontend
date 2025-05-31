using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgDomain.RRHH
{
    [Serializable]
    public class CuentaUsuario
    {
        private int idCuentUsuario;
        private string username;
        private string password;
        private bool activa;

        public int IdCuentUsuario { get => idCuentUsuario; set => idCuentUsuario = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool Activa { get => activa; set => activa = value; }
    }
}
