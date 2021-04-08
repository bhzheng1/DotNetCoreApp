using System.Threading.Tasks;

namespace FileProcessor.ApiCaller.DocRepo
{
    public interface IDocRepoApiKeyProvider
    {
        Task<ApiKey> Apply();
    }
}