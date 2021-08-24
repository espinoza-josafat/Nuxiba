using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nuxiba.TestArch.Tools.Helpers
{
    public static class Cryptography
    {
        public static string Encrypt(string cadena)
        {
            return Encrypt(cadena, "+vgB9Yx*qUD6Fujc");
        }

        public static string Encrypt(string cadena, string clave)
        {
            var cadenaBytes = Encoding.UTF8.GetBytes(cadena);
            var claveBytes = Encoding.UTF8.GetBytes(clave);
            
            var rij = new RijndaelManaged();
            
            rij.Mode = CipherMode.ECB;
            
            rij.BlockSize = 256;
            
            rij.Padding = PaddingMode.Zeros;
            
            var encriptador = rij.CreateEncryptor(claveBytes, rij.IV);
            
            var memStream = new MemoryStream();
            
            var cifradoStream = new CryptoStream(memStream, encriptador, CryptoStreamMode.Write);
            
            cifradoStream.Write(cadenaBytes, 0, cadenaBytes.Length);
            
            cifradoStream.FlushFinalBlock();
            
            var cipherTextBytes = memStream.ToArray();
            
            memStream.Close();
            cifradoStream.Close();
            
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string cadena)
        {
            return Decrypt(cadena, "+vgB9Yx*qUD6Fujc");
        }

        public static string Decrypt(string cadena, string clave)
        {
            var cadenaBytes = Convert.FromBase64String(cadena);
            var claveBytes = Encoding.UTF8.GetBytes(clave);
            
            var rij = new RijndaelManaged();
            
            rij.Mode = CipherMode.ECB;
            
            rij.BlockSize = 256;
            
            rij.Padding = PaddingMode.Zeros;
            
            var desencriptador = rij.CreateDecryptor(claveBytes, rij.IV);
            
            var memStream = new MemoryStream(cadenaBytes);
            
            var cifradoStream = new CryptoStream(memStream, desencriptador, CryptoStreamMode.Read);
            
            var lectorStream = new StreamReader(cifradoStream);
            
            var resultado = lectorStream.ReadToEnd();
            
            memStream.Close();
            cifradoStream.Close();
            
            return resultado;
        }
    }
}