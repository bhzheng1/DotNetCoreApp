using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;

namespace WebApi.CertificateAuthenticationClient
{
    //TODO
    public class CertificateHelper
    {
        protected internal static X509Certificate2 GetServiceCertificate(string subjectName, string env)
        {
            if (env == "Local")
            {
                using (var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
                {
                    certStore.Open(OpenFlags.ReadOnly);
                    var certCollection = certStore.Certificates.Find(
                                               X509FindType.FindBySubjectDistinguishedName, subjectName, true);
                    X509Certificate2 certificate = null;
                    if (certCollection.Count > 0)
                    {
                        certificate = certCollection[0];
                    }
                    return certificate;
                }
            }
            else
            {
                using (var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine))
                {
                    certStore.Open(OpenFlags.ReadOnly);
                    var certCollection = certStore.Certificates.Find(
                                               X509FindType.FindBySubjectDistinguishedName, subjectName, true);
                    X509Certificate2 certificate = null;
                    if (certCollection.Count > 0)
                    {
                        certificate = certCollection[0];
                    }
                    return certificate;
                }
            }
        }

        protected internal static string GetCertificateSubjectNameBasedOnEnvironment(string environment)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environment}.json", optional: false);

            var configuration = builder.Build();
            return configuration["ServerCertificateSubject"];
        }
    }
}
