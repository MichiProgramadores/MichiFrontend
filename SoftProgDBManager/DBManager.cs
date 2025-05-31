using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoftProgDBManager
{
    public class DBManager
    {
        private static DBManager dbManager;
        private string url;
        private string hostname;
        private string usuario;
        private string password;
        private string database;
        private string puerto;
        private string clave;
        private string nombreArchivo = "properties.txt";
        private MySqlConnection con;
        private MySqlCommand com;
        private DBManager()
        {
            string ruta = Path.Combine
                (AppDomain.CurrentDomain.BaseDirectory,nombreArchivo);
            if (File.Exists(ruta))
            {
                Dictionary<string, string> valores = new Dictionary<string, string>();
                foreach (string line in File.ReadLines(ruta))
                {
                    var partes = line.Split(new[] { '=' }, 2);
                    if (partes.Length == 2)
                        valores[partes[0].Trim()] = partes[1].Trim().TrimEnd(';');
                }
                valores.TryGetValue("server", out hostname);
                valores.TryGetValue("user", out usuario);
                valores.TryGetValue("database", out database);
                valores.TryGetValue("port", out puerto);
                valores.TryGetValue("clave", out clave);
                valores.TryGetValue("passwordencryptado", out password);
                password = Desencriptar(password, clave);
                url = $"server={hostname};user={usuario};password={password};database={database};port={puerto};";
            }
            con = new MySqlConnection(url);
        }

        public string Desencriptar(string password, string clave)
        {
            byte[] temp = Convert.FromBase64String(clave);
            Aes aes = Aes.Create();
            aes.Key = temp;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            var decryptor = aes.CreateDecryptor();
            byte[] bytesEncriptado = Convert.FromBase64String(password);
            byte[] bytesPlano = decryptor.TransformFinalBlock(bytesEncriptado, 0, bytesEncriptado.Length);
            return Encoding.UTF8.GetString(bytesPlano);
        }

        public static DBManager getInstance()
        {
            if (dbManager == null)
                dbManager = new DBManager();
            return dbManager;
        }

        public string Url
        {
            get => url;
        }

        public MySqlConnection Connection
        {
            get
            {
                AbrirConexion();
                return con;
            }
        }

        private void AbrirConexion()
        {
            if(con.State != ConnectionState.Open)
                con.Open();
        }

        public void CerrarConexion()
        {
            if(con.State != ConnectionState.Closed)
                con.Close();
        }

        //Métodos para llamadas a Procedimientos Almacenados
        public int EjecutarProcedimiento(string nombreProcedimiento,
            MySqlParameter[] parametros)
        {
            int resultado = 0;
            try
            {
                AbrirConexion();
                com = con.CreateCommand();
                com.CommandText = nombreProcedimiento;
                com.CommandType = CommandType.StoredProcedure;
                if(parametros != null)
                    com.Parameters.AddRange(parametros);
                resultado = com.ExecuteNonQuery();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
            return resultado;
        }

        public MySqlDataReader EjecutarProcedimientoLectura(string nombreProcedimiento, MySqlParameter[] parametros)
        {
            MySqlDataReader lector = null;
            try
            {
                AbrirConexion();
                com = con.CreateCommand();
                com.CommandText = nombreProcedimiento;
                com.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                    com.Parameters.AddRange(parametros);
                lector = com.ExecuteReader();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lector;
        }

        public int EjecutarProcedimientoTransaccion(string nombreProcedimiento, MySqlParameter[] parametros, MySqlTransaction transaccion)
        {
            int resultado = 0;
            try
            {
                com = con.CreateCommand();
                com.Transaction = transaccion;
                com.CommandText = nombreProcedimiento;
                com.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                    com.Parameters.AddRange(parametros);
                resultado = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultado;
        }
    }
}
