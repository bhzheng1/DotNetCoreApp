using System.Security.Cryptography.X509Certificates;

namespace WebApi.CertificateAuthentication
{
    public class CertificateValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            string[] allowedThumbprints = {
                "a771da7af356971b32efdffb0b577c2032fbf3aa".ToUpper()
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}
