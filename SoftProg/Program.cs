using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace SoftProg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine
                ("Mi primer programa en C#");
            //Area area = new Area();
            //area.Nombre = "AREA PROG3";

            //AreaBO boArea = new AreaBO();
            //daoArea.insertar(area);

            //BindingList<Area>
            //areasDB = boArea.listarTodos();

            //oreach (Area a in areasDB)
            //System.Console.WriteLine(a.IdArea + ". " + a.Nombre);

            //Empleado empleado = new Empleado();
            //empleado.Nombre = "MANUEL";
            //empleado.DNI = "28762111";
            //empleado.ApellidoPaterno = "TUPIA";
            //empleado.Area = areasDB[0];
            //empleado.Genero = 'M';
            //empleado.Cargo = "JEFE DE FINANZAS";
            //empleado.Sueldo = 2500.00;
            //empleado.FechaNacimiento = 
            //    new DateTime(1993,03,19);


            //EmpleadoBO boEmpleado = new EmpleadoBO();

            //int resultado = boEmpleado.insertar(empleado);
            //if (resultado != 0)
            //{
            //    System.Console.WriteLine("Se ha registrado correctamente");
            //}

            //Empleado emp = boEmpleado.obtenerEmpleadoPorId(2);

            Encriptamiento encryptamiento = new Encriptamiento();
            //String clave = encryptamiento.GenerarClave();
            String password = "prog3user0681";
            
            String clave = "P3MEtPB2wdUXrgoAkrMqEw==";
            string passwordencriptado = encryptamiento.Encriptar(password, clave);
            System.Console.WriteLine(passwordencriptado);

        }
    }
}
