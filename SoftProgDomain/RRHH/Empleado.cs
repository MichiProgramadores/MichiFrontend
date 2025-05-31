using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgDomain.RRHH
{
    [Serializable]
    public class Empleado : Persona
    {
        private Area area;
        private CuentaUsuario cuentaUsuario;
        private string cargo;
        private double sueldo;
        private bool activo;

        public Area Area { get => area; set => area = value; }
        public string Cargo { get => cargo; set => cargo = value; }
        public double Sueldo { get => sueldo; set => sueldo = value; }
        public bool Activo { get => activo; set => activo = value; }
        public CuentaUsuario CuentaUsuario { get => cuentaUsuario; set => cuentaUsuario = value; }
    }
}
