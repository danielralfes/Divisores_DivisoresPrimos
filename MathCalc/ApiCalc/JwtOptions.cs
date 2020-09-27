using System.Text;

namespace MathCalc.ApiCalc
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }

        public byte[] GetSecretKeyBytes()
        {
            return Encoding.UTF8.GetBytes(SecretKey ?? "default key");
        }
    }
}
