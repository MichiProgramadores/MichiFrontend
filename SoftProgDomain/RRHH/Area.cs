using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgDomain.RRHH
{
    [Serializable]
    public class Area
    {
        private int idArea;
        private string nombre;
        private bool activo;

        public Area() { }
        public Area(string nombre)
        {
            this.nombre = nombre;
        }

        public int IdArea
        {
            get { return idArea; }
            set { idArea = value; }
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Activo { get => activo; set => activo = value; }
    
        public string Formato { get => idArea + " - "
                + nombre;
        }
    }
}
