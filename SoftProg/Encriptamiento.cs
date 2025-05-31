using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoftProg
{
    public class Encriptamiento
    {
        public string GenerarClave()
        {
            Aes aes = Aes.Create();
            aes.KeySize = 128;
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }

        public string Encriptar(string password, string clave)
        {
            byte[] temp = Convert.FromBase64String(clave);
            
            Aes aes = Aes.Create();
            aes.Key = temp;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;

            var encryptor = aes.CreateEncryptor();
            byte[] bytesTexto = Encoding.UTF8.GetBytes(password);
            byte[] bytesEncriptado = encryptor.TransformFinalBlock(bytesTexto, 0, bytesTexto.Length);
            return Convert.ToBase64String(bytesEncriptado);
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
    }
}
