using System.Security.Cryptography;
using System.Text;

namespace project_get_discount_back.Helpers
{
    public class Cryptography
    {
        public string Encrypt(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                throw new ArgumentException("A senha não pode ser nula ou vazia");
            }

            try
            {
                var sb = new StringBuilder();
                foreach (var c in MD5.HashData(Encoding.ASCII.GetBytes(valor)))
                {
                    sb.Append(c.ToString("X2"));
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
