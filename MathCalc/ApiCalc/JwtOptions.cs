using System.Text;

namespace MathCalc.ApiCalc
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }

        public byte[] GetSecretKeyBytes()
        {
            return Encoding.UTF8.GetBytes(SecretKey ?? "1A4Ud3YSVhc643en_N31UGOo4VFt-iB7kICBDrhM25eb8UIYmNn2o7FgZYhXMiiQYH3XzA3Un4MwVValDeeq5tvqOoH5HIcXhkB84RJOd_4-hiHkZ6fFK1Z1zL9btb5IlMT5JYf1-KcFhMZds3AXsYEDLN5V-NWhAZMZYg4b7pqpHrx-dS6JMQ2POjqQWvhYjNBRS1hPDtgHa_60-d9Ms3mEx4LLP_snGGczrVSn5ai5rXc74mg_iAJQWU8J1N0Jc0V6TpHVqVMhjTrqKZ4WKe9geHQeBsAw6-EAAi9ayrPYhHxlqQi0uGqhoiOC23ClMPRbLZFKBxtEu0HbKRup3w");
        }
    }
}
