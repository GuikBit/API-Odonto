using System.Security.Cryptography;
using System.Text;

namespace DentistaApi.Models.Utils
{
    public static class Cripto
    {
        public static string GerarHash(this string senha)
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(senha);

            array = hash.ComputeHash(array);

            var strHera = new StringBuilder();

            foreach (var item in array)
            {
                strHera.Append(item.ToString("x2"));
            }

            return strHera.ToString();
        }
    }
}
