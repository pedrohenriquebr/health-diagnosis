using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Util
{
    class Hash
    {
        public static string md5(string senha)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder md5_senha = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                md5_senha.Append(data[i].ToString("x2"));
            }
            return md5_senha.ToString();
        }
    }
}