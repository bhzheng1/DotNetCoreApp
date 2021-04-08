using System.Threading.Tasks;

namespace StartupModule
{
    public interface IApplicationInitializer {
        Task Invoke();
    }
}