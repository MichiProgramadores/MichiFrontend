using SoftProgDomain.Logistica.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgDomain.Logistica.Ventas
{
    public class LineaOrdenVenta
    {
        private int idLineaOrdenVenta;
        private Producto producto;
        private int cantidad;
        private double subtotal;
        public int IdLineaOrdenVenta { get => idLineaOrdenVenta; set => idLineaOrdenVenta = value; }
        public Producto Producto { get => producto; set => producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double Subtotal { get => subtotal; set => subtotal = value; }
    }
}
